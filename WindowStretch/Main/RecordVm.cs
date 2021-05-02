using Reactive.Bindings;
using System;
using System.Windows.Forms;
using WindowStretch.Model;

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
            Bind(recordStartBtn, model.StartRecord);
            Bind(recordEndBtn, model.EndRecord);
        }

        private void selectRecordFolderBtn_Click(object sender, EventArgs e)
        {
            folderSelectDlg.SelectedPath = recordSaveTxt.Text;
            if (folderSelectDlg.ShowDialog() == DialogResult.OK)
                recordSaveTxt.Text = folderSelectDlg.SelectedPath;
        }

        private void Bind(Button button, ReactiveCommand command)
        {
            button.Enabled = command.CanExecute();

            command.CanExecuteChanged += (_, __) =>
            {
                BeginInvoke((Action)delegate ()
                {
                    button.Enabled = command.CanExecute();
                });
            };

            button.Click += (_, __) => command.Execute();
        }
    }
}
