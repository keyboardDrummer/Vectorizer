using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Vectorizer.Logic.Algorithm;
using Vectorizer.Logic.Picture;
using Vectorizer.Logic.Scoring;
using Vectorizer.Logic.Scoring.PictureMutation;

namespace VectorizerTest
{
    class FullEvaluationScoringAlgorithm : TestAlgorithm
    {
        public FullEvaluationScoringAlgorithm(Bitmap image, VectorPicture initialPicture) 
            : base(image, initialPicture, ScoringMethodEnum.FullEvaluationScoring)
        {
        }
    }
}
