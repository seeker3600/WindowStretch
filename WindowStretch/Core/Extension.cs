using Reactive.Bindings;
using System;
using System.Reactive.Linq;

namespace WindowStretch.Core
{
    public static class Extension
    {
        /// <summary>
        /// データを最低 <paramref name="interval"/> の期間を置いて後続に流す。
        /// </summary>
        /// <returns>Hotなので注意。</returns>
        public static IObservable<T> ThrottleNoIgnore<T>(this IObservable<T> src, TimeSpan interval)
        {
            return src.Publish(pub =>
            {
                var count = 0;
                var obj = new object();

                var timer = new ReactiveTimer(interval);

                pub.Subscribe(data =>
                    {
                        lock (obj)
                        {
                            count++;
                            if (!timer.IsEnabled) timer.Start();
                        }
                    });

                return timer
                    .Where(_ =>
                    {
                        lock (obj)
                        {
                            if (count == 0)
                            {
                                timer.Reset();
                                return false;
                            }

                            count--;
                        }

                        return true;
                    })
                    .Zip(pub, (_, data) => data)
                    .Publish()
                    .RefCount();
            });
        }
    }
}
