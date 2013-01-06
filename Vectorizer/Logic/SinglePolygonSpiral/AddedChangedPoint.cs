using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Generic.Maths;
using Polygon = Vectorizer.Logic.Picture.Polygon;

namespace Vectorizer.Logic.SinglePolygonSpiral
{
    class AddedChangedPoint : PointMutation
    {
        private readonly IntVector2 newPoint;
        private readonly IList<IntVector2> addedPoints;

        public AddedChangedPoint(int index, IntVector2 newPoint, IList<IntVector2> addedPoints) : base(index)
        {
            this.newPoint = newPoint;
            this.addedPoints = addedPoints;
        }

        public IntVector2 NewPoint
        {
            get { return newPoint; }
        }

        public IList<IntVector2> AddedPoints
        {
            get { return addedPoints; }
        }

    }
}
