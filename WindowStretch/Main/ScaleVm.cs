using System;
using System.Windows.Forms;
using WindowStretch.Model;
using static WindowStretch.Main.Binder;

#pragma warning disable IDE1006 // 命名スタイル

namespace WindowStretch.Main
{
    public partial class MainForm
    {
        private void SetupScaleOverlayModel()
        {
            var model = new ScaleOverlayModel();

            // モデルのバインド
            scaleEnableChk.DataBindings.Add(Bind(nameof(scaleEnableChk.Checked), model.ScaleEnabled));

            watchTimer.Tick += (_, __) => model.Tick();
        }
    }
}
