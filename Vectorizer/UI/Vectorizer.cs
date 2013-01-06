using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Generic;
using Generic.Maths;
using Vectorizer.Logic.Picture;

namespace Vectorizer.UI
{
    public partial class VectorizerForm : Form
    {
        const int MINIMUM_REFRESH_PERIOD = 100;
        private Bitmap lowResBestImage;
        private Bitmap lowResLastImage;
        private Bitmap differenceImage;

        public VectorizerForm()
        {
            InitializeComponent();

            myController.AlgorithmChanged += AlgorithmChanged;
            myController.MoveNextPressed += () => updateLast = true;
            myController.TargetImageChanged += () => Target.Refresh();
            myController.NoBetterSampleFound += () => consoleTextBox.AppendText("No better sample found.");
            LowResBest.PaintMethod += LowResBestPaint;
            HighResBest.PaintMethod += HighRestBestPaint;
            LowResLast.PaintMethod += LowResLastPaint;
            HighResLast.PaintMethod += HighResLastPaint;
            Difference.PaintMethod += (s,e) => RenderBitmap(e,differenceImage);
            Target.PaintMethod += TargetPaint;

            var emptyImage = new Bitmap(1, 1);
            lowResBestImage = emptyImage;
            lowResLastImage = emptyImage;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (myController.State == Controller.States.Running)
                myController.Stop();
        }

        private void AlgorithmChanged()
        {
            myController.Algorithm.LastChanged += UpdateLast;
            myController.Algorithm.FoundBetter += InvokeBetterSampleFound;
            InvokeBetterSampleFound(0);
            updateLast = true;
            UpdateLast();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            myController.SetAlgorithm();
        }

        DateTime previousLastUpdate = DateTime.Now;
        bool updateLast;
        void UpdateLast()
        {
            if (!updateLast && (!updateLastSwitch.IsOn || ((DateTime.Now - previousLastUpdate).TotalMilliseconds <= MINIMUM_REFRESH_PERIOD)))
                return;

            updateLast = false;
            previousLastUpdate = DateTime.Now;
            BeginInvoke(new Action(() =>
            {
                RedrawLowResLast();
                LowResLast.Refresh();
                HighResLast.Refresh();
            }));
        }

        DateTime previousBetterSampleFound = DateTime.Now;
        private void InvokeBetterSampleFound(double oldScore)
        {
            BeginInvoke(new Action(BetterSampleFound));
        }

        private void BetterSampleFound()
        {
            consoleTextBox.AppendText("Better sample found! Error is: " + (int)(1 / myController.Algorithm.CurrentScore) + Environment.NewLine);
            if ((DateTime.Now - previousBetterSampleFound).TotalMilliseconds <= MINIMUM_REFRESH_PERIOD)
                return;

            previousBetterSampleFound = DateTime.Now;
            RedrawLowResBest();
            RedrawDifference();
            HighResBest.Refresh();
            Difference.Refresh();
            LowResBest.Refresh();
        }

        void RedrawDifference()
        {
            differenceImage = lowResBestImage.Maybe(() =>
            {
                lock(myController.TargetImage)
                lock(lowResBestImage)
                {
                    return GetBitmapDifference(myController.TargetImage, lowResBestImage);
                }
            });
        }

        IntVector2 TargetSize { get { return myController.TargetSize; } }
        void RedrawLowResBest()
        {
            lowResBestImage = myController.Algorithm.Current.Maybe(current =>
            {
                lock (current)
                    return current.ManualDraw(TargetSize.X, TargetSize.Y);
            });
        }

        void RedrawLowResLast()
        {
            lowResLastImage = myController.Algorithm.LastAttempt.Maybe(la =>
            {
                lock (la)
                    return la.ManualDraw(TargetSize.X, TargetSize.X);
            });
        }

        static Bitmap GetBitmapDifference(Image first, Bitmap second)
        {
            lock(second)
            {
                var result = new Bitmap(first);
                for (var x = 0; x < result.Width; x++)
                {
                    for (var y = 0; y < result.Height; y++)
                    {
                        var bitmapPixel = second.GetPixel(x, y);
                        result.SetPixel(x, y, SubstractColors(result.GetPixel(x, y), bitmapPixel));
                    }
                }
                return result;
            }
        }

        static Color SubstractColors(Color color1, Color color2)
        {
            return Color.FromArgb(255, MidColorComponent(color1.R, color2.R), MidColorComponent(color1.G, color2.G),MidColorComponent(color1.B, color2.B));
        }

        static int MidColorComponent(byte color1, byte color2)
        {
            return 128+(color1 - color2)/2;
        }

        void RenderBitmap(PaintEventArgs e, Bitmap image)
        {
            e.Graphics.Clear(Color.Black);
            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            image.Maybe(() =>
            {
                lock (image)
                    e.Graphics.DrawImage(image, 0, 0, LowResBest.CanvasSize.Width, LowResBest.CanvasSize.Height);
            });
        }


        private void TargetPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            lock(myController.TargetImage)
                e.Graphics.DrawImage(myController.TargetImage, 0, 0, Target.CanvasSize.Width, Target.CanvasSize.Height);
        }

        private void LowResLastPaint(object sender, PaintEventArgs e)
        {
            RenderBitmap(e, lowResLastImage);
        }

        private void LowResBestPaint(object snder, PaintEventArgs e)
        {
            RenderBitmap(e, lowResBestImage);
        }

        private void HighRestBestPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);
            if (myController.Algorithm == null || myController.Algorithm.CurrentPicture == null)
                return;

            DrawVectorPicture(e, myController.Algorithm.CurrentPicture, HighResBest);
        }

        private void HighResLastPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);
            var lastAttempt = myController.Algorithm.LastAttempt;
            if (myController.Algorithm == null || lastAttempt == null)
                return;

            DrawVectorPicture(e, lastAttempt.GetVectorPicture(),HighResLast);
        }

        private void RedrawButtonClick(object sender, EventArgs e)
        {
            RedrawLowResBest();
            RedrawLowResLast();
            LowResBest.Refresh();
            HighResBest.Refresh();
            LowResLast.Refresh();
            HighResLast.Refresh();
        }

        private void ClearConsoleButtonClick(object sender, EventArgs e)
        {
            consoleTextBox.Clear();
        }

        void DrawVectorPicture(PaintEventArgs e, VectorPicture lastAttempt, CanvasBox canvasBox)
        {
            var buffer = new Bitmap(canvasBox.CanvasSize.Width, canvasBox.CanvasSize.Height, PixelFormat.Format24bppRgb);
            var bufferGraphics = Graphics.FromImage(buffer);
            bufferGraphics.SmoothingMode = SmoothingMode.HighQuality;

            var scale = new FloatVector2((float)canvasBox.CanvasSize.Width / TargetSize.X,
                (float)canvasBox.CanvasSize.Height / TargetSize.Y);
            lock (lastAttempt)
                lastAttempt.SmoothDraw(bufferGraphics, scale);
            e.Graphics.DrawImage(buffer, 0, 0);
        }
    }
}