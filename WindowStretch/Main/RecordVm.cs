using System;
using System.Windows.Forms;
using WindowStretch.Model;
using static WindowStretch.Main.Binder;

#pragma warning disable IDE1006 // 命名スタイル

namespace WindowStretch.Main
{
    public partial class MainForm
    {
        private void SetupRecordModel()
        {
            var model = new RecordModel();

            // モデルのバインド
            recordSaveTxt.DataBindings.Add(Bind(nameof(recordSaveTxt.Text), model.SaveFolder));
            recordStartBtn.Bind(model.StartRecord);
            recordEndBtn.Bind(model.EndRecord);
        }

        private void selectRecordFolderBtn_Click(object sender, EventArgs e)
        {
            folderSelectDlg.SelectedPath = recordSaveTxt.Text;
            if (folderSelectDlg.ShowDialog() == DialogResult.OK)
                recordSaveTxt.Text = folderSelectDlg.SelectedPath;
        }
    }
}
