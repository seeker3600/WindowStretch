using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowStretch.Core
{
    public static class WindowUtils
    {

#if DEBUG
        public const string ProcessName = "Haribote";
#else
        public const string ProcessName = "umamusume";
#endif

        /// <summary>
        /// 監視対象のアプリのウィンドウハンドルを取得する。
        /// </summary>
        /// <returns>ウィンドウハンドル。取得に失敗した場合は <see cref="IntPtr.Zero"/></returns>
        public static IntPtr GetHwnd()
        {
            var proc = Process.GetProcessesByName(ProcessName).FirstOrDefault();
            var hwnd = proc?.MainWindowHandle ?? IntPtr.Zero; // WaitForInputIdleは「権限がない」エラーになった

            return hwnd;
        }

        public static void ExecuteUsingShell(string filename)
        {
            var info = new ProcessStartInfo(filename)
            {
                UseShellExecute = true
            };

            Process.Start(info);
        }
    }
}
