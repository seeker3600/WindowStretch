using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Haribote
{
    public partial class Form1 : Form
    {
        //private float FixedRatio;

        //private Size BeforeSize;

        public Form1()
        {
            InitializeComponent();

            //FixedRatio = 2.0f;
            //Height = (int)(Width / FixedRatio);
            //BeforeSize = Size;
        }

        private bool EnableEvent = true;

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            //if (BeforeSize.Width != Width)
            //{
            //    var n = new Size(Width, (int)(Width / FixedRatio));
            //    Size = n;
            //    BeforeSize = Size;
            //}
            //else if (BeforeSize.Height != Height)
            //{
            //    Width = (int)(Height * FixedRatio);
            //    BeforeSize = Size;
            //}
            EnableEvent = false;
            if (Width > Height)
                wideRadio.Checked = true;
            else
                tallRatio.Checked = true;
            EnableEvent = true;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            sizeLabel.Text = $"{Width} x {Height}";
        }

        private void ratioRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (!EnableEvent) return;
            //FixedRatio = wideRadio.Checked ? 2.0f : 0.5f;
            Size = new Size(Height, Width);
        }

    }
}
