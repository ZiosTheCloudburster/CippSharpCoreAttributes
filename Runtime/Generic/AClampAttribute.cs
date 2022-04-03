#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
#endif

namespace CippSharp.Core.Attributes
{
    /// <summary>
    /// Well if you want the 'slider' use the <see cref="RangeAttribute"/> of unity!
    /// But if you only need to clamp a value without having 'range' from one side...
    /// </summary>
    public abstract class AClampAttribute : AFieldAttribute
    {
        public int IntegerMinValue { get; protected set; } = 0;
        public float FloatMinValue { get; protected set; } = 0;
        
        public int IntegerMaxValue { get; protected set; } = 1;
        public float FloatMaxValue { get; protected set; } = 1;

        #region Custom Editor
#if UNITY_EDITOR
        [CustomPropertyDrawer(typeof(AClampAttribute), true)]
        public class AClampAttributeDrawer : PropertyDrawer
        {
            public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            {
                return EditorGUIUtils.GetPropertyHeight(property, label);
            }
    
            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                if (!(property.propertyType == SerializedPropertyType.Integer || property.propertyType == SerializedPropertyType.Float))
                {
                    EditorGUI.LabelField(position, label.text, $"{nameof(ClampValueAttribute)} works with integer and float properties!");
                }
                else
                {
                    if (attribute is AClampAttribute clampAttribute)
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
                    else
                    {
                        EditorGUI.LabelField(position, label.text, "Invalid attribute.");
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
#endif
        #endregion
    }
}
