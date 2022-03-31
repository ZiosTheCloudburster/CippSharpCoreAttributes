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
        
        public ButtonDecoratorAttribute(string name, string callback, GUIButtonStyle style = GUIButtonStyle.MiniButton)
        {
            this.Pairs[name] = callback;
            this.GraphicStyle = style;
//            this.DisplayName = name;
//            this.Callback = callback;
        }

        /// <summary>
        /// USAGE: Pair Format Template "DisplayName, Callback"
        /// </summary>
        /// <param name="pairs"></param>
        public ButtonDecoratorAttribute(params string[] pairs)
        {
            if (ArrayUtils.IsNullOrEmpty(pairs))
            {
                return;
            }

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
        
#if UNITY_EDITOR
        [CustomPropertyDrawer(typeof(ButtonDecoratorAttribute), true)]
        public class ButtonDecoratorAttributeDrawer : DecoratorDrawer
        {
//            readonly List<Object> potentialTargets = new List<Object>();
            
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
                            EditorGUIUtils.DrawButtonWithCallback(r, pair.Key, () => ClickCallback(pair.Value), style);
                        }
                    }
                }
                
                base.OnGUI(position);
            }

            private void ClickCallback(string callback)
            {
//                potentialTargets.Clear();
                Debug.Log($"Clicked {callback} button.");

                SerializedObjectUtils.GetActiveEditorTargetsObjectsPairs().ForEach(FilterAndInvokeCallback);
                void FilterAndInvokeCallback(KeyValuePair<Editor, Object[]> pair)
                {
                    foreach (var o in pair.Value)
                    {
                        SerializedObject serializedObject = new SerializedObject(o);
                            
//                        SerializedPropertyUtils.GetAllPropertiesOfType(serializedObject, SerializedPropertyType.Generic);
                    }
                }
              
                Debug.Log($"All targets count {allTargets.Count()}.");
//                TODO: this filters only behaviours and not scriptables or properties
                int filteredTargets = 0;
                foreach (Object o in allTargets)
                {
                    object obj = o;
                    if (ReflectionUtils.HasMember(obj, callback, out MemberInfo member))
                    {
                        filteredTargets++;
                        //TODO: Relative serializedObject update
                        ReflectionUtils.TryCallMethod(obj, callback, out _, null);
                        //TODO: Relative serializedObject apply
                    }
                }
                
                Debug.Log($"Button Clicked on {filteredTargets} targets.");
            }
        }
#endif
    }
}
