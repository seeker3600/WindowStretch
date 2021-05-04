using System;
using System.Diagnostics;
using System.Reactive.Linq;

namespace WindowStretch.Core
{
    public static class ShellUtils
    {
        /// <summary>
        /// 指定されたプログラムを開始する。または、ファイル・URLを開く。
        /// </summary>
        public static void StartProcess(string filename)
        {
            var info = new ProcessStartInfo(filename)
            {
                UseShellExecute = true
            };

            Process.Start(info);
        }

        public static IObservable<bool> StartProcess(this IObservable<string> src)
        {
            return src
                .Do(StartProcess)
                .Select(_ => true)
                .Catch(Observable.Return(false))
                .Repeat();
        }
    }
}
