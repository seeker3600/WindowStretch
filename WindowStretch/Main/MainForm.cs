using System;
using System.Reactive.Linq;
using System.Threading;
using System.Windows.Forms;
using WindowStretch.Model;

#pragma warning disable IDE1006 // 命名スタイル

namespace WindowStretch.Main
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// コンポーネントの <paramref name="propertyName"/> と
        /// <paramref name="dataSource"/> の <c>Value</c> プロパティをバインドする。
        /// </summary>
        public static Binding Bind(string propertyName, object dataSource) =>
            new Binding(propertyName, dataSource, "Value", false, DataSourceUpdateMode.OnPropertyChanged);

        private readonly WindowCtlModel Ctl = new WindowCtlModel();

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
        }

        private void SetupMainForm()
        {
            // WindowState, Visible のバインド
            Ctl.WindowVisible
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

            // ステータスラベルのバインド
            Ctl.StatusSink
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(msg => statusLbl.Text = msg);

            StatusDrain = Ctl.StatusDrain;

            Ctl.Load();

            FormClosed += (_, __) => Ctl.Save();
        }

        private void MainForm_Resize(object sender, EventArgs e)
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
