using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using Generic;
using Generic.Maths;
using Vectorizer.Logic.Drawing;

namespace Vectorizer.Logic.Picture
{
    [Serializable]
    public class Polygon : Generic.Maths.Polygon, IGenCloneable<Polygon>, IEquatable<Polygon>
    {
        public VColor Color { get; set; }

        public Polygon(VColor color) : this(new List<IntVector2>(), color)
        {
        }

        public Polygon(IList<IntVector2> points, VColor vColor )
        {
            Points = points;
            Color = vColor;
        }

        public Polygon(Polygon clone)
        {
            Points = new List<IntVector2>(clone.Points.Count);
            foreach (var point in clone.Points)
            {
                Points.Add(point);
                Color = clone.Color;
            }
        }

        public void AddPoint(int x, int y)
        {
            Points.Add(new IntVector2(x,y));
        }

        public Boolean Equals(Polygon poly)
        {
            return Color.Equals(poly.Color) && base.Equals(poly);
        }

        public void SmoothDraw(Graphics graphics, FloatVector2 scale)
        {
            Brush brush = new SolidBrush(Color);
            var systemPoints = Points.Select(point => (PointF)(point*scale));
            graphics.FillPolygon(brush, systemPoints.ToArray());
        }

        public new Polygon Clone()
        {
            return new Polygon(this);
        }
    }
}
