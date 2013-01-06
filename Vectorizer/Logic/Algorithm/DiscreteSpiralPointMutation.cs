using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Generic.Collections.List;
using Generic.Maths;
using Vectorizer.Logic.Drawing;
using Rectangle = System.Drawing.Rectangle;

namespace Vectorizer.Logic.Algorithm
{
    internal class DiscreteSpiralPointMutation
    {
        readonly ImageData sourceData;
        private const double EXP = 0.5;

        public IEnumerable<IntVector2> SpiralPoints(IntVector2 start)
        {
            var rectangle = new Rectangle(0, 0, sourceData.Width, sourceData.Height);
            var spiralPoints = SpiralFromCached(start);
            var linearlyDistributed = DistributionCache.Select(index => spiralPoints[index]);
            var result = linearlyDistributed.Where(point => rectangle.Contains(point));
            return result.ToLazyList();
        }

        public DiscreteSpiralPointMutation(ImageData sourceData, double exp = EXP)
        {
            this.sourceData = sourceData;
            var random = new Random(0);
            int pixelCount = 4 * sourceData.Height * sourceData.Width;
            DistributionCache = Distribution.DistinctSampling(random, pixelCount, exp).ToLazyList();
        }

        public readonly IList<int> DistributionCache;
        public readonly IList<IntVector2> Spiral = SpiralFrom(IntVector2.Zero).ToLazyList();

        public IList<IntVector2> SpiralFromCached(IntVector2 point)
        {
            return Spiral.SelectList(offset => offset + point);
        }

        public static IEnumerable<IntVector2> SpiralFrom(IntVector2 point)
        {
            var direction = new Direction();

            var currentPoint = point;
            for (int size = 1;; size++)
            {
                for (int corner = 0; corner < 2; corner++)
                {
                    for (int step = 0; step < size; step++)
                    {
                        currentPoint = direction.Move(currentPoint);
                        yield return currentPoint;
                    }
                    direction.Turn();
                }
            }
        }

        private class Direction
        {
            private DirectionEnum direction = DirectionEnum.Up;

            public void Turn()
            {
                direction = (DirectionEnum) ((int) (direction + 1)%4);
            }

            public Point Move(Point point)
            {
                switch (direction)
                {
                    case DirectionEnum.Up:
                        return new Point(point.X, point.Y + 1);
                    case DirectionEnum.Down:
                        return new Point(point.X, point.Y - 1);
                    case DirectionEnum.Right:
                        return new Point(point.X + 1, point.Y);
                    case DirectionEnum.Left:
                        return new Point(point.X - 1, point.Y);
                    default:
                        throw new Exception();
                }
            }

            private enum DirectionEnum
            {
                Up,
                Right,
                Down,
                Left
            }
        }
    }
}