using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Windows.Sdk;
using System.Drawing;
using System.Diagnostics;

namespace WindowStretch.Core
{
    using static StretchMode;

    public static class StretchUtils
    {
        public static void Stretch(IntPtr hwndIp, StretchPattern pat)
        {
            var hwnd = new HWND(hwndIp);

            Rectangle rect;
            bool alwaysTop = pat.AlwaysTop;
            bool resetPosition = false;

            switch (pat.Mode)
            {
                case FullScreen:
                    alwaysTop = true;
                    resetPosition = true;
                    rect = GetAfterSizeWhenFullScreen(hwnd);
                    break;

                case MaxOnDesktop:
                    resetPosition = true;
                    rect = GetAfterSizeWhenMaxOnDesktop(hwnd);
                    break;

                case Manual:
                    rect = new Rectangle(0, 0, pat.ManualWidth, pat.ManualHeight);
                    break;

                default:
                    return;
            }

            var hwndTop = alwaysTop ? Constants.HWND_TOPMOST : Constants.HWND_NOTOPMOST;

            SetWindowPos_uFlags flags = default;
            if (!resetPosition)
                flags |= SetWindowPos_uFlags.SWP_NOMOVE;

            PInvoke.SetWindowPos(hwnd, hwndTop, rect.X, rect.Y, rect.Width, rect.Height, flags);
        }

        // ratio > 1 なら横長、ratio < 1 なら縦長
        public static float GetWindowAspectRatio(IntPtr hwndIp)
        {
            if (!PInvoke.GetWindowRect(new HWND(hwndIp), out var rect))
                throw new InvalidOperationException(nameof(PInvoke.GetWindowRect));

            var res = (float)(rect.right - rect.left) / (rect.bottom - rect.top);
            Debug.Assert(res > 0);

            return res;
        }

        private static Rectangle GetAfterSizeWhenMaxOnDesktop(HWND hwnd)
        {
            var ratio = GetWindowAspectRatio(hwnd);

            // ウィンドウ領域の寸法を計算
            var area = Screen.FromHandle(hwnd).WorkingArea;
            var (width, height) = GetInnerMaxSize(area.Width, area.Height, ratio);

            // ウィンドウ位置を計算
            var left = area.X + (area.Width - width) / 2;
            var top = area.Y + (area.Height - height) / 2;

            // ウィンドウ領域を返却
            return new Rectangle(left, top, width, height);
        }

        private static Rectangle GetBorder(HWND hwnd)
        {
            // クライアント領域の寸法を取得
            if (!PInvoke.GetClientRect(hwnd, out var clientRect))
                throw new InvalidOperationException(nameof(PInvoke.GetClientRect));

            // クライアント領域の位置を取得
            var point = new POINT() { x = 0, y = 0 };
            if (!PInvoke.ClientToScreen(hwnd, ref point))
                throw new InvalidOperationException(nameof(PInvoke.ClientToScreen));

            // クライアント領域を計算
            clientRect.left += point.x;
            clientRect.right += point.x;
            clientRect.top += point.y;
            clientRect.bottom += point.y;

            // ウィンドウ領域を取得
            if (!PInvoke.GetWindowRect(hwnd, out var windowRect))
                throw new InvalidOperationException(nameof(PInvoke.GetWindowRect));

            return Rectangle.FromLTRB(
                windowRect.left - clientRect.left,
                windowRect.top - clientRect.top,
                windowRect.right - clientRect.right,
                windowRect.bottom - clientRect.bottom
            );
        }

        private static Rectangle GetAfterSizeWhenFullScreen(HWND hwnd)
        {
            var ratio = GetWindowAspectRatio(hwnd);

            // ウィンドウ領域の寸法を計算
            var area = Screen.FromHandle(hwnd).Bounds;
            var border = GetBorder(hwnd);
            var (width, height) = GetInnerMaxSize(area.Width + border.Width, area.Height + border.Height, ratio);

            // ウィンドウ位置を計算
            var left = area.X + (area.Width + border.Width - width) / 2 + border.Left;
            var top = area.Y + (area.Height + border.Height - height) / 2 + border.Top;

            // ウィンドウ領域を返却
            return new Rectangle(left, top, width, height);
        }

        private static (int width, int height) GetInnerMaxSize(int width, int height, float ratio)
        {
            if (width / ratio <= height)
                return (width, (int)(width / ratio));
            else
                return ((int)(height * ratio), height);
        }

    }
}
