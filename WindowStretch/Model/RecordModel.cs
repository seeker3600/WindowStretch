using AudioSwitcher.AudioApi.CoreAudio;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using ScreenRecorderLib;
using System;
using System.IO;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using WindowStretch.Core;
using WindowStretch.Properties;

namespace WindowStretch.Model
{
    public class RecordModel : IDisposable
    {
        private enum ModelState
        {
            Ready,
            Starting,
            Recording,
            Stopping
        }

        public ReactiveProperty<string> SaveFolder { get; } =
            Settings.Default.ToReactivePropertyAsSynchronized(conf => conf.RecordSaveFolder);

        public ReactiveCommand OpenSaveFolder { get; } = new ReactiveCommand();

        private readonly Subject<ModelState> State = new Subject<ModelState>();

        public ReactiveCommand StartRecord { get; }

        public ReactiveCommand EndRecord { get; }

        public IObservable<string> StatusMsg => Status;

        private readonly Subject<string> Status = new Subject<string>();

        public RecordModel()
        {
            StartRecord = State.Select(s => s == ModelState.Ready).ToReactiveCommand();
            EndRecord = State.Select(s => s == ModelState.Recording).ToReactiveCommand();
            State.OnNext(ModelState.Ready);

            StartRecord
                .ObserveOn(TaskPoolScheduler.Default)
                .SelectMany(_ => DoRecord())
                .CatchIgnore((Exception _) => Status.OnNext("録画に失敗しました。"))
                .Repeat()
                .Subscribe();

            OpenSaveFolder
                .Select(_ => SaveFolder.Value)
                .StartProcess()
                .Select(r => r ? "フォルダを開きました。" : "フォルダを開けませんでした。")
                .Subscribe(Status);
        }

        private async Task<string> DoRecord()
        {
            try
            {
                State.OnNext(ModelState.Starting);
                Status.OnNext("準備しています...");

                var hwnd = TargetAppUtils.GetHwnd() ?? throw new InvalidOperationException();
                var volume = GetVolume();

                using (var recorder = Recorder.CreateRecorder(new RecorderOptions()
                {
                    RecorderMode = RecorderMode.Video,
                    IsHardwareEncodingEnabled = true,
                    IsThrottlingDisabled = true,
                    IsLowLatencyEnabled = false,
                    IsMp4FastStartEnabled = false,
                    DisplayOptions = new DisplayOptions
                    {
                        WindowHandle = hwnd,
                    },
                    RecorderApi = RecorderApi.WindowsGraphicsCapture,
                    AudioOptions = new AudioOptions
                    {
                        IsAudioEnabled = true,
                        Channels = AudioChannels.Stereo,
                        Bitrate = AudioBitrate.bitrate_128kbps,
                        IsInputDeviceEnabled = false,
                        InputVolume = 0.0f,
                        OutputVolume = 1.0f / volume * 3, // TODO x3ってなんだよ
                    },
                    VideoOptions = new VideoOptions
                    {
                        BitrateMode = BitrateControlMode.UnconstrainedVBR,
                        Bitrate = 4 * 1000 * 1000,
                        Framerate = 120, // TODO これより下げるとコマ落ちする、なんでだ
                        IsFixedFramerate = false,
                        EncoderProfile = H264Profile.High
                    },
                    MouseOptions = new MouseOptions
                    {
                        IsMouseClicksDetected = false,
                        IsMousePointerEnabled = false,
                    },
                }))
                {
                    var filename = Path.Combine(SaveFolder.Value, $"{DateTime.Now:yyyy-MM-dd HH-mm-ss}.mp4");

                    recorder.Record(filename);
                    State.OnNext(ModelState.Recording);
                    Status.OnNext("録画を開始しました。");

                    await EndRecord;

                    State.OnNext(ModelState.Stopping);
                    recorder.Stop();
                    Status.OnNext("録画を終了しました。");

                    return filename;
                }
            }
            finally
            {
                State.OnNext(ModelState.Ready);
            }
        }

        private static float GetVolume()
        {
            using (var ctl = new CoreAudioController())
            {
                var defaultPlaybackDevice = ctl.DefaultPlaybackDevice;
                return (float)(defaultPlaybackDevice.Volume / 100.0);
            }
        }

        public void Dispose()
        {
        }
    }
}
