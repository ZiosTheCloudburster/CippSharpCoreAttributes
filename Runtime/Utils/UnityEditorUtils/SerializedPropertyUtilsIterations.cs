#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace CippSharp.Core
{
    public static partial class SerializedPropertyUtils
    {
        /// <summary>
        /// WARNING: this loses references to property path and name 
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static IEnumerable<SerializedProperty> GetChildren(this SerializedProperty property)
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
        
        /// <summary>
        /// Yes, even nested ones.
        /// 
        /// REMEMBER: if you want to save the reference to a property during the iteration you need to use <see cref="SerializedProperty.Copy"/>
        /// method. It's unity flow, follow it!
        /// </summary>
        public static void IterateAllChildren(SerializedProperty property, SerializedPropertyAction @delegate)
        {
            IEnumerator childrenEnumerator = property.GetEnumerator();
            while (childrenEnumerator.MoveNext())
            {
                SerializedProperty childProperty = childrenEnumerator.Current as SerializedProperty;
                if (childProperty == null)
                {
                    continue;
                }
                
                @delegate.Invoke(childProperty);
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
