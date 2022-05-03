using System;
using System.Runtime.InteropServices;

namespace VideoSource.DirectShow
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential), ComVisible(false)]
    internal class AM_MEDIA_TYPE
    {
        public Guid MajorType;
        public Guid SubType;
        [MarshalAs(UnmanagedType.Bool)] public bool bFixedSizeSamples;
        [MarshalAs(UnmanagedType.Bool)] public bool bTemporalCompression;
        public uint lSampleSize;
        public Guid FormatType;
        public IntPtr pUnk;
        public uint cbFormat;
        public IntPtr pbFormat;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode), ComVisible(false)]
    internal class FILTER_INFO
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string achName;

        [MarshalAs(UnmanagedType.IUnknown)] public object pGraph;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode), ComVisible(false)]
    internal class PIN_INFO
    {
        public IBaseFilter pFilter;
        public PinDirection dir;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string achName;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 8), ComVisible(false)]
    internal struct VIDEO_STREAM_CONFIG_CAPS
    {
        public Guid Guid;
        public uint VideoStandard;
        public SIZE InputSize;
        public SIZE MinCroppingSize;
        public SIZE MaxCroppingSize;
        public int CropGranularityX;
        public int CropGranularityY;
        public int CropAlignX;
        public int CropAlignY;
        public SIZE MinOutputSize;
        public SIZE MaxOutputSize;
        public int OutputGranularityX;
        public int OutputGranularityY;
        public int StretchTapsX;
        public int StretchTapsY;
        public int ShrinkTapsX;
        public int ShrinkTapsY;
        public long MinFrameInterval;
        public long MaxFrameInterval;
        public int MinBitsPerSecond;
        public int MaxBitsPerSecond;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential), ComVisible(false)]
    internal struct VIDEOINFOHEADER
    {
        public RECT SrcRect;
        public RECT TrgRect;
        public int BitRate;
        public int BitErrorRate;
        public long AvgTimePerFrame;
        public BITMAPINFOHEADER bmiHeader;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential), ComVisible(false)]
    internal struct VIDEOINFOHEADER2
    {
        public RECT SrcRect;
        public RECT TrgRect;
        public int BitRate;
        public int BitErrorRate;
        public long AvgTimePerFrame;
        public int InterlaceFlags;
        public int CopyProtectFlags;
        public int PictAspectRatioX;
        public int PictAspectRatioY;
        public int ControlFlags; // or Reserved1
        public int Reserved2;
        public BITMAPINFOHEADER bmiHeader;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 2), ComVisible(false)]
    internal struct BITMAPINFOHEADER
    {
        public int biSize;
        public int biWidth;
        public int biHeight;
        public short biPlanes;
        public short biBitCount;
        public int biCompression;
        public int biSizeImage;
        public int biXPelsPerMeter;
        public int biYPelsPerMeter;
        public int biClrUsed;
        public int biClrImportant;

        public override string ToString() => $"BMHeader: biSize={biSize}, biWidth={biWidth}, biHeight={biHeight}, biPlanes={biPlanes}, " +
                                             $"biBitCount={biBitCount}, biCompression={biCompression}, biSizeImage={biSizeImage}, " +
                                             $"biXPelsPerMeter={biXPelsPerMeter}, biYPelsPerMeter={biYPelsPerMeter}, biClrUsed={biClrUsed}";
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential), ComVisible(false)]
    internal struct WAVEFORMATEX
    {
        public ushort wFormatTag;
        public ushort nChannels;
        public uint nSamplesPerSec;
        public uint nAvgBytesPerSec;
        public short nBlockAlign;
        public short wBitsPerSample;
        public short cbSize;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 8), ComVisible(false)]
    internal struct SIZE
    {
        public int cx;
        public int cy;

        public override string ToString() => $"{{{cx}, {cy}}}";
        // for debugging.

        public (int, int) ToTuple => (cx, cy);
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential), ComVisible(false)]
    internal struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;

        public override string ToString() => $"{{{Left}, {Top}, {Right}, {Bottom}}}";
        // for debugging.
    }
}