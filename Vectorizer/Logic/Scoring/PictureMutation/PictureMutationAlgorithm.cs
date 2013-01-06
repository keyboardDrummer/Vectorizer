using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Generic;
using Vectorizer.Logic.Algorithm;
using Vectorizer.Logic.Picture;
using Vectorizer.Logic.Scoring.Incremental;

namespace Vectorizer.Logic.Scoring.PictureMutation
{
    public abstract class PictureMutationAlgorithm : HillClimberAlgorithm<PictureMutationPosingAsPicture>, IAlgorithm
    {
        readonly ScoringMethodEnum scoringMethodEnum;

        public enum ScoringMethodEnum
        {
            ErrorDataScoring,
            FullEvaluationScoring
        }

        IScoringMethod scoringMethod;
        public VectorPicture CurrentPicture { get; set; }

        public virtual void SetImage(Bitmap image)
        {
            SourceData = new ImageData(image);
            switch (scoringMethodEnum)
            {
                case ScoringMethodEnum.ErrorDataScoring:
                    scoringMethod = new ErrorDataScoring(SourceData);
                    break;
                case ScoringMethodEnum.FullEvaluationScoring:
                    scoringMethod = new FullEvaluationScoring(this);
                    break;
            }
        }

        protected PictureMutationAlgorithm(ScoringMethodEnum scoringMethodEnum)
        {
            this.scoringMethodEnum = scoringMethodEnum;
            FoundBetter += s =>
            {
                CurrentPicture = Current.Mutation.ApplyToPicture(Current.Original);
                scoringMethod.FoundBetter(CurrentPicture);
            };
        }

        public abstract IEnumerable<IPictureMutation> GetOutgoingMutationsOfCurrent();
        public abstract VectorPicture GetInitialPicture(IAlgorithm algorithm);

        protected override sealed IEnumerable<PictureMutationPosingAsPicture> GetOutgoingOfCurrent()
        {
            return GetOutgoingMutationsOfCurrent().Select(
                mutation => new PictureMutationPosingAsPicture(Current.Original, mutation));
        }

        protected override sealed double Score(PictureMutationPosingAsPicture solution)
        {
            var picture = solution.GetVectorPicture();
            return 1.0 / (scoringMethod.GetError(solution.Mutation) + GetPictureError(picture));
        }

        private int GetPictureError(VectorPicture picture)
        {
            return picture.Polygons.Count + picture.Polygons.Select(p => p.Points.Count).Sum();
        }

        public void SetPicture(VectorPicture vectorPicture)
        {
            CurrentPicture = vectorPicture;
            SetCurrent(new PictureMutationPosingAsPicture(CurrentPicture, new EmptyMutation()));
        }

        public void SetInitial()
        {
            SetPicture(GetInitialPicture(this));
        }

        IVectorPicture IEnumerator<IVectorPicture>.Current
        {
            get { return Current; }
        }

        public ImageData SourceData { get; private set; }
    }
}
