using System.Collections.Generic;

namespace CippSharp.Core
{
    /// <summary>
    /// Hold static helpful methods for arrays.
    /// </summary>
    public static partial class ArrayUtils
    {
        #region Conversions

        /// <summary>
        /// To Dictionary from an IEnumerable of KeyValuePairs of same Types as Dictionary
        /// </summary>
        /// <param name="array"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="F"></typeparam>
        /// <returns></returns>
        public static Dictionary<T, F> ToDictionary<T, F>(IEnumerable<KeyValuePair<T, F>> array)
        {
            Dictionary<T, F> newDictionary = new Dictionary<T, F>();
            foreach (var keyValuePair in array)
            {
                newDictionary[keyValuePair.Key] = keyValuePair.Value;
            }
            return newDictionary;
        }
        
        #endregion

        #region Is Null or Empty


//        /// <summary>
//        /// Returns true if the given list asset is null or empty
//        /// </summary>
//        /// <param name="asset"></param>
//        /// <typeparam name="T"></typeparam>
//        /// <returns></returns>
//        public static bool IsNullOrEmpty<T>(AListDataAsset<T> asset)
//        {
//            return asset == null || asset.Count < 1;
//        }
//
//        /// <summary>
//        /// Returns true if the given list container is null or empty
//        /// </summary>
//        /// <param name="listContainer"></param>
//        /// <typeparam name="T"></typeparam>
//        /// <returns></returns>
//        public static bool IsNullOrEmpty<T>(ListContainer<T> listContainer)
//        {
//            return listContainer == null || listContainer.Count < 1;
//        }
//        
//        /// <summary>
//        /// Returns true if the given array container is null or empty
//        /// </summary>
//        /// <param name="arrayContainer"></param>
//        /// <typeparam name="T"></typeparam>
//        /// <returns></returns>
//        public static bool IsNullOrEmpty<T>(ArrayContainer<T> arrayContainer)
//        {
//            return arrayContainer == null || arrayContainer.Length < 1;
//        }

        #endregion
        
        // #region Iterators
        //
        //
        //
        // #endregion

        // #region Resize Array of UnityEngine.Object
        //
        // public delegate T AddObjectDelegate<T>(int newIndex) where T : Object;
        // public delegate void RemoveObjectDelegate<T>(T element) where T : Object;
        //
        // /// <summary>
        // /// Resize an array of unity engine objects by adding or destroying elements.
        // /// </summary>
        // /// <param name="list"></param>
        // /// <param name="newLength"></param>
        // /// <param name="addObjectDelegate"></param>
        // /// <param name="removeObjectDelegate"></param>
        // public static void Resize<T>(ref List<T> list, int newLength, AddObjectDelegate<T> addObjectDelegate, RemoveObjectDelegate<T> removeObjectDelegate) where T : Object
        // {
        //     int count = list.Count;
        //     if (count == newLength)
        //     {
        //         return;   
        //     }
        //     else if (count > newLength)
        //     {
        //         //remove items
        //         for (int i = count - 1; i >= newLength; i--)
        //         {
        //             T element = list[i];
        //             removeObjectDelegate.Invoke(element);
        //             list.RemoveAt(i);
        //         }
        //     }
        //     else if (count < newLength)
        //     {
        //         //add items
        //         int delta = newLength - count;
        //         for (int i = 0; i < delta; i++)
        //         {
        //             T newElement = addObjectDelegate.Invoke(count + i);
        //             list.Add(newElement);   
        //         }
        //     }
        // }
        //
        // #endregion
    }
}
