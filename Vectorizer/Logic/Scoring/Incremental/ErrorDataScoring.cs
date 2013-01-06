using Vectorizer.Logic.Algorithm;
using Vectorizer.Logic.Drawing;
using Vectorizer.Logic.Picture;
using Vectorizer.Logic.Scoring.PictureMutation;

namespace Vectorizer.Logic.Scoring.Incremental
{
    public class ErrorDataScoring : IScoringMethod
    {
        private readonly ImageData image;
        private ErrorData errorData;

        public ErrorDataScoring(ImageData image)
        {
            this.image = image;
            errorData = new ErrorData(image);
        }

        public void FoundBetter(VectorPicture picture)
        {
            errorData = new ErrorData(picture, image);
        }

        public int GetError(IPictureMutation mutation)
        {
            return mutation.GetError(errorData);
        }
    }
}