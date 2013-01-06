using System;
using System.Collections.Generic;
using Vectorizer.Logic.Drawing;
using Vectorizer.Logic.Picture;

namespace Vectorizer.Logic.Algorithm
{
    public interface IAlgorithm : IEnumerator<IVectorPicture>
    {
        Double CurrentScore { get; }
        ImageData SourceData { get; }
    }
}