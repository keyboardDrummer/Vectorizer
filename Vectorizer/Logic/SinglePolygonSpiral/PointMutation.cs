using Vectorizer.Logic.Picture;

namespace Vectorizer.Logic.SinglePolygonSpiral
{
    abstract class PointMutation
    {
        protected readonly int index;

        protected PointMutation(int index)
        {
            this.index = index;
        }

        public int Index
        {
            get { return index; }
        }
    }
}
