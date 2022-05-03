using System;
using System.Runtime.InteropServices;

namespace VideoSource.DirectShow
{
    internal static class COMUtils
    {
        /// <summary>Create an instance of a COM object</summary>
        public static object CoCreateInstance(this Guid clsid)
        {
            var type = Type.GetTypeFromCLSID(clsid);
            return type == null ? default : Activator.CreateInstance(type);
        }

        /// <summary>Create an instance of a COM object and safe cast to T</summary>
        public static T CoCreateInstance<T>(this Guid clsid) where T : class
        {
            return clsid.CoCreateInstance() as T;
        }

        /// <summary>Release an instance of a COM object (null-checked)</summary>
        public static void ReleaseInstance<T>(T com) where T : class
        {
            if (com == null) return;
            Marshal.ReleaseComObject(com);
        }

        public static T PtrToStructure<T>(this IntPtr ptr)
        {
            return (T) Marshal.PtrToStructure(ptr, typeof(T));
        }

        public static void ThrowForHR(this int hr)
        {
            Marshal.ThrowExceptionForHR(hr);
        }

        /// <summary>
        /// Use reflection to walk a class looking for a property containing a specified guid
        /// </summary>
        /// <returns>String representing property name that matches, or Guid.ToString() for no match</returns>
        public static string GuidString(this Guid guid, Type targetType)
        {
            object o = null;
            // Read the fields from the class
            var fields = targetType.GetFields();
            // Walk the returned array
            foreach (var m in fields)
            {
                // Read the value of the property.  The parameter is ignored.
                o = m.GetValue(o);
                // Compare it with the sought value
                if (o != null && (Guid) o == guid) return m.Name;
            }

            return guid.ToString();
        }
    }
}