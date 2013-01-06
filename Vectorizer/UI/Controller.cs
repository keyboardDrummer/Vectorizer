using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Generic;
using Generic.Collections.List;
using Generic.Maths;
using Vectorizer.Logic.Scoring.PictureMutation;
using Vectorizer.Logic.SinglePolygonSetPointsSpiralMutation;
using Vectorizer.Logic.SinglePolygonSpiral;

namespace Vectorizer.UI
{
    public partial class Controller : UserControl
    {
        public enum AlgorithmEnum
        {
            ConstantPointCount,
            SingleDeleteChangeInsert,
            SinglePolygonFullSpace
        }

        public enum States
        {
            NoPicture,
            Stopped,
            Running
        }

        const int TIME_OUT = 500;

        readonly List<AlgorithmEnum> algorithms = ListUtil.New(AlgorithmEnum.ConstantPointCount, AlgorithmEnum.SingleDeleteChangeInsert, AlgorithmEnum.SinglePolygonFullSpace);
        readonly List<Control> switchButtons;
        States states = States.NoPicture;
        Bitmap targetImage;
        Thread workingThread;
        IntVector2 targetSize;

        public Controller()
        {
            InitializeComponent();
            algorithms.ForEach(a => algorithmSelectMenu.Items.Add(a));
            algorithmSelectMenu.SelectedItem = AlgorithmEnum.SingleDeleteChangeInsert;
            switchButtons = new List<Control>
            {
                startButton, loadResults, nextPicture, untilBetter, startButton, saveButton, stopButton, clearResults
            };
            SetButtonAvailability();
        }

        public PictureMutationAlgorithm Algorithm { get; private set; }

        public States State
        {
            get { return states; }
            private set
            {
                states = value;
                SetButtonAvailability();
                StateChanged(State);
            }
        }

        public Bitmap TargetImage
        {
            get
            {
                return targetImage; 
            }
            set
            {
                State = States.Stopped;
                targetImage = value;
                targetSize = TargetImage.Size;
                Algorithm.SetImage(targetImage);
                if (Algorithm.CurrentPicture == null)
                    Algorithm.SetInitial();
                TargetImageChanged();
            }
        }

        public IntVector2 TargetSize
        {
            get { return targetSize; }
        }

        public event Action AlgorithmChanged = delegate { };
        public event Action MoveNextPressed = delegate { };
        public event Action TargetImageChanged = delegate { };
        public event Action NoBetterSampleFound = delegate { };
        public event Action<States> StateChanged = delegate { };

        void SetButtonAvailability()
        {
            var onButtons = new List<Control>();
            if (states == States.NoPicture || states == States.Stopped)
            {
                onButtons.Add(loadPictureButton);
                onButtons.Add(loadResults);
            }
            if (states == States.Stopped)
            {
                onButtons.Add(nextPicture);
                onButtons.Add(untilBetter);
                onButtons.Add(startButton);
                onButtons.Add(saveButton);
                onButtons.Add(clearResults);
            }
            if (states == States.Running)
                onButtons.Add(stopButton);
            foreach (var control in switchButtons)
                control.Enabled = onButtons.Contains(control);
        }

        void OpenPictureDialogFileOk(object sender, CancelEventArgs e)
        {
            TargetImage = new Bitmap(Image.FromFile(openPictureDialog.FileName));
        }

        void LoadPictureButtonClick(object sender, EventArgs e)
        {
            openPictureDialog.ShowDialog();
        }

        void NextPictureClick(object sender, EventArgs e)
        {
            Algorithm.MoveNext();
            MoveNextPressed();
        }

        void UntilBetterClick(object sender, EventArgs e)
        {
            ThreadStart work = delegate
            {
                UntilBetter();
                Invoke(new Action(delegate { State = States.Stopped; }));
            };
            workingThread = new Thread(work);
            State = States.Running;
            workingThread.Start();
        }

        bool UntilBetter()
        {
            var startPicture = Algorithm.Current;
            while (State == States.Running && Algorithm.Current.Equals(startPicture))
            {
                if (!Algorithm.MoveNext())
                    return false;
            }
            return true;
        }

        void StartButtonClick(object sender, EventArgs e)
        {
            ThreadStart work = delegate
            {
                while (State == States.Running)
                {
                    if (UntilBetter())
                        continue;

                    Invoke(new Action(() =>
                    {
                        NoBetterSampleFound();
                        State = States.Stopped;
                    }));
                    break;
                }
            };
            State = States.Running;
            workingThread = new Thread(work);
            workingThread.Start();
        }

        void StopButtonClick(object sender, EventArgs e)
        {
            Stop();
        }

        public void SetAlgorithm()
        {
            switch ((AlgorithmEnum)algorithmSelectMenu.SelectedItem)
            {
                case AlgorithmEnum.SingleDeleteChangeInsert:
                    Algorithm = new SingleDeleteChangeInsert(PictureMutationAlgorithm.ScoringMethodEnum.FullEvaluationScoring);
                    break;
                case AlgorithmEnum.SinglePolygonFullSpace:
                    Algorithm = new SinglePolygonFullSpace(PictureMutationAlgorithm.ScoringMethodEnum.FullEvaluationScoring);
                    break;
                case AlgorithmEnum.ConstantPointCount:
                    Algorithm = new ConstantPointCount(PictureMutationAlgorithm.ScoringMethodEnum.FullEvaluationScoring);
                    break;
            }
            TargetImage.Maybe(() =>
            {
                Algorithm.SetImage(TargetImage);
                Algorithm.SetInitial();
            });
            AlgorithmChanged();
        }

        void ClearResultsClick(object sender, EventArgs e)
        {
            Algorithm.SetInitial();
        }

        public void Stop()
        {
            State = States.Stopped;
            workingThread.Join(TIME_OUT);
        }

        void AlgorithmSelectMenuSelectedIndexChanged(object sender, EventArgs e)
        {
            SetAlgorithm();
        }

        void SaveButtonClick(object sender, EventArgs e)
        {
            var fileDialog = new SaveFileDialog();
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                var file = fileDialog.OpenFile();
                Serialize.Save(file,new SavedState(targetImage,Algorithm.Current.GetVectorPicture()));
            }
        }

        private void LoadResultsClick(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                var file = fileDialog.OpenFile();
                var state = Serialize.Load<SavedState>(file);
                TargetImage = state.Image;
                lock(TargetImage)
                    Algorithm.SetImage(TargetImage);
                Algorithm.SetPicture(state.VectorPicture);
            }
        }
    }
}
