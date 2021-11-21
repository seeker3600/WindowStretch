using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace WindowStretch.Core
{
    public class BitmapZipper : IDisposable
    {
        public event Action<Bitmap> CompleteMerege = _ => { };

        private Bitmap Canvas = null;

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

            var hash1 = GetHashCodes(Canvas);
            var hash2 = GetHashCodes(bitmap);

            var bodyStart = BodyStart(hash1, hash2, 100);
            if (bodyStart == -1) return;

            var reverseBodyStart = BodyStart(hash1.AsEnumerable().Reverse(), hash2.AsEnumerable().Reverse(), 100);
            if (reverseBodyStart == -1) return;

            var hash1Body = hash1.GetRange(bodyStart, hash1.Count - reverseBodyStart - bodyStart);
            var hash2Body = hash2.GetRange(bodyStart, hash2.Count - reverseBodyStart - bodyStart);
            var duplication = bodyStart + LikestDuplicate(hash1Body, hash2Body, 30);

            //if (duplication + hash2Body.Count <= bodyStart + hash1Body.Count)
            //    return;

            var result = new Bitmap(Canvas.Width, duplication + bitmap.Height - bodyStart);

            try
            {
                using (var g = Graphics.FromImage(result))
                {
                    g.DrawImage(Canvas, Point.Empty);

                    g.DrawImage(
                        bitmap,
                        new Rectangle(0, duplication, bitmap.Width, bitmap.Height - bodyStart),
                        Rectangle.FromLTRB(0, bodyStart, bitmap.Width, bitmap.Height),
                        GraphicsUnit.Pixel);
                }

                Bitmap old = Canvas;
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

        private static int BodyStart(IEnumerable<int> left, IEnumerable<int> right, int threthold)
        {
            var bodyStart = left
                .Zip(right, (First, Second) => (First, Second))
                .Select((t, y) => (t.First, t.Second, y))
                .FirstOrDefault(t => Math.Abs(t.First - t.Second) > threthold);

            return bodyStart != default ? bodyStart.y : -1;
        }

        private static List<int> GetHashCodes(Bitmap bmp)
        {
            var res = new List<int>();

            var data = bmp.LockBits(
                new Rectangle(Point.Empty, bmp.Size),
                ImageLockMode.ReadOnly,
                bmp.PixelFormat);

            try
            {
                var pixels = new byte[data.Stride * data.Height];
                Marshal.Copy(data.Scan0, pixels, 0, data.Stride * data.Height);

                for (int y = 0; y < data.Height; y++)
                {
                    int hash = 0;
                    var stride = pixels.AsSpan(data.Stride * y, data.Stride - 4);

                    foreach (var px in stride)
                    {
                        hash += px;
                    }

                    res.Add(hash);
                }
            }
            finally
            {
                bmp.UnlockBits(data);
            }

            return res;
        }

        private static int LikestDuplicate(List<int> left, List<int> right, int minimum)
        {
            var scores = Enumerable.Range(0, left.Count - minimum)
                .Select(d => (
                    score: left
                        .Skip(d)
                        .Take(minimum)
                        .Zip(right.Take(minimum), (first, second) => (long)Math.Abs(first - second))
                        .Sum(),
                    d))
                .ToList();

            var avg = scores.Select(t => t.score).Average();
            var dupli = scores.MinBy(t => t.score);

            if (dupli.score >= avg * 0.2)
                return left.Count;

            return dupli.d;
        }

        public string Save(string foldername)
        {
            if (Canvas == null)
                throw new InvalidOperationException();

            var filename = Path.Combine(foldername, $"{DateTime.Now:yyyy-MM-dd HH-mm-ss}.png");

            Canvas.Save(filename, ImageFormat.Png);

            return filename;
        }

        public void Dispose()
        {
            Canvas?.Dispose();
        }
    }

    public static class EnumPlus
    {
        ///<summary>条件付きMin。</summary>
        public static T MinBy<T, U>(this IEnumerable<T> xs, Func<T, U> key) where U : IComparable<U>
        {
            return xs.Aggregate((a, b) => key(a).CompareTo(key(b)) < 0 ? a : b);
        }
    }
}
