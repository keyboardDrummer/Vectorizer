using System;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Vectorizer.UI
{
    public partial class NextPerSecond : UserControl
    {
        int lastChangedCount;
        DateTime lastTime;
        double currentAverage;
        const double REDUCTION = 0.9;
        readonly System.Timers.Timer timer = new System.Timers.Timer();

        public NextPerSecond()
        {
            InitializeComponent();
            timer.SynchronizingObject = this;
            timer.Elapsed += UpdateTimerTick;
        }

        Controller myController;
        public Controller MyController 
        { 
            get { return myController; }
            set
            {
                if (value != null)
                {
                    myController = value;
                    myController.StateChanged += StateChanged;
                    myController.AlgorithmChanged += () => MyController.Algorithm.LastChanged += LastChanged;
                }
            } 
        }

        void StateChanged(Controller.States state)
        {
            if (state == Controller.States.Running)
            {
                currentAverage = 0;
                lastTime = DateTime.Now;
                NextPerSecondLabel.Text = "n/a";
                lastChangedCount = 0;
                firstTick = true;
                timer.Start();
            }
            else
                timer.Stop();
        }

        private void LastChanged()
        {
            lastChangedCount++;
        }

        bool firstTick;
        void UpdateTimerTick(object sender, ElapsedEventArgs e)
        {
            if (firstTick)
            {
                firstTick = false;
                Console.Write((DateTime.Now - e.SignalTime).TotalMilliseconds);
            }
            NextPerSecondLabel.Text = ((int)currentAverage).ToString();
            var now = DateTime.Now;
            var secondsPassed = (1 + (now - lastTime).TotalMilliseconds) / 1000.0;
            var newCurrentAverage = currentAverage * REDUCTION + (1-REDUCTION) * lastChangedCount / secondsPassed;
            if (Double.IsNaN(newCurrentAverage))
                throw new Exception();
            currentAverage = newCurrentAverage;
            NextPerSecondLabel.Text = ((int)currentAverage).ToString();
            lastChangedCount = 0;
            lastTime = now;
        }
    }
}
