using System;
using System.Windows.Forms;
using WindowStretch.Model;

#pragma warning disable IDE1006 // 命名スタイル

namespace WindowStretch.Main
{
    public partial class MainForm
    {
        private void SetupScreenshotModel()
        {
            var model = new ScreenshotModel();

            // モデルのバインド
            scrshotSaveTxt.DataBindings.Add(Bind(nameof(scrshotSaveTxt.Text), model.SaveFolder));
            scrshotTakeAndOpenChk.DataBindings.Add(Bind(nameof(scrshotTakeAndOpenChk.Checked), model.OpenViewer));
            model.StatusMsg.Subscribe(StatusDrain);

            takeScrshotBtn.Click += (_, __) => model.SaveToSpecified.Execute();
            scrshotDragLbl.MouseDown += (_, e) => model.DragAreaMouseMove.Execute(e);

            model.CompleteSaveToTemp += DragImageFileFromArea;

            // データの読み込み
            model.Load();
            FormClosed += (_, __) => model.Dispose();
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
