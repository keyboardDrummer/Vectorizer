using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Generic.Collections.Enumerables;
using Generic.Collections.List;
using Generic.Maths;
using Vectorizer.Logic.Algorithm;
using Vectorizer.Logic.Drawing;
using Vectorizer.Logic.Picture;
using Vectorizer.Logic.Scoring.PictureMutation;
using Polygon = Vectorizer.Logic.Picture.Polygon;

namespace Vectorizer.Logic.SinglePolygonSetPointsSpiralMutation
{
    public class ConstantPointCount : PictureMutationAlgorithm
    {
        DiscreteSpiralPointMutation discreteSpiralPointMutation;
        public ConstantPointCount(ScoringMethodEnum scoringMethod)
            : base(scoringMethod)
        {
        }

        public override void SetImage(Bitmap image)
        {
            base.SetImage(image);
            discreteSpiralPointMutation = new DiscreteSpiralPointMutation(SourceData);
        }

        public override VectorPicture GetInitialPicture(IAlgorithm algorithm)
        {
            var source = algorithm.SourceData;
            var result = new VectorPicture();
            int x0 = (int) (source.Width*0.25);
            int x1 = (int) (source.Width*0.75);
            int y0 = (int) (source.Height*0.25);
            int y1 = (int) (source.Height*0.75);
            var points = new List<IntVector2>
                {
                    new IntVector2(x0, y0),
                    new IntVector2(x0, y1),
                    new IntVector2(x1, y1),
                    new IntVector2(x1 + 2, y1),
                    new IntVector2(x1, y0),
                    new IntVector2(x1 + 2, y0)
                };
            var polygon = new Polygon(points, new VColor(Color.White));
            result.Polygons.Add(polygon);
            return result;
        }

        public override IEnumerable<IPictureMutation> GetOutgoingMutationsOfCurrent()
        {
            return CurrentPicture.Polygons[0].Points.Indexes().SelectList(OutgoingForPoint).Intertwine();
        }

        private IEnumerable<PolygonChanged> OutgoingForPoint(int pointIndex)
        {
            var polygon = CurrentPicture.Polygons[0];
            var unfiltered = discreteSpiralPointMutation.SpiralPoints(polygon.Points[pointIndex]).Select(
                point => ReplacePoint(point, pointIndex));
            var filtered = unfiltered.Where(x => !x.NewPolygon.IsSelfIntersecting());
            return filtered;
        }

        private PolygonChanged ReplacePoint(Point point, int pointIndex)
        {
            var polyClone = CurrentPicture.Polygons[0].Clone();
            polyClone.Points.RemoveAt(pointIndex);
            polyClone.Points.Insert(pointIndex, point);
            return new PolygonChanged(polyClone, 0);
        }
    }
}