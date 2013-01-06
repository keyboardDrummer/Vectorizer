
using Generic.Maths;

namespace Vectorizer.Logic.Picture
{
    public class ColouredPoint : IntVector2
    {
        public ColouredPoint(IntVector2 point, VColor color)
            : base(point)
        {
            Color = color;
        }

        public ColouredPoint(int x, int y, VColor color) : base(x, y)
        {
            Color = color;
        }

        public VColor Color { get; private set; }
    }
}