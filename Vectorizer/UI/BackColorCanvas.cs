using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Vectorizer
{
    public partial class BackColorCanvas : UserControl
    {
        public PaintEventHandler PaintMethod { get; set; }

        public BackColorCanvas()
        {
            InitializeComponent();
            DoubleBuffered = true;
            SetStyle(ControlStyles.Opaque, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (PaintMethod == null)
                e.Graphics.Clear(BackColor);
            else PaintMethod(this,e);
        }
    }
}
