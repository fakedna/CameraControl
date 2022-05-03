using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using VideoSource.DirectShow;

namespace VideoSource.DShowImpl
{
    internal sealed class DShowPropertyItems : IPropertyItems
    {
        public IReadOnlyDictionary<CameraControlProperty, Property> CameraControl => _cameraControl;

        public IReadOnlyDictionary<VideoProcAmpProperty, Property> VideoProcAmp => _videoProcAmp;

        public DShowPropertyItems(IBaseFilter vCapSource, bool showNotSupported = false)
        {
            if (vCapSource == null) return;
            // ReSharper disable once SuspiciousTypeConversion.Global
            GetCameraControlProps(vCapSource as IAMCameraControl, showNotSupported);
            // ReSharper disable once SuspiciousTypeConversion.Global
            GetVideoProcAmpProps(vCapSource as IAMVideoProcAmp, showNotSupported);
        }

        private void GetCameraControlProps(IAMCameraControl camCtrl, bool showNotSupported)
        {
            if (camCtrl == null) return;
            foreach (CameraControlProperty prop in Enum.GetValues(typeof(CameraControlProperty)))
            {
                try
                {
                    int min = 0, max = 0, step = 0, def = 0, flags = 0;
                    camCtrl.GetRange(prop, ref min, ref max, ref step, ref def, ref flags); // COMException if not supports.

                    void Set(CameraControlFlags flag, int value) => camCtrl.Set(prop, value, (int) flag);

                    int Get()
                    {
                        var value = 0;
                        camCtrl.Get(prop, ref value, ref flags);
                        return value;
                    }

                    _cameraControl.Add(prop, new Property(min, max, step, def, flags, Set, Get));
                }
                catch (Exception e)
                {
                    if (e is not COMException) throw;
                    if (showNotSupported) _cameraControl.Add(prop, new Property(0));
                }
            }
        }

        private void GetVideoProcAmpProps(IAMVideoProcAmp vidCtrl, bool showNotSupported)
        {
            if (vidCtrl == null) return;
            foreach (VideoProcAmpProperty prop in Enum.GetValues(typeof(VideoProcAmpProperty)))
            {
                try
                {
                    int min = 0, max = 0, step = 0, def = 0, flags = 0;
                    vidCtrl.GetRange(prop, ref min, ref max, ref step, ref def, ref flags); // COMException if not supports.

                    void Set(CameraControlFlags flag, int value) => vidCtrl.Set(prop, value, (int) flag);

                    int Get()
                    {
                        var value = 0;
                        vidCtrl.Get(prop, ref value, ref flags);
                        return value;
                    }

                    _videoProcAmp.Add(prop, new Property(min, max, step, def, flags, Set, Get));
                }
                catch (Exception e)
                {
                    if (e is not COMException) throw;
                    if (showNotSupported) _videoProcAmp.Add(prop, new Property(0));
                }
            }
        }

        private readonly Dictionary<CameraControlProperty, Property> _cameraControl = new();

        private readonly Dictionary<VideoProcAmpProperty, Property> _videoProcAmp = new();
    }
}