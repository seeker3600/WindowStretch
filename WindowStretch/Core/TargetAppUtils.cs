using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.Windows.Sdk;

namespace WindowStretch.Core
{
    public static class TargetAppUtils
    {

        /// <summary>処理の対象になるアプリのプロセス名。</summary>
#if DEBUG
        public const string ProcessName = "Haribote";
#else
        public const string ProcessName = "umamusume";
#endif

        /// <summary>
        /// 監視対象のアプリのウィンドウハンドルを取得する。
        /// </summary>
        /// <returns>ウィンドウハンドル。取得に失敗した場合は <c>null</c></returns>
        private static HWND? FindHwnd()
        {
            var proc = Process.GetProcessesByName(ProcessName).FirstOrDefault();
            var hwnd = proc?.MainWindowHandle ?? IntPtr.Zero; // WaitForInputIdleは「権限がない」エラーになった

            return hwnd != IntPtr.Zero ? (HWND)hwnd : (HWND?)null;
        }

        private static HWND? Cache = null;

        private const long WS_MINIMIZE = 0x20000000L;

        /// <summary>
        /// 監視対象のアプリが最小化されていないことを確認する。
        /// </summary>
        /// <returns>最小化されていなければ<c>true</c>。</returns>
        private static bool IsNormal(HWND hwnd)
        {
            var info = new WINDOWINFO();
            if (!PInvoke.GetWindowInfo(hwnd, ref info))
                throw new InvalidOperationException(nameof(PInvoke.SetWindowPos));

            return (info.dwStyle & WS_MINIMIZE) == 0;
        }

        /// <summary>
        /// 監視対象のアプリのウィンドウハンドルを取得する。
        /// </summary>
        /// <returns>
        /// ウィンドウハンドル。ただし、ウィンドウが最小化されている場合、nullを返す。
        /// </returns>
        public static HWND? GetHwnd()
        {
            try
            {
                if (Cache == null) Cache = FindHwnd();

                return Cache is HWND hwnd && IsNormal(hwnd) ? hwnd : (HWND?)null;
            }
            catch (Exception)
            {
                Cache = null;
                return null;
            }
        }
    }
}
