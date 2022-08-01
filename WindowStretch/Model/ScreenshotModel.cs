using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Diagnostics;
using System.IO;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowStretch.Core;
using WindowStretch.Properties;

namespace WindowStretch.Model
{
    public sealed class ScreenshotModel : IDisposable
    {
        private enum ModelState
        {
            Ready,
            Recording
        }

        public ReactiveProperty<string> SaveFolder { get; } =
            Settings.Default.ToReactivePropertyAsSynchronized(conf => conf.ShotSaveFolder);

        public ReactiveCommand OpenSaveFolder { get; } = new ReactiveCommand();

        public ReactiveProperty<bool> OpenViewer { get; } =
            Settings.Default.ToReactivePropertyAsSynchronized(conf => conf.ShotOpenViewer);

        public ReactiveCommand SaveToSpecified { get; } = new ReactiveCommand();

        private readonly Subject<ModelState> State = new Subject<ModelState>();

        public ReactiveCommand StartRollshot { get; }

        public ReactiveCommand EndRollshot { get; }


        public ReactiveCommand<MouseEventArgs> DragAreaMouseMove { get; } = new ReactiveCommand<MouseEventArgs>();

        public event Action<string> CompleteSaveToTemp = _ => { };

        public IObservable<string> StatusMsg => Status;

        private readonly Subject<string> Status = new Subject<string>();

        public ScreenshotModel()
        {
            Status.AddTo(Disposer);

            SaveToSpecified
                .Select(_ => ScreenshotUtils.Take(SaveFolder.Value))
                .Do(OpenImageFile)
                .Select(_ => "スクリーンショットを取得しました。")
                .Catch(Observable.Return("スクリーンショットの取得に失敗しました。"))
                .Repeat()
                .Subscribe(Status)
                .AddTo(Disposer);

            var result = new Subject<string>().AddTo(Disposer);

            DragAreaMouseMove
                .Where(e => e.Button.HasFlag(MouseButtons.Left))
                .Select(_ => ScreenshotUtils.Take(Path.GetTempPath()))
                .Do(f => CompleteSaveToTemp(f))
                .Catch(Observable.Return(""))
                .Repeat()
                .Subscribe(result)
                .AddTo(Disposer);

            result
                .Select(f => string.IsNullOrEmpty(f) ?
                    "スクリーンショットの取得に失敗しました。" : "スクリーンショットを取得しました。")
                .Subscribe(Status)
                .AddTo(Disposer);

            result
                .Where(f => !string.IsNullOrEmpty(f))
                .Delay(TimeSpan.FromSeconds(60))
                .Do(File.Delete)
                .Repeat()
                .Subscribe()
                .AddTo(Disposer);

            OpenSaveFolder
                .Select(_ => SaveFolder.Value)
                .StartProcess()
                .Select(r => r ? "フォルダを開きました。" : "フォルダを開けませんでした。")
                .Subscribe(Status)
                .AddTo(Disposer);

            StartRollshot = State.Select(s => s == ModelState.Ready).ToReactiveCommand();
            EndRollshot = State.Select(s => s == ModelState.Recording).ToReactiveCommand();
            State.OnNext(ModelState.Ready);

            StartRollshot
                .ObserveOn(TaskPoolScheduler.Default)
                .SelectMany(_ => DoRollshot())
                .CatchIgnore((Exception _) => Status.OnNext("撮影に失敗しました。"))
                .Repeat()
                .Subscribe();

        }

        private void OpenImageFile(string filename)
        {
            if (OpenViewer.Value) ShellUtils.StartProcess(filename);
        }

        private async Task<string> DoRollshot()
        {
            try
            {
                State.OnNext(ModelState.Recording);
                Status.OnNext("撮影しています...");

                var cont = true;
                var sw = new Stopwatch();

                using (EndRollshot.Subscribe(_ => cont = false))
                using (var p = new BitmapZipper())
                {
                    while (cont)
                    {
                        sw.Restart();

                        using (var bitmap = ScreenshotUtils.Take())
                        {
                            p.Merge(bitmap);
                        }

                        sw.Stop();
                        var msecs = (int)(TimeSpan.FromMilliseconds(100) - sw.Elapsed).TotalMilliseconds;

                        if (msecs > 0) await Task.Delay(msecs);
                    }

                    return p.SaveDefaultName(SaveFolder.Value);
                }
            }
            finally
            {
                State.OnNext(ModelState.Ready);
                Status.OnNext("撮影が完了しました。");
            }
        }

        private readonly CompositeDisposable Disposer = new CompositeDisposable();

        public void Dispose()
        {
            Disposer.Dispose();
        }
    }

}
