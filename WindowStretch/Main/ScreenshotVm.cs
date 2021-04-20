using System;
using System.Reactive.Linq;
using System.Windows.Forms;
using WindowStretch.Model;

#pragma warning disable IDE1006 // 命名スタイル

namespace WindowStretch.Main
{
    using static Extension;

    public partial class MainForm
    {
        private readonly ScreenshotModel Scrshot = new();

        private void SetupScreenshotModel()
        {
            scrshotSaveTxt.DataBindings.Add(Bind(nameof(scrshotSaveTxt.Text), Scrshot.SaveFolder));
            scrshotTakeAndOpenChk.DataBindings.Add(Bind(nameof(scrshotTakeAndOpenChk.Checked), Scrshot.OpenViewer));

            Scrshot.Load();

            FormClosed += (_, __) => Scrshot.Save();
        }

        private void takeScrshotBtn_Click(object sender, EventArgs e)
        {
            Scrshot.SaveToSpecified.Execute();
        }

        private void selectScrshotFolderBtn_Click(object sender, EventArgs e)
        {
            scrshotFolderDlg.SelectedPath = scrshotSaveTxt.Text;
            if (scrshotFolderDlg.ShowDialog() == DialogResult.OK)
                scrshotSaveTxt.Text = scrshotFolderDlg.SelectedPath;
        }

        private void scrshotDragLbl_MouseDown(object sender, MouseEventArgs e)
        {
            Scrshot.SaveToTemp.Execute();
        }
    }
}
