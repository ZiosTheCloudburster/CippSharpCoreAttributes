using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CippSharp.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class ButtonDecoratorAttribute : AFieldAttribute
    {
        public string DisplayName { get; protected set; }
        public string Callback { get; protected set; }
        public GUIButtonStyle GraphicStyle { get; protected set; }
        
        public ButtonDecoratorAttribute(string name, string callback)
        {
            this.DisplayName = name;
            this.Callback = callback;
        }
#if UNITY_EDITOR
        [CustomPropertyDrawer(typeof(ButtonDecoratorAttribute), true)]
        public class ButtonDecoratorAttributeDrawer : DecoratorDrawer
        {
//            readonly List<Object> potentialTargets = new List<Object>();
            
            public override float GetHeight()
            {
                return EditorGUIUtils.LineHeight;
            }

            public override void OnGUI(Rect position)
            {
                if (this.attribute is ButtonDecoratorAttribute buttonDecoratorAttribute)
                {
                    Rect rect = position;
                    rect.height = EditorGUIUtils.SingleLineHeight;
                    GUIStyle style = null;
                    if (buttonDecoratorAttribute.GraphicStyle == GUIButtonStyle.MiniButton)
                    {
                        style = EditorStyles.miniButton;
                    }
//                    bool buttonClicked = Event.current.rawType == EventType.MouseDown && rect.Contains(Event.current.mousePosition);
//                    if (buttonClicked)
//                    {
//                        
//                    }
                    EditorGUIUtils.DrawButtonWithCallback(rect, buttonDecoratorAttribute.DisplayName, () => ClickCallback(buttonDecoratorAttribute.Callback), style);
                }
                
                base.OnGUI(position);
            }

            private void ClickCallback(string callback)
            {
//                potentialTargets.Clear();
              
                Editor[] editors = ActiveEditorTracker.sharedTracker.activeEditors;
                IEnumerable<Object> allTargets = editors.SelectMany(e => SerializedObjectUtils.GetTargetObjects(e.serializedObject));
               
                //TODO: this filters only behaviours and not scriptables or properties
                foreach (Object o in allTargets)
                {
                    object obj = o;
                    if (ReflectionUtils.HasMember(o, callback, out MemberInfo member))
                    {
                        //TODO: Relative serializedObject update
                        ReflectionUtils.TryCallMethod(o, callback, out _, null);
                        //TODO: Relative serializedObject apply
                    }
                }
                
                Debug.Log($"Button Clicked.");
            }
        }
#endif
    }
}
