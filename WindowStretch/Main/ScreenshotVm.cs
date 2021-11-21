using System;
using System.Windows.Forms;
using WindowStretch.Model;
using static WindowStretch.Main.Binder;

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
            exploreScrshotBtn.Bind(model.OpenSaveFolder);
            model.StatusMsg.Subscribe(StatusDrain);

            takeScrshotBtn.Bind(model.SaveToSpecified);
            startRollshotBtn.Bind(model.StartRollshot);
            endRollshotBtn.Bind(model.EndRollshot);

            scrshotDragLbl.MouseDown += (_, e) => model.DragAreaMouseMove.Execute(e);
            FormClosed += (_, __) => model.Dispose();

            // ドラッグ処理をバインド
            model.CompleteSaveToTemp += DragImageFileFromArea;
        }

        private void selectScrshotFolderBtn_Click(object sender, EventArgs e)
        {
            folderSelectDlg.SelectedPath = scrshotSaveTxt.Text;
            if (folderSelectDlg.ShowDialog() == DialogResult.OK)
                scrshotSaveTxt.Text = folderSelectDlg.SelectedPath;
        }

        private void DragImageFileFromArea(string filename)
        {
            BeginInvoke((Action)delegate ()
            {
                var dataObj = new DataObject(DataFormats.FileDrop, new[] { filename });
                scrshotDragLbl.DoDragDrop(dataObj, DragDropEffects.Copy);
            });
        }
    }
}
