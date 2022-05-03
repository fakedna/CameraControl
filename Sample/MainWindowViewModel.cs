using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using VideoSource;

namespace CameraControl
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly List<VideoDeviceInfo> _cameraList;
        private readonly List<PropertyViewModel> _properties = new();
        private readonly List<string> _resolutions = new();
        private readonly IVideoSourceProvider _provider;
        private readonly ImageController _controller;
        private readonly RelayCommand _playCommand;
        private readonly FpsMeter _fpsMeter;
        private string _selectedResolution;
        private string _selectedCamera;
        private IVideoSource _source;
        private VideoFormat _format;

        public MainWindowViewModel()
        {
            _fpsMeter = new FpsMeter();
            _controller = new ImageController();
            _provider = VideoSourceProvider.Instance();
            _cameraList = _provider.GetDevices().ToList();
            _playCommand = new RelayCommand(PlayPressed, CanPressPlay);
        }

        private void UpdateSource()
        {
            _source?.Dispose();
            var index = _cameraList.FindIndex(x => x.Name == _selectedCamera);
            _source = _provider.GetVideoSource(_cameraList[index]);
            _source.OnStatusChanged += OnSourceStatusChanged;
            UpdateResolutions();
            UpdateProperties();
        }

        private void UpdateResolutions()
        {
            _resolutions.Clear();
            var caps = _source.SupportedFormats;
            var list = caps.OrderByDescending(x => x.MinOutputSize);
            foreach (var cap in list)
            {
                if (!FormatUtils.IsSupportedFormat(cap.SubType)) continue;
                var str = $"[{cap.SubType}] {cap.InputSize.Item1}x{cap.InputSize.Item2}";
                if (!_resolutions.Contains(str)) _resolutions.Add(str);
                Console.WriteLine($"Caps detected: {cap.ToString()}");
            }

            OnPropertyChanged(nameof(ResolutionList));
        }

        private void UpdateProperties()
        {
            _properties.Clear();
            var props = _source.Properties;
            foreach (var (key, value) in props.CameraControl)
            {
                if (!value.Available) continue;
                _properties.Add(new PropertyViewModel(key.ToString(), value));
                Console.WriteLine($"CamControl property detected: Name={key.ToString()}, {value}");
            }

            foreach (var (key, value) in props.VideoProcAmp)
            {
                if (!value.Available) continue;
                _properties.Add(new PropertyViewModel(key.ToString(), value));
                Console.WriteLine($"VideoProc property detected: Name={key.ToString()}, {value}");
            }

            OnPropertyChanged(nameof(Properties));
        }

        private void SetResolution()
        {
            var str = _selectedResolution.Split(' ');
            var format = str[0].Trim('[', ']');
            var size = str[1].Split('x');
            try
            {
                _source.SetOptions(new VideoOptions(format, (int.Parse(size[0]), int.Parse(size[1])), 0));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            _format = _source.CurrentFormat;
            _controller.Update(_format.Width, _format.Height, _format.BitmapStride, FormatUtils.GetPixelFormat(_format.SubType));
            OnPropertyChanged(nameof(ImageSource));
            OnPropertyChanged(nameof(RChannel));
            OnPropertyChanged(nameof(GChannel));
            OnPropertyChanged(nameof(BChannel));
            Console.WriteLine($"Camera format={_format}");
        }

        private void OnFrameChanged(object sender, FrameEventArgs e)
        {
            _fpsMeter.Update(e.SampleTime);
            OnPropertyChanged(nameof(Fps));
            _controller.Draw(e.RawData);
        }

        private void OnSourceStatusChanged(object sender, StatusEventArgs e)
        {
            _fpsMeter.Stop();
            Console.WriteLine($"Source status={e.Status}");
            CommandManager.InvalidateRequerySuggested(); // update command availability
            OnPropertyChanged(nameof(Fps));
            OnPropertyChanged(nameof(Status));
            if (e.Status != VideoDeviceStatus.Capturing) _controller.Clear();
        }

        private bool CanPressPlay(object arg) => _source != null && _source.IsAvailable;

        private void PlayPressed(object obj)
        {
            if (_source.Status == VideoDeviceStatus.Ready)
            {
                _source.StartCapture();
                _source.OnFrameChanged += OnFrameChanged;
            }
            else if (_source.Status == VideoDeviceStatus.Capturing)
            {
                _source.OnFrameChanged -= OnFrameChanged;
                _source.StopCapture();
            }
        }

        public ObservableCollection<PropertyViewModel> Properties => new(_properties);

        public ImageSource ImageSource => _controller.Image;

        public ObservableCollection<string> CameraList => new(_cameraList.Select(x => x.Name));

        public ObservableCollection<string> ResolutionList => new(_resolutions);

        public string SelectedCamera
        {
            get => _selectedCamera;
            set
            {
                SetProperty(ref _selectedCamera, value);
                UpdateSource();
            }
        }

        public string SelectedResolution
        {
            get => _selectedResolution;
            set
            {
                SetProperty(ref _selectedResolution, value);
                SetResolution();
            }
        }

        public string Fps => $"FPS: {_fpsMeter?.GetFrameRate()}";

        public ICommand PlayCommand => _playCommand;

        public string Status => _source?.Status.ToString();

        public byte ChannelMaxValue => byte.MaxValue;

        public byte ChannelMinValue => byte.MinValue;

        public byte RChannel
        {
            get => _controller.GetColorChannel(ColorChannels.R);
            set
            {
                _controller.SetColorChannel(ColorChannels.R, value);
                OnPropertyChanged();
            }
        }
        public byte GChannel
        {
            get => _controller.GetColorChannel(ColorChannels.G);
            set
            {
                _controller.SetColorChannel(ColorChannels.G, value);
                OnPropertyChanged();
            }
        }
        public byte BChannel
        {
            get => _controller.GetColorChannel(ColorChannels.B);
            set
            {
                _controller.SetColorChannel(ColorChannels.B, value);
                OnPropertyChanged();
            }
        }

        protected override void OnDisposing()
        {
            _controller?.Dispose();
            _source?.Dispose();
            base.OnDisposing();
        }
    }
}