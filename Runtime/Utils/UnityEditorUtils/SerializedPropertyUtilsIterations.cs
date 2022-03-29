#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;

namespace CippSharp.Core
{
    public static partial class SerializedPropertyUtils
    {
        /// <summary>
        /// Get children of property
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static IEnumerable<SerializedProperty> GetChildren(SerializedProperty property)
        {
            property = property.Copy();
            var nextElement = property.Copy();
            bool hasNextElement = nextElement.NextVisible(false);
            if (!hasNextElement)
            {
                nextElement = null;
            }
 
            property.NextVisible(true);
            while (true)
            {
                if ((SerializedProperty.EqualContents(property, nextElement)))
                {
                    yield break;
                }
 
                yield return property;
 
                bool hasNext = property.NextVisible(false);
                if (!hasNext)
                {
                    break;
                }
            }
        }
        
        #region Iterations
        
        /// <summary>
        /// Invokes a callback during a for iteration of a serialized property array.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="callback"></param>
        public static void For(SerializedProperty[] array, ForSerializedPropertyAction callback)
        {
            for (int i = 0; i < array.Length; i++)
            {
                callback.Invoke(array[i], i);
            }
        }
        
        #endregion
    }
}
#endif
