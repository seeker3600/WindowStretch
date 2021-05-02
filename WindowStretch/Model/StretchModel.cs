using Microsoft.Windows.Sdk;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Windows.Forms;
using WindowStretch.Core;
using WindowStretch.Properties;

namespace WindowStretch.Model
{
    public class StretchModel : IDisposable
    {
        public static List<StretchModeEntry> ModeEntries() => StretchModeEntry.Entries();

        public StretchPatternVm Wide { get; } = new StretchPatternVm(c => c.WideMode, c => c.WideAlwaysTop, c => c.WideAllowExcess);

        public StretchPatternVm Tall { get; } = new StretchPatternVm(c => c.TallMode, c => c.TallAlwaysTop, c => c.TallAllowExcess);

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

            if (hwndN is HWND hwnd)
            {
                try
                {
                    var size = StretchUtils.GetWindowSize(hwnd);

                    if (BeforeSize != size)
                    {
                        var ptnVm = size.Width >= size.Height ? Wide : Tall;
                        var stretched = StretchUtils.Stretch(hwnd, ptnVm.ToPattern());
                        BeforeSize = stretched == Size.Empty ? size : stretched;

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

        public void Dispose()
        {
        }
    }

    public class StretchPatternVm
    {
        public ReactiveProperty<StretchMode> Mode { get; }

        public ReactiveProperty<bool> AlwaysTop { get; }

        public ReactiveProperty<bool> AllowExcess { get; }

        public ReadOnlyReactivePropertySlim<bool> AlwaysTopEnabled { get; }

        public ReadOnlyReactivePropertySlim<bool> AllowExcessEnabled { get; }

        internal StretchPatternVm(
            Expression<Func<Settings, StretchMode>> mode,
            Expression<Func<Settings, bool>> alwaysTop,
            Expression<Func<Settings, bool>> allowExcess)
        {
            Mode = Settings.Default.ToReactivePropertyAsSynchronized(mode);
            AlwaysTop = Settings.Default.ToReactivePropertyAsSynchronized(alwaysTop);
            AllowExcess = Settings.Default.ToReactivePropertyAsSynchronized(allowExcess);

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