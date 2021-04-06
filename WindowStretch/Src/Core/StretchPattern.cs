using System;

namespace WindowStretch.Core
{
    public class StretchPattern
    {
        public StretchPattern(StretchMode mode, bool alwaysTop, bool excess)
        {
            //if (mode == StretchMode.Manual)
            //{
            //    if (manualWidth <= 0) throw new ArgumentOutOfRangeException(nameof(manualWidth));
            //    if (manualHeight <= 0) throw new ArgumentOutOfRangeException(nameof(manualHeight));
            //}

            Mode = mode;
            AlwaysTop = alwaysTop;
            Excess = excess;
            ManualWidth = 0;
            ManualHeight = 0;
        }

        public StretchMode Mode { get; }

        public bool AlwaysTop { get; }

        public bool Excess { get; }

        public int ManualWidth { get; }

        public int ManualHeight { get; }
    }
}
