using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Numerics;

namespace WindowStretch.Core
{
    class BodyResult
    {
        /// <summary>endはボディ部を含まない。</summary>
        public readonly (int Start, int End) Left;

        /// <summary>endはボディ部を含まない。</summary>
        public readonly (int Start, int End) Right;

        public BodyResult((int Start, int End) left, (int Start, int End) right)
        {
            Left = left;
            Right = right;
        }
    }

    public class BitmapZipper : IDisposable
    {
#if DEBUG
        /// <summary>閾値A。フッタとヘッダの検出を行う。</summary>
        private static readonly float ThretholdA = float.TryParse(Environment.GetEnvironmentVariable("THRETHOLD_A"), out var v) ? v : 0.038f;

        /// <summary>閾値B。ボディ部の重なりの検出を行う。</summary>
        private static readonly float ThretholdB = float.TryParse(Environment.GetEnvironmentVariable("THRETHOLD_B"), out var v) ? v : 0.040214f;

        /// <summary>ボディ部の重なり検出面。この範囲が閾値以下なら一致したとみなす。</summary>
        private static readonly float DuplicationRange = float.TryParse(Environment.GetEnvironmentVariable("DRANGE"), out var v) ? v : 0.4f;

        /// <summary>デバッグ出力の内容。</summary>
        private static readonly string SaveInterResult = Environment.GetEnvironmentVariable("INTER") ?? "";

        private int No = 0;
#else
        /// <summary>閾値A。フッタとヘッダの検出を行う。</summary>
        private static readonly float ThretholdA = 0.038f;

        /// <summary>閾値B。ボディ部の重なりの検出を行う。</summary>
        private static readonly float ThretholdB = 0.040214f;

        /// <summary>ボディ部の重なり検出面。この範囲が閾値以下なら一致したとみなす。</summary>
        private static readonly float DuplicationRange = 0.4f;
#endif

        /// <summary>
        /// 比較する際に両端を無視するバイト数。
        /// 後ろはパディングが入っていることがあるので、余裕を見て4バイト無視する
        /// </summary>
        private const int TrimBytes = 25 * 3; // + 4

        /// <summary>マージが正常に完了したときのイベント。引数のビットマップは変更しないこと。</summary>
        public event Action<Bitmap> CompleteMerege = _ => { };

        /// <summary>マージ結果。</summary>
        private Bitmap Canvas = null;

        /// <summary><see cref="Canvas"/> と前回の<c>bitmap</c>のボディ部。</summary>
        private BodyResult LastResult = null;

        /// <summary>処理の排他を行うオブジェクト。</summary>
        private readonly object Locker = new object();

        public BitmapZipper()
        {
#if DEBUG
            Console.WriteLine($"THRETHOLD_A = {Environment.GetEnvironmentVariable("THRETHOLD_A")} -> {ThretholdA}");
            Console.WriteLine($"THRETHOLD_B = {Environment.GetEnvironmentVariable("THRETHOLD_B")} -> {ThretholdB}");
            Console.WriteLine($"DRANGE = {Environment.GetEnvironmentVariable("DRANGE")} -> {DuplicationRange}");
#endif
        }

        public void Merge(Bitmap bitmap)
        {
            lock (Locker)
            {
                if (Canvas == null)
                    MergeFirst(bitmap);
                else
                    MergeSecond(bitmap);

#if DEBUG
                if (SaveInterResult == "canvas")
                    Canvas.Save($"{No:00000}.png");
                else if (SaveInterResult == "bitmap")
                    bitmap.Save($"{No:00000}.png");
                No++;
#endif
            }
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

            BodyResult body2;

            using (var canvasData = BitmapSpan.Wrap(Canvas))
            using (var argData = BitmapSpan.Wrap(bitmap))
            {
                // 上下から比較してボディを検出
                BodyResult body = BodyRange(canvasData, argData, ThretholdA, LastResult);
                if (body == null) return;

                // ボディ同士の被りを検出
                body2 = LikestDuplicate(canvasData, argData, ThretholdB, body, DuplicationRange);
                if (body2 == null) return;
            }

            // canvasのボディ直下、フッター直上にbitmapのボディとフッタを貼り付け
            var drawHeight = bitmap.Height - body2.Right.Start;
            var result = new Bitmap(Canvas.Width, body2.Left.Start + drawHeight);

            try
            {
                using (var g = Graphics.FromImage(result))
                {

                    g.DrawImage(Canvas, Point.Empty);

                    g.DrawImage(
                        bitmap,
                        new Rectangle(0, body2.Left.Start, bitmap.Width, drawHeight),
                        Rectangle.FromLTRB(0, body2.Right.Start, bitmap.Width, bitmap.Height),
                        GraphicsUnit.Pixel);

                    var old = Canvas;
                    Canvas = result;
                    old.Dispose();

                    LastResult = body2;
                }
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
        /// <param name="lastBodies">最後に貼り付けたボディ部の位置。ボディ部は小さくなっていくという前提。</param>
        /// <returns>ボディ部の高さ方向のインデックス。</returns>
        private static BodyResult BodyRange(BitmapSpan leftBmp, BitmapSpan rightBmp, float threthold, BodyResult lastBodies)
        {
            Debug.Assert(0.0f < threthold && threthold < 1.0f);

            var leftData = leftBmp.Data;
            var rightData = rightBmp.Data;

            //threthold = thretholdRaw / (2090 * 255) =
            var thretholdRaw = (int)((leftData.Stride - (TrimBytes * 2)) * 255.0f * threthold);     // 閾値の生の値。GetLineDistanceを参照。
            var scans = lastBodies ?? new BodyResult((0, leftData.Height), (0, rightData.Height));  // デフォルトは全体がスキャン対象

            Debug.Assert(thretholdRaw > 0);

            // 上から1行ずつ比較する
            for (int leftS = scans.Left.Start, rightS = scans.Right.Start; leftS < leftData.Height && rightS < rightData.Height; leftS++, rightS++)
            {
                // ヘッダの閾値チェック
                int dist1 = GetLineDistance(leftBmp, rightBmp, leftS, rightS);
                if (dist1 >= thretholdRaw)
                {
                    // 下から1行ずつ比較する
                    for (int leftE = scans.Left.End - 1, rightE = scans.Right.End - 1; leftE > leftS && rightE > rightS; leftE--, rightE--)
                    {
                        // フッタの閾値チェック
                        int dist2 = GetLineDistance(leftBmp, rightBmp, leftE, rightE);
                        if (dist2 >= thretholdRaw)
                        {
                            return new BodyResult((leftS, leftE), (rightS, rightE));
                        }
                    }

                    break;
                }
            }

            return null;
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

            var strideLeft = left.Slice(leftStride * yl + TrimBytes, leftStride - (TrimBytes * 2));
            var strideRight = right.Slice(rightStride * yr + TrimBytes, rightStride - (TrimBytes * 2));

            var vectorLength = Vector<byte>.Count;
            var diffTemp = Vector<ushort>.Zero;

            var ary = new byte[vectorLength];
            var tmp = ary.AsSpan();

            for (int i = 0, c = strideLeft.Length - vectorLength; i < c; i += vectorLength)
            {
                // TODO .net 5以降は直接Spanを渡すことができる
                strideLeft.Slice(i, vectorLength).CopyTo(tmp);
                var leftVec = new Vector<byte>(ary);

                strideRight.Slice(i, vectorLength).CopyTo(tmp);
                var rightVec = new Vector<byte>(ary);

                var diffVec = Vector.Max(leftVec, rightVec) - Vector.Min(leftVec, rightVec);
                Vector.Widen(diffVec, out var short1, out var short2);

                diffTemp += short1 + short2;
            }

            int diff = 0;
            for (int i = 0; i < Vector<ushort>.Count; i++) diff += diffTemp[i];

            for (int i = strideLeft.Length / vectorLength * vectorLength; i < strideLeft.Length; i++)
            {
                var a = strideLeft[i];
                var b = strideRight[i];
                diff += Math.Max(a, b) - Math.Min(a, b);
            }

#if DEBUG
            int diff2 = 0;

            for (int i = 0; i < strideLeft.Length; i++)
            {
                var a = strideLeft[i];
                var b = strideRight[i];
                diff2 += Math.Max(a, b) - Math.Min(a, b);
            }

            Debug.Assert(diff == diff2);
#endif

            return diff;
        }

        /// <summary>
        /// ボディ部同士を比較し、重なりを検出する。
        /// </summary>
        /// <param name="leftBmp">画像1。こちらの方が<paramref name="rightBmp"/>以上のサイズであること。</param>
        /// <param name="rightBmp">画像2。</param>
        /// <param name="threthold">同じ内容と判断する閾値。<c>0.0～1.0</c>の範囲を指定する。重なり検出面単位において、白-白が<c>0.0</c>、黒-白が<c>1.0</c>を表す。</param>
        /// <param name="body">ボディ部の高さ方向のインデックス。</param>
        /// <param name="d">重なり検出を試す面積の割合。<paramref name="rightBmp"/>を基準とする。<c>0.0～1.0</c>の範囲を指定する。この割合の面積が一致すれば一致したとみなす。</param>
        /// <returns>貼り付けるべき位置。Leftの位置にRightの内容以降を貼り付けるといい感じになる。</returns>
        private static BodyResult LikestDuplicate(BitmapSpan leftBmp, BitmapSpan rightBmp, float threthold, BodyResult body, float d)
        {
            Debug.Assert(0.0f < threthold && threthold < 1.0f);
            Debug.Assert(0.0f < d && d < 1.0f);

            var rightBodySize = body.Right.End - body.Right.Start;  // rightのボディの高さ
            var dup = (int)(rightBodySize * d);                     // 重なり検出を試す高さ
            // thretholdRaw/(2090*290*255)=
            var thretholdRaw = (int)((leftBmp.Data.Stride - (TrimBytes * 2)) * dup * 255.0f * threthold);   // 閾値の生の値。GetLineDistanceを参照。

            Debug.Assert(thretholdRaw > 0);
            Debug.Assert(dup >= 10);
            //Console.WriteLine();

            // leftのボディ下部から、rightのボディ高さ分だけの範囲を比較する
            var list = new List<(int y, int diff)>();
            for (int yl = body.Left.End - rightBodySize; yl < body.Left.End - dup; yl++)
            {
                int diff = 0;

                // 重なり検出範囲の行をすべて比較
                for (int y = 0; y < dup; y++)
                {
                    diff += GetLineDistance(leftBmp, rightBmp, yl + y, body.Right.Start + y);

                    // 違い過ぎる場合はループを切り上げる
                    if (diff >= thretholdRaw)
                        break;
                }

                //Console.WriteLine(diff);
                // 区切りの候補となる位置をlistに追加
                if (diff < thretholdRaw)
                    list.Add((yl, diff));
            }

            var thretholdRaw2 = list.Count > 0 ? list.Min(t => t.diff) : thretholdRaw;

            // 上にスクロールしている場合、キャンセルする
            for (int yl = body.Left.End - rightBodySize; yl < body.Left.End - dup; yl++)
            {
                int diff = 0;

                // 重なり検出範囲の行をすべて比較
                for (int y = 0; y < dup; y++)
                {
                    diff += GetLineDistance(leftBmp, rightBmp, yl + y, body.Right.End - dup + y);

                    // 違い過ぎる場合はループを切り上げる
                    if (diff >= thretholdRaw2)
                        break;
                }

                // 上にスクロールしている場合、キャンセルする
                if (diff < thretholdRaw2)
                    return null;
            }

            var res = list.Count > 0
                ? list.MinBy(t => t.diff).y      // 重なりがある場合、一番それらしい位置から継ぎ足しを開始する
                : body.Left.End;                 // ボディ部に重なりがない場合、leftのボディの下にrightのボディを追加する

            return new BodyResult((res, res + rightBodySize), body.Right);
        }

        /// <summary>
        /// 結合した画像を保存する。
        /// </summary>
        /// <param name="filename">保存するファイル名。</param>
        /// <returns><paramref name="filename"/>をそのまま返す。</returns>
        public string SaveAs(string filename)
        {
            lock (Locker)
            {
                if (Canvas == null)
                    throw new InvalidOperationException();

                Canvas.Save(filename, ImageFormat.Png);

                return filename;
            }
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
            lock (Locker)
            {
                Canvas?.Dispose();
                Canvas = null;
            }
        }
    }
}
