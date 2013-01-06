using System.Drawing;
using Generic.Maths;
using Vectorizer.Logic.Picture;

namespace Vectorizer.Logic.Scoring.PictureMutation
{
    public class PictureMutationPosingAsPicture : IVectorPicture
    {
        public PictureMutationPosingAsPicture(VectorPicture original, IPictureMutation mutation)
        {
            Original = original;
            Mutation = mutation;
        }

        public IPictureMutation Mutation { get; set; }
        public VectorPicture Original { get; set; }

        public void SmoothDraw(Graphics graphics, FloatVector2 scale)
        {
             GetVectorPicture().SmoothDraw(graphics, scale);
        }

        public Bitmap ManualDraw(int width, int height)
        {
            return GetVectorPicture().ManualDraw(width, height);
        }

        public VectorPicture GetVectorPicture()
        {
            return Mutation.ApplyToPicture(Original);
        }
    }
}