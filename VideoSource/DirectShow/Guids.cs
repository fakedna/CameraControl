using System;

// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

namespace VideoSource.DirectShow
{
    internal static class DsGuid
    {
        //CLSID
        public static readonly Guid AudioInputDeviceCategory = new("{33D9A762-90C8-11d0-BD43-00A0C911CE86}");
        public static readonly Guid AudioRendererCategory = new("{E0F158E1-CB04-11d0-BD4E-00A0C911CE86}");
        public static readonly Guid VideoInputDeviceCategory = new("{860BB310-5D01-11d0-BD3B-00A0C911CE86}");
        public static readonly Guid VideoCompressorCategory = new("{33D9A760-90C8-11d0-BD43-00A0C911CE86}");

        public static readonly Guid NullRenderer = new("{C1F400A4-3F08-11D3-9F0B-006008039E37}");
        public static readonly Guid SampleGrabber = new("{C1F400A0-3F08-11D3-9F0B-006008039E37}");

        public static readonly Guid FilterGraph = new("{E436EBB3-524F-11CE-9F53-0020AF0BA770}");
        public static readonly Guid SystemDeviceEnum = new("{62BE5D10-60EB-11d0-BD3B-00A0C911CE86}");
        public static readonly Guid CaptureGraphBuilder2 = new("{BF87B6E1-8C27-11d0-B3F0-00AA003761C5}");

        //IID
        public static readonly Guid IPropertyBag = new("{55272A00-42CB-11CE-8135-00AA004BB851}");
        public static readonly Guid IBaseFilter = new("{56a86895-0ad4-11ce-b03a-0020af0ba770}");
        public static readonly Guid IAMStreamConfig = new("{C6E13340-30AC-11d0-A18C-00A0C9118956}");
    }

    internal static class MediaType
    {
        public static readonly Guid Video = new("{73646976-0000-0010-8000-00AA00389B71}");
        public static readonly Guid Audio = new("{73647561-0000-0010-8000-00AA00389B71}");
    }

    internal static class MediaSubType
    {
        public static readonly Guid None = new("{E436EB8E-524F-11CE-9F53-0020AF0BA770}");
        public static readonly Guid I420 = new("{30323449-0000-0010-8000-00aa00389b71}");
        public static readonly Guid YUYV = new("{56595559-0000-0010-8000-00AA00389B71}");
        public static readonly Guid IYUV = new("{56555949-0000-0010-8000-00AA00389B71}");
        public static readonly Guid YVU9 = new("{39555659-0000-0010-8000-00AA00389B71}");
        public static readonly Guid YUY2 = new("{32595559-0000-0010-8000-00AA00389B71}");
        public static readonly Guid YVYU = new("{55595659-0000-0010-8000-00AA00389B71}");
        public static readonly Guid UYVY = new("{59565955-0000-0010-8000-00AA00389B71}");
        public static readonly Guid MJPG = new("{47504A4D-0000-0010-8000-00AA00389B71}");
        public static readonly Guid RGB565 = new("{E436EB7B-524F-11CE-9F53-0020AF0BA770}");
        public static readonly Guid RGB555 = new("{E436EB7C-524F-11CE-9F53-0020AF0BA770}");
        public static readonly Guid RGB24 = new("{E436EB7D-524F-11CE-9F53-0020AF0BA770}");
        public static readonly Guid RGB32 = new("{E436EB7E-524F-11CE-9F53-0020AF0BA770}");
        public static readonly Guid ARGB32 = new("{773C9AC0-3274-11D0-B724-00AA006C1A01}");
        public static readonly Guid PCM = new("{00000001-0000-0010-8000-00AA00389B71}");
        public static readonly Guid WAVE = new("{E436EB8B-524F-11CE-9F53-0020AF0BA770}");
    }

    internal static class FormatType
    {
        public static readonly Guid None = new("{0F6417D6-C318-11D0-A43F-00A0C9223196}");
        public static readonly Guid VideoInfo = new("{05589F80-C356-11CE-BF01-00AA0055595A}");
        public static readonly Guid VideoInfo2 = new("{F72A76A0-EB0A-11d0-ACE4-0000C0CC16BA}");
        public static readonly Guid WaveFormatEx = new("{05589F81-C356-11CE-BF01-00AA0055595A}");
    }

    internal static class PinCategory
    {
        public static readonly Guid CAPTURE = new("{fb6c4281-0353-11d1-905f-0000c0cc16ba}");
        public static readonly Guid PREVIEW = new("{fb6c4282-0353-11d1-905f-0000c0cc16ba}");
        public static readonly Guid STILL = new("{fb6c428a-0353-11d1-905f-0000c0cc16ba}");
    }
}