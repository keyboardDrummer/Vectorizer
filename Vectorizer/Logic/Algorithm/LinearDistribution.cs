using System;
using System.Collections.Generic;
using System.Linq;
using Generic.Collections.List;
using Generic.Collections.NEnumerable;
using Generic.Collections.NEnumerator;

namespace Vectorizer.Logic.Algorithm
{
    public static class LinearDistribution
    {
        /* Linear Distribution
         * x from 0 to 1
         * P(x) = 2*(1-x)
         * A(x) = x - x^2/2
         * -1/2 (x - 1)^2 + 1/2 = A(x)
         * x = 1 - Root (- A(x)*2 + 1 )   (twee mogelijkheden +- bij wortel)
         * A(x) = random.NextDouble()/2
         */

        public static double Sample(Random random)
        {
            return 1 - Math.Sqrt(1 - random.NextDouble());
        }

        public static IEnumerator<T> ApplySlow<T>(IList<T> xs)
        {
            var random = new Random();
            return Lists.Repeat(() => (int) (xs.Count*Sample(random))).Distinct().Transform(x => xs[x]).GetEnumerator();
        }

        public static IEnumerator<int> DistinctSampling(Random random, int count)
        {
            var indexes = Lists.FromTo(0, count);
            return Lists.Repeat(delegate
                {
                    int index = (int) (count*Sample(random));
                    int element = indexes[index];
                    indexes = LazyRemoveIntRewrite(indexes, index);
                    return element;
                }).GetEnumerator();
        }

        private static IList<int> TakeIntRewrite(this IList<int> xs, int amount)
        {
            if (xs is Concat<int>)
            {
                var concat = (Concat<int>) xs;
                if (concat.First.Count >= amount)
                    return concat.First.TakeIntRewrite(amount);
                return concat.First.ConcatRewrite(concat.Second.TakeIntRewrite(amount - concat.First.Count));
            }

            var fromTo = (FromToInt) xs;
            return Lists.FromTo(fromTo.From, fromTo.From + amount - 1);
        }

        private static IList<int> DropIntRewrite(this IList<int> xs, int amount)
        {
            if (xs is Concat<int>)
            {
                var concat = (Concat<int>) xs;
                if (concat.First.Count <= amount)
                    return concat.Second.DropIntRewrite(amount - concat.First.Count);

                return concat.First.DropIntRewrite(amount).ConcatRewrite(concat.Second);
            }
            var fromTo = (FromToInt) xs;
            return Lists.FromTo(fromTo.From + amount, fromTo.To);
        }

        private static IList<int> LazyRemoveIntRewrite(IList<int> xs, int index)
        {
            return xs.TakeIntRewrite(index).ConcatRewrite(xs.DropIntRewrite(index+1));
        }

        private static IList<T> LazyRemove<T>(IList<T> xs, int index)
        {
            return xs.Take(index).Concat(xs.Drop(index+1));
        }

        public static IEnumerator<T> Apply<T>(IList<T> xs)
        {
            var random = new Random();
            return DistinctSampling(random, xs.Count).Transform(x => xs[x]);
        }
    }
}