using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Windows.Forms;
using WindowStretch.Model;

#pragma warning disable IDE1006 // 命名スタイル

namespace WindowStretch.Main
{
    public partial class MainForm : Form
    {
        private readonly WindowCtlModel Ctl = new WindowCtlModel();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // サイズ変更タブのバインド
            SetupStretchModel();

            // アプリ起動タブのバインド
            SetupStartModel();

            // スクリーンショットタブのバインド
            var sts3 = SetupScreenshotModel();

            // メイン画面のバインド
            SetupMainForm(SreVm.StatusMsg, SttVm.StatusMsg, sts3);
        }

        private void SetupMainForm(params IObservable<string>[] states)
        {
            // WindowState, Visible のバインド
            Ctl.WindowVisible
                .ObserveOn(SynchronizationContext.Current!)
                .Subscribe(visible =>
                {
                    if (visible)
                    {
                        Visible = true;
                        WindowState = FormWindowState.Normal;
                        Activate();
                    }
                    else
                    {
                        WindowState = FormWindowState.Minimized;
                        Visible = false;
                    }
                });

            // ステータスラベルのバインド
            Observable.Merge(states)
                .DistinctUntilChanged()
                .ObserveOn(SynchronizationContext.Current!)
                .Subscribe(msg => statusLbl.Text = msg);

            Ctl.Load();

            FormClosed += (_, __) => Ctl.Save();
        }

        private void MainForm_Resize(object? sender, EventArgs e)
        {
            Ctl.WindowState.Value = WindowState;
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button.HasFlag(MouseButtons.Left))
                Ctl.WindowState.Value = FormWindowState.Normal;
        }

        private void showToolMitem_Click(object sender, EventArgs e)
        {
            Ctl.WindowState.Value = FormWindowState.Normal;
        }

        private void exitMItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
