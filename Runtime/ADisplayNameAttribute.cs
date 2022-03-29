using System;
using UnityEngine;
#if UNITY_EDITOR
using CippSharp.Core;
using UnityEditor;
#endif

namespace CippSharp.Core
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public abstract class ADisplayNameAttribute : PropertyAttribute
    {
        public string NewDisplayName { get; protected set; }
    }
}

namespace CippSharpEditor.Core
{
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ADisplayNameAttribute), true)]
    public class ADisplayNameAttributeDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtils.GetPropertyHeight(property, label);
        }
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (attribute is DisplayNameSuffixAttribute suffixAttribute)
            {
                label.text += suffixAttribute.NewDisplayName;
            }
            else if (attribute is ADisplayNameAttribute d)
            {
                label.text = d.NewDisplayName;
            }
            
            EditorGUIUtils.DrawProperty(position, property, label);
        }
    }
#endif
}