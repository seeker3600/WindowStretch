using Reactive.Bindings;

namespace WindowStretch.Src.Main
{
    public static class Extension
    {
        public static T? AsRp<T>(this object? obj) =>
            obj != null ? ((ReactivePropertySlim<T>)obj).Value : default;
    }

}
