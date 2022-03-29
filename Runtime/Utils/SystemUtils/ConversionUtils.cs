
namespace CippSharp.Core
{
    public static class ConversionUtils
    {
        /// <summary>
        /// 1 m/s = 3.6 km/h
        /// </summary>
        public const double MsToKmh = 3.6f; 
        
        /// <summary>
        /// 1 km/h = 0.6213711922 mi/h
        /// </summary>
        public const double KmhToMph = 0.62137119223733396961743418436332d;
        
        /// <summary>
        /// 1 mi/h = 1.609344 km/h
        /// </summary>
        public const double MphToKmh = 1.609344d;

        /// <summary>
        /// 1 m/ss = 12960.000004562 km/hh
        /// </summary>
        public const double MssToKmhh = 12960.000004562d;
        
        
        /// <summary>
        /// Converts a float from m/s to km/h
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static float FromMStoKMH(float input)
        {
            return input * (float)MsToKmh;
        }

        /// <summary>
        /// Converts a float from m/s to km/h
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static float FromKMHToMS(float input)
        {
            return input / (float)MsToKmh;
        }

        /// <summary>
        /// Converts a float from m/s^2 to km/h^2
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static float FromMSStoKMHH(float input)
        {
            return (float)((double)input * MssToKmhh);
        }

        /// <summary>
        /// Converts a float from m/s^2 to km/h^2
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static float FromKMHHToMSS(float input)
        {
            return (float)((double)input / MssToKmhh);
        }
    }
}
