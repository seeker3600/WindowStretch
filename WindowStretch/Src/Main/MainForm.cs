using System;
using System.Reactive.Linq;
using System.Windows.Forms;
using WindowStretch.Src.Main;

#pragma warning disable IDE1006 // 命名スタイル

namespace WindowStretch.Main
{
    public partial class MainForm : Form
    {
        private static Binding Bind(string propertyName, object dataSource) =>
            new(propertyName, dataSource, "Value", false, DataSourceUpdateMode.OnPropertyChanged);


        private readonly StretchVm SreVm;

        private readonly StartVm SttVm;

        public MainForm()
        {
            SreVm = new(this);
            SttVm = new();

            InitializeComponent();
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            // サイズ変更タブのバインド
            modeBoxW.DisplayMember = "Text";
            modeBoxW.ValueMember = "Mode";
            modeBoxW.DataSource = StretchVm.ModeEntries();

            modeBoxT.DisplayMember = "Text";
            modeBoxT.ValueMember = "Mode";
            modeBoxT.DataSource = StretchVm.ModeEntries();

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
                .Subscribe(msg => Invoke((MethodInvoker)delegate
            {
                statusLbl.Text = msg;
            }));

            SreVm.Load();
            SttVm.Load();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SreVm.Save();
            SttVm.Save();
        }

        private void MainForm_Resize(object sender, System.EventArgs e)
        {
            SreVm.WindowState.Value = WindowState;
        }

        private void notifyIcon1_Click(object sender, System.EventArgs e)
        {
            SreVm.WindowState.Value = FormWindowState.Normal;
        }

        private void watchTimer_Tick(object sender, System.EventArgs e)
        {
            SreVm.Tick();
        }

        private void updateBtn_Click(object sender, System.EventArgs e)
        {
            SreVm.Refresh();
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            SttVm.Start();
        }
    }
}
