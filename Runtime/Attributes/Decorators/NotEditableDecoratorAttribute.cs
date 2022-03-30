using System;
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
#endif

namespace CippSharp.Core.Attributes
{
    /// <summary>
    /// Disable GUI on first Line of current property
    /// (useful if the serialized property is single line height)
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class NotEditableDecoratorAttribute : ANotEditableAttribute
    {
#if UNITY_EDITOR
        [CustomPropertyDrawer(typeof(NotEditableDecoratorAttribute), true)]
        public class NotEditableDecoratorAttributeDrawer : DecoratorDrawer
        {
            public override float GetHeight()
            {
                return 0;
            }

            public override void OnGUI(Rect position)
            {
                bool guiStatus = GUI.enabled;
                GUI.enabled = false;
                base.OnGUI(position);
            }
        }
#endif
    }
}
