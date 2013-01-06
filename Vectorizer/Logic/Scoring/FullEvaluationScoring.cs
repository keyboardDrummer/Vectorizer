using System;
using System.Collections.Generic;
using System.Linq;
using Generic.Collections.Enumerables;
using Generic.Maths;
using Vectorizer.Logic.Drawing.NBoxPolygonDrawer;
using Vectorizer.Logic.Picture;
using Vectorizer.Logic.Scoring.PictureMutation;
using Polygon = Vectorizer.Logic.Picture.Polygon;

namespace Vectorizer.Logic.Scoring
{
    public class FullEvaluationScoring : IScoringMethod
    {
        readonly PictureMutationAlgorithm algorithm;

        readonly Func<Polygon, IEnumerable<IntVector2>> drawer = polygon => new BoxPolygonDrawer(polygon).GetPoints();
        //readonly Func<Polygon, IEnumerable<IntVector2>> drawer = PerYPolygonDrawer.GetPoints;

        public FullEvaluationScoring(PictureMutationAlgorithm algorithm)
        {
            this.algorithm = algorithm;
        }

        public void FoundBetter(VectorPicture picture)
        {
        }

public int GetError(IPictureMutation mutation)
{
    var picture = mutation.ApplyToPicture(algorithm.CurrentPicture);
    var donePixels = new bool[algorithm.SourceData.Width,algorithm.SourceData.Height];
    var totalError = 0;
    totalError += ScorePolygons(donePixels, picture);
    totalError += ScoreRemainingPixels(donePixels);
    return totalError;
}

int ScoreRemainingPixels(bool[,] donePixels)
{
    return algorithm.SourceData.GetPoints().Where(point => !donePixels[point.X, point.Y]).Sum(point => algorithm.SourceData[point].Length);
}

int ScorePolygons(bool[,] donePixels, VectorPicture picture)
{
    var totalError = 0;
    foreach (var polygon in Enumerable.Reverse(picture.Polygons))
    {
        foreach (var point in drawer(polygon).Where(point => !donePixels[point.X, point.Y]))
        {
            donePixels[point.X, point.Y] = true;
            totalError += algorithm.SourceData[point].GetAbsColorError(polygon.Color);
        }
    }
    return totalError;
}
    }
}
