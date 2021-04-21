using System;
using System.Reactive.Linq;
using System.Threading;
using System.Windows.Forms;
using WindowStretch.Model;

#pragma warning disable IDE1006 // 命名スタイル

namespace WindowStretch.Main
{
    using static Extension;

    public partial class MainForm
    {
        private ScreenshotModel Scrshot;

        private void SetupScreenshotModel()
        {
            Scrshot = new();

            scrshotSaveTxt.DataBindings.Add(Bind(nameof(scrshotSaveTxt.Text), Scrshot.SaveFolder));
            scrshotTakeAndOpenChk.DataBindings.Add(Bind(nameof(scrshotTakeAndOpenChk.Checked), Scrshot.OpenViewer));

            Scrshot.Load();

            Scrshot.CompleteSaveToTemp += DragImageFileFromArea;

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
            Scrshot.DragAreaMouseMove.Execute(e);
        }

        private void DragImageFileFromArea(string filename)
        {
            var dataObj = new DataObject(DataFormats.FileDrop, new[] { filename });
            scrshotDragLbl.DoDragDrop(dataObj, DragDropEffects.Copy);
        }
    }
}
