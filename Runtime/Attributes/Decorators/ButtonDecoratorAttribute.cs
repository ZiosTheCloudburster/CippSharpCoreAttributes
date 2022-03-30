using System;
using UnityEditor;
using UnityEngine;

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
            public override float GetHeight()
            {
                return base.GetHeight();
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
                    bool buttonClicked = Event.current.rawType == EventType.MouseDown && rect.Contains(Event.current.mousePosition);
                    if (buttonClicked)
                    {
                        Debug.Log("Button Clicked 0");
                    }
                    EditorGUIUtils.DrawButtonWithCallback(rect, buttonDecoratorAttribute.DisplayName, () => ClickCallback(rect), style);
                }
                base.OnGUI(position);
            }

            private void ClickCallback(Rect rect)
            {
                bool buttonClicked = Event.current.rawType == EventType.MouseDown && rect.Contains(Event.current.mousePosition);
                if (buttonClicked)
                {
                    Debug.Log("Button Clicked 1");
                }
            }
        }
#endif
    }
}
