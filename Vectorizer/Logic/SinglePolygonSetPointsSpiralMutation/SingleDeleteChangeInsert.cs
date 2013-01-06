using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Generic.Collections.Enumerables;
using Generic.Collections.List;
using Generic.Maths;
using Vectorizer.Logic.Algorithm;
using Vectorizer.Logic.Picture;
using Vectorizer.Logic.Scoring.PictureMutation;
using Polygon = Vectorizer.Logic.Picture.Polygon;

namespace Vectorizer.Logic.SinglePolygonSetPointsSpiralMutation
{
    internal class SingleDeleteChangeInsert : PictureMutationAlgorithm
    {
        const int CHANGES_PER_INSERT = 1;
        DiscreteSpiralPointMutation changeSpiralPointMutation;
        DiscreteSpiralPointMutation insertSpiralPointMutation;

        public SingleDeleteChangeInsert(ScoringMethodEnum scoringMethod)
            : base(scoringMethod)
        {
        }

        protected Polygon Polygon
        {
            get { return CurrentPicture.Polygons[0]; }
        }

        public override void SetImage(Bitmap image)
        {
            base.SetImage(image);
            insertSpiralPointMutation = new DiscreteSpiralPointMutation(SourceData, 0.5);
            changeSpiralPointMutation = new DiscreteSpiralPointMutation(SourceData, 0.0005);
        }

        public override VectorPicture GetInitialPicture(IAlgorithm algorithm)
        {
            var picture = new VectorPicture();
            picture.Polygons.Add(new Polygon(Color.White));
            var polygon = picture.Polygons[0];
            int x0 = (int)(algorithm.SourceData.Width * 0.25);
            int x1 = (int)(algorithm.SourceData.Width * 0.75);
            int y0 = (int)(algorithm.SourceData.Height * 0.25);
            int y1 = (int)(algorithm.SourceData.Height * 0.75);
            polygon.Points.Add(new IntVector2(x0, y0));
            polygon.Points.Add(new IntVector2(x0, y1));
            polygon.Points.Add(new IntVector2(x1, y1));
            polygon.Points.Add(new IntVector2(x1, y0));
            polygon.Color = new VColor(Color.White);
            return picture;
        }

        public override IEnumerable<IPictureMutation> GetOutgoingMutationsOfCurrent()
        {
            return Polygon.Points.Indexes().SelectList(OutGoingForPoint).Intertwine();
        }

        public IEnumerable<PolygonChanged> OutGoingForPoint(int index)
        {
            var changedPointPolygons = MutatePoint(index);
            var insertedPointPolygons = InsertPoint(index);
            var removedPointPolygons = ListUtil.Singleton(RemovePoint(index));
            var changeInsertPolygons = ListUtil.New<IEnumerable<IList<PolygonChanged>>>(
                changedPointPolygons.DivideInfinite(CHANGES_PER_INSERT), insertedPointPolygons.Select(
                x => ListUtil.New(x))).Intertwine().SelectMany(x => x);
            var combined = removedPointPolygons.Concat(changeInsertPolygons);
            return combined.Where(x => !x.NewPolygon.IsSelfIntersecting());
        }

        IEnumerable<PolygonChanged> MutatePoint(int pointIndex)
        {
            return changeSpiralPointMutation.SpiralPoints(Polygon.Points[pointIndex]).Select(point => ReplacePoint(point, pointIndex));
        }

        IEnumerable<PolygonChanged> InsertPoint(int pointIndex)
        {
            var halfWayPoint = (Polygon.Points[pointIndex] + Polygon.Points[(pointIndex + 1) % Polygon.Points.Count]) / 2;
            return insertSpiralPointMutation.SpiralPoints(halfWayPoint).Select(point => InsertPoint(point, pointIndex));
        }

        PolygonChanged RemovePoint(int pointIndex)
        {
            var newPolygon = Polygon.Clone();
            newPolygon.Points.RemoveAt(pointIndex);
            return new PolygonChanged(newPolygon, 0);
        }

        PolygonChanged ReplacePoint(Point point, int pointIndex)
        {
            var newPolygon = Polygon.Clone();
            newPolygon.Points.RemoveAt(pointIndex);
            newPolygon.Points.Insert(pointIndex, point);
            return new PolygonChanged(newPolygon, 0);
        }

        PolygonChanged InsertPoint(Point point, int pointIndex)
        {
            var newPolygon = Polygon.Clone();
            newPolygon.Points.Insert(pointIndex, point);
            return new PolygonChanged(newPolygon, 0);
        }
    }
}
