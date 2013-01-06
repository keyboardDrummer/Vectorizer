using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vectorizer.Logic.Picture;
using Vectorizer.Logic.Scoring.PictureMutation;

namespace Vectorizer.Logic.Scoring
{
    public interface IScoringMethod
    {
        void FoundBetter(VectorPicture picture);
        int GetError(IPictureMutation mutation);
    }
}
