/*
 *  Author: Alessandro Salani (Cippman)
 */

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
#endif

namespace CippSharp.Core.Attributes
{
    /// <summary>
    /// Class BitMaskField Attribute.
    /// </summary>
    public class BitMaskAttribute : AFieldAttribute
    {
        #region Custom Editor
#if UNITY_EDITOR
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
                if (property.propertyType == SerializedPropertyType.Enum)
                {
                    property.intValue = EditorGUI.MaskField(position, label, property.intValue, property.enumNames);
                }
                else
                {
                    EditorGUI.LabelField(position, label.text, $"{nameof(BitMaskAttribute)} works only with enums.");
                }
            }
        }
#endif
        #endregion
    }
}