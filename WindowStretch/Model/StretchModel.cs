using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Windows.Forms;
using WindowStretch.Core;
using WindowStretch.Properties;

namespace WindowStretch.Model
{
    using static Settings;

    public class StretchModel : IDisposable
    {
        public static List<StretchModeEntry> ModeEntries() => StretchModeEntry.Entries();

        public StretchPatternVm Wide { get; } = new StretchPatternVm();

        public StretchPatternVm Tall { get; } = new StretchPatternVm();

        public IObservable<string> StatusMsg => Status;

        private readonly Subject<string> Status = new Subject<string>();

        public ReactivePropertySlim<Rectangle> WindowRect { get; } = new ReactivePropertySlim<Rectangle>();

        private Size? BeforeSize = null;

        public void Refresh() => BeforeSize = null;

        public void Tick()
        {
            if (Control.MouseButtons.HasFlag(MouseButtons.Left))
            {
                Status.OnNext($"監視を一時停止しています。");
                return;
            }

            var procName = WindowUtils.ProcessName;
            var hwndN = WindowUtils.GetHwnd();

            if (hwndN is IntPtr hwnd)
            {
                try
                {
                    var size = StretchUtils.GetWindowSize(hwnd);

                    if (BeforeSize != size)
                    {
                        var ptnVm = size.Width >= size.Height ? Wide : Tall;
                        BeforeSize = StretchUtils.Stretch(hwnd, ptnVm.ToPattern());

                        Status.OnNext($"アプリ {procName} のウィンドウサイズを変更しました。");
                    }
                    else
                        Status.OnNext($"アプリ {procName} を監視しています。");

                    WindowRect.Value = OverlapUtils.GetNonOverlap(hwnd, WindowRect.Value);
                }
                catch (Exception)
                {
                    Status.OnNext($"アプリ {procName} の監視が失敗しました。管理者権限が必要かもしれません。");
                }
            }
            else
            {
                Status.OnNext($"アプリ {procName} が見つかりません。");
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

        private void Save()
        {
            Default.WideMode = Wide.Mode.Value;
            Default.WideAlwaysTop = Wide.AlwaysTop.Value;
            Default.WideAllowExcess = Wide.AllowExcess.Value;
            Default.TallMode = Tall.Mode.Value;
            Default.TallAlwaysTop = Tall.AlwaysTop.Value;
            Default.TallAllowExcess = Tall.AllowExcess.Value;

            Default.Save();
        }

        public void Dispose()
        {
            Save();
        }
    }

    public class StretchPatternVm
    {
        public ReactivePropertySlim<StretchMode> Mode { get; } = new ReactivePropertySlim<StretchMode>();

        public ReactivePropertySlim<bool> AlwaysTop { get; } = new ReactivePropertySlim<bool>();

        public ReactivePropertySlim<bool> AllowExcess { get; } = new ReactivePropertySlim<bool>();

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
            new StretchPattern(Mode.Value, AlwaysTop.Value, AllowExcess.Value);
    }
}