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

        public ReactiveCommand<Rectangle> ReMoveWhenToolResized { get; } = new ReactiveCommand<Rectangle>();

        public ReadOnlyReactivePropertySlim<Point> NonOverlapPosition { get; }

        public WindowCtlModel()
        {
            WindowVisible = WindowState
                .Select(state => state != Minimized)
                .ToReadOnlyReactivePropertySlim();

            NonOverlapPosition = ReMoveWhenToolResized
                .Where(ValidTrigger)
                .Merge(WindowVisible.Where(v => v).Select(_ => Rectangle.Empty))
                .Scan(Rectangle.Empty, SelectLastNonEmpty)
                .Select(MoveToNonOverlapPosition)
                .Where(p => p.HasValue)
                .Select(p => p.Value)
                .ToReadOnlyReactivePropertySlim(mode: DistinctUntilChanged | IgnoreException);
        }

        private bool ValidTrigger(Rectangle r)
        {
            if (!WindowVisible.Value) return false;
            if (!r.IsEmpty && NonOverlapPosition.Value == r.Location) return false;
            if (Control.MouseButtons.HasFlag(MouseButtons.Left)) return false;

            return true;
        }

        private Rectangle SelectLastNonEmpty(Rectangle acc, Rectangle r)
        {
            if (acc.IsEmpty) return r;
            return r.IsEmpty ? acc : r;
        }

        private Point? MoveToNonOverlapPosition(Rectangle now)
        {
            try
            {
                if (now.IsEmpty) return null;

                var hwndN = TargetAppUtils.GetHwnd();
                if (!(hwndN is HWND hwnd)) return null;

                return OverlapUtils.GetNonOverlap(hwnd, now).Location;
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
