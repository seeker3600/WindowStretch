using Reactive.Bindings;
using System;
using System.Windows.Forms;

namespace WindowStretch.Main
{
    public static class Binder
    {
        /// <summary>
        /// コンポーネントの <paramref name="propertyName"/> と
        /// <paramref name="dataSource"/> の <c>Value</c> プロパティをバインドする。
        /// </summary>
        public static Binding Bind(string propertyName, object dataSource) =>
            new Binding(propertyName, dataSource, "Value", false, DataSourceUpdateMode.OnPropertyChanged);

        public static void Bind(this Button button, ReactiveCommand command)
        {
            button.Enabled = command.CanExecute();

            command.CanExecuteChanged += (_, __) =>
            {
                button.BeginInvoke((Action)delegate ()
                {
                    button.Enabled = command.CanExecute();
                });
            };

            button.Click += (_, __) => command.Execute();
        }
    }
}
