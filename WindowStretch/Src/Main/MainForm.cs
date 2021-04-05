using Reactive.Bindings;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowStretch.Main
{
    public partial class MainForm : Form
    {
        private static Binding Bind(string propertyName, object dataSource) =>
            new(propertyName, dataSource, "Value", false, DataSourceUpdateMode.OnPropertyChanged);

        private readonly StretchVm Vm;

        public MainForm()
        {
            InitializeComponent();

            Vm = new(this);

            modeBoxW.DisplayMember = "Text";
            modeBoxW.ValueMember = "Mode";
            modeBoxW.DataSource = StretchVm.ModeEntries();

            modeBoxT.DisplayMember = "Text";
            modeBoxT.ValueMember = "Mode";
            modeBoxT.DataSource = StretchVm.ModeEntries();

            modeBoxW.DataBindings.Add(Bind(nameof(modeBoxW.SelectedValue), Vm.Wide.Mode));
            alwaysTopChkW.DataBindings.Add(Bind(nameof(alwaysTopChkW.Checked), Vm.Wide.AlwaysTop));
            alwaysTopChkW.DataBindings.Add(Bind(nameof(alwaysTopChkW.Enabled), Vm.Wide.AlwaysTopEnabled));

            modeBoxT.DataBindings.Add(Bind(nameof(modeBoxT.SelectedValue), Vm.Tall.Mode));
            alwaysTopChkT.DataBindings.Add(Bind(nameof(alwaysTopChkT.Checked), Vm.Tall.AlwaysTop));
            alwaysTopChkT.DataBindings.Add(Bind(nameof(alwaysTopChkT.Enabled), Vm.Tall.AlwaysTopEnabled));

            statusLbl.DataBindings.Add(Bind(nameof(statusLbl.Text), Vm.StatusMsg));
        }

        private void watchTimer_Tick(object sender, System.EventArgs e)
        {
            Vm.Tick();
        }

        private async void updateBtn_Click(object sender, System.EventArgs e)
        {
            // TODO changed event in vm
            await Task.Delay(100);
            Vm.Update();
        }

        private void MainForm_Resize(object sender, System.EventArgs e)
        {
            Vm.WindowState.Value = WindowState;
        }

        private void notifyIcon1_Click(object sender, System.EventArgs e)
        {
            Vm.WindowState.Value = FormWindowState.Normal;
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            Vm.Load();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Vm.Save();
        }
    }
}
