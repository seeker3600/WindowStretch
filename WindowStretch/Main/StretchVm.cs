using System;
using System.Reactive.Linq;
using System.Threading;
using System.Windows.Forms;
using WindowStretch.Model;
using static WindowStretch.Main.Binder;

#pragma warning disable IDE1006 // 命名スタイル

namespace WindowStretch.Main
{
    public partial class MainForm
    {
        private void SetupStretchModel()
        {
            var model = new StretchModel();

            // コンポーネントを初期設定
            modeBoxW.DisplayMember = "Text";
            modeBoxW.ValueMember = "Mode";
            modeBoxW.DataSource = StretchModel.ModeEntries();

            modeBoxT.DisplayMember = "Text";
            modeBoxT.ValueMember = "Mode";
            modeBoxT.DataSource = StretchModel.ModeEntries();

            // モデルのバインド
            modeBoxW.DataBindings.Add(Bind(nameof(modeBoxW.SelectedValue), model.Wide.Mode));
            alwaysTopChkW.DataBindings.Add(Bind(nameof(alwaysTopChkW.Checked), model.Wide.AlwaysTop));
            alwaysTopChkW.DataBindings.Add(Bind(nameof(alwaysTopChkW.Enabled), model.Wide.AlwaysTopEnabled));
            allowExcessChkW.DataBindings.Add(Bind(nameof(allowExcessChkW.Checked), model.Wide.AllowExcess));
            allowExcessChkW.DataBindings.Add(Bind(nameof(allowExcessChkW.Enabled), model.Wide.AllowExcessEnabled));

            modeBoxT.DataBindings.Add(Bind(nameof(modeBoxT.SelectedValue), model.Tall.Mode));
            alwaysTopChkT.DataBindings.Add(Bind(nameof(alwaysTopChkT.Checked), model.Tall.AlwaysTop));
            alwaysTopChkT.DataBindings.Add(Bind(nameof(alwaysTopChkT.Enabled), model.Tall.AlwaysTopEnabled));
            allowExcessChkT.DataBindings.Add(Bind(nameof(allowExcessChkT.Checked), model.Tall.AllowExcess));
            allowExcessChkT.DataBindings.Add(Bind(nameof(allowExcessChkT.Enabled), model.Tall.AllowExcessEnabled));

            RefreshSize = model.Refresh;
            watchTimer.Tick += (_, __) => model.Tick();
            FormClosed += (_, __) => model.Dispose();

            // ステータスのバインド
            model.StatusMsg
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(s => stretchStatusLbl.Text = s);

            // WindowRectのバインド
            model.WindowRect.Value = Bounds;
            model.WindowRect
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(newRect => Location = newRect.Location);

            LocationChanged += (_, __) =>
            {
                if (WindowState != FormWindowState.Minimized) model.WindowRect.Value = Bounds;
            };
        }

        private Action RefreshSize;

        private void updateBtn_Click(object sender, EventArgs e)
        {
            RefreshSize?.Invoke();
        }

    }
}
