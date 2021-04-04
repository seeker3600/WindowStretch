using System;

namespace WindowStretch.Core
{
    public class StretchPattern
    {
        public StretchPattern(StretchMode mode, bool alwaysTop, int manualWidth, int manualHeight)
        {
            if (mode == StretchMode.Manual)
            {
                if (manualWidth <= 0) throw new ArgumentOutOfRangeException(nameof(manualWidth));
                if (manualHeight <= 0) throw new ArgumentOutOfRangeException(nameof(manualHeight));
            }

            Mode = mode;
            AlwaysTop = alwaysTop;
            ManualWidth = manualWidth;
            ManualHeight = manualHeight;
        }

        public StretchMode Mode { get; }

        public bool AlwaysTop { get; }

        public int ManualWidth { get; }

        public int ManualHeight { get; }
    }
}
