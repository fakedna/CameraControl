using System;
using VideoSource;
using VideoSource.DirectShow;

namespace CameraControl
{
    public class PropertyViewModel  : BaseViewModel
    {
        private readonly Property _prop;
        private readonly string _name;
        private CameraControlFlags _isAuto;
        private int _value;
        private bool _changeEnabled;

        public PropertyViewModel(string name, Property property)
        {
            _name = name;
            _prop = property;
            _isAuto = property.CanAuto ? CameraControlFlags.Auto : CameraControlFlags.Manual;
            _value = property.GetValue();
            _changeEnabled = !IsAuto;
        }

        public Property Property => _prop;

        public string Name => _name;

        public int TickFrequency
        {
            get
            {
                var range = Math.Abs(_prop.Max - _prop.Min);
                return range > 25 ? range / 25 : 1;
            }
        }

        public int Min => _prop.Min;

        public int Max => _prop.Max;

        public bool IsSnapEnabled => _prop.Step >= 10;

        public bool CanAuto => _prop.CanAuto;

        public bool IsAuto
        {
            get => _isAuto == CameraControlFlags.Auto;
            set
            {
                if (!SetProperty(ref _isAuto, value ? CameraControlFlags.Auto : CameraControlFlags.Manual)) return;
                ChangeEnabled = !IsAuto;
                _prop.SetValue(_isAuto, _value);
                _value = _prop.GetValue();
                OnPropertyChanged(nameof(Value));
            }
        }

        public bool ChangeEnabled
        {
            get => _changeEnabled;
            set => SetProperty(ref _changeEnabled, value);
        }

        public int Value
        {
            get => _value;
            set
            {
                if (SetProperty(ref _value, value)) _prop.SetValue(_isAuto, _value);
            }
        }
    }
}