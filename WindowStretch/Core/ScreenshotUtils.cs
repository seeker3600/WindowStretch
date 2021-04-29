﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.Windows.Sdk;


namespace WindowStretch.Core
{
    public static class ScreenshotUtils
    {
        /// <summary>
        /// 指定されたフォルダへ、既定のウィンドウのスクリーンショットを保存する。
        /// </summary>
        /// <param name="foldername">スクリーンショットを保存するフォルダ</param>
        /// <returns>保存したスクリーンショット。フルパス</returns>
        public static string Take(string foldername)
        {
            var hwndIp = WindowUtils.GetHwnd() ?? throw new InvalidOperationException();

            var bmp = CaptureScreenshot(hwndIp);
            var filename = Path.Combine(foldername, $"{DateTime.Now:yyyy-MM-dd HH-mm-ss}.png");

            bmp.Save(filename, ImageFormat.Png);

            return filename;
        }

        /// <summary>
        /// 指定されたウィンドウについて、スクリーン座標でのクライアント領域の四角形を取得する。
        /// </summary>
        private static Rectangle GetClientRect(HWND hwnd)
        {
            // クライアント領域の寸法を取得
            if (!PInvoke.GetClientRect(hwnd, out var rect))
                throw new InvalidOperationException(nameof(PInvoke.GetClientRect));

            // クライアント領域の位置を取得
            var point = new POINT() { x = 0, y = 0 };
            if (!PInvoke.ClientToScreen(hwnd, ref point))
                throw new InvalidOperationException(nameof(PInvoke.ClientToScreen));

            var res = Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
            res.Offset(point.x, point.y);

            return res;
        }

        /// <summary>
        /// 指定されたウィンドウのスクリーンショットを取得する。
        /// </summary>
        private static Bitmap CaptureScreenshot(IntPtr hwndIp)
        {
            //Bitmapの作成
            var bounds = GetClientRect(new HWND(hwndIp));
            var bmp = new Bitmap(bounds.Width, bounds.Height);

            //Graphicsの作成
            using (var g = Graphics.FromImage(bmp))
            {
                //画面全体をコピーする
                g.CopyFromScreen(bounds.Location, new Point(0, 0), bmp.Size);

                return bmp;
            }
        }

        /// <summary>
        /// 指定されたファイルを、デフォルトアプリで開く。
        /// </summary>
        public static void OpenFileUseShell(string filename)
        {
            var info = new ProcessStartInfo(filename)
            {
                UseShellExecute = true
            };

            Process.Start(info);
        }
    }
}
