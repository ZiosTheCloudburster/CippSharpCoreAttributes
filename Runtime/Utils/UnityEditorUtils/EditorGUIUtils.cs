//
// Author: Alessandro Salani (Cippo)
//
#if UNITY_EDITOR
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace CippSharp.Core.Attributes
{
    public static partial class EditorGUIUtils
    {
        /// <summary>
        /// Wrap of unity's default single line height.
        /// </summary>
        public static readonly float SingleLineHeight = EditorGUIUtility.singleLineHeight;
  
        /// <summary>
        /// Wrap of unity's default vertical spacing between lines.
        /// </summary>
        public static readonly float VerticalSpacing = EditorGUIUtility.standardVerticalSpacing;
  
        /// <summary>
        /// Sum of <see cref="SingleLineHeight"/> + <seealso cref="VerticalSpacing"/>.
        /// </summary>
        public static readonly float LineHeight = SingleLineHeight + VerticalSpacing;

        
        //Reflection
        private const BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy; 
        
        /// <summary>
        /// As Unity draws Property Field Internal?
        /// </summary>
        public static readonly MethodInfo PropertyFieldInternalMethodInfo = typeof(EditorGUI).GetMethod("PropertyFieldInternal", flags);
        /// <summary>
        /// As Unity draws single line Property Field
        /// </summary>
        public static readonly MethodInfo DefaultPropertyFieldMethodInfo = typeof(EditorGUI).GetMethod("DefaultPropertyField", flags);


        /// <summary>
        /// Retrieve the original rect space divided in horizontal by length.
        /// By default a space of 2 is considered between each element.
        ///
        /// Count must be >= 1
        /// </summary>
        /// <returns></returns>
        public static Rect[] DivideSpaceHorizontal(Rect position, int count, float space = 2)
        {
            if (count < 1)
            {
                return null;
            }

            if (count == 1)
            {
                return new[] {position};
            }

            Rect[] subdivisions = new Rect[count];
            float startingX = position.x;
            float totalWidth = position.width;
//            float totalSpaceBetweenElements = (count - 1) * space;
            float lastX = startingX;
            for (int i = 0; i < count; i++)
            {
                Rect rI = position;
                float elementWidth = (totalWidth / count) - space * 0.5f;
                if (i != 0)
                {
                    rI.x = lastX + space * 0.5f;
                }
                if (i == count -1)
                {
                    elementWidth += space * 0.5f;
                }
                rI.width = elementWidth;
                lastX += rI.width;
                subdivisions[i] = rI;
            }

            return subdivisions;
        }
    }
}
#endif
