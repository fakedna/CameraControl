using System;
using System.Collections.Generic;
using VideoSource.DirectShow;

namespace VideoSource
{
    public interface IPropertyItems
    {
        /// <summary>
        /// Camera Control properties
        /// (Pan, Tilt, Roll, Zoom, Exposure, Iris, Focus)
        /// </summary>
        IReadOnlyDictionary<CameraControlProperty, Property> CameraControl { get; }

        /// <summary>
        /// Video Processing Amplifier properties
        /// (Brightness, Contrast, Hue, Saturation, Sharpness, Gamma, ColorEnable, WhiteBalance, BacklightCompensation, Gain)
        /// </summary>
        IReadOnlyDictionary<VideoProcAmpProperty, Property> VideoProcAmp { get; }
    }

    public readonly struct Property
    {
        public readonly int Min;
        public readonly int Max;
        public readonly int Step;
        public readonly int Default;
        public readonly CameraControlFlags Flags;
        public readonly Action<CameraControlFlags, int> SetValue;
        public readonly Func<int> GetValue;
        public readonly bool Available;
        public readonly bool CanAuto;

        public Property(int @default)
        {
            Min = 0;
            Max = 0;
            Step = 0;
            Default = @default;
            Flags = 0;
            CanAuto = false;
            SetValue = (_, _) => { };
            GetValue = () => @default;
            Available = false;
        }

        public Property(int min, int max, int step, int @default, int flags, Action<CameraControlFlags, int> set, Func<int> get)
        {
            Min = min;
            Max = max;
            Step = step;
            Default = @default;
            Flags = (CameraControlFlags) flags;
            CanAuto = (Flags & CameraControlFlags.Auto) == CameraControlFlags.Auto;
            SetValue = set;
            GetValue = get;
            Available = true;
        }

        public override string ToString()
        {
            return $"Available={Available}, Min={Min}, Max={Max}, Step={Step}, Default={Default}, Flags={Flags}";
        }
    }
}