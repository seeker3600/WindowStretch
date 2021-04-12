using System.Windows.Forms;

namespace WindowStretch.Main
{
    public static class Extension
    {
        public static Binding Bind(string propertyName, object dataSource) =>
            new(propertyName, dataSource, "Value", false, DataSourceUpdateMode.OnPropertyChanged);
    }

}
