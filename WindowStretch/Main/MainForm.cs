using Reactive.Bindings;
using System;
using System.Drawing;
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
            model.WindowVisible
                .ObserveOn(SynchronizationContext.Current)
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

            Resize += (_, __) => model.WindowState.Value = WindowState;
            showToolMitem.Click += (_, __) => model.WindowState.Value = FormWindowState.Normal;

            notifyIcon1.MouseClick += (_, e) =>
            {
                if (e.Button.HasFlag(MouseButtons.Left))
                    model.WindowState.Value = FormWindowState.Normal;
            };

            // ステータスラベルのバインド
            StatusDrain
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(msg => functionStatusLbl.Text = msg);

            // Locationのバインド
            model.NonOverlapPosition
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(newLoc => Location = newLoc);

            ResizeEnd += (_, __) =>
            {
                if (WindowState != FormWindowState.Minimized) // && model.CheckAndMoveToolWnd.CanExecute()
                    model.CheckAndMoveToolWnd.Execute(Bounds);
            };

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
