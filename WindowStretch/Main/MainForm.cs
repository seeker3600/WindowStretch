using System;
using System.Reactive.Linq;
using System.Threading;
using System.Windows.Forms;
using WindowStretch.Model;
using WindowStretch.Properties;

#pragma warning disable IDE1006 // 命名スタイル

namespace WindowStretch.Main
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// コンポーネントの <paramref name="propertyName"/> と
        /// <paramref name="dataSource"/> の <c>Value</c> プロパティをバインドする。
        /// </summary>
        private static Binding Bind(string propertyName, object dataSource) =>
            new Binding(propertyName, dataSource, "Value", false, DataSourceUpdateMode.OnPropertyChanged);

        /// <summary>ステータスに表示する文字列のオブザーバ</summary>
        private IObserver<string> StatusDrain;

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
            model.StatusSink
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(msg => statusLbl.Text = msg);

            StatusDrain = model.StatusDrain;

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
