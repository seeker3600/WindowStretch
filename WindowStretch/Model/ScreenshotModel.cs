using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.IO;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Windows.Forms;
using WindowStretch.Core;
using WindowStretch.Properties;

namespace WindowStretch.Model
{
    public sealed class ScreenshotModel : IDisposable
    {
        public ReactiveProperty<string> SaveFolder { get; } =
            Settings.Default.ToReactivePropertyAsSynchronized(conf => conf.ShotSaveFolder);

        public ReactiveProperty<bool> OpenViewer { get; } =
            Settings.Default.ToReactivePropertyAsSynchronized(conf => conf.ShotOpenViewer);

        public ReactiveCommand SaveToSpecified { get; } = new ReactiveCommand();

        public ReactiveCommand<MouseEventArgs> DragAreaMouseMove { get; } = new ReactiveCommand<MouseEventArgs>();

        public event Action<string> CompleteSaveToTemp = _ => { };

        public IObservable<string> StatusMsg { get; }

        public ScreenshotModel()
        {
            var status = new Subject<string>().AddTo(Disposer);
            StatusMsg = status.AsObservable();

            SaveToSpecified
                .Select(_ => ScreenshotUtils.Take(SaveFolder.Value))
                .Do(OpenImageFile)
                .Select(_ => "スクリーンショットを取得しました。")
                .Catch(Observable.Return("スクリーンショットの取得に失敗しました。"))
                .Repeat()
                .Subscribe(status)
                .AddTo(Disposer);

            var result = new Subject<string>().AddTo(Disposer);

            DragAreaMouseMove
                .Where(e => e.Button.HasFlag(MouseButtons.Left))
                .Select(_ => ScreenshotUtils.Take(Path.GetTempPath()))
                .ObserveOn(SynchronizationContext.Current)
                .Do(f => CompleteSaveToTemp?.Invoke(f))
                .Catch(Observable.Return(""))
                .Repeat()
                .Subscribe(result)
                .AddTo(Disposer);

            result
                .Select(f => string.IsNullOrEmpty(f) ?
                    "スクリーンショットの取得に失敗しました。" : "スクリーンショットを取得しました。")
                .Subscribe(status)
                .AddTo(Disposer);

            result
                .Where(f => !string.IsNullOrEmpty(f))
                .Delay(TimeSpan.FromSeconds(5))
                .Do(File.Delete)
                .Repeat()
                .Subscribe()
                .AddTo(Disposer);
        }

        private void OpenImageFile(string filename)
        {
            if (OpenViewer.Value) ScreenshotUtils.OpenFileUseShell(filename);
        }

        private readonly CompositeDisposable Disposer = new CompositeDisposable();

        public void Dispose()
        {
            Disposer.Dispose();
        }
    }

}
