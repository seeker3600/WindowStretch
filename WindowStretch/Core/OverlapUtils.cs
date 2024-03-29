﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Windows.Sdk;

namespace WindowStretch.Core
{
    public static class OverlapUtils
    {
        /// <summary>
        /// <paramref name="hwndIp"/> のウィンドウに重ならない位置まで <paramref name="move"/> を移動する。
        /// </summary>
        /// <returns>
        /// <paramref name="move"/>の移動先。同じ位置を返すことがある。重なりが解消しないことがある。
        /// 大きさは変更しない。
        /// </returns>
        public static Rectangle GetNonOverlap(HWND hwnd, Rectangle move)
        {
            if (!PInvoke.GetWindowRect(hwnd, out var f))
                throw new InvalidOperationException(nameof(PInvoke.GetWindowRect));

            var fix = Rectangle.FromLTRB(f.left, f.top, f.right, f.bottom);

            // 重なってなければそのままの位置
            if (!fix.IntersectsWith(move)) return move;

            var area = Screen.FromHandle(hwnd).Bounds;
            if (fix.Contains(area)) return move;

            // 移動方向を判定する
            var spaces = new (Size moving, Size overlap, MoveDirection d)[] {
                (new Size(fix.Right - move.Left, 0), new Size(Math.Max((fix.Right + move.Width) - area.Right, 0), 0), MoveDirection.Right),
                (new Size(fix.Left - move.Right, 0), new Size(Math.Min((fix.Left  - move.Width) - area.Left,  0), 0), MoveDirection.Left),
                (new Size(0, fix.Bottom - move.Top), new Size(0, Math.Max((fix.Bottom + move.Height) - area.Bottom ,0)), MoveDirection.Bottom),
                (new Size(0, fix.Top - move.Bottom), new Size(0, Math.Min((fix.Top    - move.Height) - area.Top,    0)), MoveDirection.Top),
            };

            var (moving, overlap, _) = spaces
                .OrderBy(sp => Math.Abs(sp.overlap.Width + sp.overlap.Height))
                .ThenBy(sp => Math.Abs(sp.moving.Width + sp.moving.Height))
                .First();

            // 重なりが大きすぎるなら、そのままの位置
            //var threshold = move.Size * 0.5f;
            if (overlap.Width > move.Width * 0.5f || overlap.Height > move.Height * 0.5f)
                return move;

            var res = move.Location + moving - overlap;
            return new Rectangle(res, move.Size);
        }

        /// <summary>移動する方向</summary>
        private enum MoveDirection
        {
            Left,
            Top,
            Right,
            Bottom
        }
    }
}
