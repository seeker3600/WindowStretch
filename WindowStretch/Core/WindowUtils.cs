using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Windows.Sdk;

namespace WindowStretch.Core
{
    static class WindowUtils
    {
        /// <summary>
        /// ウィンドウのクライアント領域をスクリーン座標で取得する。
        /// </summary>
        /// <param name="hwnd">ウィンドウハンドル</param>
        public static Rectangle GetClientRectOnScreen(HWND hwnd)
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

            return Rectangle.FromLTRB(
                clientRect.left,
                clientRect.top,
                clientRect.right,
                clientRect.bottom
            );
        }
    }
}
