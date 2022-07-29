using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace WindowStretch.Core
{
    class BitmapSpan : IDisposable
    {
        private bool disposedValue;

        private readonly Bitmap Bmp;

        public readonly BitmapData Data;

        public BitmapSpan(Bitmap bitmap)
        {
            Bmp = bitmap;

            Data = bitmap.LockBits(
                new Rectangle(Point.Empty, bitmap.Size),
                ImageLockMode.ReadOnly,
                bitmap.PixelFormat);
        }

        public ReadOnlySpan<byte> GetReadOnlySpan()
        {
            var length = Data.Stride * Data.Height;

            // TODO .net 5.0以降、unsafeコードの代わりに以下を使用できる。が、結局安全ではないので注意。
            //return MemoryMarshal.CreateSpan(ref Unsafe.AddByteOffset(ref Unsafe.NullRef<byte>(), Data.Scan0), length);

            unsafe
            {
                return new Span<byte>(Data.Scan0.ToPointer(), length);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // マネージド状態を破棄します (マネージド オブジェクト)
                    Bmp.UnlockBits(Data);
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public static BitmapSpan Wrap(Bitmap bitmap)
        {
            return new BitmapSpan(bitmap);
        }

    }
}
