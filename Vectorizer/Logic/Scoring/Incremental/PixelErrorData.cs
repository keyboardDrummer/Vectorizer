using System.Collections.Generic;
using System.Linq;
using Generic.Collections.Enumerables;
using Generic.Collections.List;
using Generic.Maths;
using Vectorizer.Logic.Algorithm;
using Vectorizer.Logic.Drawing;
using Vectorizer.Logic.Picture;
using Polygon = Vectorizer.Logic.Picture.Polygon;

namespace Vectorizer.Logic.Scoring.Incremental
{
    public class PixelErrorData
    {
        private readonly ISet<Polygon> polygons = new HashSet<Polygon>();
        private readonly VColor targetColor;

        public PixelErrorData(IntVector2 point, ImageData source)
        {
            targetColor = source[point];
        }

        public void RemovePolygon(Polygon polygon)
        {
            polygons.Remove(polygon);
        }

        public void AddPolygon(Polygon polygon)
        {
            polygons.Add(polygon);
        }

        public int GetErrorDeltaWhenPolygonRemoved(IList<Polygon> polygonOrder, int removedIndex)
        {
            int topIndex = GetTopIndex(polygonOrder);
            if (topIndex == removedIndex)
            {
                int errorBeforeRemove = GetError(polygonOrder[removedIndex]);
                int errorAfterRemove = GetError(polygonOrder.RemoveAtLazily(removedIndex));
                return errorAfterRemove - errorBeforeRemove;
            }
            return 0;
        }

        public int GetErrorDeltaWhenPolygonAdded(IList<Polygon> polygonOrder, Polygon addedPolygon, int addedIndex)
        {
            int topIndex = GetTopIndex(polygonOrder);
            int currentError = GetError(polygonOrder,topIndex);
            if (topIndex <= addedIndex)
            {
                int errorAfterAdded = GetError(addedPolygon);
                return errorAfterAdded - currentError;
            }
            return 0;
        }

        public int GetError(IList<Polygon> polygonOrder, int topIndex)
        {
            return topIndex == -1 ? targetColor.Length : GetError(polygonOrder[topIndex]);
        }

        public int GetError(IList<Polygon> polygonOrder)
        {
            var topIndex = GetTopIndex(polygonOrder);
            return topIndex == -1 ? targetColor.Length : GetError(polygonOrder[topIndex]);
        }

        private int GetError(Polygon polygon)
        {
            return targetColor.GetAbsColorError(polygon.Color);
        }

        private int GetTopIndex(IList<Polygon> polygons)
        {
            var containedIndicesSortedDescending = from i in polygons.Indexes().Reverse()
                                                   let p = polygons[i]
                                                   where this.polygons.Contains(p)
                                                   select i;
            return containedIndicesSortedDescending.ConcatUtil(-1).First();
        }
    }
}