using System;
using System.Collections.Generic;
using System.Linq;

namespace CippSharp.Core
{
    using Debug = UnityEngine.Debug;
    
    /// <summary>
    /// Hold static helpful methods for arrays.
    /// </summary>
    public static partial class ArrayUtils
    {
        #region Add

        /// <summary>
        /// Add an element to a list only if it is new
        /// </summary>
        /// <param name="list"></param>
        /// <param name="element"></param>
        /// <typeparam name="T"></typeparam>
        public static void AddIfNew<T>(List<T> list, T element)
        {
            if (!list.Contains(element))
            {
                list.Add(element);
            }
        }

        #endregion

        #region Clear

        /// <summary>
        /// Clear not null elements from an enumerable
        /// </summary>
        /// <param name="enumerable"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [Obsolete("2021/08/14 → Use SelectNotNullElements instead. This will be removed in future versions.")]
        public static IEnumerable<T> ClearNullEntries<T>(IEnumerable<T> enumerable) where T : class
        {
            return SelectNotNullElements(enumerable);
        }

        #endregion

        #region Contains / Find
        
        /// <summary>
        /// Similar to Any of <see> <cref>System.linq</cref> </see>
        /// it retrieve a valid index of the first element matching the predicate.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="predicate"></param>
        /// <param name="index"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool Any<T>(List<T> list, Predicate<T> predicate, out int index)
        {
            index = IndexOf(list, predicate);
            return index > -1;
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
        public static bool Any<T>(T[] array, Predicate<T> predicate, out int index)
        {
            index = IndexOf(array, predicate);
            return index > -1;
        }
        
        /// <summary>
        /// Find Method
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="predicate"></param>
        /// <param name="result"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool Find<T>(ICollection<T> collection, Predicate<T> predicate, out T result)
        {
            foreach (var element in collection)
            {
                if (!predicate.Invoke(element))
                {
                    continue;
                }
                
                result = element;
                return true;
            }
            
            result = default;
            return false;
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
        public static int IndexOf<T>(T[] array, Predicate<T> predicate)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (predicate.Invoke(array[i]))
                {
                    return i;
                }
            }

            return -1;
        }
        
        /// <summary>
        /// Retrieve index if list contains an element with given predicate.
        /// Otherwise -1
        /// </summary>
        /// <param name="list"></param>
        /// <param name="predicate"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>-1 if it fails</returns>
        public static int IndexOf<T>(List<T> list, Predicate<T> predicate)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (predicate.Invoke(list[i]))
                {
                    return i;
                }
            }

            return -1;
        }
        
        /// <summary>
        /// Retrieve index if enumerable contains an element with given predicate.
        /// Otherwise -1
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="predicate"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static int IndexOf<T>(IEnumerable<T> enumerable, Predicate<T> predicate)
        {
            T[] array = enumerable.ToArray();
            return IndexOf(array, predicate);
        }

        #endregion

        #region Is Null or Empty
        
        /// <summary>
        /// Returns true if the given array is null or empty
        /// </summary>
        /// <param name="array"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(T[] array)
        {
            return array == null || array.Length < 1;
        }
        
        /// <summary>
        /// Returns true if the given list is null or empty
        /// </summary>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(List<T> list)
        {
            return list == null || list.Count < 1;
        }

        /// <summary>
        /// Returns true if the given dictionary is null or empty
        /// </summary>
        /// <param name="dictionary"></param>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <returns></returns>
        public static bool IsNullOrEmpty<K, V>(Dictionary<K, V> dictionary)
        {
            return dictionary == null || dictionary.Count < 1;
        }

        /// <summary>
        /// Returns true if the given collection is null or empty
        /// </summary>
        /// <param name="collection"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(ICollection<T> collection)
        {
            return collection == null || collection.Count < 1;
        }
        
        /// <summary>
        /// Returns true if the given enumerable is null or empty
        /// </summary>
        /// <param name="enumerable"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(IEnumerable<T> enumerable)
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
        public static bool IsValidIndex<T>(int index, T[] array)
        {
            return index >= 0 && index < array.Length;
        }
        
        /// <summary>
        /// Returns true if the given index is in the list range.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsValidIndex<T>(int index, List<T> list)
        {
            return index >= 0 && index < list.Count;
        }

        /// <summary>
        /// Returns true if the given index is in the list range.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="enumerable"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsValidIndex<T>(int index, IEnumerable<T> enumerable)
        {
            return index >= 0 && index < enumerable.Count();
        }
        
        #endregion

        #region Random Element

        /// <summary>
        /// Retrieve a random element in array.
        /// </summary>
        /// <param name="array"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T RandomElement<T>(T[] array)
        {
            int index = UnityEngine.Random.Range(0, array.Length);
            return array[index];
        }
        
        /// <summary>
        /// Retrieve a random element in list.
        /// </summary>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T RandomElement<T>(List<T> list)
        {
            int index = UnityEngine.Random.Range(0, list.Count);
            return list[index];
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
        public static void RemoveAt<T>(ref T[] array, int index)
        {
            array = array.Where((e, i) => i != index).ToArray();
        }
        
        /// <summary>
        /// Remove an element from array.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="element"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] Remove<T>(T[] array, T element)
        {
            return array.Where(e => (e.Equals(element) == false)).ToArray();
        }
        
        /// <summary>
        /// Remove from a list by predicate
        /// </summary>
        /// <param name="list"></param>
        /// <param name="predicate"></param>
        /// <typeparam name="T"></typeparam>
        public static void Remove<T>(List<T> list, Predicate<T> predicate)
        {
            int index = IndexOf(list, predicate);
            if (index > -1)
            {
                list.RemoveAt(index);
            }
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
        public static IEnumerable<F> SelectIf<T, F>(IEnumerable<T> array, Predicate<T> predicate, Func<T, F> func)
        {
            return (from element in array where predicate.Invoke(element) select func.Invoke(element));
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
        public static IEnumerable<F> SelectManyIf<T, F>(IEnumerable<T> array, Predicate<T> predicate, Func<T, IEnumerable<F>> func)
        {
            List<F> fs = new List<F>();
            foreach (var element in array)
            {
                if (predicate.Invoke(element))
                {
                    fs.AddRange(func.Invoke(element));
                }
            }
            return fs;
        }
        
        /// <summary>
        /// Select not null elements from an enumerable
        /// </summary>
        /// <param name="enumerable"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> SelectNotNullElements<T>(IEnumerable<T> enumerable) where T : class
        {
            return enumerable.Where(e => e != null);
        }

        #endregion
        
        #region Sub Array
        
        /// <summary>
        /// Same as substring, but for arrays.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] SubArray<T>(T[] array, int index, int length)
        {
            if (TrySubArray(array, index, length, out T[] subArray))
            {
                return subArray;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Try to get a subArray from an array
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <param name="subArray"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool TrySubArray<T>(T[] array, int index, int length, out T[] subArray)
        {
            try
            {
                T[] result = new T[length];
                Array.Copy(array, index, result, 0, length);
                subArray = result;
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                subArray = null;
                return false;
            }
        }
        
        #endregion

        #region Take

        /// <summary>
        /// Take until count!
        /// If there aren't enough elements only the few (less than count) are returned.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="count"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> TakeUntil<T>(IEnumerable<T> collection, int count)
        {
            return collection.Count() <= count ? collection : collection.Take(count);
        }

        #endregion
    }
}
