using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using WindowStretch.Core;
using WindowStretch.Properties;

namespace WindowStretch.Main
{
    using static FormWindowState;

    public class StretchVm
    {
        public static List<StretchModeEntry> ModeEntries() => StretchModeEntry.Entries();

        public StretchPatternVm Wide { get; } = new();

        public StretchPatternVm Tall { get; } = new();

        public ReactivePropertySlim<string> StatusMsg { get; } = new();

        public ReactivePropertySlim<FormWindowState> WindowState { get; } = new();

        public StretchVm(MainForm form)
        {
            WindowState
                .Where(st => st == Minimized)
                .Subscribe(_ => form.Visible = false);

            WindowState
                .Pairwise()
                .Where(st => st.OldItem != Normal && st.NewItem == Normal)
                .Subscribe(_ =>
                {
                    form.Visible = true;
                    form.WindowState = Normal;
                    form.Activate();
                });
        }

        private float? BeforeRatio = null;

        public void Update()
        {
            BeforeRatio = null;
            Tick();
        }

#if DEBUG
        public const string ProcessName = "Haribote";
#else
        public const string ProcessName = "umamusume";
#endif

        // TODO fat vm
        public void Tick()
        {
            var procs = Process.GetProcessesByName(ProcessName);

            if (procs.Length == 0)
            {
                StatusMsg.Value = $"アプリ {ProcessName} が見つかりません。本ツールが管理者権限で起動していないかもしれません。";
                return;
            }

            procs[0].WaitForInputIdle();

            var hwnd = procs[0].MainWindowHandle;
            var ratio = StretchUtils.GetWindowAspectRatio(hwnd);

            if (!BeforeRatio.HasValue || BeforeRatio != ratio)
            {
                if (ratio >= 1.0f)
                    StretchUtils.Stretch(hwnd, Wide.ToPattern());
                else
                    StretchUtils.Stretch(hwnd, Tall.ToPattern());

                BeforeRatio = ratio;
                StatusMsg.Value = $"アプリ {ProcessName} のウィンドウサイズを変更しました。";
            }
            else
                StatusMsg.Value = $"アプリ {ProcessName} を監視しています。";
        }

        public void Load()
        {
            Wide.Mode.Value = (StretchMode)Settings.Default.WideMode;
            Wide.AlwaysTop.Value = Settings.Default.WideAlwaysTop;
            Tall.Mode.Value = (StretchMode)Settings.Default.TallMode;
            Tall.AlwaysTop.Value = Settings.Default.TallAlwaysTop;

            WindowState.Value = (FormWindowState)Settings.Default.WindowState;
        }

        public void Save()
        {
            Settings.Default.WideMode = (int)Wide.Mode.Value;
            Settings.Default.WideAlwaysTop = Wide.AlwaysTop.Value;
            Settings.Default.TallMode = (int)Tall.Mode.Value;
            Settings.Default.TallAlwaysTop = Tall.AlwaysTop.Value;

            Settings.Default.WindowState = (int)WindowState.Value;

            Settings.Default.Save();
        }
    }

    public class StretchPatternVm
    {
        public ReactivePropertySlim<StretchMode> Mode { get; } = new();

        public ReactivePropertySlim<bool> AlwaysTop { get; } = new();

        public ReactivePropertySlim<int> ManualWidth { get; } = new();

        public ReactivePropertySlim<int> ManualHeight { get; } = new();

        public ReadOnlyReactivePropertySlim<bool> AlwaysTopEnabled { get; }

        public StretchPatternVm()
        {
            AlwaysTopEnabled = Mode
                .Select(m => m != StretchMode.FullScreen && m != StretchMode.None)
                .ToReadOnlyReactivePropertySlim();
        }

        public StretchPattern ToPattern() =>
            new(Mode.Value, AlwaysTop.Value, ManualWidth.Value, ManualHeight.Value);
    }
}