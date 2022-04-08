// using System;

using System;
using System.Collections.Generic;
using System.Linq;

namespace CippSharp.Core
{
    /// <summary>
    /// Hold static helpful methods for arrays.
    /// </summary>
    public static partial class ArrayUtils
    {
        #region For
          
        /// <summary>
        /// Perform a for on an array
        /// </summary>
        /// <param name="array"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static void For<T>(ref T[] array, ForRefAction<T> action)
        {
            for (int i = 0; i < array.Length; i++)
            {
                action.Invoke(ref array[i], i);
            }
        }

        /// <summary>
        /// Perform a for on an list
        /// </summary>
        /// <param name="list"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static void For<T>(ref List<T> list, ForRefAction<T> action)
        {
            for (int i = 0; i < list.Count; i++)
            {
                T element = list[i];
                action.Invoke(ref element, i);
                list[i] = element;
            }
        }

        /// <summary>
        /// Perform a foreach
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> For<T>(IEnumerable<T> enumerable, ForRefAction<T> action)
        {
            T[] array = enumerable.ToArray();
            For(ref array, action);
            return array;
        }

        #endregion
        
//        #region For Each
        
//        /// <summary>
//        /// Perform a foreach on an array, using System.Array.Foreach method
//        /// </summary>
//        /// <param name="array"></param>
//        /// <param name="action"></param>
//        /// <typeparam name="T"></typeparam>
//        /// <returns></returns>
//        public static void ForEach<T>(ref T[] array, Action<T> action)
//        {
//            Array.ForEach(array, action);
//        }

//        /// <summary>
//        /// Perform a foreach
//        /// </summary>
//        /// <param name="collection"></param>
//        /// <param name="action"></param>
//        /// <typeparam name="T"></typeparam>
//        /// <returns></returns>
//        public static ICollection<T> ForEach<T>(ICollection<T> collection, Action<T> action)
//        {
//            foreach (var element in collection)
//            {
//                action.Invoke(element);
//            }
//            
//            return collection;
//        }
        
//        /// <summary>
//        /// Iterates an IEnumerable 
//        /// </summary>
//        /// <param name="enumeration"></param>
//        /// <param name="action"></param>
//        /// <typeparam name="T"></typeparam>
//        public static IEnumerable<T> ForEach<T>(IEnumerable<T> enumeration, Action<T> action)
//        {
//            foreach (var element in enumeration)
//            {
//                action.Invoke(element);
//            }
//
//            return enumeration;
//        }
//        
//        #endregion
    }
}