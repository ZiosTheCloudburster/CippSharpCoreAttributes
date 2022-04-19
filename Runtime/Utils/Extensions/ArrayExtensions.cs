//
// Author: Alessandro Salani (Cippo)
//

using System;
using System.Collections.Generic;

namespace CippSharp.Core.Attributes.Extensions
{
    internal static class ArrayExtensions 
    {    
        #region For Each
        
        /// <summary>
        /// Perform a foreach on an array, using System.Array.Foreach method
        /// </summary>
        /// <param name="array"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static void ForEach<T>(this T[] array, Action<T> action)
        {
            Array.ForEach(array, action);
        }

        #endregion
        
        #region Is Null or Empty
        
        /// <summary>
        /// Returns true if the given array is null or empty
        /// </summary>
        /// <param name="array"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this T[] array)
        {
            return ArrayUtils.IsNullOrEmpty(array);
        }
        
        /// <summary>
        /// Returns true if the given dictionary is null or empty
        /// </summary>
        /// <param name="dictionary"></param>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <returns></returns>
        public static bool IsNullOrEmpty<K, V>(this Dictionary<K, V> dictionary)
        {
            return ArrayUtils.IsNullOrEmpty(dictionary);
        }
        
        #endregion
        
        #region Select

        /// <summary>
        /// Select If util. Similar to System.linq Select but with a predicate to check.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="predicate"></param>
        /// <param name="func"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="F"></typeparam>
        /// <returns></returns>
        public static IEnumerable<F> SelectIf<T, F>(this IEnumerable<T> array, Predicate<T> predicate, Func<T, F> func)
        {
            return ArrayUtils.SelectIf(array, predicate, func);
        }

        #endregion
    }
}
