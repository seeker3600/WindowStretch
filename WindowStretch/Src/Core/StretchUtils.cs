using System;
using System.Windows.Forms;
using Microsoft.Windows.Sdk;
using System.Drawing;
using System.Diagnostics;

namespace WindowStretch.Core
{
    using static StretchMode;

    public static class StretchUtils
    {
        /// <summary>
        /// 指定されたウィンドウを、パターンに従ってサイズ変更する。
        /// </summary>
        /// <exception cref="InvalidOperationException">WindowsAPI呼び出しに失敗した。</exception>
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
                    rect = GetAfterSizeWhenFullScreen(hwnd, pat.Excess);
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

            if (!PInvoke.SetWindowPos(hwnd, hwndTop, rect.X, rect.Y, rect.Width, rect.Height, flags))
                throw new InvalidOperationException(nameof(PInvoke.SetWindowPos));
        }

        /// <summary>
        /// 指定されたウィンドウの縦横比を計算する。
        /// </summary>
        /// <returns>縦横比。ratio ＞ 1 なら横長、ratio ＜ 1 なら縦長</returns>
        public static float GetWindowAspectRatio(IntPtr hwndIp)
        {
            if (!PInvoke.GetWindowRect(new HWND(hwndIp), out var rect))
                throw new InvalidOperationException(nameof(PInvoke.GetWindowRect));

            var res = (float)(rect.right - rect.left) / (rect.bottom - rect.top);
            Debug.Assert(res > 0);

            return res;
        }

        /// <summary>
        /// 指定されたウィンドウを、縦横比を維持して最大化表示する位置とサイズを計算する。
        /// </summary>
        /// <remarks>
        /// ウィンドウ領域（枠線やタイトルバーを含む）を、画面の作業領域（タスクバーなどを除く）に最大化する。
        /// 画面の中央に移動する。
        /// </remarks>
        private static Rectangle GetAfterSizeWhenMaxOnDesktop(HWND hwnd)
        {
            var ratio = GetWindowAspectRatio(hwnd);

            // ウィンドウ領域の寸法を計算
            var area = Screen.FromHandle(hwnd).WorkingArea;
            var (width, height) = GetTangentSize(area.Size, ratio);

            // ウィンドウ位置を計算
            var left = area.X + (area.Width - width) / 2;
            var top = area.Y + (area.Height - height) / 2;

            // ウィンドウ領域を返却
            return new Rectangle(left, top, width, height);
        }

        /// <summary>
        /// 指定されたウィンドウの枠の太さを取得する。
        /// </summary>
        /// <returns>
        /// 上下左右それぞれ、クライアント領域の縁からウィンドウ領域の縁までの長さを返す。
        /// <c>left</c>と<c>top</c>は負数が想定される。これにより、<c>Width</c>と<c>Height</c>は正しい値を返す。
        /// </returns>
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

        /// <summary>
        /// 指定されたウィンドウを、縦横比を維持してフルスクリーン表示する位置とサイズを計算する。
        /// </summary>
        /// <param name="excess">端に少し隙間ができるとき、はみ出しを許容して隙間を埋める</param>
        /// <remarks>
        /// クライアント領域（枠線やタイトルバーを除く）を、画面全体に最大化する。
        /// ウィンドウ領域としては、画面全体より大きいサイズにしようとする。対象アプリによっては失敗するかもしれない。
        /// タスクバーなどを無視するため、最前面にする必要がある。
        /// </remarks>
        private static Rectangle GetAfterSizeWhenFullScreen(HWND hwnd, bool excess)
        {

            // スクリーン領域の寸法を計算
            // 実際の画面全体にウィンドウの枠線分を追加することで、
            // ウィンドウ領域をスクリーン領域いっぱいに拡大＝クライアント領域が実際の画面全体に拡大される
            var area = Screen.FromHandle(hwnd).Bounds;
            var border = GetBorder(hwnd);
            area.Offset(border.Location);
            area.Size += border.Size;

            // 幅を合わせたときの高さのズレが、固定パーセント以下か判定
            var inscribe = true;
            var ratio = GetWindowAspectRatio(hwnd);
            if (excess && Math.Abs(area.Width / ratio - area.Height) < (area.Height * 0.05)) inscribe = false;

            // ウィンドウ領域の寸法を計算
            var (width, height) = GetTangentSize(area.Size, ratio, inscribe);

            // ウィンドウ位置を計算
            var left = area.X + (area.Width - width) / 2;
            var top = area.Y + (area.Height - height) / 2;

            // ウィンドウ領域を返却
            return new Rectangle(left, top, width, height);
        }

        /// <summary>
        /// <paramref name="rect"/>に内接or外接する、縦横比<paramref name="ratio"/>の長方形を計算する。
        /// </summary>
        /// <returns>
        /// <paramref name="inscribe"/>が<c>true</c>(デフォルト値)の場合、内接する(内側に収まる)長方形を返す。
        /// <c>false</c>の場合、外接する長方形を返す。
        /// </returns>
        private static (int width, int height) GetTangentSize(Size rect, float ratio, bool inscribe = true)
        {
            // 横幅を揃えたとき、高さがheightより低くなるならtrue
            var shortHeight = rect.Width / ratio <= rect.Height;

            if (shortHeight == inscribe)
                return (rect.Width, (int)(rect.Width / ratio));
            else
                return ((int)(rect.Height * ratio), rect.Height);
        }
    }
}
