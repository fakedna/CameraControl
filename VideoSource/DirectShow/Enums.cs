using System;
using System.Runtime.InteropServices;

namespace VideoSource.DirectShow
{
    [ComVisible(false)]
    internal enum PinDirection
    {
        Input = 0,
        Output = 1,
    }

    [ComVisible(false)]
    internal enum FilterState
    {
        Unknown = -2,
        InTransition = -1,
        Stopped = 0,
        Paused = 1,
        Running = 2,
    }

    [ComVisible(false)]
    public enum CameraControlProperty
    {
        Pan = 0,
        Tilt = 1,
        Roll = 2,
        Zoom = 3,
        Exposure = 4,
        Iris = 5,
        Focus = 6,
    }

    [ComVisible(false), Flags]
    public enum CameraControlFlags
    {
        Auto = 0x0001,
        Manual = 0x0002,
    }

    [ComVisible(false)]
    public enum VideoProcAmpProperty
    {
        Brightness = 0,
        Contrast = 1,
        Hue = 2,
        Saturation = 3,
        Sharpness = 4,
        Gamma = 5,
        ColorEnable = 6,
        WhiteBalance = 7,
        BacklightCompensation = 8,
        Gain = 9
    }
}