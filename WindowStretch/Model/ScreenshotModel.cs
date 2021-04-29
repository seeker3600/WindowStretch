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
    using static Settings;

    public sealed class ScreenshotModel : IDisposable
    {
        public ReactivePropertySlim<string> SaveFolder { get; } = new ReactivePropertySlim<string>();

        public ReactivePropertySlim<bool> OpenViewer { get; } = new ReactivePropertySlim<bool>();

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

            Load();
        }

        private void Load()
        {
            SaveFolder.Value = Default.ShotSaveFolder;
            OpenViewer.Value = Default.ShotOpenViewer;
        }

        private void Save()
        {
            Default.ShotSaveFolder = SaveFolder.Value;
            Default.ShotOpenViewer = OpenViewer.Value;

            Default.Save();
        }

        private void OpenImageFile(string filename)
        {
            if (OpenViewer.Value) ScreenshotUtils.OpenFileUseShell(filename);
        }

        private readonly CompositeDisposable Disposer = new CompositeDisposable();

        public void Dispose()
        {
            Save();
            Disposer.Dispose();
        }
    }

}
