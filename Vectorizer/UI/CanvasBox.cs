using System;
using System.Drawing;
using System.Windows.Forms;

namespace Vectorizer.UI
{
    public partial class CanvasBox : UserControl
    {
        public CanvasBox()
        {
            InitializeComponent();
        }

        public String LabelText
        {
            get { return label.Text; }
            set { label.Text = value; }
        }

        public Color TextColor
        {
            get { return label.ForeColor; }
            set { label.ForeColor = value; }
        }

        public PaintEventHandler PaintMethod
        {
            get
            {
                return canvas.PaintMethod;
            }
            set
            {
                canvas.PaintMethod += value;
            }
        }

        public override Color BackColor
        {
            get { return base.BackColor; }
            set 
            { 
                base.BackColor = value;
                canvas.BackColor = value;
            }
        }

        public Size CanvasSize
        {
            get { return canvas.Size; }
        }
    }
}
