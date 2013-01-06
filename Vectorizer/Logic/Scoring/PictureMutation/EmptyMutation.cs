using Vectorizer.Logic.Picture;
using Vectorizer.Logic.Scoring.Incremental;

namespace Vectorizer.Logic.Scoring.PictureMutation
{
    public class EmptyMutation : IPictureMutation
    {
        public VectorPicture ApplyToPicture(VectorPicture picture)
        {
            return picture;
        }

        public int GetError(ErrorData errorData)
        {
            return errorData.TotalError;
        }
    }
}
