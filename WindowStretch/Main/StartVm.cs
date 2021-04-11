using Reactive.Bindings;
using System;
using System.Diagnostics;
using WindowStretch.Properties;

namespace WindowStretch.Main
{
    public class StartVm
    {
        public ReactivePropertySlim<string> Uri { get; } = new();

        public ReactivePropertySlim<bool> StartWithMe { get; } = new();

        public ReactivePropertySlim<string> Status { get; } = new();

        public void Load()
        {
            Uri.Value = Settings.Default.StartAppUri;
            StartWithMe.Value = Settings.Default.StartWithMe;

            if (StartWithMe.Value) Start();
        }

        public void Save()
        {
            Settings.Default.StartAppUri = Uri.Value;
            Settings.Default.StartWithMe = StartWithMe.Value;

            Settings.Default.Save();
        }

        public void Start()
        {
            try
            {
                var info = new ProcessStartInfo(Uri.Value)
                {
                    UseShellExecute = true
                };

                Process.Start(info);

                Status.Value = "アプリを起動しました。";
            }
            catch (Exception)
            {
                Status.Value = "アプリの起動に失敗しました。タイプミス、管理者権限などを確認してください。";
            }
        }
    }
}
