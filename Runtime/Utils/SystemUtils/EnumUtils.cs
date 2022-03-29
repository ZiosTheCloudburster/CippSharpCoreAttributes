using System;

namespace CippSharp.Core
{
    public static class EnumUtils
    {
        /// <summary>
        /// Determines if an enum has the given flag defined bitwise.
        /// Fallback equivalent to .NET's Enum.HasFlag().
        ///
        /// To keep in loving memory of - when .NET in really previous UnityVersions didn't have this -
        /// </summary>
        public static bool HasFlag(Enum value, Enum flag)
        {
            long lValue = Convert.ToInt64(value);
            long lFlag = Convert.ToInt64(flag);
            return (lValue & lFlag) != 0;
        }	
    }
}
