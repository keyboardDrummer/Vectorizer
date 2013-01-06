using System;
using System.Collections.Generic;
using System.Drawing;
using Generic.Collections.Enumerables;
using Generic.Maths;
using Vectorizer.Logic.Drawing.NBoxPolygonDrawer;

namespace Vectorizer.Logic.Picture
{
    [Serializable]
    public class VectorPicture : IVectorPicture
    {
        public VectorPicture()
        {
            Polygons = new List<Polygon>();
        }

        public List<Polygon> Polygons { get; set; }

        public void SmoothDraw(Graphics graphics, FloatVector2 scale)
        {
            graphics.Clear(Color.Black);
            foreach (var polygon in Polygons)
            {
                polygon.SmoothDraw(graphics, scale);
            }
        }

        public Bitmap ManualDraw(Int32 width, Int32 height)
        {
            var image = new Bitmap(width, height);
            foreach (var polygon in Polygons)
            {
                foreach(var x in BoxPolygonDrawer.GetPoints(polygon))
                {
                    ((Action<IntVector2>)(point => image.SetPixel(point.X, point.Y, polygon.Color)))(x);
                }
            }
            return image;
        }

        public VectorPicture Clone()
        {
            var p = new VectorPicture {Polygons = new List<Polygon>(Polygons.Count)};
            foreach (var pol in Polygons)
            {
                p.Polygons.Add(pol.Clone());
            }
            return p;
        }
    }
}