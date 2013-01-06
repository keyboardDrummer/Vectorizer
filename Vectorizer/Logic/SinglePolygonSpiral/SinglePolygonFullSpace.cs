using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Generic.Collections.List;
using Generic.Maths;
using Vectorizer.Logic.Algorithm;
using Vectorizer.Logic.Drawing;
using Generic.Collections.Enumerables;
using Vectorizer.Logic.Picture;
using Vectorizer.Logic.Scoring.PictureMutation;
using Polygon = Vectorizer.Logic.Picture.Polygon;

namespace Vectorizer.Logic.SinglePolygonSpiral
{
    class SinglePolygonFullSpace : PictureMutationAlgorithm
    {
        DiscreteSpiralPointMutation discreteSpiralPointMutation;
        public SinglePolygonFullSpace(ScoringMethodEnum scoringMethod)
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
            discreteSpiralPointMutation = new DiscreteSpiralPointMutation(SourceData);
        }

        public override VectorPicture GetInitialPicture(IAlgorithm algorithm)
        {
            var picture = new VectorPicture();
            var polygon = new Polygon(Color.White);
            picture.Polygons.Add(polygon);
            var x0 = (int) (algorithm.SourceData.Width*0.25);
            var x1 = (int) (algorithm.SourceData.Width*0.75);
            var y0 = (int) (algorithm.SourceData.Height*0.25);
            var y1 = (int) (algorithm.SourceData.Height*0.75);
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

        private IEnumerable<PolygonChanged> OutGoingForPoint(int index)
        {
            return ListUtil.FromTo(1, Polygon.Points.Count).SelectList(range => OutGoingForPointRange(index, range)).IntertwineRate(2);
        }

        private IEnumerable<PolygonChanged> OutGoingForPointRange(int index, int range)
        {
            var polygonMutations = Diagonalizes.DiagonalizeListRate(ListUtil.FromTo(index, index + range - 1).SelectList(i =>
                GetPointMutationsForIndex(i % Polygon.Points.Count)),2).ConcatUtil().Select(xs => CombinePointMutations(xs.ToLazyList()));
            var changedPolygons = polygonMutations.Select(polygon => new PolygonChanged(polygon,0));
            var withoutSelfIntersections = changedPolygons.Where(changedPolygon => !changedPolygon.NewPolygon.IsSelfIntersecting());
            return withoutSelfIntersections;
        }

        private Polygon CombinePointMutations(IEnumerable<PointMutation> pointMutations)
        {
            var clone = Polygon.Clone();
            foreach(var addedChanged in pointMutations.OfType<AddedChangedPoint>())
            {
                clone.Points[addedChanged.Index] = addedChanged.NewPoint;
                foreach(var x in addedChanged.AddedPoints)
                {
                    ((Action<IntVector2>)(point => clone.Points.Insert(addedChanged.Index, point)))(x);
                }
            }
            foreach(var deleted in pointMutations.OfType<DeletedPoint>().OrderByDescending(dp => dp.Index))
            {
                clone.Points.RemoveAt(deleted.Index);
            }
            return clone;
        }

        private IEnumerable<PointMutation> GetPointMutationsForIndex(int index)
        {
            var removedPointPolygons = ListUtil.Singleton((PointMutation) new DeletedPoint(index));
            var changedPointPolygons = MutatePoint(index);
            var insertedPointPolygons = EnumerableUtil.From(0).Select(i => InsertPoint(index));
            var changedInserted = EnumerableUtil.ConcatUtil(changedPointPolygons, insertedPointPolygons);
            var changedInsertedDiagonal = Diagonalizes.DiagonalizeListRateEmpty(changedInserted, 2).ConcatUtil().Select(xs => CombineAddedChangedMutations(xs.ToLazyList()));
            var combined = removedPointPolygons.Concat(changedInsertedDiagonal);
            return combined;
        }

        private PointMutation CombineAddedChangedMutations(IList<PointMutation> pointMutations)
        {
            var changed = (ChangedPoint)pointMutations[0];
            var addeds = pointMutations.Skip(1).Cast<AddedPoints>();
            return new AddedChangedPoint(changed.Index,changed.NewPoint,addeds.SelectMany(added => added.Points).ToLazyList());
        }

        private IEnumerable<PointMutation> MutatePoint(int pointIndex)
        {
            return discreteSpiralPointMutation.SpiralPoints(Polygon.Points[pointIndex]).Select(point => (PointMutation)new ChangedPoint(pointIndex, point));
        }

        private IEnumerable<PointMutation> InsertPoint(int pointIndex)
        {
            var mid = (Polygon.Points[pointIndex] + Polygon.Points[(pointIndex + 1)%Polygon.Points.Count])/2;
            return discreteSpiralPointMutation.SpiralPoints(mid).Select(point => (PointMutation)new AddedPoints(pointIndex, ListUtil.Singleton(point)));
        }
    }
}
