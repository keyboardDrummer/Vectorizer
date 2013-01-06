using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Generic.Maths;
using Polygon = Vectorizer.Logic.Picture.Polygon;

namespace Vectorizer.Logic.SinglePolygonSpiral
{
    class AddedPoints : PointMutation
    {
        private readonly IList<IntVector2> points;

        public AddedPoints(int index, IList<IntVector2> points) : base(index)
        {
            this.points = points;
        }

        public IList<IntVector2> Points
        {
            get { return points; }
        }

    }
}
