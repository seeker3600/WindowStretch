using Reactive.Bindings;
using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Windows.Forms;
using WindowStretch.Core;
using WindowStretch.Properties;

namespace WindowStretch.Model
{
    using static FormWindowState;
    using static Settings;

    public class WindowCtlModel
    {
        public ReactivePropertySlim<FormWindowState> WindowState { get; } = new ReactivePropertySlim<FormWindowState>();

        public ReadOnlyReactivePropertySlim<bool> WindowVisible { get; }

        public IObserver<string> StatusDrain => Status;

        private Subject<string> Status = new Subject<string>();

        public ReadOnlyReactivePropertySlim<string> StatusSink { get; }

        public WindowCtlModel()
        {
            WindowVisible = WindowState
                .Select(state => state != Minimized)
                .ToReadOnlyReactivePropertySlim(mode: ReactivePropertyMode.DistinctUntilChanged);

            // ステータスラベルのバインド
            StatusSink = Status
                .DistinctUntilChanged()
                .ThrottleNoIgnore(TimeSpan.FromSeconds(1))
                .ToReadOnlyReactivePropertySlim();
        }

        public void Load()
        {
            WindowState.Value = Default.WindowState;
        }

        public void Save()
        {
            Default.WindowState = WindowState.Value;

            Default.Save();
        }
    }
}
