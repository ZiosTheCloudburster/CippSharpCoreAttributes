//using System.Collections.Generic;
//
//namespace CippSharp.Core
//{
//    /// <summary>
//    /// Hold static helpful methods for arrays.
//    /// </summary>
//    public static partial class ArrayUtils
//    {
//        #region Conversions
//
//        /// <summary>
//        /// To Dictionary from an IEnumerable of KeyValuePairs of same Types as Dictionary
//        /// </summary>
//        /// <param name="array"></param>
//        /// <typeparam name="T"></typeparam>
//        /// <typeparam name="F"></typeparam>
//        /// <returns></returns>
//        public static Dictionary<T, F> ToDictionary<T, F>(IEnumerable<KeyValuePair<T, F>> array)
//        {
//            Dictionary<T, F> newDictionary = new Dictionary<T, F>();
//            foreach (var keyValuePair in array)
//            {
//                newDictionary[keyValuePair.Key] = keyValuePair.Value;
//            }
//            return newDictionary;
//        }
//        
//        #endregion
//        
//    }
//}
