using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Generic.Maths;
using Polygon = Vectorizer.Logic.Picture.Polygon;

namespace Vectorizer.Logic.SinglePolygonSpiral
{
    class ChangedPoint : PointMutation
    {
        private readonly IntVector2 newPoint;

        public ChangedPoint(int index, IntVector2 newPoint) : base(index)
        {
            this.newPoint = newPoint;
        }

        public IntVector2 NewPoint
        {
            get { return newPoint; }
        }

    }
}
