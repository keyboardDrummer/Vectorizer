using System.Collections.Generic;
using System.Drawing;
using Generic.Collections.Enumerables;
using Generic.Maths;
using Vectorizer.Logic.Picture;

namespace Vectorizer.Logic.Algorithm
{
    public class ImageData
    {
        private readonly VColor[,] data;

        public ImageData(Bitmap image)
        {
            Width = image.Width;
            Height = image.Height;
            data = new VColor[Width,Height];
            foreach (var point in GetPoints())
            {
                var c = new VColor(image.GetPixel(point.X, point.Y));
                this[point] = c;
            }
        }

        public int Width { get; set; }
        public int Height { get; set; }

        public Point Min
        {
            get { return new IntVector2(0, 0); }
        }

        public Point Max
        {
            get { return new IntVector2(Width, Height); }
        }

        public VColor this[IntVector2 point]
        {
            get { return data[point.X, point.Y]; }
            set { data[point.X, point.Y] = value; }
        }

        public VColor this[int x, int y]
        {
            get { return data[x, y]; }
            set { data[x, y] = value; }
        }

        public IEnumerable<IntVector2> GetPoints()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    yield return new IntVector2(x, y);
                }
            }
        }

        public IEnumerable<ColouredPoint> GetColouredPoints()
        {
            return GetPoints().Transform(point => new ColouredPoint(point, this[point]));
        }
    }
}