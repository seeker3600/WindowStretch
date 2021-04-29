using System;
using System.Reactive.Linq;
using WindowStretch.Model;

#pragma warning disable IDE1006 // 命名スタイル

namespace WindowStretch.Main
{
    public partial class MainForm
    {
        private readonly StretchModel SreVm = new StretchModel();

        private void SetupStretchModel()
        {
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

            SreVm.StatusMsg.Subscribe(StatusDrain);

            SreVm.WindowRect
                .Where(newRect => Location != newRect.Location)
                .Subscribe(newRect => Location = newRect.Location);

            SreVm.Load();

            FormClosed += (_, __) => SreVm.Save();
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

    }
}
