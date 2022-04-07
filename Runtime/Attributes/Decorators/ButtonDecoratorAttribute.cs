using System;
using System.Collections.Generic;
using System.Linq;
using CippSharp.Core.Extensions;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CippSharp.Core.Attributes
{
    /// <summary>
    /// Displays GUI buttons before the property.
    /// A Decorator doesn't have any reference to the serialized property, so during on click,
    /// it tries to find the most correct case.
    ///
    /// It doesn't cover all cases like a PropertyDrawer that ensure the 100% reference.
    /// Thi is more like 80-85% yes. Don't do things like nested classes with fields and this attribute with same names...
    /// well you'll gather what you sow 
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class ButtonDecoratorAttribute : ACustomPropertyAttribute
    {
        /// <summary>
        /// Ensure a more specific pair.
        /// </summary>
        public string FieldNameOrIdentifier { get; protected set; } = string.Empty;
        
        /// <summary>
        /// Horizontal Layout of buttons
        /// </summary>
        public Dictionary<string, string> Pairs { get; protected set; } = new Dictionary<string, string>();

        /// <summary>
        /// Graphic Sytle of all buttons.
        /// </summary>
        public GUIButtonStyle GraphicStyle { get; protected set; } = GUIButtonStyle.MiniButton;

        /// <summary>
        /// Default constructor is private
        /// </summary>
        protected ButtonDecoratorAttribute()
        {
            
        }
        
        public ButtonDecoratorAttribute(string fieldName, string name, string callback, GUIButtonStyle style = GUIButtonStyle.MiniButton) : this ()
        {
            this.FieldNameOrIdentifier = fieldName;
            this.Pairs[name] = callback;
            this.GraphicStyle = style;
        }

        /// <summary>
        /// USAGE: Pair Format Template "DisplayName, Callback"
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="style"></param>
        /// <param name="pairs"></param>
        public ButtonDecoratorAttribute(string fieldName, GUIButtonStyle style, params string[] pairs) : this(fieldName, pairs)
        {
            this.GraphicStyle = style;
        }
        
        /// <summary>
        /// USAGE: Pair Format Template "DisplayName, Callback"
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="pairs"></param>
        public ButtonDecoratorAttribute(string fieldName, params string[] pairs) : this ()
        {
            this.FieldNameOrIdentifier = fieldName;
            if (!ArrayUtils.IsNullOrEmpty(pairs))
            {
                foreach (var pair in pairs)
                {
                    string[] split = pair.Split(new [] {","}, StringSplitOptions.RemoveEmptyEntries);
                    if (split.Length == 2)
                    {
                        //Display Name
                        split[0] = split[0].TrimStart(new char[] {' ',}).RemoveSpecialCharacters().TrimEnd(new char[]{' ', ','});
                        //Callback
                        split[1] = split[1].TrimStart(new char[] {' ', ','});
                        //to storage!
                        this.Pairs[split[0]] = split[1];
                    }
                }
            }
        }

        /// <summary>
        /// Used by attribute predicate in editor
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool HasSameValues(ButtonDecoratorAttribute other)
        {
            if (this.FieldNameOrIdentifier != other.FieldNameOrIdentifier)
            {
                return false;
            }
           
            if (this.GraphicStyle != other.GraphicStyle)
            {
                return false;
            }

            int count = this.Pairs.Count;
            //Avoid empty iterations
            if (count <= 0)
            {
                return false;
            }
            
            if (count != other.Pairs.Count)
            {
                return false;
            }

            var array = this.Pairs.ToArray();
            var otherArray = other.Pairs.ToArray();
            bool all = true;
            for (int i = 0; i < count; i++)
            {
                var element = array[i];
                var otherElement = otherArray[i];
                if (element.Key == otherElement.Key && element.Value == otherElement.Value)
                {
                    continue;
                }

                all = false;
                break;
            }

            return all;
        }
        
#if UNITY_EDITOR
        [CustomPropertyDrawer(typeof(ButtonDecoratorAttribute), true)]
        public class ButtonDecoratorAttributeDrawer : DecoratorDrawer
        {
            public override float GetHeight()
            {
                if (this.attribute is ButtonDecoratorAttribute buttonDecoratorAttribute)
                {
                    Dictionary<string, string> buttons = buttonDecoratorAttribute.Pairs;
                    if (buttons.IsNullOrEmpty())
                    {
                        return 0f;
                    }
                }

                return EditorGUIUtils.LineHeight;
            }

            public override void OnGUI(Rect position)
            {
                if (this.attribute is ButtonDecoratorAttribute buttonDecoratorAttribute)
                {
                    Dictionary<string, string> buttons = buttonDecoratorAttribute.Pairs;
                    if (!buttons.IsNullOrEmpty())
                    {
                        Rect smallRect = position;
                        smallRect.height = EditorGUIUtils.SingleLineHeight;
                        GUIStyle style = null;
                        if (buttonDecoratorAttribute.GraphicStyle == GUIButtonStyle.MiniButton)
                        {
                            style = EditorStyles.miniButton;
                        }

                        Rect[] rects = EditorGUIUtils.DivideSpaceHorizontal(smallRect, buttons.Count);
                        for (int i = 0; i < rects.Length; i++)
                        {
                            var r = rects[i];
                            var pair = buttons.ElementAt(i);
                            EditorGUIUtils.DrawButtonWithCallback(r, pair.Key, () => OnClickCallback(pair.Value), style);
                        }
                    }
                }
                
                base.OnGUI(position);
            }

            private void OnClickCallback(string callback)
            {
                ButtonDecoratorAttribute buttonDecoratorAttribute = (ButtonDecoratorAttribute)attribute;
                DecoratorDrawersUtils.IteratePropertiesWithAttribute<ButtonDecoratorAttribute>(AttributePredicate, CheckDelegate);
                bool AttributePredicate(ButtonDecoratorAttribute c)
                {
                    return buttonDecoratorAttribute.HasSameValues(c);
                    // && c.Pairs.ContainsValue(callback);
                }
                void CheckDelegate(Object target, SerializedObject serializedObject, SerializedProperty[] properties)
                {
                    Undo.RecordObject(target, "Before Click");
                    serializedObject.Update();
                    
                    serializedObject.Update();
                        
                    foreach (var property in properties)
                    {
//                            Debug.Log($"Checking {property.propertyPath} on {o.name}.", o);
                        SerializedPropertyUtils.TryEditLastParentLevel(property, OnLastParentLevel);
                        void OnLastParentLevel(ref object context)
                        {
                            if (ReflectionUtils.TryCallMethod(context, callback, out _, null))
                            {
//                                    Debug.Log($"Button Clicked on {o.name} at {property.propertyPath}.", o);
                            }
                        }
                    }
                        
                    serializedObject.ApplyModifiedProperties();
                }
            }
        }
#endif
    }
}
