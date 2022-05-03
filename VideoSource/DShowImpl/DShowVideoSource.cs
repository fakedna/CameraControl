using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using VideoSource.DirectShow;

namespace VideoSource.DShowImpl
{
    internal sealed class DShowVideoSource : Disposable, IVideoSource
    {
        private CaptureFilterGraph _graph;
        private VideoDeviceStatus _status;
        private IPropertyItems _properties;
        private readonly IMoniker _moniker;

        public DShowVideoSource(VideoDeviceInfo info, IMoniker moniker)
        {
            DeviceInfo = info;
            Status = VideoDeviceStatus.InitStarted;
            _moniker = moniker;
            _graph = new CaptureFilterGraph();
            _graph.Create(_moniker, new VideoOptions());
            Status = _graph.IsCreated ? VideoDeviceStatus.Ready : VideoDeviceStatus.InitFailed;
        }

        public VideoDeviceInfo DeviceInfo { get; }

        public VideoDeviceStatus Status
        {
            get => _status;
            private set
            {
                _status = value;
                OnStatusChanged?.Invoke(this, new StatusEventArgs(_status));
            }
        }

        public bool IsAvailable => Status != VideoDeviceStatus.InitFailed && Status != VideoDeviceStatus.InitStarted && Status != VideoDeviceStatus.Disposed;

        public VideoFormat CurrentFormat
        {
            get
            {
                if (_status == VideoDeviceStatus.InitStarted || _status == VideoDeviceStatus.InitFailed) return new VideoFormat();
                var mt = new AM_MEDIA_TYPE();
                try
                {
                    _graph.GetSampleGrabber().GetConnectedMediaType(mt).ThrowForHR();
                    return mt.GetVideoFormat();
                }
                finally
                {
                    mt.Release();
                }
            }
        }

        public IEnumerable<VideoCapabilities> SupportedFormats
        {
            get
            {
                if (_status == VideoDeviceStatus.InitStarted || _status == VideoDeviceStatus.InitFailed) return Array.Empty<VideoCapabilities>();
                IPin pin = null;
                try
                {
                    pin = _graph.GetCaptureSource().GetPin(PinDirection.Output);
                    return pin.GetVideoCapabilities();
                }
                finally
                {
                    COMUtils.ReleaseInstance(pin);    
                }
            }
        }

        public IPropertyItems Properties
        {
            get
            {
                if (_status == VideoDeviceStatus.InitStarted || _status == VideoDeviceStatus.InitFailed) return null;
                return _properties ??= new DShowPropertyItems(_graph.GetCaptureSource(), true);
            }
        }

        public void SetOptions(VideoOptions options)
        {
            ThrowIfDisposed();
            if (_status == VideoDeviceStatus.InitStarted || _status == VideoDeviceStatus.InitFailed) return;
            var restart = Status == VideoDeviceStatus.Capturing;
            StopCapture();
            _graph.Dispose();
            _graph = new CaptureFilterGraph();
            _graph.Create(_moniker, options);
            if (restart) StartCapture();
        }

        public void StartCapture()
        {
            ThrowIfDisposed();
            if (Status != VideoDeviceStatus.Ready) return;
            _graph.FrameData.OnUpdated += OnFrameUpdated;
            _graph.Play(FilterState.Running);
            Status = VideoDeviceStatus.Capturing;
        }

        public void StopCapture()
        {
            ThrowIfDisposed();
            if (Status != VideoDeviceStatus.Capturing) return;
            _graph.FrameData.OnUpdated -= OnFrameUpdated;
            _graph.Play(FilterState.Stopped);
            Status = VideoDeviceStatus.Ready;
        }

        private void OnFrameUpdated(FrameData data)
        {
            OnFrameChanged?.Invoke(this, new FrameEventArgs(data.RawData, data.SampleTime));
        }

        public byte[] GetRawFrameData()
        {
            ThrowIfDisposed();
            if (Status != VideoDeviceStatus.Capturing) return Array.Empty<byte>();
            return _graph.FrameData.GetValue().RawData;
        }

        private event EventHandler<FrameEventArgs> OnFrameChanged;
        private event EventHandler<StatusEventArgs> OnStatusChanged;

        event EventHandler<FrameEventArgs> IVideoSource.OnFrameChanged
        {
            add
            {
                ThrowIfDisposed();
                OnFrameChanged += value;
            }
            remove
            {
                if (IsDisposed) return;
                OnFrameChanged -= value;
            }
        }

        event EventHandler<StatusEventArgs> IVideoSource.OnStatusChanged
        {
            add
            {
                ThrowIfDisposed();
                OnStatusChanged += value;
            }
            remove
            {
                if (IsDisposed) return;
                OnStatusChanged -= value;
            }
        }

        protected override void DisposeUnmanaged()
        {
            _graph.Dispose();
            COMUtils.ReleaseInstance(_moniker);
        }

        protected override void DisposeManaged()
        {
            StopCapture();
            Status = VideoDeviceStatus.Disposed;
            OnFrameChanged = null;
            OnStatusChanged = null;
        }
    }
}