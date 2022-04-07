#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
#endif

namespace CippSharp.Core.Attributes
{
    public abstract class ADisplayNameAttribute : ACustomPropertyAttribute
    {
        public enum Target : sbyte
        {
            Undefined = -1,
            Prefix = 0,
            NewDisplayName,
            Suffix,
        }
        
        public string Prefix { get; protected set; }
        public bool AddPrefix { get; protected set; }
        public string NewDisplayName { get; protected set; }
        public bool OverrideDisplayName { get; protected set; }
        public string Suffix { get; protected set; }
        public bool AddSuffix { get; protected set; }

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
                if (attribute is ADisplayNameAttribute displayNameAttribute)
                {
                    string tmp = (displayNameAttribute.OverrideDisplayName) ? displayNameAttribute.NewDisplayName : label.text;
                    if (displayNameAttribute.AddPrefix)
                    {
                        tmp = displayNameAttribute.Prefix + tmp;
                    }

                    if (displayNameAttribute.AddSuffix)
                    {
                        tmp += displayNameAttribute.Suffix;
                    }

                    label.text = tmp;
                }
                
                EditorGUIUtils.DrawProperty(position, property, label);
            }
        }
#endif
    }
}