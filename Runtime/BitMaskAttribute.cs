/*
 *  Author: Alessandro Salani (Cippman)
 */

using UnityEngine;
#if UNITY_EDITOR
using CippSharp.Core;
using UnityEditor;
#endif

namespace CippSharp.Core
{
    /// <summary>
    /// Class BitMaskField Attribute.
    /// </summary>
    public class BitMaskAttribute : PropertyAttribute
    {
	
    }
}

#if UNITY_EDITOR
namespace CippSharpEditor.Core
{
    /// <summary>
    /// Class EnumMaskFieldDrawer.
    /// </summary>
    [CustomPropertyDrawer(typeof(BitMaskAttribute))]
    public class BitMaskDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            property.intValue =	EditorGUI.MaskField(position, label, property.intValue, property.enumNames);
        }
    }
}
#endif
