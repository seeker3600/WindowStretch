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

    public class WindowCtlModel : IDisposable
    {
        public ReactiveProperty<FormWindowState> WindowState { get; } =
            Settings.Default.ToReactivePropertyAsSynchronized(conf => conf.WindowState);

        public ReadOnlyReactivePropertySlim<bool> WindowVisible { get; }

        public ReactiveCommand<Rectangle> CheckAndMoveToolWnd { get; }

        public ReadOnlyReactivePropertySlim<Point> NonOverlapPosition { get; }

        public WindowCtlModel()
        {
            WindowVisible = WindowState
                .Select(state => state != Minimized)
                .ToReadOnlyReactivePropertySlim();

            CheckAndMoveToolWnd =
                WindowVisible.ToReactiveCommand<Rectangle>();

            NonOverlapPosition = CheckAndMoveToolWnd
                .Merge(WindowVisible.Where(v => v).Select(_ => Rectangle.Empty))
                .Scan(Rectangle.Empty, SelectLastNonEmpty)
                .Where(r => !r.IsEmpty && r.Location != NonOverlapPosition.Value)
                .Select(MoveToNonOverlapPosition)
                .Where(p => p.HasValue)
                .Select(p => p.Value)
                .ToReadOnlyReactivePropertySlim(
                    mode: ReactivePropertyMode.DistinctUntilChanged | ReactivePropertyMode.IgnoreException);
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
