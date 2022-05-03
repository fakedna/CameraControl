using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;

namespace VideoSource.DirectShow
{
    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("0000010c-0000-0000-C000-000000000046"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IPersist
    {
        [PreserveSig]
        int GetClassID([Out] out Guid pClassID);
    }


    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("56a86899-0ad4-11ce-b03a-0020af0ba770"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMediaFilter : IPersist
    {
        #region IPersist Methods

        [PreserveSig]
        new int GetClassID([Out] out Guid pClassID);

        #endregion

        [PreserveSig]
        int Stop();

        [PreserveSig]
        int Pause();

        [PreserveSig]
        int Run([In] long tStart);

        [PreserveSig]
        int GetState([In] int dwMilliSecsTimeout, [Out] out FilterState filtState);

        [PreserveSig]
        int SetSyncSource([In] IReferenceClock pClock);

        [PreserveSig]
        int GetSyncSource([Out] out IReferenceClock pClock);
    }

    [ComVisible(true), ComImport, Guid("56a8689f-0ad4-11ce-b03a-0020af0ba770"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IFilterGraph
    {
        int AddFilter([In] IBaseFilter pFilter, [In, MarshalAs(UnmanagedType.LPWStr)] string pName);
        int RemoveFilter([In] IBaseFilter pFilter);
        int EnumFilters([In, Out] ref IEnumFilters ppEnum);
        int FindFilterByName([In, MarshalAs(UnmanagedType.LPWStr)] string pName, [In, Out] ref IBaseFilter ppFilter);

        int ConnectDirect([In] IPin ppinOut, [In] IPin ppinIn, [In, MarshalAs(UnmanagedType.LPStruct)]
            AM_MEDIA_TYPE pmt);

        int Reconnect([In] IPin ppin);
        int Disconnect([In] IPin ppin);
        int SetDefaultSyncSource();
    }

    [ComVisible(true), ComImport, Guid("56a868a9-0ad4-11ce-b03a-0020af0ba770"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IGraphBuilder : IFilterGraph
    {
        int Connect([In] IPin ppinOut, [In] IPin ppinIn);
        int Render([In] IPin ppinOut);

        int RenderFile([In, MarshalAs(UnmanagedType.LPWStr)] string lpcwstrFile,
            [In, MarshalAs(UnmanagedType.LPWStr)] string lpcwstrPlayList);

        int AddSourceFilter([In, MarshalAs(UnmanagedType.LPWStr)] string lpcwstrFileName,
            [In, MarshalAs(UnmanagedType.LPWStr)] string lpcwstrFilterName, [In, Out] ref IBaseFilter ppFilter);

        int SetLogFile(IntPtr hFile);
        int Abort();
        int ShouldOperationContinue();
    }

    [ComVisible(true), ComImport, Guid("56a868b1-0ad4-11ce-b03a-0020af0ba770"),
     InterfaceType(ComInterfaceType.InterfaceIsDual)]
    internal interface IMediaControl
    {
        int Run();
        int Pause();
        int Stop();
        int GetState(int msTimeout, out int pfs);
        int RenderFile(string strFilename);

        int AddSourceFilter([In] string strFilename, [In, Out, MarshalAs(UnmanagedType.IDispatch)]
            ref object ppUnk);

        int get_FilterCollection([In, Out, MarshalAs(UnmanagedType.IDispatch)]
            ref object ppUnk);

        int get_RegFilterCollection([In, Out, MarshalAs(UnmanagedType.IDispatch)]
            ref object ppUnk);

        int StopWhenReady();
    }

    [ComVisible(true), ComImport, Guid("93E5A4E0-2D50-11d2-ABFA-00A0C9C6E38D"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface ICaptureGraphBuilder2
    {
        int SetFiltergraph([In] IGraphBuilder pfg);
        int GetFiltergraph([In, Out] ref IGraphBuilder ppfg);

        int SetOutputFileName([In] ref Guid pType, [In, MarshalAs(UnmanagedType.LPWStr)] string lpstrFile,
            [In, Out] ref IBaseFilter ppbf, [In, Out] ref IFileSinkFilter ppSink);

        int FindInterface([In] ref Guid pCategory, [In] ref Guid pType, [In] IBaseFilter pbf, [In] IntPtr riid,
            [In, Out, MarshalAs(UnmanagedType.IUnknown)]
            ref object ppint);

        int RenderStream([In] ref Guid pCategory, [In] ref Guid pType, [In, MarshalAs(UnmanagedType.IUnknown)]
            object pSource, [In] IBaseFilter pfCompressor, [In] IBaseFilter pfRenderer);

        int ControlStream([In] ref Guid pCategory, [In] ref Guid pType, [In] IBaseFilter pFilter,
            [In] IntPtr pstart, [In] IntPtr pstop, [In] short wStartCookie, [In] short wStopCookie);

        int AllocCapFile([In, MarshalAs(UnmanagedType.LPWStr)] string lpstrFile, [In] long dwlSize);

        int CopyCaptureFile([In, MarshalAs(UnmanagedType.LPWStr)] string lpwstrOld,
            [In, MarshalAs(UnmanagedType.LPWStr)] string lpwstrNew, [In] int fAllowEscAbort,
            [In] IAMCopyCaptureFileProgress pFilter);

        int FindPin([In] object pSource, [In] int pindir, [In] ref Guid pCategory, [In] ref Guid pType,
            [In, MarshalAs(UnmanagedType.Bool)] bool fUnconnected, [In] int num, [Out] out IntPtr ppPin);
    }

    [ComVisible(true), ComImport, Guid("a2104830-7c70-11cf-8bce-00aa00a3f1a6"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IFileSinkFilter
    {
        int SetFileName([In, MarshalAs(UnmanagedType.LPWStr)] string pszFileName,
            [In, MarshalAs(UnmanagedType.LPStruct)]
            AM_MEDIA_TYPE pmt);

        int GetCurFile([In, Out, MarshalAs(UnmanagedType.LPWStr)]
            ref string pszFileName,
            [Out, MarshalAs(UnmanagedType.LPStruct)]
            out AM_MEDIA_TYPE pmt);
    }

    [ComVisible(true), ComImport, Guid("670d1d20-a068-11d0-b3f0-00aa003761c5"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IAMCopyCaptureFileProgress
    {
        int Progress(int iProgress);
    }


    [ComVisible(true), ComImport, Guid("C6E13370-30AC-11d0-A18C-00A0C9118956"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IAMCameraControl
    {
        int GetRange([In] CameraControlProperty property, [In, Out] ref int pMin, [In, Out] ref int pMax,
            [In, Out] ref int pSteppingDelta, [In, Out] ref int pDefault, [In, Out] ref int pCapsFlag);

        int Set([In] CameraControlProperty property, [In] int lValue, [In] int flags);
        int Get([In] CameraControlProperty property, [In, Out] ref int lValue, [In, Out] ref int flags);
    }


    [ComVisible(true), ComImport, Guid("C6E13360-30AC-11d0-A18C-00A0C9118956"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IAMVideoProcAmp
    {
        int GetRange([In] VideoProcAmpProperty property, [In, Out] ref int pMin, [In, Out] ref int pMax,
            [In, Out] ref int pSteppingDelta, [In, Out] ref int pDefault, [In, Out] ref int pCapsFlag);

        int Set([In] VideoProcAmpProperty property, [In] int lValue, [In] int flags);
        int Get([In] VideoProcAmpProperty property, [In, Out] ref int lValue, [In, Out] ref int flags);
    }


    [ComVisible(true), ComImport, Guid("6A2E0670-28E4-11D0-A18C-00A0C9118956"),
     SuppressUnmanagedCodeSecurity, InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IAMVideoControl
    {
        int GetCaps([In] IPin pPin, [Out] out int pCapsFlags);
        int SetMode([In] IPin pPin, [In] int mode);
        int GetMode([In] IPin pPin, [Out] out int mode);
        int GetCurrentActualFrameRate([In] IPin pPin, [Out] out long actualFrameRate);

        int GetMaxAvailableFrameRate([In] IPin pPin, [In] int iIndex, [In] SIZE dimensions,
            [Out] out long maxAvailableFrameRate);

        int GetFrameRateList([In] IPin pPin, [In] int iIndex, [In] SIZE dimensions, [Out] out int listSize,
            [Out] out IntPtr frameRates);
    }

    [ComVisible(true), ComImport, Guid("56a86895-0ad4-11ce-b03a-0020af0ba770"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IBaseFilter : IMediaFilter
    {
        #region IPersist Methods

        [PreserveSig]
        new int GetClassID([Out] out Guid pClassID);

        #endregion

        #region IMediaFilter Methods

        [PreserveSig]
        new int Stop();

        [PreserveSig]
        new int Pause();

        [PreserveSig]
        new int Run(long tStart);

        [PreserveSig]
        new int GetState([In] int dwMilliSecsTimeout, [Out] out FilterState filtState);

        [PreserveSig]
        new int SetSyncSource([In] IReferenceClock pClock);

        [PreserveSig]
        new int GetSyncSource([Out] out IReferenceClock pClock);

        #endregion

        [PreserveSig]
        int EnumPins([Out] out IEnumPins ppEnum);

        [PreserveSig]
        int FindPin([In, MarshalAs(UnmanagedType.LPWStr)] string Id, [Out] out IPin ppPin);

        [PreserveSig]
        int QueryFilterInfo([Out] out FILTER_INFO pInfo);

        [PreserveSig]
        int JoinFilterGraph([In] IFilterGraph pGraph, [In, MarshalAs(UnmanagedType.LPWStr)] string pName);

        [PreserveSig]
        int QueryVendorInfo([Out, MarshalAs(UnmanagedType.LPWStr)] out string pVendorInfo);
    }


    /// <summary>
    /// フィルタ グラフ内のフィルタを列挙するインタフェース.
    /// </summary>
    [ComVisible(true), ComImport, Guid("56a86893-0ad4-11ce-b03a-0020af0ba770"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IEnumFilters
    {
        int Next([In] int cFilters, [In, Out] ref IBaseFilter ppFilter, [In, Out] ref int pcFetched);
        int Skip([In] int cFilters);
        void Reset();
        void Clone([In, Out] ref IEnumFilters ppEnum);
    }

    [ComVisible(true), ComImport, Guid("C6E13340-30AC-11d0-A18C-00A0C9118956"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IAMStreamConfig
    {
        [PreserveSig]
        int SetFormat([In, MarshalAs(UnmanagedType.LPStruct)]
            AM_MEDIA_TYPE pmt);

        [PreserveSig]
        int GetFormat([Out] out AM_MEDIA_TYPE pmt);

        [PreserveSig]
        int GetNumberOfCapabilities(out int piCount, out int piSize);

        [PreserveSig]
        int GetStreamCaps([In] int iIndex, [Out] out AM_MEDIA_TYPE ppmt, [In] IntPtr pSCC);
    }

    [ComVisible(true), ComImport, Guid("56a8689a-0ad4-11ce-b03a-0020af0ba770"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMediaSample
    {
        int GetPointer(ref IntPtr ppBuffer);
        int GetSize();
        int GetTime(ref long pTimeStart, ref long pTimeEnd);

        int SetTime([In, MarshalAs(UnmanagedType.LPStruct)]
            ulong pTimeStart, [In, MarshalAs(UnmanagedType.LPStruct)]
            ulong pTimeEnd);

        int IsSyncPoint();
        int SetSyncPoint([In, MarshalAs(UnmanagedType.Bool)] bool bIsSyncPoint);
        int IsPreroll();
        int SetPreroll([In, MarshalAs(UnmanagedType.Bool)] bool bIsPreroll);
        int GetActualDataLength();
        int SetActualDataLength(int len);

        int GetMediaType([In, Out, MarshalAs(UnmanagedType.LPStruct)]
            ref AM_MEDIA_TYPE ppMediaType);

        int SetMediaType([In, MarshalAs(UnmanagedType.LPStruct)]
            AM_MEDIA_TYPE pMediaType);

        int IsDiscontinuity();
        int SetDiscontinuity([In, MarshalAs(UnmanagedType.Bool)] bool bDiscontinuity);
        int GetMediaTime(ref long pTimeStart, ref long pTimeEnd);

        int SetMediaTime([In, MarshalAs(UnmanagedType.LPStruct)]
            ulong pTimeStart, [In, MarshalAs(UnmanagedType.LPStruct)]
            ulong pTimeEnd);
    }

    [ComVisible(true), ComImport, Guid("89c31040-846b-11ce-97d3-00aa0055595a"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IEnumMediaTypes
    {
        int Next([In] int cMediaTypes, [In, Out, MarshalAs(UnmanagedType.LPStruct)]
            ref AM_MEDIA_TYPE ppMediaTypes, [In, Out] ref int pcFetched);

        int Skip([In] int cMediaTypes);
        int Reset();
        int Clone([In, Out] ref IEnumMediaTypes ppEnum);
    }

    [ComVisible(true), ComImport, Guid("56a86891-0ad4-11ce-b03a-0020af0ba770"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IPin
    {
        int Connect([In] IPin pReceivePin, [In, MarshalAs(UnmanagedType.LPStruct)]
            AM_MEDIA_TYPE pmt);

        int ReceiveConnection([In] IPin pReceivePin, [In, MarshalAs(UnmanagedType.LPStruct)]
            AM_MEDIA_TYPE pmt);

        int Disconnect();
        int ConnectedTo([In, Out] ref IPin ppPin);

        int ConnectionMediaType([Out, MarshalAs(UnmanagedType.LPStruct)]
            AM_MEDIA_TYPE pmt);

        int QueryPinInfo([Out] PIN_INFO pInfo);
        int QueryDirection(ref PinDirection pPinDir);

        int QueryId([In, Out, MarshalAs(UnmanagedType.LPWStr)]
            ref string id);

        int QueryAccept([In, MarshalAs(UnmanagedType.LPStruct)]
            AM_MEDIA_TYPE pmt);

        int EnumMediaTypes([In, Out] ref IEnumMediaTypes ppEnum);
        int QueryInternalConnections(IntPtr apPin, [In, Out] ref int nPin);
        int EndOfStream();
        int BeginFlush();
        int EndFlush();
        int NewSegment(long tStart, long tStop, double dRate);
    }

    [ComVisible(true), ComImport, Guid("56a86892-0ad4-11ce-b03a-0020af0ba770"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IEnumPins
    {
        int Next([In] int cPins, [In, Out] ref IPin ppPins, [In, Out] ref int pcFetched);
        int Skip([In] int cPins);
        void Reset();
        void Clone([In, Out] ref IEnumPins ppEnum);
    }

    [ComVisible(true), ComImport, Guid("56a86897-0ad4-11ce-b03a-0020af0ba770"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IReferenceClock
    {
        int GetTime(ref long pTime);
        int AdviseTime(long baseTime, long streamTime, IntPtr hEvent, ref int pdwAdviseCookie);
        int AdvisePeriodic(long startTime, long periodTime, IntPtr hSemaphore, ref int pdwAdviseCookie);
        int Unadvise(int dwAdviseCookie);
    }

    [ComVisible(true), ComImport, Guid("29840822-5B84-11D0-BD3B-00A0C911CE86"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface ICreateDevEnum
    {
        int CreateClassEnumerator([In] ref Guid pType, [In, Out] ref IEnumMoniker ppEnumMoniker, [In] int dwFlags);
    }

    [ComVisible(true), ComImport, Guid("55272A00-42CB-11CE-8135-00AA004BB851"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IPropertyBag
    {
        [PreserveSig]
        int Read(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszPropName,
            [Out, MarshalAs(UnmanagedType.Struct)] out object pVar,
            [In] IErrorLog pErrorLog
        );

        [PreserveSig]
        int Write(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszPropName,
            [In, MarshalAs(UnmanagedType.Struct)] ref object pVar
        );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("3127CA40-446E-11CE-8135-00AA004BB851"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IErrorLog
    {
        [PreserveSig]
        int AddError(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszPropName,
            [In] EXCEPINFO pExcepInfo);
    }

    [ComVisible(true), ComImport, Guid("6B652FFF-11FE-4fce-92AD-0266B5D7C78F"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface ISampleGrabber
    {
        int SetOneShot([In, MarshalAs(UnmanagedType.Bool)] bool oneShot);

        int SetMediaType([In, MarshalAs(UnmanagedType.LPStruct)]
            AM_MEDIA_TYPE pmt);

        int GetConnectedMediaType([Out, MarshalAs(UnmanagedType.LPStruct)]
            AM_MEDIA_TYPE pmt);

        int SetBufferSamples([In, MarshalAs(UnmanagedType.Bool)] bool bufferThem);
        int GetCurrentBuffer(ref int pBufferSize, IntPtr pBuffer);
        int GetCurrentSample(IntPtr ppSample);
        int SetCallback(ISampleGrabberCB pCallback, int whichMethodToCallback);
    }

    [ComVisible(true), ComImport, Guid("0579154A-2B53-4994-B0D0-E773148EFF85"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface ISampleGrabberCB
    {
        [PreserveSig]
        int SampleCB(double sampleTime, IMediaSample pSample);

        [PreserveSig]
        int BufferCB(double sampleTime, IntPtr pBuffer, int bufferLen);
    }
}