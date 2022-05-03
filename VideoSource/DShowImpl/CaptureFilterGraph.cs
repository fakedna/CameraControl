using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using VideoSource.DirectShow;

namespace VideoSource.DShowImpl
{
    internal sealed class CaptureFilterGraph : Disposable
    {
        private IBaseFilter _source;
        private IBaseFilter _compressor;
        private IBaseFilter _renderer;
        private IGraphBuilder _graph;
        private ISampleGrabber _grabber;
        private ICaptureGraphBuilder2 _builder;
        private SampleGrabberCallback _sampler;

        // Create Filter Graph
        // +--------------------+  +----------------+ +---------------+
        // |Video Capture Source|→|  Sample Grabber |→| Null Renderer |
        // +--------------------+  +----------------+ +---------------+
        //                                 ↓ GetRawData()
        public void Create(IMoniker moniker, VideoOptions options)
        {
            try
            {
                _graph = DsGuid.FilterGraph.CoCreateInstance<IGraphBuilder>();

                // VideoCaptureSource
                _source = moniker.GetBaseFilter();
                if (!options.IsNull)
                {
                    var pin = _source.GetPin(PinDirection.Output);
                    pin.SetVideoOptions(options);
                    COMUtils.ReleaseInstance(pin);
                }
                _graph.AddFilter(_source, "VideoCapture").ThrowForHR();

                // SampleGrabber
                _compressor = DsGuid.SampleGrabber.CoCreateInstance<IBaseFilter>();
                _graph.AddFilter(_compressor, "SampleGrabber").ThrowForHR();
                _grabber = (ISampleGrabber) _compressor;
                // If SampleGrabber's media type is not set, it will use source's media type.
                // If media type is set, it will try to resample data to media type specified.
                // var mt = new AM_MEDIA_TYPE();
                // mt.MajorType = MediaType.Video;
                // mt.SubType = MediaSubType.RGB24;
                // _grabber.SetMediaType(mt).ThrowForHR();
                _grabber.SetBufferSamples(true).ThrowForHR(); //start sampling with sample grabber

                // SampleGrabberCB
                _sampler = new SampleGrabberCallback();
                _grabber.SetCallback(_sampler, 1).ThrowForHR(); // WhichMethodToCallback = BufferCB

                // Null Renderer
                _renderer = DsGuid.NullRenderer.CoCreateInstance<IBaseFilter>();
                _graph.AddFilter(_renderer, "NullRenderer").ThrowForHR();

                // Builder
                _builder = DsGuid.CaptureGraphBuilder2.CoCreateInstance<ICaptureGraphBuilder2>();
                _builder.SetFiltergraph(_graph).ThrowForHR();
                var pinCategory = PinCategory.CAPTURE;
                var mediaType = MediaType.Video;
                _builder.RenderStream(ref pinCategory, ref mediaType, _source, _compressor, _renderer).ThrowForHR();
                IsCreated = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Dispose();
                throw;
            }
        }

        public bool IsCreated { get; private set; } = false;

        public void Play(FilterState state)
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            var mediaControl = _graph as IMediaControl;
            if (mediaControl == null) return;
            switch (state)
            {
                case FilterState.Paused:
                    mediaControl.Pause().ThrowForHR();
                    break;
                case FilterState.Stopped:
                    mediaControl.Stop().ThrowForHR();
                    break;
                default:
                    mediaControl.Run().ThrowForHR();
                    break;
            }
        }

        public FilterState GetState()
        {
            var mediaControl = _graph as IMediaControl;
            if (mediaControl == null) return FilterState.Unknown;
            var result = (uint) mediaControl.GetState(50, out var state);
            return result switch
            {
                ErrorCodes.S_OK => (FilterState) state,
                ErrorCodes.VFW_S_STATE_INTERMEDIATE => FilterState.InTransition,
                ErrorCodes.VFW_S_CANT_CUE => FilterState.Unknown,
                _ => FilterState.Unknown
            };
        }

        public IBaseFilter GetCaptureSource() => _source;

        public ISampleGrabber GetSampleGrabber() => _grabber;


        public IObservedProperty<FrameData> FrameData => _sampler;

        protected override void DisposeUnmanaged()
        {
            COMUtils.ReleaseInstance(_builder);
            COMUtils.ReleaseInstance(_grabber);
            COMUtils.ReleaseInstance(_graph);
        }

        // This can be modified to return a pointer to sample buffer if you want to reduce memory usage.
        private class SampleGrabberCallback : ISampleGrabberCB, IObservedProperty<FrameData>
        {
            private byte[] _buffer;
            private double _sampleTime;
            private IMediaSample _sample;
            private readonly object _lock = new();

            public event Action<FrameData> OnUpdated;

            public FrameData GetValue()
            {
                if (_buffer == null) return new FrameData();
                lock (_lock) return new FrameData(_sampleTime, _buffer);
            }

            // never called.
            public int SampleCB(double sampleTime, IMediaSample pSample)
            {
                lock (_lock)
                {
                    _sampleTime = sampleTime;
                    _sample = pSample;
                }

                return 0;
            }

            // called when each sample completed.
            // The data processing thread blocks until the callback method returns.
            // If the callback does not return quickly, it can interfere with playback.
            public int BufferCB(double sampleTime, IntPtr pBuffer, int bufferLen)
            {
                lock (_lock)
                {
                    _sampleTime = sampleTime;
                    if (_buffer == null || _buffer.Length != bufferLen) _buffer = new byte[bufferLen];
                    Marshal.Copy(pBuffer, _buffer, 0, bufferLen);
                    OnUpdated?.Invoke(new FrameData(_sampleTime, _buffer));
                }

                return 0;
            }
        }
    }

    internal readonly struct FrameData
    {
        public readonly double SampleTime;
        public readonly byte[] RawData;

        public FrameData(double sampleTime, byte[] rawData)
        {
            SampleTime = sampleTime;
            RawData = rawData;
        }
    }

    internal interface IObservedProperty<out T>
    {
        event Action<T> OnUpdated;
        T GetValue();
    }
}