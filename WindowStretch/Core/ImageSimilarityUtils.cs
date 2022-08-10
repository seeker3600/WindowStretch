using System.Drawing;
using System.Drawing.Imaging;
using System.Numerics;

namespace WindowStretch.Core
{
    /// <summary>
    /// DHashによる画像の類似度比較。
    /// </summary>
    /// <remarks>
    /// https://tech.unifa-e.com/entry/2017/11/27/111546
    /// </remarks>
    public static class ImageSimilarityUtils
    {
        /// <summary>
        /// 画像の一部の領域のDHashを取得する。
        /// </summary>
        /// <param name="bitmap">DHashを取得する画像</param>
        /// <param name="rectf"><paramref name="bitmap"/>の一部の領域。縦横を0.0～1.0とした範囲。</param>
        /// <returns>指定した領域から計算したDHashの値を返す。</returns>
        public static ulong GetDHash(Bitmap bitmap, RectangleF rectf)
        {
            // rectfについて、bitmap内での実際の位置を計算する
            Rectangle srcRect = Rectangle.FromLTRB(
                (int)(rectf.Left * bitmap.Width),
                (int)(rectf.Top * bitmap.Height),
                (int)(rectf.Right * bitmap.Width),
                (int)(rectf.Bottom * bitmap.Height)
                );

            using (var temp = new Bitmap(9, 8))
            {
                // グレースケールに変換して、tempに縮小貼り付けする
                using (Graphics g = Graphics.FromImage(temp))
                using (var ia = new ImageAttributes())
                {
                    ia.SetColorMatrix(_cm);
                    g.DrawImage(bitmap, _destPoints, srcRect, GraphicsUnit.Pixel, ia);
                }

                // 各ピクセルとその隣の明るさを比較し、隣の方が明るければ対応するビットを立てる
                using (var wrap = BitmapSpan.Wrap(temp))
                {
                    var span = wrap.GetReadOnlySpan();
                    int psize = Image.GetPixelFormatSize(wrap.Data.PixelFormat) / 8; // bit->Byte

                    ulong res = 0LU;
                    for (int i = 0; i < 64; i++)
                    {
                        int a = (i / 8) * wrap.Data.Stride + (i % 8) * psize + 1; // +1はαチャネルを飛ばすため
                        int b = a + psize;

                        if (span[a] < span[b])
                        {
                            res |= 1LU << i;
                        }
                    }

                    return res;
                }
            }
        }

        /// <summary>NTSC 係数</summary>
        /// <remarks>https://dobon.net/vb/dotnet/graphics/grayscale.html</remarks>
        private static readonly ColorMatrix _cm = new ColorMatrix(new float[][]{
            new float[]{0.299f, 0.299f, 0.299f, 0 ,0},
            new float[]{0.587f, 0.587f, 0.587f, 0, 0},
            new float[]{0.114f, 0.114f, 0.114f, 0, 0},
            new float[]{0, 0, 0, 1, 0},
            new float[]{0, 0, 0, 0, 1}
            });

        /// <summary>画像を縮小して貼り付ける領域。左上、右上、左下</summary>
        private static readonly Point[] _destPoints = { new Point(0, 0), new Point(9, 0), new Point(0, 8) };

        /// <summary>
        /// DHashを元に「似ているか」判定する。
        /// </summary>
        /// <param name="left">画像1のDHash</param>
        /// <param name="right">画像2のDHash</param>
        /// <returns>「似ている」場合はtrue、そうでなければfalse。</returns>
        public static bool NearlyEquals(ulong left, ulong right)
        {
            var count = BitOperations.PopCount(left ^ right);
            return count <= 10;
        }
    }
}
