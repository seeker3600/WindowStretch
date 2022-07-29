using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace WindowStretch.Core
{
    public class BitmapZipper : IDisposable
    {
        public event Action<Bitmap> CompleteMerege = _ => { };

        private Bitmap Canvas = null;

        private static readonly float ThretholdA = float.TryParse(Environment.GetEnvironmentVariable("THRETHOLD_A"), out var v) ? v : 1e-4f;

        private static readonly float ThretholdB = float.TryParse(Environment.GetEnvironmentVariable("THRETHOLD_B"), out var v) ? v : 0.05f;

        public void Merge(Bitmap bitmap)
        {
            if (Canvas == null)
                MergeFirst(bitmap);
            else
                MergeSecond(bitmap);
        }

        private void MergeFirst(Bitmap bitmap)
        {
            Debug.Assert(Canvas == null);

            Canvas = new Bitmap(bitmap);

            CompleteMerege(Canvas);
        }

        private void MergeSecond(Bitmap bitmap)
        {
            Debug.Assert(Canvas != null);

            (int start, int end) body;
            int duplication;

            using (var canvasData = BitmapSpan.Wrap(Canvas))
            using (var argData = BitmapSpan.Wrap(bitmap))
            {
                // 上下から比較してボディを検出
                body = BodyRange(canvasData, argData, ThretholdA);

                if (body.start == -1 || body.end == -1)
                    return;

                // ボディ同士の被りを検出
                duplication = LikestDuplicate(canvasData, argData, ThretholdB, body, 0.3f);
                // duplication = body.end;
            }

            // canvasのボディ直下、フッター直上にbitmapのボディ以降を挿入
            var result = new Bitmap(Canvas.Width, duplication + bitmap.Height - body.start);

            try
            {
                using (var g = Graphics.FromImage(result))
                {
                    g.DrawImage(Canvas, Point.Empty);

                    g.DrawImage(
                        bitmap,
                        new Rectangle(0, duplication, bitmap.Width, bitmap.Height - body.start),
                        Rectangle.FromLTRB(0, body.start, bitmap.Width, bitmap.Height),
                        GraphicsUnit.Pixel);
                }

                var old = Canvas;
                Canvas = result;
                old.Dispose();
            }
            catch (Exception)
            {
                result.Dispose();
                throw;
            }

            CompleteMerege(Canvas);
        }

        /// <summary>
        /// <paramref name="leftBmp"/>と<paramref name="rightBmp"/>のボディ部（共通しない範囲）を検出する。
        /// </summary>
        /// <param name="leftBmp">画像1。こちらの方が<paramref name="rightBmp"/>以上のサイズであること。</param>
        /// <param name="rightBmp">画像2。</param>
        /// <param name="threthold">同じ内容と判断する閾値。<c>0.0～1.0</c>の範囲を指定する。行単位において、白-白が<c>0.0</c>、黒-白が<c>1.0</c>を表す。</param>
        /// <returns><paramref name="leftBmp"/>を基準としたボディ部の高さ方向のインデックス。endはボディ部を含まない。</returns>
        private static (int start, int end) BodyRange(BitmapSpan leftBmp, BitmapSpan rightBmp, float threthold)
        {
            Debug.Assert(0.0f < threthold && threthold < 1.0f);

            var leftData = leftBmp.Data;
            var rightData = rightBmp.Data;

            var thretholdRaw = (int)((leftData.Stride - 4) * 255.0f * threthold);  // 閾値の生の値。GetLineDistanceを参照。

            Debug.Assert(thretholdRaw > 0);

            var start = -1;

            // 上から1行ずつ比較する
            for (int y = 0; y < leftData.Height && y < rightData.Height; y++)
            {
                // 閾値チェック
                if (GetLineDistance(leftBmp, rightBmp, y, y) >= thretholdRaw)
                {
                    start = y;
                    break;
                }
            }

            var end = -1;

            // 下から1行ずつ比較する
            for (int yl = leftData.Height - 1, yr = rightData.Height - 1; yl >= 0 && yr >= 0; yl--, yr--)
            {
                // 閾値チェック
                if (GetLineDistance(leftBmp, rightBmp, yl, yr) >= thretholdRaw)
                {
                    end = yl + 1;
                    break;
                }
            }

            return (start, end);
        }

        /// <summary>
        /// 行の距離を計算する。
        /// </summary>
        /// <param name="leftBmp">画像1。</param>
        /// <param name="rightBmp">画像2。</param>
        /// <param name="yl">画像1の行位置。</param>
        /// <param name="yr">画像2の行位置。</param>
        /// <returns>
        /// 指定した行同士の距離。最小値は0、最大値は<c>255 * (leftBmp.Data.Stride - 4)</c>。
        /// 実体としては、各色の値の差を足し合わせたもの。
        /// </returns>
        private static int GetLineDistance(BitmapSpan leftBmp, BitmapSpan rightBmp, int yl, int yr)
        {
            var left = leftBmp.GetReadOnlySpan();
            var right = rightBmp.GetReadOnlySpan();

            var leftStride = leftBmp.Data.Stride;
            var rightStride = rightBmp.Data.Stride;

            // 後ろはパディングが入っていることがあるので、余裕を見て4バイト無視する
            var strideLeft = left.Slice(leftStride * yl, leftStride - 4);
            var strideRight = right.Slice(rightStride * yr, rightStride - 4);

            int diff = 0;
            for (int i = 0; i < strideLeft.Length; i++)
            {
                var a = strideLeft[i];
                var b = strideRight[i];
                diff += Math.Max(a, b) - Math.Min(a, b);
            }

            return diff;
        }

        /// <summary>
        /// ボディ部同士を比較し、重なりを検出する。
        /// </summary>
        /// <param name="leftBmp">画像1。こちらの方が<paramref name="rightBmp"/>以上のサイズであること。</param>
        /// <param name="rightBmp">画像2。</param>
        /// <param name="threthold">同じ内容と判断する閾値。<c>0.0～1.0</c>の範囲を指定する。重なり検出面単位において、白-白が<c>0.0</c>、黒-白が<c>1.0</c>を表す。</param>
        /// <param name="body"><paramref name="leftBmp"/>を基準としたボディ部の高さ方向のインデックス。endはボディ部を含まない。</param>
        /// <param name="d">重なり検出を試す面積の割合。<paramref name="rightBmp"/>を基準とする。<c>0.0～1.0</c>の範囲を指定する。この割合の面積が一致すれば一致したとみなす。</param>
        /// <returns><paramref name="leftBmp"/>を基準としたY方向の位置。ここから<paramref name="rightBmp"/>のボディ部を開始するといい感じになる。</returns>
        private static int LikestDuplicate(BitmapSpan leftBmp, BitmapSpan rightBmp, float threthold, (int start, int end) body, float d)
        {
            Debug.Assert(0.0f < threthold && threthold < 1.0f);
            Debug.Assert(0.0f < d && d < 1.0f);

            var leftHeight = leftBmp.Data.Height;
            var rightHeight = rightBmp.Data.Height;

            var footerSize = leftHeight - body.end;
            var rightBodySize = rightHeight - footerSize - body.start;

            var dup = (int)(rightBodySize * d);                                             // 重なり検出を試す高さ
            var thretholdRaw = (int)((leftBmp.Data.Stride - 4) * dup * 255.0f * threthold); // 閾値の生の値。GetLineDistanceを参照。
            var compareStart = leftHeight - rightHeight + body.start;                       // leftのボディ下部から、rightのボディ高さ分だけの範囲を比較する

            Debug.Assert(thretholdRaw > 0);
            Debug.Assert(dup >= 10);

            var list = new List<(int y, int diff)>();
            for (int yl = compareStart; yl < body.end - dup; yl++)
            {
                int diff = 0;

                // 重なり検出範囲の行をすべて比較
                for (int y = 0; y < dup; y++)
                {
                    diff += GetLineDistance(leftBmp, rightBmp, yl + y, body.start + y);

                    // 違い過ぎる場合はループを切り上げる
                    if (diff >= thretholdRaw)
                        break;
                }

                // 区切りの候補となる位置をlistに追加
                if (diff < thretholdRaw)
                    list.Add((yl, diff));
            }

            // ボディ部に重なりがない場合、leftのボディの下にrightのボディを追加する
            if (list.Count == 0)
                return body.end;

            return list.MinBy(t => t.diff).y;
        }

        /// <summary>
        /// 結合した画像を保存する。
        /// </summary>
        /// <param name="filename">保存するファイル名。</param>
        /// <returns><paramref name="filename"/>をそのまま返す。</returns>
        public string SaveAs(string filename)
        {
            if (Canvas == null)
                throw new InvalidOperationException();

            Canvas.Save(filename, ImageFormat.Png);

            return filename;
        }

        /// <summary>
        /// 結合した画像を保存する。ファイル名は勝手に決める。
        /// </summary>
        /// <param name="foldername">保存するフォルダ。</param>
        /// <returns>保存したファイル名のフルパス。</returns>
        public string SaveDefaultName(string foldername)
        {
            var filename = Path.Combine(foldername, $"{DateTime.Now:yyyy-MM-dd HH-mm-ss}.png");
            return SaveAs(filename);
        }

        public void Dispose()
        {
            Canvas?.Dispose();
        }
    }
}
