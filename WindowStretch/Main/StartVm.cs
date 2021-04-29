using WindowStretch.Model;

#pragma warning disable IDE1006 // 命名スタイル

namespace WindowStretch.Main
{
    public partial class MainForm
    {
        private void SetupStartModel()
        {
            var model = new StartModel();

            // モデルのバインド
            appUriTxt.DataBindings.Add(Bind(nameof(appUriTxt.Text), model.Uri));
            startWithMeChk.DataBindings.Add(Bind(nameof(startWithMeChk.Checked), model.StartWithMe));
            model.StatusMsg.Subscribe(StatusDrain);

            startBtn.Click += (_, __) => model.Start();
            gameStartMItem.Click += (_, __) => model.Start();

            // データの読み込み
            model.Load();
            FormClosed += (_, __) => model.Dispose();
        }
    }
}
