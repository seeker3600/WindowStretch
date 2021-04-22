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
        private IObservable<string> SetupScreenshotModel()
        {
            var model = new ScreenshotModel();

            scrshotSaveTxt.DataBindings.Add(Bind(nameof(scrshotSaveTxt.Text), model.SaveFolder));
            scrshotTakeAndOpenChk.DataBindings.Add(Bind(nameof(scrshotTakeAndOpenChk.Checked), model.OpenViewer));

            takeScrshotBtn.Click += (_, _) => model.SaveToSpecified.Execute();
            scrshotDragLbl.MouseDown += (_, e) => model.DragAreaMouseMove.Execute(e);
            FormClosed += (_, __) => model.Dispose();

            model.CompleteSaveToTemp += DragImageFileFromArea;

            return model.StatusMsg;
        }

        private void selectScrshotFolderBtn_Click(object sender, EventArgs e)
        {
            scrshotFolderDlg.SelectedPath = scrshotSaveTxt.Text;
            if (scrshotFolderDlg.ShowDialog() == DialogResult.OK)
                scrshotSaveTxt.Text = scrshotFolderDlg.SelectedPath;
        }

        private void DragImageFileFromArea(string filename)
        {
            var dataObj = new DataObject(DataFormats.FileDrop, new[] { filename });
            scrshotDragLbl.DoDragDrop(dataObj, DragDropEffects.Copy);
        }
    }
}
