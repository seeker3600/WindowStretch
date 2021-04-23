using Reactive.Bindings;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Forms;
using WindowStretch.Properties;

namespace WindowStretch.Model
{
    using static FormWindowState;
    using static Settings;

    public class WindowCtlModel
    {
        public ReactivePropertySlim<FormWindowState> WindowState { get; } = new();

        public ReadOnlyReactivePropertySlim<bool> WindowVisible { get; }

        public WindowCtlModel()
        {
            WindowVisible = WindowState
                .Select(state => state != Minimized)
                .ToReadOnlyReactivePropertySlim(mode: ReactivePropertyMode.DistinctUntilChanged);
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
