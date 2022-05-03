using System.Collections.Generic;
using VideoSource.DirectShow;

namespace VideoSource.DShowImpl
{
    internal sealed class DShowVideoSourceProvider : IVideoSourceProvider
    {
        public IEnumerable<VideoDeviceInfo> GetDevices()
        {
            var monikers = DShowUtils.GetMonikers(DsGuid.VideoInputDeviceCategory);
            var result = new VideoDeviceInfo[monikers.Length];
            var index = 0;
            foreach (var moniker in monikers)
            {
                var prop = moniker.GetPropertyBag();
                try
                {
                    var name = prop.GetProperty("FriendlyName");
                    var description = prop.GetProperty("Description");
                    var path = prop.GetProperty("DevicePath");
                    result[index] = new VideoDeviceInfo(index, name, description, path);
                }
                finally
                {
                    COMUtils.ReleaseInstance(moniker);
                    COMUtils.ReleaseInstance(prop);
                    index++;
                }
            }

            return result;
        }

        public IVideoSource GetVideoSource(VideoDeviceInfo info)
        {
            var moniker = DShowUtils.GetMoniker(DsGuid.VideoInputDeviceCategory, info.Index);
            return moniker == null ? null : new DShowVideoSource(info, moniker);
        }
    }
}