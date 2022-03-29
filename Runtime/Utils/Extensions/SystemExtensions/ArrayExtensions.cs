//
// Author: Alessandro Salani (Cippo)
//

using System;
using System.Collections.Generic;
using System.Linq;

namespace CippSharp.Core.Extensions
{
    public static class ArrayExtensions 
    {
        #region Add
        
        /// <summary>
        /// Add an element to a list only if it is new
        /// </summary>
        /// <param name="list"></param>
        /// <param name="element"></param>
        /// <typeparam name="T"></typeparam>
        public static void AddIfNew<T>(this List<T> list, T element)
        {
            ArrayUtils.AddIfNew(list, element);
        }
        
        #endregion
        
        #region Clear

        /// <summary>
        /// Clear null entries from an enumerable
        /// </summary>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [Obsolete("2021/08/14 → Use SelectNotNullElements instead. This will be removed in future versions.")]
        public static IEnumerable<T> ClearNullEntries<T>(this IEnumerable<T> list) where T : class
        {
            return ArrayUtils.ClearNullEntries(list);
        }

        #endregion

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
        
        #region Conversions

        /// <summary>
        /// To Dictionary from an IEnumerable of KeyValuePairs of same Types as Dictionary
        /// </summary>
        /// <param name="enumerable"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="F"></typeparam>
        /// <returns></returns>
        public static Dictionary<T, F> ToDictionary<T, F>(this IEnumerable<KeyValuePair<T, F>> enumerable)
        {
            return ArrayUtils.ToDictionary(enumerable);
        }
        
        #endregion

        #region For
          
        /// <summary>
        /// Perform a for on an array
        /// </summary>
        /// <param name="array"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static void For<T>(this T[] array, ForRefAction<T> action)
        {
            ArrayUtils.For(ref array, action);
        }

        /// <summary>
        /// Perform a for on an list
        /// </summary>
        /// <param name="list"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static void For<T>(this List<T> list, ForRefAction<T> action)
        {
            ArrayUtils.For(ref list, action);
        }

        /// <summary>
        /// Perform a foreach
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> For<T>(this IEnumerable<T> enumerable, ForRefAction<T> action)
        {
            return ArrayUtils.For(enumerable, action);
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
        
        /// <summary>
        /// Iterates an IEnumerable 
        /// </summary>
        /// <param name="enumeration"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (var element in enumeration)
            {
                action.Invoke(element);
            }

            return enumeration;
        }
        
        #endregion
        
        #region Index Of

        /// <summary>
        /// Retrieve index if array contains an element with given predicate.
        /// Otherwise -1
        /// </summary>
        /// <param name="array"></param>
        /// <param name="predicate"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>-1 if it fails</returns>
        public static int IndexOf<T>(this T[] array, Predicate<T> predicate)
        {
            return ArrayUtils.IndexOf(array, predicate);
        }
        
        /// <summary>
        /// Retrieve index if list contains an element with given predicate.
        /// Otherwise -1
        /// </summary>
        /// <param name="list"></param>
        /// <param name="predicate"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>-1 if it fails</returns>
        public static int IndexOf<T>(this List<T> list, Predicate<T> predicate)
        {
            return ArrayUtils.IndexOf(list, predicate);
        }
        
        /// <summary>
        /// Retrieve index if enumerable contains an element with given predicate.
        /// Otherwise -1
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="predicate"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static int IndexOf<T>(this IEnumerable<T> enumerable, Predicate<T> predicate)
        {
            return ArrayUtils.IndexOf(enumerable, predicate);
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
        
        #region Is Valid Index

        /// <summary>
        /// Returns true if the given index is the array range.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="array"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsValidIndex<T>(this int index, T[] array)
        {
            return ArrayUtils.IsValidIndex(index, array);
        }
        
        /// <summary>
        /// Returns true if the given index is in the list range.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsValidIndex<T>(this int index, List<T> list)
        {
            return ArrayUtils.IsValidIndex(index, list);
        }

        /// <summary>
        /// Returns true if the given index is in the list range.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="enumerable"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsValidIndex<T>(this int index, IEnumerable<T> enumerable)
        {
            return ArrayUtils.IsValidIndex(index, enumerable);
        }
        
        #endregion
        
        #region Random Element

        /// <summary>
        /// Retrieve a random element in array.
        /// </summary>
        /// <param name="array"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T RandomElement<T>(this T[] array)
        {
            return ArrayUtils.RandomElement(array);
        }
        
        /// <summary>
        /// Retrieve a random element in list.
        /// </summary>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T RandomElement<T>(this List<T> list)
        {
            return ArrayUtils.RandomElement(list);
        }

        #endregion

        #region Remove Element

        /// <summary>
        /// Remove an array element at the given index and retrieve the resulting array.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static void RemoveAt<T>(this T[] array, int index)
        {
            ArrayUtils.RemoveAt(ref array, index);
        }
        
        /// <summary>
        /// Remove an element from array.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="element"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] Remove<T>(this T[] array, T element)
        {
            return ArrayUtils.Remove(array, element);
        }
        
        /// <summary>
        /// Remove from a list by predicate
        /// </summary>
        /// <param name="list"></param>
        /// <param name="predicate"></param>
        /// <typeparam name="T"></typeparam>
        public static void Remove<T>(this List<T> list, Predicate<T> predicate)
        {
            ArrayUtils.Remove(list, predicate);
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
        
        #region Take
        
        /// <summary>
        /// Take elements Until count
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="count"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> TakeUntil<T>(this IEnumerable<T> collection, int count)
        {
            return ArrayUtils.TakeUntil(collection, count);
        }
        
        #endregion
    }
}
