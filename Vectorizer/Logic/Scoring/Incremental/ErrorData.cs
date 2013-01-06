using System;
using System.Collections.Generic;
using System.Linq;
using Generic.Collections.Enumerables;
using Generic.Collections.List;
using Generic.Collections.Set;
using Generic.Maths;
using Vectorizer.Logic.Algorithm;
using Vectorizer.Logic.Drawing;
using Vectorizer.Logic.Drawing.NBoxPolygonDrawer;
using Vectorizer.Logic.Picture;
using Polygon = Vectorizer.Logic.Picture.Polygon;

namespace Vectorizer.Logic.Scoring.Incremental
{
    public class ErrorData
    {
        private PixelErrorData[,] pixelErrorDatas;
        private readonly IList<Polygon> polygonOrder;

        public ErrorData(ImageData source)
        {
            polygonOrder = new List<Polygon>();
            CreatePixels(source);
        }

        public ErrorData(VectorPicture picture, ImageData source) : this(source)
        {
            AddPolygons(picture.Polygons);
            polygonOrder = picture.Polygons.ToList();
        }

        public int TotalError { get; private set; }
        
        public PixelErrorData this[IntVector2 point]
        {
            get { return pixelErrorDatas[point.X, point.Y]; }
            set { pixelErrorDatas[point.X, point.Y] = value; }
        }

        private static IEnumerable<IntVector2> GetPolygonPoints(Polygon polygon)
        {
            //return PerYPolygonDrawer.GetPoints(polygon);
            return new BoxPolygonDrawer(polygon).GetPoints();
        }

        private void AddPolygons(IList<Polygon> polygons)
        {
            foreach (int i in polygons.Indexes().Reverse())
            {
                var polygon = polygons[i];
                AddPolygon(polygon, i);
            }
        }

        public int TotalErrorWhenPolygonChanged(Polygon polygon, int index)
        {
            int totalError = TotalError;
            var newPoints = GetPolygonPoints(polygon).ToHashSet();
            var oldPoints = GetPolygonPoints(polygonOrder[index]).ToHashSet();
            var addedPoints = newPoints.Difference(oldPoints);
            var removedPoints = oldPoints.Difference(newPoints);
            foreach(var x in removedPoints)
            {
                ((Action<IntVector2>)(point => { totalError += this[point].GetErrorDeltaWhenPolygonRemoved(polygonOrder, index); }))(x);
            }
            foreach(var x in addedPoints)
            {
                ((Action<IntVector2>)(point => { totalError += this[point].GetErrorDeltaWhenPolygonAdded(polygonOrder, polygon, index); }))(x);
            }
            return totalError;
        }

        public void AddPolygon(Polygon polygon, int index)
        {
            foreach(var x in GetPolygonPoints(polygon))
            {
                ((Action<IntVector2>)(point =>
                {
                    TotalError += this[point].GetErrorDeltaWhenPolygonAdded(polygonOrder, polygon, index);
                    this[point].AddPolygon(polygon);
                }))(x);
            }
            polygonOrder.Insert(index,polygon);
        }

        private void CreatePixels(ImageData source)
        {
            pixelErrorDatas = new PixelErrorData[source.Width,source.Height];
            foreach (var point in source.GetPoints())
            {
                this[point] = new PixelErrorData(point, source);
                TotalError += this[point].GetError(polygonOrder);
            }
        }
    }
}