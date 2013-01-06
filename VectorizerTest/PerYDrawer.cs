using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Generic.Maths;
using Vectorizer.Logic.Drawing;
using Polygon = Vectorizer.Logic.Picture.Polygon;

namespace VectorizerTest
{
    class PerYDrawer : IDrawer
    {
        public IEnumerable<IntVector2> Draw(Polygon polygon)
        {
            return PerYPolygonDrawer.GetPoints(polygon);
        }
    }
}
