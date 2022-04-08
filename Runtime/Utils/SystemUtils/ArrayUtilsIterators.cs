//// using System;
//
//using System;
//using System.Collections.Generic;
//using System.Linq;
//
//namespace CippSharp.Core
//{
//    /// <summary>
//    /// Hold static helpful methods for arrays.
//    /// </summary>
//    public static partial class ArrayUtils
//    {
//      
//        
////        #region For Each
//        
////        /// <summary>
////        /// Perform a foreach on an array, using System.Array.Foreach method
////        /// </summary>
////        /// <param name="array"></param>
////        /// <param name="action"></param>
////        /// <typeparam name="T"></typeparam>
////        /// <returns></returns>
////        public static void ForEach<T>(ref T[] array, Action<T> action)
////        {
////            Array.ForEach(array, action);
////        }
//
////        /// <summary>
////        /// Perform a foreach
////        /// </summary>
////        /// <param name="collection"></param>
////        /// <param name="action"></param>
////        /// <typeparam name="T"></typeparam>
////        /// <returns></returns>
////        public static ICollection<T> ForEach<T>(ICollection<T> collection, Action<T> action)
////        {
////            foreach (var element in collection)
////            {
////                action.Invoke(element);
////            }
////            
////            return collection;
////        }
//        
////        /// <summary>
////        /// Iterates an IEnumerable 
////        /// </summary>
////        /// <param name="enumeration"></param>
////        /// <param name="action"></param>
////        /// <typeparam name="T"></typeparam>
////        public static IEnumerable<T> ForEach<T>(IEnumerable<T> enumeration, Action<T> action)
////        {
////            foreach (var element in enumeration)
////            {
////                action.Invoke(element);
////            }
////
////            return enumeration;
////        }
////        
////        #endregion
//    }
//}