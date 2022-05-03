using System;
using System.Collections.Generic;
using VideoSource.DShowImpl;

namespace VideoSource
{
    public enum VideoInputFormat
    {
        DirectShow,
    }

    public class VideoSourceProvider : IVideoSourceProvider
    {
        public static IVideoSourceProvider Instance() => Instance(GetAppropriateFormat());
        public static IVideoSourceProvider Instance(VideoInputFormat inputFormat)
        {
            return inputFormat switch
            {
                VideoInputFormat.DirectShow => new DShowVideoSourceProvider(),
                _ => throw new ArgumentOutOfRangeException(nameof(inputFormat), inputFormat, null)
            };
        }

        private static VideoInputFormat GetAppropriateFormat()
        {
            if (OperatingSystem.IsWindows()) return VideoInputFormat.DirectShow;
            return VideoInputFormat.DirectShow; //TODO
        }

        public IEnumerable<VideoDeviceInfo> GetDevices()
        {
            return Instance().GetDevices();
        }

        public IVideoSource GetVideoSource(VideoDeviceInfo info)
        {
            return Instance().GetVideoSource(info);
        }
    }
}