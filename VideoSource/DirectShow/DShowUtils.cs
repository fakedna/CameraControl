using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace VideoSource.DirectShow
{
    internal static class DShowUtils
    {
        /// <summary>
        /// Retrives device monikers of category specified.
        /// Remember to release each moniker if no longer needed!
        /// </summary>
        public static IMoniker[] GetMonikers(Guid category)
        {
            IEnumMoniker enumerator = null;
            ICreateDevEnum device = null;
            var array = new ArrayList(); //using ArrayList cuz we don't know devices count
            try
            {
                device = DsGuid.SystemDeviceEnum.CoCreateInstance<ICreateDevEnum>();
                device.CreateClassEnumerator(ref category, ref enumerator, 0).ThrowForHR();
                if (enumerator == null) return Array.Empty<IMoniker>();
                var monikers = new IMoniker[1];
                var fetched = IntPtr.Zero;
                while (enumerator.Next(monikers.Length, monikers, fetched) == 0)
                {
                    try
                    {
                        // don't release resulting moniker
                        array.Add(monikers[0]);
                    }
                    catch
                    {
                        COMUtils.ReleaseInstance(monikers[0]);
                        throw;
                    }
                }

                var result = new IMoniker[array.Count];
                array.CopyTo(result);
                return result;
            }
            finally
            {
                COMUtils.ReleaseInstance(enumerator);
                COMUtils.ReleaseInstance(device);
            }
        }

        /// <summary>
        /// Retrives device moniker in category by index.
        /// </summary>
        public static IMoniker GetMoniker(Guid category, int index)
        {
            var monikers = GetMonikers(category);
            IMoniker result = null;
            for (var i = 0; i < monikers.Length; i++)
            {
                if (i == index) result = monikers[i];
                else COMUtils.ReleaseInstance(monikers[i]);
            }

            return result;
        }

        public static IPropertyBag GetPropertyBag(this IMoniker moniker)
        {
            var guid = typeof(IPropertyBag).GUID;
            moniker.BindToStorage(null!, null, ref guid, out var value);
            return value as IPropertyBag;
        }

        public static string GetProperty(this IPropertyBag bag, string propertyName)
        {
            try
            {
                bag.Read(propertyName, out var value, new ErrorLog()).ThrowForHR();
                return (string) value;
            }
            catch
            {
                return null;
            }
        }

        public static IBaseFilter GetBaseFilter(this IMoniker moniker)
        {
            var guid = typeof(IBaseFilter).GUID;
            moniker.BindToObject(null!, null, ref guid, out var value);
            return value as IBaseFilter;
        }

        public static IPin GetPin(this IBaseFilter filter, PinDirection pinDirection, int pinIndex = 0)
        {
            IEnumPins pins = null;
            var direction = PinDirection.Input;
            try
            {
                IPin pin = null;
                filter.EnumPins(out pins).ThrowForHR();
                var fetched = 0;
                var index = 0;
                while (pins.Next(1, ref pin, ref fetched) == 0)
                {
                    if (fetched == 0) break; //no pins at all
                    pin.QueryDirection(ref direction).ThrowForHR();
                    if (direction == pinDirection)
                    {
                        if (index == pinIndex) return pin;
                        index++;
                    }
                    else
                    {
                        COMUtils.ReleaseInstance(pin);
                    }
                }

                return null;
            }
            finally
            {
                COMUtils.ReleaseInstance(pins);
            }
        }

        /// <summary>
        ///  Free the nested interfaces within a PinInfo struct.
        /// </summary>
        public static void FreePinInfo(PIN_INFO pinInfo)
        {
            if (pinInfo.pFilter == null) return;
            COMUtils.ReleaseInstance(pinInfo.pFilter);
            pinInfo.pFilter = null;
        }

        private class ErrorLog : IErrorLog
        {
            public int AddError(string pszPropName, EXCEPINFO pExcepInfo)
            {
                throw new COMException($"Prop={pszPropName}, wCode={pExcepInfo.wCode}, sCode={pExcepInfo.scode}");
            }
        }
    }
}