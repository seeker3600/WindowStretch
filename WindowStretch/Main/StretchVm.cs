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
    using static Settings;

    public class StretchVm
    {
        public static List<StretchModeEntry> ModeEntries() => StretchModeEntry.Entries();

        public StretchPatternVm Wide { get; } = new();

        public StretchPatternVm Tall { get; } = new();

        public ReactivePropertySlim<string> StatusMsg { get; } = new();

        public ReactivePropertySlim<Rectangle> WindowRect { get; } = new();

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
                    var ptnVm = size.Width >= size.Height ? Wide : Tall;
                    BeforeSize = StretchUtils.Stretch(hwnd, ptnVm.ToPattern());

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
            Wide.Mode.Value = Default.WideMode;
            Wide.AlwaysTop.Value = Default.WideAlwaysTop;
            Wide.AllowExcess.Value = Default.WideAllowExcess;
            Tall.Mode.Value = Default.TallMode;
            Tall.AlwaysTop.Value = Default.TallAlwaysTop;
            Tall.AllowExcess.Value = Default.TallAllowExcess;
        }

        public void Save()
        {
            Default.WideMode = Wide.Mode.Value;
            Default.WideAlwaysTop = Wide.AlwaysTop.Value;
            Default.WideAllowExcess = Wide.AllowExcess.Value;
            Default.TallMode = Tall.Mode.Value;
            Default.TallAlwaysTop = Tall.AlwaysTop.Value;
            Default.TallAllowExcess = Tall.AllowExcess.Value;

            Default.Save();
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