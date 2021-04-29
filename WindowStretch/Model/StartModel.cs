using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Diagnostics;
using System.Reactive.Subjects;
using WindowStretch.Properties;

namespace WindowStretch.Model
{
    public class StartModel : IDisposable
    {
        public ReactiveProperty<string> Uri { get; } =
            Settings.Default.ToReactivePropertyAsSynchronized(conf => conf.StartAppUri);

        public ReactiveProperty<bool> StartWithMe { get; } =
            Settings.Default.ToReactivePropertyAsSynchronized(conf => conf.StartWithMe);

        public IObservable<string> StatusMsg => Status;

        private readonly Subject<string> Status = new Subject<string>();

        public void OnLoad()
        {
            if (StartWithMe.Value) Start();
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
        }
    }
}
