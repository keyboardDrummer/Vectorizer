using System;
using System.Collections.Generic;
using System.Linq;
using Generic.Maths;
using Generic.Maths.Lines;
using Polygon = Vectorizer.Logic.Picture.Polygon;

namespace Vectorizer.Logic.Drawing
{
    public static class PerYPolygonDrawer
    {
        public static IEnumerable<IntVector2> GetPoints(Polygon polygon)
        {
            int yTop = int.MinValue;
            int yBot = int.MaxValue;
            foreach (var point in polygon.Points)
            {
                yTop = Math.Max(point.Y, yTop);
                yBot = Math.Min(point.Y, yBot);
            }
            var lines = polygon.GetLines().Where(line => !line.IsHorizontal()).ToList();
            for (int y = yBot; y < yTop; y++)
            {
                var lineXs = GetLineIntersections(lines, y);

                for (int lineCoupleId = 0; lineCoupleId < Math.Floor(lineXs.Count/2.0); lineCoupleId++)
                {
                    int xMax = lineXs[2*lineCoupleId + 1];
                    int xMin = lineXs[2*lineCoupleId];
                    for (int x = xMin; x <= xMax; x++)
                    {
                        yield return new IntVector2(x, y);
                    }
                }
            }
        }

        //I must ignore either all lines with the top at y, or with the bottom at y. Else I won't get even amounts of intersections.
        private static List<int> GetLineIntersections(IEnumerable<IBoundedLine> allLines, int y)
        {
            var lines = from line in allLines
                        where Math.Ceiling(line.BottomY) <= y && y < Math.Floor(line.TopY)
                        select line;

            var intersections = (from line in lines select (int) line.X(y)).ToList();
            intersections.Sort();
            return intersections;
        }
    }
}