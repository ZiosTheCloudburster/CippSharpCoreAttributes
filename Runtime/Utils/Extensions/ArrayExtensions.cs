//
// Author: Alessandro Salani (Cippo)
//

using System;
using System.Collections.Generic;
using System.Linq;

namespace CippSharp.Core.Attributes.Extensions
{
    internal static class ArrayExtensions 
    {
        #region Contains

        /// <summary>
        /// Similar to Any of <see> <cref>System.linq</cref> </see>
        /// it retrieve a valid index of the first element matching the predicate.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="predicate"></param>
        /// <param name="index"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool Any<T>(this List<T> list, Predicate<T> predicate, out int index)
        {
            return ArrayUtils.Any(list, predicate, out index);
        }
        
        /// <summary>
        /// Similar to Any of <see> <cref>System.linq</cref> </see>
        /// it retrieve a valid index of the first element matching the predicate.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="predicate"></param>
        /// <param name="index"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool Any<T>(this T[] array, Predicate<T> predicate, out int index)
        {
            return ArrayUtils.Any(array, predicate, out index);
        }

        #endregion
        
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

        /// <summary>
        /// Perform a foreach
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ICollection<T> ForEach<T>(this ICollection<T> collection, Action<T> action)
        {
            foreach (var element in collection)
            {
                action.Invoke(element);
            }
            
            return collection;
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
        /// Returns true if the given list is null or empty
        /// </summary>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this List<T> list)
        {
            return ArrayUtils.IsNullOrEmpty(list);
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

        /// <summary>
        /// Returns true if the given collection is null or empty
        /// </summary>
        /// <param name="collection"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this ICollection<T> collection)
        {
            return ArrayUtils.IsNullOrEmpty(collection);
        }
        
        /// <summary>
        /// Returns true if the given enumerable is null or empty
        /// </summary>
        /// <param name="enumerable"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || !enumerable.Any();
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
        
        /// <summary>
        /// Select Many If predicate. Similar to System.linq Select but with a predicate to check.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="predicate"></param>
        /// <param name="func"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="F"></typeparam>
        /// <returns></returns>
        public static IEnumerable<F> SelectManyIf<T, F>(this IEnumerable<T> array, Predicate<T> predicate, Func<T, IEnumerable<F>> func)
        {
            return ArrayUtils.SelectManyIf(array, predicate, func);
        }
        
        /// <summary>
        /// Select not null elements from an enumerable
        /// </summary>
        /// <param name="enumerable"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> SelectNotNullElements<T>(this IEnumerable<T> enumerable) where T : class
        {
            return ArrayUtils.SelectNotNullElements(enumerable);
        }

        #endregion
        
        #region Sub Array
        
        /// <summary>
        /// As substring, but for arrays.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] SubArray<T>(this T[] array, int index, int length)
        {
            return ArrayUtils.SubArray(array, index, length);
        }
        
        #endregion
    }
}
