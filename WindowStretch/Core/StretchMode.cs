using System;
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

    public class StretchModeEntry
    {
        public string Text { get; }

        public StretchMode Mode { get; }

        public StretchModeEntry(string text, StretchMode mode)
        {
            Text = text ?? throw new ArgumentNullException(nameof(text));
            Mode = mode;
        }

        public static List<StretchModeEntry> Entries() => new List<StretchModeEntry>()
        {
            new StretchModeEntry("何もしない", StretchMode.None),
            new StretchModeEntry("全画面で表示する", StretchMode.FullScreen),
            new StretchModeEntry("デスクトップ上の最大サイズ", StretchMode.MaxOnDesktop),
            //new StretchModeEntry("指定サイズで表示する", StretchMode.Manual),
        };
    }
}
