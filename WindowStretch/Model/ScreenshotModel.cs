﻿using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.IO;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using WindowStretch.Core;
using WindowStretch.Properties;

namespace WindowStretch.Model
{
    using static Settings;

    public sealed class ScreenshotModel : IDisposable
    {
        public ReactivePropertySlim<string> SaveFolder { get; } = new();

        public ReactivePropertySlim<bool> OpenViewer { get; } = new();

        public ReactiveCommand SaveToSpecified { get; } = new();

        public ReactiveCommand SaveToTemp { get; } = new();

        public IObservable<string> CompleteSaveToTemp { get; }

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

            SaveToTemp
                .Select(_ => ScreenshotUtils.Take(Path.GetTempPath()))
                .Catch(Observable.Return(""))
                .Repeat()
                .Subscribe(result)
                .AddTo(Disposer);

            result
                .Select(f => string.IsNullOrEmpty(f) ?
                    "スクリーンショットの取得に失敗しました。" : "スクリーンショットを取得しました。")
                .Subscribe(status)
                .AddTo(Disposer);

            CompleteSaveToTemp = result
                .Where(f => !string.IsNullOrEmpty(f));

            CompleteSaveToTemp
                .Delay(TimeSpan.FromSeconds(10))
                .Subscribe(FileDelete)
                .AddTo(Disposer);
        }

        public void Load()
        {
            SaveFolder.Value = Default.ShotSaveFolder;
            OpenViewer.Value = Default.ShotOpenViewer;
        }

        public void Save()
        {
            Default.ShotSaveFolder = SaveFolder.Value;
            Default.ShotOpenViewer = OpenViewer.Value;

            Default.Save();
        }

        private void OpenImageFile(string filename)
        {
            if (OpenViewer.Value) WindowUtils.ExecuteUsingShell(filename);
        }

        private static void FileDelete(string filename)
        {
            try
            {
                File.Delete(filename);
            }
            catch (Exception)
            {
            }
        }

        private readonly CompositeDisposable Disposer = new();

        public void Dispose()
        {
            Disposer.Dispose();
        }
    }

}