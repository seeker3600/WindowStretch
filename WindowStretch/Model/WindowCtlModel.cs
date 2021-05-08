using Microsoft.Windows.Sdk;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Drawing;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Forms;
using WindowStretch.Core;
using WindowStretch.Properties;

namespace WindowStretch.Model
{
    using static FormWindowState;
    using static ReactivePropertyMode;

    public class WindowCtlModel : IDisposable
    {
        public ReactiveProperty<FormWindowState> WindowState { get; } =
            Settings.Default.ToReactivePropertyAsSynchronized(conf => conf.WindowState);

        public ReadOnlyReactivePropertySlim<bool> WindowVisible { get; }

        public ReactiveCommand<Rectangle> WindowViewed { get; } = new ReactiveCommand<Rectangle>();

        public ReactiveCommand<Rectangle> WindowResized { get; } = new ReactiveCommand<Rectangle>();

        public ReactiveCommand<Rectangle> TimerTick { get; } = new ReactiveCommand<Rectangle>();

        public ReadOnlyReactivePropertySlim<Rectangle> NonOverlapLocation { get; }

        public WindowCtlModel()
        {
            WindowVisible = WindowState
                .Select(state => state != Minimized)
                .ToReadOnlyReactivePropertySlim();

            var a = WindowViewed.Where(r => ValidTrigger(r, true, true));
            var b = WindowResized.Where(r => ValidTrigger(r, false, false));
            var c = TimerTick.Where(r => ValidTrigger(r, false, true));

            NonOverlapLocation = Observable
                .Merge(a, b, c)
                .Select(MoveToNonOverlapPosition)
                .Where(p => p.HasValue)
                .Select(p => p.Value)
                .ToReadOnlyReactivePropertySlim(mode: DistinctUntilChanged | IgnoreException);
        }

        private bool ValidTrigger(Rectangle r, bool ignoreMouse, bool ignoreLastLoc)
        {
            if (r.IsEmpty) return false;
            if (!WindowVisible.Value) return false;
            if (!ignoreMouse && Control.MouseButtons.HasFlag(MouseButtons.Left)) return false;
            if (!ignoreLastLoc && NonOverlapLocation.Value == r) return false;

            return true;
        }

        private Rectangle? MoveToNonOverlapPosition(Rectangle now)
        {
            try
            {
                if (now.IsEmpty) return null;

                var hwndN = TargetAppUtils.GetHwnd();
                if (!(hwndN is HWND hwnd)) return null;

                return OverlapUtils.GetNonOverlap(hwnd, now);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Dispose()
        {
        }
    }
}
