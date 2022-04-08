namespace CippSharp.Core.Attributes.Extensions
{
    public static class IntegerExtensions
    {
        /// <summary>
        /// Retrieve if this integer is odd
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static bool IsOdd(this int i)
        {
            return IntUtils.IsOdd(i);
        }
    }
}
