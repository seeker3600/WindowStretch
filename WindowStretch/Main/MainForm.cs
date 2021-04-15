using System;
using System.Reactive.Linq;
using System.Windows.Forms;
using WindowStretch.Model;

#pragma warning disable IDE1006 // 命名スタイル

namespace WindowStretch.Main
{
    public partial class MainForm : Form
    {
        private readonly WindowCtlModel Ctl = new();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // 画面制御データのバインド
            // 画面表示直後の非表示設定はうまくいかない。初回のみ待ちを入れる
            var first = Ctl.WindowVisible
                .Take(1)
                .Delay(TimeSpan.FromMilliseconds(250))
                .Do(_ => Resize += MainForm_Resize);

            Ctl.WindowVisible
                .Skip(1)
                .Merge(first)
                .Subscribe(visible =>
                {
                    BeginInvoke((MethodInvoker)(() =>
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
                    }));
                });

            // ステータスラベルのバインド
            SreVm.StatusMsg
                .Merge(SttVm.Status)
                .Subscribe(msg => statusLbl.Text = msg);

            // サイズ変更タブのバインド
            SetupStretchModel();

            // アプリ起動タブのバインド
            SetupStartModel();

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
