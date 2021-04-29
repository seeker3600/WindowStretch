using Reactive.Bindings;
using System;
using System.Diagnostics;
using System.Reactive.Subjects;
using WindowStretch.Properties;

namespace WindowStretch.Model
{
    using static Settings;

    public class StartModel : IDisposable
    {
        public ReactivePropertySlim<string> Uri { get; } = new ReactivePropertySlim<string>();

        public ReactivePropertySlim<bool> StartWithMe { get; } = new ReactivePropertySlim<bool>();

        public IObservable<string> StatusMsg => Status;

        private readonly Subject<string> Status = new Subject<string>();

        public void Load()
        {
            Uri.Value = Default.StartAppUri;
            StartWithMe.Value = Default.StartWithMe;

            if (StartWithMe.Value) Start();
        }

        private void Save()
        {
            Default.StartAppUri = Uri.Value;
            Default.StartWithMe = StartWithMe.Value;

            Default.Save();
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

                Status.OnNext("アプリを起動しました。");
            }
            catch (Exception)
            {
                Status.OnNext("アプリの起動に失敗しました。タイプミス、管理者権限などを確認してください。");
            }
        }

        public void Dispose()
        {
            Save();
        }
    }
}
