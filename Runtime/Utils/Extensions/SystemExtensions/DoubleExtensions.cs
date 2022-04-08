namespace CippSharp.Core.Attributes.Extensions
{
    public static class DoubleExtensions
    {
        /// <summary>
        /// Retrieve a percentage float of passed one
        /// </summary>
        /// <returns></returns>
        public static double Perc(this double value, double perc)
        {
            return DoubleUtils.Perc(value, perc);
        }
    }
}
