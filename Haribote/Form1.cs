using System;
using System.Drawing;
using System.Windows.Forms;

#pragma warning disable IDE1006 // 命名スタイル

namespace Haribote
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            if (Width > Height)
                wideRadio.Checked = true;
            else
                tallRatio.Checked = true;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            sizeLabel.Text = $"{Width} x {Height}";
        }

        private void ratioRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (Width > Height != wideRadio.Checked)
                Size = new Size(Height, Width);
        }

        private void smallBtn_Click(object sender, EventArgs e)
        {
            Size = new Size(300, 600);
            Form1_ResizeEnd(sender, e);
        }

        private void scrRatioBtn_Click(object sender, EventArgs e)
        {
            var area = Screen.FromControl(this).Bounds;
            Size = new Size(area.Width / 3, area.Height / 3);
            Form1_ResizeEnd(sender, e);
        }
    }
}
