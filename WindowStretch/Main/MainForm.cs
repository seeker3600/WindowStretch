using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Windows.Forms;
using WindowStretch.Model;
using WindowStretch.Properties;

#pragma warning disable IDE1006 // 命名スタイル

namespace WindowStretch.Main
{
    public partial class MainForm : Form
    {
        /// <summary>ステータスに表示する文字列のオブザーバ</summary>
        private readonly Subject<string> StatusDrain = new Subject<string>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // メイン画面のバインド
            SetupMainForm();

            // サイズ変更タブのバインド
            SetupStretchModel();

            // アプリ起動タブのバインド
            SetupStartModel();

            // スクリーンショットタブのバインド
            SetupScreenshotModel();

            // 録画タブのバインド
            SetupRecordModel();
        }

        private void SetupMainForm()
        {
            var model = new WindowCtlModel();

            // WindowState, Visible のバインド
            Shown += (___, _____) =>
            {
                model.WindowVisible
                    .ObserveOn(SynchronizationContext.Current)
                    .Subscribe(visible =>
                    {
                        if (visible)
                        {
                            Visible = true;
                            WindowState = FormWindowState.Normal;

                            Activate();
                            BringToFront();

                            model.WindowViewed.Execute(Bounds);
                        }
                        else
                        {
                            WindowState = FormWindowState.Minimized;
                            Visible = false;
                        }
                    });

                model.WindowState.ForceNotify();

                Resize += (_, __) => model.WindowState.Value = WindowState;
                showToolMitem.Click += (_, __) => model.WindowState.Value = FormWindowState.Normal;

                notifyIcon1.MouseClick += (_, e) =>
                {
                    if (e.Button.HasFlag(MouseButtons.Left))
                        model.WindowState.Value = FormWindowState.Normal;
                };
            };

            // ステータスラベルのバインド
            StatusDrain
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(msg => functionStatusLbl.Text = msg);

            // Locationのバインド
            model.NonOverlapLocation
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(newLoc => Location = newLoc.Location);

            ResizeEnd += (_, __) => model.WindowResized.Execute(Bounds);
            watchTimer.Tick += (_, __) => model.TimerTick.Execute(Bounds);

            // モデルのバインド
            FormClosed += (_, __) => model.Dispose();
        }

        private void exitMItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Default.Save();
        }
    }
}
