#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
using UnityEngine;
#endif

namespace CippSharp.Core.Attributes
{
    using Color = UnityEngine.Color;
    
    /// <summary>
    /// Highlight current field / single line with target color 
    /// </summary>
    public class BeginColorDecoratorAttribute : ACustomPropertyAttribute
    {
        public string FieldNameOrIdentifier { get; protected set; } = string.Empty;
        /// <summary>
        /// Color property member of the class
        /// </summary>
        public string ColorProperty { get; protected set; } = string.Empty;
        public bool HasColorProperty => !string.IsNullOrEmpty(ColorProperty);
        public Color NewGUIColor { get; protected set; } = Color.white;

        protected BeginColorDecoratorAttribute()
        {
            
        }

        /// <summary>
        /// This requires 'a hook' as fieldName
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="colorProperty"></param>
        public BeginColorDecoratorAttribute(string fieldName, string colorProperty) : this()
        {
            this.FieldNameOrIdentifier = fieldName;
            this.ColorProperty = colorProperty;
        }

        /// <summary>
        /// Color from HSV
        /// </summary>
        /// <param name="h"></param>
        /// <param name="s"></param>
        /// <param name="v"></param>
        public BeginColorDecoratorAttribute(float h, float s, float v)
        {
            this.NewGUIColor = Color.HSVToRGB(h, s, v);
        }

        /// <summary>
        /// Color classic rgba
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="a"></param>
        public BeginColorDecoratorAttribute(float r, float g, float b, float a)
        {
            this.NewGUIColor = new Color(r, g, b, a);
        }

        /// <summary>
        /// Has Equal values
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        internal bool HasEqualValues(BeginColorDecoratorAttribute other)
        {
            if (this.FieldNameOrIdentifier != other.FieldNameOrIdentifier)
            {
                return false;
            }

            if (this.ColorProperty != other.ColorProperty)
            {
                return false;
            }

            if (this.NewGUIColor != other.NewGUIColor)
            {
                return false;
            }

            return true;
        }

        #region Custom Editor
#if UNITY_EDITOR
        [CustomPropertyDrawer(typeof(BeginColorDecoratorAttribute), true)]
        public class HighlighterDecoratorAttributeDrawer : DecoratorDrawer
        {
            protected static bool GUIColorAffected = false;
            protected static Color OriginalGUIColor = Color.white;
            
            public override float GetHeight()
            {
                return 0;
            }

            public override void OnGUI(Rect position)
            {
                if (attribute is BeginColorDecoratorAttribute highlighterDecoratorAttribute)
                {
                    if (!GUIColorAffected)
                    {
                        GUIColorAffected = true;
                        OriginalGUIColor = GUI.color;
                    }
                    GUI.color = GetColor(highlighterDecoratorAttribute);
                }
            }

            private Color GetColor(BeginColorDecoratorAttribute beginColorDecoratorAttribute)
            {
                if (!beginColorDecoratorAttribute.HasColorProperty)
                {
                    return beginColorDecoratorAttribute.NewGUIColor;
                }
                else
                {
                    SerializedProperty property = DecoratorDrawersUtils
                        .GetPropertiesWithAttribute<BeginColorDecoratorAttribute>(AttributePredicate).FirstOrDefault();

                    bool AttributePredicate(BeginColorDecoratorAttribute c)
                    {
                        return beginColorDecoratorAttribute.HasEqualValues(c);
                    }

                    if (property != null)
                    {
                        if (SerializedPropertyUtils.TryGetParentLevel(property, out object context))
                        {
                            if (ReflectionUtils.TryGetMemberValue<Color>(context, beginColorDecoratorAttribute.ColorProperty, out Color tmpColor))
                            {
                                return tmpColor;
                            }
                        }
                    }
                }

                return Color.clear;
            }
        }
#endif
        #endregion
        
    }
}
