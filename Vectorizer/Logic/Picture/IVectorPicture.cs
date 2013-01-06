using System.Drawing;
using Generic.Maths;

namespace Vectorizer.Logic.Picture
{
    public interface IVectorPicture
    {
        void SmoothDraw(Graphics graphics, FloatVector2 scale);

        Bitmap ManualDraw(int width, int height);
    }
}