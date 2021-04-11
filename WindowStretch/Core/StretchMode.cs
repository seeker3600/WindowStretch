using System.Collections.Generic;

namespace WindowStretch.Core
{
    public enum StretchMode
    {
        None,
        FullScreen,
        MaxOnDesktop,
        Manual
    }

    public record StretchModeEntry(string Text, StretchMode Mode)
    {
        public static List<StretchModeEntry> Entries() => new()
        {
            new StretchModeEntry("何もしない", StretchMode.None),
            new StretchModeEntry("全画面で表示する", StretchMode.FullScreen),
            new StretchModeEntry("デスクトップ上の最大サイズ", StretchMode.MaxOnDesktop),
            //new StretchModeEntry("指定サイズで表示する", StretchMode.Manual),
        };
    }
}
