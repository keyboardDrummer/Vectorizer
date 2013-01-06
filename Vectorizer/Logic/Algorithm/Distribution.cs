using System;
using System.Collections.Generic;
using System.Linq;
using Generic.Collections.List;

namespace Vectorizer.Logic.Algorithm
{
    public static class Distribution
    {
        /* Linear Distribution
         * x from 0 to 1
         * P(x) = 2*(1-x)
         * A(x) = x - x^2/2
         * -1/2 (x - 1)^2 + 1/2 = A(x)
         * x = 1 - Root (- A(x)*2 + 1 )   (twee mogelijkheden +- bij wortel)
         * A(x) = random.NextDouble()/2
         */

        public static double Sample(Random random, double exp = 0.5)
        {
            return 1 - Math.Pow(1 - random.NextDouble(), exp);
        }

        public static IEnumerable<T> ApplySlow<T>(Random random, IList<T> xs)
        {
            return ListUtil.Repeat(() => (int)(xs.Count * Sample(random))).Distinct().Take(xs.Count).Select(index => xs[index]);
        }

        public static IEnumerable<int> DistinctSampling(Random random, int count, double exp = 0.5)
        {
            var indexes = ListUtil.FromTo(0, count);
            int itemsLeft = count;
            for (int i = 0; i < count; i++)
            {
                int index = (int)(itemsLeft * Sample(random, exp));
                int element = indexes[index];
                indexes = RemoveRewrite(indexes, index);
                itemsLeft--;
                yield return element;
            }
        }

        static IList<int> RemoveRewrite(this IList<int> xs, int index)
        {
            if (xs is Concat<int>)
            {
                var concat = (Concat<int>)xs;
                if (concat.First.Count <= index)
                    return concat.First.ConcatRewrite(RemoveRewrite(concat.Second, index - concat.First.Count));

                return RemoveRewrite(concat.First, index).ConcatRewrite(concat.Second);
            }
            var fromTo = (FromToInt)xs;
            var midValue = fromTo.From+index;
            return ListUtil.FromTo(fromTo.From, midValue - 1).ConcatRewrite(ListUtil.FromTo(midValue + 1, fromTo.To));
        }
    }
}
