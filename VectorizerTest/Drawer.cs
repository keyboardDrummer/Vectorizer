using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Generic.Maths;
using Polygon = Vectorizer.Logic.Picture.Polygon;

namespace VectorizerTest
{
    interface IDrawer
    {
        IEnumerable<IntVector2> Draw(Polygon polygon);
    }
}
