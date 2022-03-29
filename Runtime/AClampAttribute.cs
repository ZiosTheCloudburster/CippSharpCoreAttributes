using System;
using UnityEngine;
#if UNITY_EDITOR
using CippSharp.Core;
using UnityEditor;
#endif

namespace CippSharp.Core
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public abstract class AClampAttribute : PropertyAttribute
    {
        public int IntegerMinValue { get; protected set; } = 0;
        public float FloatMinValue { get; protected set; } = 0;
        
        public int IntegerMaxValue { get; protected set; } = 1;
        public float FloatMaxValue { get; protected set; } = 1;
    }
}

#if UNITY_EDITOR
namespace CippSharpEditor.Core
{
    [CustomPropertyDrawer(typeof(AClampAttribute), true)]
    public class AClampAttributeDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtils.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            AClampAttribute clampAttribute = attribute as AClampAttribute;
            if (clampAttribute == null)
            {
                EditorGUI.LabelField(position, label.text, "Invalid attribute.");
            }
            else
            {
                EditorGUIUtils.DrawProperty(position, property, label);

                if (MustClampMinValue(clampAttribute))
                {
                    if (property.propertyType == SerializedPropertyType.Integer)
                    {
                        if (property.intValue < clampAttribute.IntegerMinValue)
                        {
                            property.intValue = clampAttribute.IntegerMinValue;
                        }
                    }
                    else if (property.propertyType == SerializedPropertyType.Float)
                    {
                        if (property.floatValue < clampAttribute.FloatMinValue)
                        {
                            property.floatValue = clampAttribute.FloatMinValue;
                        }
                    }
                }

                if (MustClampMaxValue(clampAttribute))
                {
                    if (property.propertyType == SerializedPropertyType.Integer)
                    {
                        if (property.intValue > clampAttribute.IntegerMaxValue)
                        {
                            property.intValue = clampAttribute.IntegerMaxValue;
                        }
                    }
                    else if (property.propertyType == SerializedPropertyType.Float)
                    {
                        if (property.floatValue > clampAttribute.FloatMaxValue)
                        {
                            property.floatValue = clampAttribute.FloatMaxValue;
                        }
                    }
                }
            }
        }

        private bool MustClampMinValue(AClampAttribute clampAttribute)
        {
            return !(clampAttribute is ClampMaxValueAttribute);
        }

        private bool MustClampMaxValue(AClampAttribute clampAttribute)
        {
            return !(clampAttribute is ClampMinValueAttribute);
        }       
    }
}
#endif
