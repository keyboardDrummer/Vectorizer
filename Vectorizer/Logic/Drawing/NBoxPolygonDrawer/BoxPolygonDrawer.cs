using System;
using System.Collections.Generic;
using System.Linq;
using Generic.Collections.Enumerables;
using Generic.Collections.Enumerators;
using Generic.Collections.List;
using Generic.Maths;
using Generic.Maths.Lines;
using Polygon = Vectorizer.Logic.Picture.Polygon;

namespace Vectorizer.Logic.Drawing.NBoxPolygonDrawer
{
    //Only works for polygons that are not self-intersecting.
    public class BoxPolygonDrawer
    {
    readonly IList<Box> boxes = new List<Box>();
    readonly IDictionary<int, int> yToBoxIndex = new Dictionary<int, int>();

    public BoxPolygonDrawer(Polygon polygon)
    {
        SetBoxes(polygon);
        AddLinesToBoxes(polygon);
        foreach (var box in boxes)
            box.SortLines();
    }

    void SetBoxes(Polygon polygon)
    {
        GetSortedYs(polygon).HoldHandsLine().ForEachIndex(
            (index,bottomTop) =>
            {
                yToBoxIndex[bottomTop.Item1] = index;
                yToBoxIndex[bottomTop.Item2] = index+1;
                boxes.Add(new Box(bottomTop.Item1, bottomTop.Item2));
            });
    }

    void AddLinesToBoxes(Polygon polygon)
    {
        foreach (var line in polygon.GetLines())
        {
            if (line.IsHorizontal())
                continue;

            int boxIndex1 = yToBoxIndex[(int)line.BottomY];
            int boxIndex2 = yToBoxIndex[(int)line.TopY];
            int boxStartIndex = Math.Min(boxIndex1, boxIndex2);
            int boxEndIndex = Math.Max(boxIndex1, boxIndex2);
            foreach(var boxIndex in ListUtil.FromTo(boxStartIndex, boxEndIndex - 1))
                boxes[boxIndex].AddLine(line);
        }
    }

     static IList<int> GetSortedYs(Polygon polygon)
    {
        return polygon.Points.SelectList(point => point.Y).Distinct().OrderBy(x => x).ToList();
    }

    public IEnumerable<IntVector2> GetPoints()
    {
        return from box in boxes
                from couple in box
                from point in GetLineBoxEnclosingPoints(box, couple.Item1, couple.Item2)
                select point;
    }

    static IEnumerable<IntVector2> GetLineBoxEnclosingPoints(Box box, IBoundedLine leftLine, IBoundedLine rightLine)
    {
        return from y in ListUtil.FromTo(box.Bottom, box.Top - 1)
                let rightX = (int)rightLine.X(y)
                from x in ListUtil.FromTo((int)leftLine.X(y), rightX)
                select (new IntVector2(x, y));
    }

        public static IEnumerable<IntVector2> GetPoints(Polygon polygon)
        {
            return new BoxPolygonDrawer(polygon).GetPoints();
        }

        //For debug purposes.

        #region Nested type: LineBoxEnclosingPointsEnumerator

        class LineBoxEnclosingPointsEnumerator : DefaultEnumerator<IntVector2>
        {
            readonly Box box;
            readonly IBoundedLine leftLine;
            readonly IBoundedLine rightLine;
            int x;
            int xRight;
            int y;

            public LineBoxEnclosingPointsEnumerator(Box box, IBoundedLine leftLine, IBoundedLine rightLine)
            {
                this.box = box;
                this.leftLine = leftLine;
                this.rightLine = rightLine;

                y = box.Bottom;
            }

            public override IntVector2 Current
            {
                get { return new IntVector2(x, y); }
            }

            public override bool MoveNext()
            {
                if (y <= box.Top)
                {
                    if (x < xRight)
                    {
                        x++;
                        return true;
                    }
                    y++;
                    x = (int)Math.Ceiling(leftLine.X(y)) - 1;
                    xRight = (int)Math.Floor(rightLine.X(y));
                    return MoveNext();
                }
                return false;
            }
        }

        #endregion
    }
}
