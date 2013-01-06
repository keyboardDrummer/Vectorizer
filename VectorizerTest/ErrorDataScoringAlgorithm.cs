using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Vectorizer.Logic.Picture;
using Vectorizer.Logic.Scoring;
using Vectorizer.Logic.Scoring.Incremental;

namespace VectorizerTest
{
    class ErrorDataScoringAlgorithm : TestAlgorithm
    {
        public ErrorDataScoringAlgorithm(Bitmap image, VectorPicture initialPicture) : base(image, initialPicture, ScoringMethodEnum.ErrorDataScoring)
        {
        }
    }
}
