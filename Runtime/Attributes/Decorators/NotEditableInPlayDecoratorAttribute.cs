#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
#endif

namespace CippSharp.Core.Attributes
{
    /// <summary>
    /// Disable GUI during runtime on first Line of current property
    /// (useful if the serialized property is single line height)
    /// </summary>
    public class NotEditableInPlayDecoratorAttribute : NotEditableDecoratorAttribute
    {
        #region Custom Editor
#if UNITY_EDITOR
        [CustomPropertyDrawer(typeof(NotEditableInPlayDecoratorAttribute), true)]
        public class NotEditableInPlayDecoratorAttributeDrawer : DecoratorDrawer
        {
            public override float GetHeight()
            {
                return 0;
            }

            public override void OnGUI(Rect position)
            {
                bool isPlaying = Application.isPlaying;
                bool guiStatus = GUI.enabled;
                GUI.enabled = !isPlaying;
                base.OnGUI(position);
            }
        }
#endif
        #endregion
    }
}
