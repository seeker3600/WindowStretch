using System;
using System.Reactive.Linq;
using System.Windows.Forms;
using WindowStretch.Model;

#pragma warning disable IDE1006 // 命名スタイル

namespace WindowStretch.Main
{
    public partial class MainForm : Form
    {
        private static Binding Bind(string propertyName, object dataSource) =>
            new(propertyName, dataSource, "Value", false, DataSourceUpdateMode.OnPropertyChanged);

        private readonly StretchModel SreVm;

        private readonly StartModel SttVm;

        private readonly WindowCtlModel Ctl;

        public MainForm()
        {
            InitializeComponent();

            SreVm = new();
            SttVm = new();
            Ctl = new();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // 画面制御データのバインド
            Ctl.WindowVisible.Subscribe(visible =>
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

            // サイズ変更タブのバインド
            modeBoxW.DisplayMember = "Text";
            modeBoxW.ValueMember = "Mode";
            modeBoxW.DataSource = StretchModel.ModeEntries();

            modeBoxT.DisplayMember = "Text";
            modeBoxT.ValueMember = "Mode";
            modeBoxT.DataSource = StretchModel.ModeEntries();

            modeBoxW.DataBindings.Add(Bind(nameof(modeBoxW.SelectedValue), SreVm.Wide.Mode));
            alwaysTopChkW.DataBindings.Add(Bind(nameof(alwaysTopChkW.Checked), SreVm.Wide.AlwaysTop));
            alwaysTopChkW.DataBindings.Add(Bind(nameof(alwaysTopChkW.Enabled), SreVm.Wide.AlwaysTopEnabled));
            allowExcessChkW.DataBindings.Add(Bind(nameof(allowExcessChkW.Checked), SreVm.Wide.AllowExcess));
            allowExcessChkW.DataBindings.Add(Bind(nameof(allowExcessChkW.Enabled), SreVm.Wide.AllowExcessEnabled));

            modeBoxT.DataBindings.Add(Bind(nameof(modeBoxT.SelectedValue), SreVm.Tall.Mode));
            alwaysTopChkT.DataBindings.Add(Bind(nameof(alwaysTopChkT.Checked), SreVm.Tall.AlwaysTop));
            alwaysTopChkT.DataBindings.Add(Bind(nameof(alwaysTopChkT.Enabled), SreVm.Tall.AlwaysTopEnabled));
            allowExcessChkT.DataBindings.Add(Bind(nameof(allowExcessChkT.Checked), SreVm.Tall.AllowExcess));
            allowExcessChkT.DataBindings.Add(Bind(nameof(allowExcessChkT.Enabled), SreVm.Tall.AllowExcessEnabled));

            // アプリ起動タブのバインド
            appUriTxt.DataBindings.Add(Bind(nameof(appUriTxt.Text), SttVm.Uri));
            startWithMeChk.DataBindings.Add(Bind(nameof(startWithMeChk.Checked), SttVm.StartWithMe));

            // ステータスラベル
            SreVm.StatusMsg
                .Merge(SttVm.Status)
                .Subscribe(msg => statusLbl.Text = msg);

            SreVm.WindowRect
                .Where(newRect => Location != newRect.Location)
                .Subscribe(newRect => Location = newRect.Location);

            SreVm.Load();
            SttVm.Load();
            Ctl.Load();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Ctl.Save();
            SreVm.Save();
            SttVm.Save();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            Ctl.WindowState.Value = WindowState;
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            Ctl.WindowState.Value = FormWindowState.Normal;
        }

        private void MainForm_LocationChanged(object sender, EventArgs e)
        {
            SreVm.WindowRect.Value = Bounds;
        }

        private void watchTimer_Tick(object sender, EventArgs e)
        {
            SreVm.Tick();
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            SreVm.Refresh();
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            SttVm.Start();
        }
    }
}
