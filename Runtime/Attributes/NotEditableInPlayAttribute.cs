
using CippSharp.Core.Attributes;
using UnityEngine;

#if UNITY_EDITOR
using CippSharp.Core;
using UnityEditor;
#endif

namespace CippSharp.Core
{
    public class NotEditableInPlayAttribute : ANotEditableAttribute
    {
        public ShowMode type { get; protected set; } = ShowMode.ReadOnly;

        public NotEditableInPlayAttribute()
        {
            
        }

        public NotEditableInPlayAttribute(ShowMode type) : this ()
        {
            this.type = type;
        }

#if UNITY_EDITOR
        
#endif


    }
}

#if UNITY_EDITOR
namespace CippSharpEditor.Core
{
    [CustomPropertyDrawer(typeof(NotEditableInPlayAttribute))]
    public class NotEditableInPlayDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (attribute is NotEditableInPlayAttribute notEditableInPlayAttribute)
            {
                if (Application.isPlaying && notEditableInPlayAttribute.type == ANotEditableAttribute.ShowMode.HideInInspector)
                {
                    return 0.0f;
                }
            }
            
            return EditorGUIUtils.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (attribute is NotEditableInPlayAttribute notEditableInPlayAttribute)
            {
                switch (notEditableInPlayAttribute.type)
                {
                    case ANotEditableAttribute.ShowMode.HideInInspector:
                        if (!Application.isPlaying)
                        {
                            EditorGUIUtils.DrawProperty(position, property, label);
                        } 
                        break;
                    case ANotEditableAttribute.ShowMode.ReadOnly:
                        if (!Application.isPlaying)
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
            else
            {
                if (!Application.isPlaying)
                {
                    EditorGUIUtils.DrawProperty(position, property, label);
                } 
            }
        }

    }
}
#endif
