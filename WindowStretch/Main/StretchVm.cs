using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Forms;
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

        public ReactivePropertySlim<Rectangle> WindowRect { get; } = new();

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

        private Size? BeforeSize = null;

        public void Refresh() => BeforeSize = null;

#if DEBUG
        public const string ProcessName = "Haribote";
#else
        public const string ProcessName = "umamusume";
#endif

        // TODO fat vm
        public void Tick()
        {
            if (Control.MouseButtons.HasFlag(MouseButtons.Left))
            {
                StatusMsg.Value = $"監視を一時停止しています。";
                return;
            }

            var proc = Process.GetProcessesByName(ProcessName).FirstOrDefault();
            var hwnd = proc?.MainWindowHandle ?? IntPtr.Zero; // WaitForInputIdleは「権限がない」エラーになった

            if (hwnd == IntPtr.Zero)
            {
                StatusMsg.Value = $"アプリ {ProcessName} が見つかりません。";
                return;
            }

            try
            {
                var size = StretchUtils.GetWindowSize(hwnd);

                if (BeforeSize != size)
                {
                    if (size.Width >= size.Height)
                        StretchUtils.Stretch(hwnd, Wide.ToPattern());
                    else
                        StretchUtils.Stretch(hwnd, Tall.ToPattern());

                    BeforeSize = size;
                    StatusMsg.Value = $"アプリ {ProcessName} のウィンドウサイズを変更しました。";
                }
                else
                    StatusMsg.Value = $"アプリ {ProcessName} を監視しています。";

                WindowRect.Value = OverlapUtils.GetNonOverlap(hwnd, WindowRect.Value);
            }
            catch (Exception)
            {
                StatusMsg.Value = $"アプリ {ProcessName} の監視が失敗しました。管理者権限が必要かもしれません。";
            }
        }

        public void Load()
        {
            Wide.Mode.Value = (StretchMode)Settings.Default.WideMode;
            Wide.AlwaysTop.Value = Settings.Default.WideAlwaysTop;
            Wide.AllowExcess.Value = Settings.Default.WideAllowExcess;
            Tall.Mode.Value = (StretchMode)Settings.Default.TallMode;
            Tall.AlwaysTop.Value = Settings.Default.TallAlwaysTop;
            Tall.AllowExcess.Value = Settings.Default.TallAllowExcess;

            WindowState.Value = (FormWindowState)Settings.Default.WindowState;
        }

        public void Save()
        {
            Settings.Default.WideMode = (int)Wide.Mode.Value;
            Settings.Default.WideAlwaysTop = Wide.AlwaysTop.Value;
            Settings.Default.WideAllowExcess = Wide.AllowExcess.Value;
            Settings.Default.TallMode = (int)Tall.Mode.Value;
            Settings.Default.TallAlwaysTop = Tall.AlwaysTop.Value;
            Settings.Default.TallAllowExcess = Tall.AllowExcess.Value;

            Settings.Default.WindowState = (int)WindowState.Value;

            Settings.Default.Save();
        }
    }

    public class StretchPatternVm
    {
        public ReactivePropertySlim<StretchMode> Mode { get; } = new();

        public ReactivePropertySlim<bool> AlwaysTop { get; } = new();

        public ReactivePropertySlim<bool> AllowExcess { get; } = new();

        public ReadOnlyReactivePropertySlim<bool> AlwaysTopEnabled { get; }

        public ReadOnlyReactivePropertySlim<bool> AllowExcessEnabled { get; }

        public StretchPatternVm()
        {
            AlwaysTopEnabled = Mode
                .Select(m => m != StretchMode.FullScreen && m != StretchMode.None)
                .ToReadOnlyReactivePropertySlim();

            AllowExcessEnabled = Mode
                .Select(m => m == StretchMode.FullScreen)
                .ToReadOnlyReactivePropertySlim();
        }

        public StretchPattern ToPattern() =>
            new(Mode.Value, AlwaysTop.Value, AllowExcess.Value);
    }
}