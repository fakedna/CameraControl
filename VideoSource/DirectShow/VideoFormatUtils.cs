using System;
using System.Runtime.InteropServices;

namespace VideoSource.DirectShow
{
    internal static class VideoFormatUtils
    {
        public static string GetString(this AM_MEDIA_TYPE mt)
        {
            return $"MT: MajorType={mt.MajorType.GuidString(typeof(MediaType))} " +
                   $"SubType={mt.SubType.GuidString(typeof(MediaSubType))} " +
                   $"FormatType={mt.FormatType.GuidString(typeof(FormatType))}";
        }

        public static VideoFormat GetVideoFormat(this AM_MEDIA_TYPE mt)
        {
            var format = new VideoFormat
            {
                MajorType = mt.MajorType.GuidString(typeof(MediaType)),
                SubType = mt.SubType.GuidString(typeof(MediaSubType)),
                HeaderType = mt.FormatType.GuidString(typeof(FormatType)),
            };

            if (mt.FormatType == FormatType.VideoInfo)
            {
                var info = mt.pbFormat.PtrToStructure<VIDEOINFOHEADER>();
                format.BitmapSize = (info.bmiHeader.biWidth, info.bmiHeader.biHeight);
                format.BitmapStride = info.bmiHeader.biWidth * (info.bmiHeader.biBitCount / 8);
                format.BitRate = info.BitRate;
                format.BitErrorRate = info.BitErrorRate;
                format.AvgTimePerFrame = info.AvgTimePerFrame;
            }
            else if (mt.FormatType == FormatType.VideoInfo2)
            {
                var info = mt.pbFormat.PtrToStructure<VIDEOINFOHEADER2>();
                format.BitmapSize = (info.bmiHeader.biWidth, info.bmiHeader.biHeight);
                format.BitmapStride = info.bmiHeader.biWidth * (info.bmiHeader.biBitCount / 8);
                format.BitRate = info.BitRate;
                format.BitErrorRate = info.BitErrorRate;
                format.AvgTimePerFrame = info.AvgTimePerFrame;
            }

            return format;
        }

        public static VideoCapabilities[] GetVideoCapabilities(this IPin pin)
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            var config = pin as IAMStreamConfig;
            if (config == null) return Array.Empty<VideoCapabilities>();
            config.GetNumberOfCapabilities(out var capCount, out var capSize).ThrowForHR();
            if (capSize != Marshal.SizeOf(typeof(VIDEO_STREAM_CONFIG_CAPS))) return Array.Empty<VideoCapabilities>();

            var result = new VideoCapabilities[capCount];
            var capData = Marshal.AllocHGlobal(capSize);

            for (var i = 0; i < capCount; i++)
            {
                config.GetStreamCaps(i, out var mt, capData).ThrowForHR();
                var caps = capData.PtrToStructure<VIDEO_STREAM_CONFIG_CAPS>();

                result[i] = new VideoCapabilities
                {
                    MajorType = mt.MajorType.GuidString(typeof(MediaType)),
                    SubType = mt.SubType.GuidString(typeof(MediaSubType)),
                    HeaderType = mt.FormatType.GuidString(typeof(FormatType)),
                    VideoStandard = caps.VideoStandard,
                    InputSize = caps.InputSize.ToTuple,
                    MinOutputSize = caps.MinOutputSize.ToTuple,
                    MaxOutputSize = caps.MaxOutputSize.ToTuple,
                    OutputGranularityX = caps.OutputGranularityX,
                    OutputGranularityY = caps.OutputGranularityY,
                    MinFrameInterval = caps.MinFrameInterval,
                    MaxFrameInterval = caps.MaxFrameInterval,
                    MinBitsPerSecond = caps.MinBitsPerSecond,
                    MaxBitsPerSecond = caps.MaxBitsPerSecond
                };

                mt.Release();
            }

            Marshal.FreeHGlobal(capData);
            return result;
        }

        public static void SetVideoOptions(this IPin pin, VideoOptions options)
        {
            // By design, VideoCaptureDevice can support a range of output formats for each media type. For example:
            // [0]: YUY2 Minimum: 160x120, Maximum: 320x240, X-axis 4STEP, Y-axis 2STEP
            // [1]: RGB8 Minimum: 640x480, Maximum: 640x480, X-axis 0STEP, Y-axis 0STEP
            // You can set the output size and frame rate within this range with SetFormat.
            // However, as far as I tried, all the USB cameras I had were returned with a fixed size (maximum and minimum are the same).

            // https://msdn.microsoft.com/ja-jp/windows/dd407352(v=vs.80)
            // Most members of VIDEO_STREAM_CONFIG_CAPS except the following are deprecated.
            // Applications should avoid using other members. Use IAMStreamConfig :: GetFormat instead.
            // --Guid: FORMAT_VideoInfo or FORMAT_VideoInfo2 etc.
            // --VideoStandard: Specify the analog TV signal format (NTSC, PAL, etc.) with the AnalogVideoStandard enumeration.
            // --MinFrameInterval, MaxFrameInterval: The range of frame rates supported by the video capture device. In units of 100 nanoseconds.

            // According to the above, VIDEO_STREAM_CONFIG_CAPS seems to be deprecated now. It seems to use IAMStreamConfig :: GetFormat instead.
            // Devices that adhere to the above specifications will return a fixed output size, but older devices that do not comply will return a variable output size.
            // For reference, the procedure to change the resolution, crop size, frame rate, etc. with VIDEO_STREAM_CONFIG_CAPS is as follows.

            // ① Frame rate (this is not deprecated)
            // Members MinFrameInterval and MaxFrameInterval of VIDEO_STREAM_CONFIG_CAPS are the minimum and maximum length of each video frame.
            // You can convert these values to frame rates using the following formula:
            // frames per second = 10,000,000 / frame duration

            // To request a specific frame rate, change the value of AvgTimePerFrame in the structure VIDEOINFOHEADER or VIDEOINFOHEADER2 in the media type.
            // The driver may not support all possible values between the minimum and maximum values, so the driver will use the closest possible value.

            // ② Cropping (partial clipping of the image)
            // MinCroppingSize = (160, 120) // Minimum Cropping size.
            // MaxCroppingSize = (320, 240) // Maximum Cropping size.
            // CropGranularityX = 4 // Horizontal subdivision.
            // CropGranularityY = 8 // Vertical subdivision.
            // CropAlignX = 2 // the top-left corner of the source rectangle can sit.
            // CropAlignY = 4 // the top-left corner of the source rectangle can sit.

            // ③ Output size
            // https://msdn.microsoft.com/ja-jp/library/cc353344.aspx
            // https://msdn.microsoft.com/ja-jp/library/cc371290.aspx
            // The VIDEO_STREAM_CONFIG_CAPS structure indicates the minimum and maximum width and height available for this media type.
            // Also indicates "step" size. Step size defines the value of the increment that can adjust the width or height.
            // For example, the device may return the following value:
            // MinOutputSize: 160 x 120
            // MaxOutputSize: 320 x 240
            // OutputGranularityX: 8 pixels (horizontal step size)
            // OutputGranularityY: 8 pixels (vertical step size)
            // Given these numbers, the width can be any value within the range (160, 168, 176, ... 304, 312, 320),
            // Height can be set to any value within the range (120, 128, 136, ... 224, 232, 240).

            // I have no USB camera of variable output size, uncomment below to debug.

            // size = new Size (168, 126);
            // vformat [0] .Caps = new DirectShow.VIDEO_STREAM_CONFIG_CAPS ()
            // {
            // Guid = DirectShow.FormatType.VideoInfo,
            // MinOutputSize = new DirectShow.SIZE () {cx = 160, cy = 120},
            // MaxOutputSize = new DirectShow.SIZE () {cx = 320, cy = 240},
            // OutputGranularityX = 4,
            // OutputGranularityY = 2
            //};

            // ReSharper disable once SuspiciousTypeConversion.Global
            var config = pin as IAMStreamConfig;
            if (config == null) return;
            config.GetNumberOfCapabilities(out var capCount, out var capSize).ThrowForHR();
            if (capSize != Marshal.SizeOf(typeof(VIDEO_STREAM_CONFIG_CAPS))) return;

            var capData = Marshal.AllocHGlobal(capSize);
            for (var i = 0; i < capCount; i++)
            {
                config.GetStreamCaps(i, out var mt, capData).ThrowForHR();
                var caps = capData.PtrToStructure<VIDEO_STREAM_CONFIG_CAPS>();

                if (!IsVideoFormatSupported(mt, caps, options))
                {
                    mt.Release();
                    continue;
                }

                if (mt.FormatType == FormatType.VideoInfo)
                {
                    var info = mt.pbFormat.PtrToStructure<VIDEOINFOHEADER>();
                    info.bmiHeader.biWidth = options.Size.Item1;
                    info.bmiHeader.biHeight = options.Size.Item2;
                    if (options.FrameRate > 0) info.AvgTimePerFrame = options.AvgTimePerFrame;
                    Marshal.StructureToPtr(info, mt.pbFormat, true);
                }
                else if (mt.FormatType == FormatType.VideoInfo2)
                {
                    var info = mt.pbFormat.PtrToStructure<VIDEOINFOHEADER2>();
                    info.bmiHeader.biWidth = options.Size.Item1;
                    info.bmiHeader.biHeight = options.Size.Item2;
                    if (options.FrameRate > 0) info.AvgTimePerFrame = options.AvgTimePerFrame;
                    Marshal.StructureToPtr(info, mt.pbFormat, true);
                }

                try
                {
                    config.SetFormat(mt).ThrowForHR();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                mt.Release();
                break;
            }

            Marshal.FreeHGlobal(capData);
        }

        private static bool IsVideoFormatSupported(AM_MEDIA_TYPE mt, VIDEO_STREAM_CONFIG_CAPS caps, VideoOptions options)
        {
            if (mt.MajorType != MediaType.Video) return false;
            if (!string.IsNullOrEmpty(options.Format) && options.Format != mt.SubType.GuidString(typeof(MediaSubType))) return false;
            if (mt.FormatType != FormatType.VideoInfo && mt.FormatType != FormatType.VideoInfo2) return false;
            for (var w = caps.MinOutputSize.cx; w <= caps.MaxOutputSize.cx; w += caps.OutputGranularityX)
            {
                if (w != options.Size.Item1) continue;
                for (var h = caps.MinOutputSize.cy; h <= caps.MaxOutputSize.cy; h += caps.OutputGranularityY)
                {
                    if (h != options.Size.Item2) continue;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        ///  Free the nested structures and release any COM objects within an AM_MEDIA_TYPE struct.
        /// </summary>
        public static void Release(this AM_MEDIA_TYPE mediaType)
        {
            if (mediaType == null) return;
            if (mediaType.cbFormat != 0)
            {
                Marshal.FreeCoTaskMem(mediaType.pbFormat);
                mediaType.cbFormat = 0;
                mediaType.pbFormat = IntPtr.Zero;
            }

            if (mediaType.pUnk != IntPtr.Zero)
            {
                Marshal.Release(mediaType.pUnk);
                mediaType.pUnk = IntPtr.Zero;
            }
        }
    }
}