#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
#endif

namespace CippSharp.Core.Attributes
{
    /// <summary>
    /// Disable GUI during runtime or hide the property during runtime
    /// </summary>
    public class NotEditableInPlayAttribute : ANotEditableAttribute
    {
        /// <summary>
        /// Show mode of the property. It should be not editable or hidden during runtime?
        /// </summary>
        private ShowMode Type { get; set; } = ShowMode.ReadOnly;

        public NotEditableInPlayAttribute()
        {
            
        }
        
        public NotEditableInPlayAttribute(ShowMode type) : this ()
        {
            this.Type = type;
        }

        #region Custom Editor
#if UNITY_EDITOR
        [CustomPropertyDrawer(typeof(NotEditableInPlayAttribute))]
        public class NotEditableInPlayDrawer : PropertyDrawer
        {
            public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            {
                if (attribute is NotEditableInPlayAttribute notEditableInPlayAttribute)
                {
                    bool isPlaying = Application.isPlaying;
                    if (isPlaying && notEditableInPlayAttribute.Type == ShowMode.HideInInspector)
                    {
                        return 0.0f;
                    }
                }
            
                return EditorGUIUtils.GetPropertyHeight(property, label);
            }

            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                bool isPlaying = Application.isPlaying;
                if (attribute is NotEditableInPlayAttribute notEditableInPlayAttribute)
                {
                    switch (notEditableInPlayAttribute.Type)
                    {
                        case ShowMode.HideInInspector:
                            if (!isPlaying)
                            {
                                EditorGUIUtils.DrawProperty(position, property, label);
                            } 
                            break;
                        case ShowMode.ReadOnly:
                            if (!isPlaying)
                            {
                                EditorGUIUtils.DrawProperty(position, property, label);
                            }
                            else
                            {
                                EditorGUIUtils.DrawNotEditableProperty(position, property, label);
                            }
                            break;
                    }
                }
            }

        }
#endif
        #endregion
    }
}
