//=============================================================================
//
// Author: F. Cucchiara
// Edit: A. Salani (Cippman)
//
//=============================================================================

using System;
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
#endif

namespace CippSharp.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public abstract class AMinMaxAttribute : ACustomPropertyAttribute
    {
        public string firstPropertyName = AttributesConstants.x;
        public string secondPropertyName = AttributesConstants.y;

        public float Min { get; protected set; } = 0.0f;
        public float Max { get; protected set; } = 1.0f;

        #region Custom Editor
#if UNITY_EDITOR
        [CustomPropertyDrawer(typeof(AMinMaxAttribute), true)]
        public class MinMaxDrawer : PropertyDrawer
        {
            /// <summary>
            /// Texts width to display numbers of slider.
            /// </summary>
            private static float TextFieldWidth = 30;
            
            public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            {
                return EditorGUIUtils.LineHeight;
            }
    
            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                AMinMaxAttribute minMaxAttribute = attribute as AMinMaxAttribute;
                if (minMaxAttribute == null)
                {
                    EditorGUI.LabelField(position, label.text, "Invalid attribute.");
                }
                else
                {
                    SerializedProperty minProperty = property.FindPropertyRelative(minMaxAttribute.firstPropertyName);
                    SerializedProperty maxProperty = property.FindPropertyRelative(minMaxAttribute.secondPropertyName);
                    if (minProperty.propertyType != SerializedPropertyType.Float || maxProperty.propertyType != SerializedPropertyType.Float)
                    {
                        EditorGUI.LabelField(position, label.text, "Invalid property.");
                    }
                    else
                    {
                        Rect labelRect = position;
                        labelRect.height = EditorGUIUtils.SingleLineHeight;
                        labelRect.width = EditorGUIUtility.labelWidth;
                        EditorGUI.LabelField(labelRect, property.displayName);
                    
                        Rect sliderRect = position;
                        sliderRect.height = EditorGUIUtils.SingleLineHeight;
                        sliderRect.x += EditorGUIUtility.labelWidth + TextFieldWidth;
                        sliderRect.width -= EditorGUIUtility.labelWidth + TextFieldWidth * 2;
                  
                        float xValue = minProperty.floatValue;
                    
                        float yValue = maxProperty.floatValue; 
                        EditorGUI.MinMaxSlider(sliderRect, ref xValue, ref yValue, minMaxAttribute.Min, minMaxAttribute.Max);
                        minProperty.floatValue = xValue;
                        maxProperty.floatValue = yValue;
                    
                        //Draw slider texts
                        Rect minValueRect = position;
                        minValueRect.x += EditorGUIUtility.labelWidth;
                        minValueRect.width = TextFieldWidth;
                        EditorGUI.LabelField(minValueRect, xValue.ToString("0.00"));
                        Rect maxValueRect = position;
                        maxValueRect.x += maxValueRect.width - TextFieldWidth;
                        maxValueRect.width = TextFieldWidth;
                        EditorGUI.LabelField(maxValueRect, yValue.ToString("0.00"));
                    }
                }
            }
        }
#endif
        #endregion
    }
}