#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
#endif

namespace CippSharp.Core.Attributes
{
    /// <summary>
    /// A decorator draws only the first 
    /// </summary>
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
