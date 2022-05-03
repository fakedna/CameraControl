using System;
using System.Collections.Generic;

namespace VideoSource
{
    public interface IVideoSource : IDisposable
    {
        VideoDeviceInfo DeviceInfo { get; }

        VideoDeviceStatus Status { get; }

        bool IsAvailable { get; }

        VideoFormat CurrentFormat { get; }

        IEnumerable<VideoCapabilities> SupportedFormats { get; }

        IPropertyItems Properties { get; }

        void SetOptions(VideoOptions options);

        void StartCapture();

        void StopCapture();

        byte[] GetRawFrameData();

        event EventHandler<FrameEventArgs> OnFrameChanged;

        event EventHandler<StatusEventArgs> OnStatusChanged;
    }
}