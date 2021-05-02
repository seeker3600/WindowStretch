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
        public ReactiveProperty<string> SaveFolder { get; } =
            Settings.Default.ToReactivePropertyAsSynchronized(conf => conf.RecordSaveFolder);

        private readonly Subject<bool> Recording = new Subject<bool>();

        public ReactiveCommand StartRecord { get; }

        public ReactiveCommand EndRecord { get; }

        public IObservable<string> StatusMsg => Status;

        private readonly Subject<string> Status = new Subject<string>();

        public RecordModel()
        {
            StartRecord = Recording.Select(b => !b).ToReactiveCommand();
            EndRecord = Recording.ToReactiveCommand();
            Recording.OnNext(false);

            StartRecord
                .ObserveOn(TaskPoolScheduler.Default)
                .SelectMany(_ => DoRecord())
                .CatchIgnore((Exception _) => Status.OnNext("録画に失敗しました。"))
                .Repeat()
                .Subscribe();
        }

        private async Task<string> DoRecord()
        {
            var hwnd = WindowUtils.GetHwnd() ?? throw new InvalidOperationException();
            var volume = GetVolume();

            using (var recorder = Recorder.CreateRecorder(new RecorderOptions()
            {
                RecorderMode = RecorderMode.Video,
                IsHardwareEncodingEnabled = true,
                DisplayOptions = new DisplayOptions
                {
                    WindowHandle = hwnd
                },
                RecorderApi = RecorderApi.WindowsGraphicsCapture,
                AudioOptions = new AudioOptions
                {
                    IsAudioEnabled = true,
                    Channels = AudioChannels.Stereo,
                    Bitrate = AudioBitrate.bitrate_192kbps,
                    IsInputDeviceEnabled = false,
                    InputVolume = 0.0f,
                    OutputVolume = 1.0f / volume * 2,
                },
                VideoOptions = new VideoOptions
                {
                    //BitrateMode = BitrateControlMode.UnconstrainedVBR,
                    //Bitrate = 8000 * 1000,
                    Framerate = 60,
                    IsFixedFramerate = true,
                    EncoderProfile = H264Profile.High
                },
                MouseOptions = new MouseOptions
                {
                    IsMouseClicksDetected = false,
                    IsMousePointerEnabled = true,
                }
            }))
            {
                try
                {
                    var filename = Path.Combine(SaveFolder.Value, $"{DateTime.Now:yyyy-MM-dd HH-mm-ss}.mp4");

                    recorder.Record(filename);
                    Recording.OnNext(true);
                    Status.OnNext("録画を開始しました。");

                    await EndRecord;

                    recorder.Stop();
                    Recording.OnNext(false);
                    Status.OnNext("録画を終了しました。");

                    return filename;
                }
                finally
                {
                    Recording.OnNext(false);
                }
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
