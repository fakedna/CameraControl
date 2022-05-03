using System;

namespace VideoSource
{
    public class FrameEventArgs : EventArgs
    {
        public byte[] RawData { get; }
        public double SampleTime { get; }

        public FrameEventArgs(byte[] rawData, double sampleTime)
        {
            RawData = rawData;
            SampleTime = sampleTime;
        }
    }

    public class StatusEventArgs : EventArgs
    {
        public VideoDeviceStatus Status { get; }

        public StatusEventArgs(VideoDeviceStatus status)
        {
            Status = status;
        }
    }
}