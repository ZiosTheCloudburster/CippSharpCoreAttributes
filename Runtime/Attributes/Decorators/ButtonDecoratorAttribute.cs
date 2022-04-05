using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Thi is more like 80-85% yes. 
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class ButtonDecoratorAttribute : AFieldAttribute
    {
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
        
        public ButtonDecoratorAttribute(string name, string callback, GUIButtonStyle style = GUIButtonStyle.MiniButton) : this ()
        {
            this.Pairs[name] = callback;
            this.GraphicStyle = style;
        }

        /// <summary>
        /// USAGE: Pair Format Template "DisplayName, Callback"
        /// </summary>
        /// <param name="style"></param>
        /// <param name="pairs"></param>
        public ButtonDecoratorAttribute(GUIButtonStyle style, params string[] pairs) : this(pairs)
        {
            this.GraphicStyle = style;
        }

        /// <summary>
        /// USAGE: Pair Format Template "DisplayName, Callback"
        /// </summary>
        /// <param name="pairs"></param>
        public ButtonDecoratorAttribute(params string[] pairs) : this ()
        {
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
                DecoratorDrawersUtils.IteratePropertiesWithAttribute<ButtonDecoratorAttribute>(AttributePredicate, CheckDelegate);
                bool AttributePredicate(ButtonDecoratorAttribute c)
                {
                    return c.Pairs.ContainsValue(callback);
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

//                SerializedObjectUtils.GetActiveEditorTargetsObjectsPairs().ForEach(FilterAndInvokeCallback);
//                void FilterAndInvokeCallback(KeyValuePair<Editor, Object[]> pair)
//                {
//                    foreach (var o in pair.Value)
//                    {
//                        SerializedObject serializedObject = new SerializedObject(o);
//                        SerializedProperty[] propertiesWithAttribute = GetPropertiesWithAttribute(serializedObject, callback);
//                        if (ArrayUtils.IsNullOrEmpty(propertiesWithAttribute))
//                        {
//                            continue;
//                        }
//
//                        Undo.RecordObject(o, "Before Click");
//                        serializedObject.Update();
//                        
//                        foreach (var property in propertiesWithAttribute)
//                        {
////                            Debug.Log($"Checking {property.propertyPath} on {o.name}.", o);
//                            SerializedPropertyUtils.TryEditLastParentLevel(property, OnLastParentLevel);
//                            void OnLastParentLevel(ref object context)
//                            {
//                                if (ReflectionUtils.TryCallMethod(context, callback, out _, null))
//                                {
////                                    Debug.Log($"Button Clicked on {o.name} at {property.propertyPath}.", o);
//                                }
//                            }
//                        }
//                        
//                        serializedObject.ApplyModifiedProperties();
//                    }
//                }
            }
            

            /// <summary>
            /// Retrieve all properties with <see cref="ButtonDecoratorAttribute"/>
            /// with match at least one of the callbacks.
            /// </summary>
            /// <param name="serializedObject"></param>
            /// <param name="callback"></param>
            /// <returns></returns>
            private static SerializedProperty[] GetPropertiesWithAttribute(SerializedObject serializedObject, string callback)
            {
                List<SerializedProperty> propertiesWithAttribute = new List<SerializedProperty>();
                SerializedProperty[] allProperties = SerializedPropertyUtils.GetAllProperties(serializedObject);
                foreach (var property in allProperties)
                {
                    if (HasAttribute(property, callback))
                    {
                        propertiesWithAttribute.Add(property);
                    }

                    if (property.propertyType == SerializedPropertyType.Generic && (property.isExpanded && property.hasChildren))
                    {
                        TrySearchInChildren(property, callback, ref propertiesWithAttribute);
                    }
                }
                
                return propertiesWithAttribute.ToArray();
            }

            /// <summary>
            /// Iterate children properties recursively, but avoid not expanded or properties with no children
            /// </summary>
            /// <param name="property"></param>
            /// <param name="callback"></param>
            /// <param name="propertiesWithAttribute"></param>
            private static void TrySearchInChildren(SerializedProperty property, string callback, ref List<SerializedProperty> propertiesWithAttribute)
            {
                List<SerializedProperty> tmpElementsWithAttribute = new List<SerializedProperty>();
                SerializedPropertyUtils.IterateAllChildren(property, OnIterate);
                void OnIterate(SerializedProperty childProperty)
                {
                    if (HasAttribute(childProperty, callback))
                    {
                        //You MUST use child property.Copy() to save the iteration in current state.
                        tmpElementsWithAttribute.Add(childProperty.Copy());
                    }
                }
                propertiesWithAttribute.AddRange(tmpElementsWithAttribute);
            }

            /// <summary>
            /// Has Attribute?
            /// </summary>
            /// <param name="property"></param>
            /// <param name="callback"></param>
            /// <returns></returns>
            private static bool HasAttribute(SerializedProperty property, string callback)
            {
                try
                {
                    FieldInfo fieldInfo = MirroredScriptAttributeUtility.GetFieldInfoFromProperty(property, out Type type);
                    List<PropertyAttribute> attributes = MirroredScriptAttributeUtility.GetPropertyAttributes(fieldInfo);
                    return !ArrayUtils.IsNullOrEmpty(attributes) && attributes.Any(a => a is ButtonDecoratorAttribute buttonDecoratorAttribute && buttonDecoratorAttribute.Pairs.ContainsValue(callback));
                }
                catch
                {
                    //Ignored
                    return false;
                }
            }
        }
#endif
    }
}
