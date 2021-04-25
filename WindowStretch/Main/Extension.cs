using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowStretch.Main
{
    public static class Extension
    {
        public static Binding Bind(string propertyName, object dataSource) =>
            new Binding(propertyName, dataSource, "Value", false, DataSourceUpdateMode.OnPropertyChanged);
    }
}
