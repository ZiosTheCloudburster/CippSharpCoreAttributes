#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
#endif

namespace CippSharp.Core.Attributes
{
    /// <summary>
    /// Disable GUI on current property.
    /// </summary>
    public class NotEditableAttribute : ANotEditableAttribute
    {
        #region Custom Editor
#if UNITY_EDITOR
        [CustomPropertyDrawer(typeof(NotEditableAttribute))]
        public class NotEditableDrawer : PropertyDrawer
        {
            public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            {
                return EditorGUIUtils.GetPropertyHeight(property, label);
            }

            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                EditorGUIUtils.DrawNotEditableProperty(position, property, label);
            }
        }
#endif
        #endregion
    }
}
