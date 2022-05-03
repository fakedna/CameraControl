using System.Collections.Generic;

namespace VideoSource
{
    public interface IVideoSourceProvider
    {
        public IEnumerable<VideoDeviceInfo> GetDevices();

        public IVideoSource GetVideoSource(VideoDeviceInfo info);
    }
}