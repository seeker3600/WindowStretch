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
        public static HWND? GetHwnd()
        {
            var proc = Process.GetProcessesByName(ProcessName).FirstOrDefault();
            var hwnd = proc?.MainWindowHandle ?? IntPtr.Zero; // WaitForInputIdleは「権限がない」エラーになった

            return hwnd != IntPtr.Zero ? (HWND)hwnd : (HWND?)null;
        }
    }
}
