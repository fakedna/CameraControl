using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace CameraControl
{
    public enum ColorChannels
    {
        R = 0,
        G = 1,
        B = 2,
        A = 3
    }

    public sealed class ImageController : IDisposable
    {
        private DispatcherOperation _operation;
        private WriteableBitmap _image;
        private Int32Rect _rect;
        private byte[] _clearBuffer;
        private int _stride;
        private byte[] _channels;

        public void Update(int width, int height, int stride, PixelFormat format)
        {
            _channels = new[] {byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue};
            _stride = stride;
            _clearBuffer = new byte[stride * height];
            _image = new WriteableBitmap(width, height, 96, 96, format, null);
            _rect = new Int32Rect(0, 0, width, height);
            Clear();
        }

        public void Draw(byte[] data)
        {
            if (_image == null) return;
            _operation = _image.Dispatcher.InvokeAsync(() =>
            {
                MultiplyChannels(ref data, _image.Format);
                _image.WritePixels(_rect, data, _stride, 0);
            }, DispatcherPriority.Render);
        }

        public void Clear()
        {
            if (_image == null) return;
            _operation?.Abort();
            _operation = _image.Dispatcher.InvokeAsync(() => _image.WritePixels(_rect, _clearBuffer, _stride, 0));
        }

        private void MultiplyChannels(ref byte[] data, PixelFormat format)
        {
            if (_channels.All(x => x == byte.MaxValue)) return;
            var r = (float) _channels[0] / byte.MaxValue;
            var g = (float) _channels[1] / byte.MaxValue;
            var b = (float) _channels[2] / byte.MaxValue;
            if (format == PixelFormats.Bgr24)
            {
                for (var i = 0; i < data.Length; i += 3)
                {
                    data[i] = (byte) (b * data[i]);
                    data[i + 1] = (byte) (g * data[i + 1]);
                    data[i + 2] = (byte) (r * data[i + 2]);
                }
            }
            else
            {
                Console.WriteLine($"Unsupported image multiply format: {format}");
            }
        }

        public byte GetColorChannel(ColorChannels channel) => _channels[(byte) channel];

        public void SetColorChannel(ColorChannels channel, byte value) => _channels[(byte) channel] = value;

        public ImageSource Image => _image;

        public void Dispose()
        {
            _operation?.Abort();
            _operation = null;
            _image = null;
            _clearBuffer = null;
            _channels = null;
            GC.Collect();
        }
    }
}