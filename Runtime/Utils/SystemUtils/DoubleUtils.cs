using UnityEngine;

namespace CippSharp.Core
{
    public static class DoubleUtils
    {
        /// <summary>
        /// Retrieve a percentage float of passed one
        /// </summary>
        /// <returns></returns>
        public static double Perc(double value, double perc)
        {
            return value * (perc / 100.0000f);
        }

//        /// <summary>
//        /// Retrieve a clamped double
//        /// </summary>
//        /// <param name="input"></param>
//        /// <param name="min"></param>
//        /// <param name="max"></param>
//        /// <returns></returns>
//        public static double Clamp(double input, double min, double max)
//        {
//            if (input < min)
//            {
//                input = min;
//            }
//
//            if (input > max)
//            {
//                input = max;
//            }
//
//            return input;
//        }
    }
}
