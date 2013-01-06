using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Generic.Maths;
using Vectorizer.Logic.Drawing.NBoxPolygonDrawer;
using Polygon = Vectorizer.Logic.Picture.Polygon;

namespace VectorizerTest
{
    class BoxDrawer : IDrawer
    {
        public IEnumerable<IntVector2> Draw(Polygon polygon)
        {
            return BoxPolygonDrawer.GetPoints(polygon);
        }
    }
}
