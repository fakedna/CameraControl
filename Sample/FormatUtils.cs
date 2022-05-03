using System;
using System.Windows.Media;

namespace CameraControl
{
    public static class FormatUtils
    {
        public static PixelFormat GetPixelFormat(string videoSubType)
        {
            return videoSubType.ToUpper() switch
            {
                "RGB24" => PixelFormats.Bgr24,
                "RGB32" => PixelFormats.Bgr32,
                "RGB555" => PixelFormats.Bgr555,
                "RGB565" => PixelFormats.Bgr565,
                "ARGB32" => PixelFormats.Bgra32,
                _ => throw new ArgumentException($"Unsupported video format: {videoSubType}")
            };
        }

        public static bool IsSupportedFormat(string videoSubType)
        {
            return videoSubType.ToUpper() switch
            {
                "RGB24" => true,
                "RGB32" => true,
                "RGB555" => true,
                "RGB565" => true,
                "ARGB32" => true,
                _ => false
            };
        }
    }
}