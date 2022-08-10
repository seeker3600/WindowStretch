using Microsoft.Windows.Sdk;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Drawing;
using System.Reactive.Subjects;
using System.Windows.Forms;
using WindowStretch.Core;
using WindowStretch.Properties;
using WindowStretch.Scale;

namespace WindowStretch.Model
{
    public class ScaleOverlayModel : IDisposable
    {
        public ReactiveProperty<bool> ScaleEnabled { get; } =
            Settings.Default.ToReactivePropertyAsSynchronized(conf => conf.ScaleEnabled);

        public ReactiveProperty<bool> ScaleAutoVisible { get; } =
            Settings.Default.ToReactivePropertyAsSynchronized(conf => conf.ScaleAutoVisible);

        public IObservable<string> StatusMsg => Status;

        private readonly Subject<string> Status = new Subject<string>();

        private readonly ScaleForm Scale = new ScaleForm()
        {
            Visible = false
        };

        private Rectangle? BeforeRect = null;

        public void Tick()
        {
            if (!ScaleEnabled.Value)
            {
                Scale.Visible = false;
                return;
            }

            if (Control.MouseButtons.HasFlag(MouseButtons.Left))
            {
                Status.OnNext($"監視を一時停止しています。");
                return;
            }

            var procName = TargetAppUtils.ProcessName;
            var hwndN = TargetAppUtils.GetHwnd();

            if (hwndN is HWND hwnd)
            {
                try
                {
                    if (ScaleAutoVisible.Value && !ExistsStaminaGauge())
                    {
                        Scale.Visible = false;
                        Status.OnNext($"体力ゲージが見つかりませんでした。");
                        return;
                    }

                    Scale.Visible = true;
                    var targetRect = WindowUtils.GetClientRectOnScreen(hwnd);

                    if (BeforeRect != targetRect)
                    {
                        var r = ScaleRectOnScreen(targetRect);
                        Scale.SetBounds(r.X, r.Y, r.Width, r.Height);

                        BeforeRect = targetRect;
                        Status.OnNext($"目盛りを移動しました。");
                    }
                    else
                        Status.OnNext($"アプリ {procName} を監視しています。");
                }
                catch (Exception)
                {
                    Scale.Visible = false;
                    Status.OnNext($"アプリ {procName} の監視が失敗しました。管理者権限が必要かもしれません。");
                }
            }
            else
            {
                Scale.Visible = false;
                Status.OnNext($"アプリ {procName} が見つかりません。");
            }
        }

        private static bool ExistsStaminaGauge()
        {
            var templateHash = Settings.Default.ScaleVisibleHash;
            var templateRect = Settings.Default.ScaleVisibleRect;

            using (var bitmap = ScreenshotUtils.Take())
            {
                var hash = ImageSimilarityUtils.GetDHash(bitmap, templateRect);
                return ImageSimilarityUtils.NearlyEquals(hash, templateHash);
            }
        }

        private static Rectangle ScaleRectOnScreen(Rectangle target)
        {
            var p = Settings.Default.ScaleRect;

            return new Rectangle(
                target.X + (int)(p.X * target.Width),
                target.Y + (int)(p.Y * target.Height),
                (int)(p.Width * target.Width),
                (int)(p.Height * target.Height)
            );
        }

        public void Dispose()
        {
        }
    }
}
