using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Generic.Collections.Enumerables;
using Generic.Maths.Lines;

namespace Vectorizer.Logic.Drawing.NBoxPolygonDrawer
{
    class Box : DefaultEnumerable<Tuple<IBoundedLine, IBoundedLine>>
    {
        private readonly List<IBoundedLine> lines = new List<IBoundedLine>();
        public int Bottom { get; private set; }
        public int Top { get; private set; }

        public Box(int bottom, int top)
        {
            Bottom = bottom; Top = top;
        }

        public void AddLine(IBoundedLine l) { lines.Add(l); }
        public void SortLines()
        {
            var mid = (Top + Bottom)/2;
            lines.Sort((first, second) => first.X(mid).CompareTo(second.X(mid)));
        }

        public override IEnumerator<Tuple<IBoundedLine, IBoundedLine>> GetEnumerator()
        {
            int coupleCount = lines.Count / 2;
            for (int coupleId = 0; coupleId < coupleCount; coupleId++)
            {
                var couple = Tuple.Create(lines[coupleId * 2], lines[coupleId * 2 + 1]);
                yield return couple;
            }
        }
    }
}
