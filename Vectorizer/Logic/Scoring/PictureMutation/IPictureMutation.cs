using Vectorizer.Logic.Picture;
using Vectorizer.Logic.Scoring.Incremental;

namespace Vectorizer.Logic.Scoring.PictureMutation
{
    public interface IPictureMutation
    {
        VectorPicture ApplyToPicture(VectorPicture picture);
        int GetError(ErrorData errorData);
    }
}