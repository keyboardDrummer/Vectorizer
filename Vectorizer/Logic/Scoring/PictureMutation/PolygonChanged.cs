using Vectorizer.Logic.Picture;
using Vectorizer.Logic.Scoring.Incremental;

namespace Vectorizer.Logic.Scoring.PictureMutation
{
    public class PolygonChanged : IPictureMutation
    {
        private readonly int index;
        private readonly Polygon newPolygon;

        public PolygonChanged(Polygon newPolygon, int index)
        {
            this.newPolygon = newPolygon;
            this.index = index;
        }

        public Polygon NewPolygon
        {
            get { return newPolygon; }
        }

        public int Index
        {
            get { return index; }
        }

        public int GetError(ErrorData errorData)
        {
            return errorData.TotalErrorWhenPolygonChanged(newPolygon, index);
        }

        public VectorPicture ApplyToPicture(VectorPicture picture)
        {
            var copy = picture.Clone();
            copy.Polygons[index] = newPolygon;
            return copy;
        }
    }
}