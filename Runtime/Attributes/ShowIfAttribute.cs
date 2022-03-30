
#if UNITY_EDITOR
using System;
using System.Reflection;
using UnityEngine;
using UnityEditor;
#endif

namespace CippSharp.Core.Attributes
{
    /// <summary>
    /// Enable GUI on current property IF the condition is matched. 
    /// </summary>
    public class ShowIfAttribute : ANotEditableAttribute
    {
        /// <summary>
        /// The field, the property, the callback with bool return type.  
        /// </summary>
        public string Condition { get; protected set; } = string.Empty;
        /// <summary>
        /// The match of the condition
        /// </summary>
        public bool ConditionMatch { get; protected set; } = true;
        
        /// <summary>
        /// How to not show the property while the condition is not matched?
        /// </summary>
        public ShowMode HideMode { get; protected set; } = ShowMode.HideInInspector;

        public ShowIfAttribute()
        {
            
        }
        
        public ShowIfAttribute(string condition, bool conditionMatch = true, ShowMode showMode = ShowMode.HideInInspector)
        {
            this.Condition = condition;
            this.ConditionMatch = conditionMatch;
            this.HideMode = showMode;
        }

        #region Custom Editor
#if UNITY_EDITOR
        [CustomPropertyDrawer(typeof(ShowIfAttribute))]
        public class ShowIfDrawer : PropertyDrawer
        {
            public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            {
                if (attribute is ShowIfAttribute showIfAttribute)
                {
                    if (ShouldDisplayProperty(showIfAttribute, property))
                    {
                        return EditorGUIUtils.GetPropertyHeight(property, label);
                    }
                    else
                    {
                        if (showIfAttribute.HideMode == ShowMode.ReadOnly)
                        {
                            return EditorGUIUtils.GetPropertyHeight(property, label);
                        }
                        else
                        {
                            return 0.0f;
                        }
                    }
                }
                else
                {
                    return 0.0f;
                }
            }

            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                if (attribute is ShowIfAttribute showIfAttribute)
                {
                    if (ShouldDisplayProperty(showIfAttribute, property))
                    {
                        EditorGUIUtils.DrawProperty(position, property, label);
                    }
                    else
                    {
                        if (showIfAttribute.HideMode == ShowMode.ReadOnly)
                        {
                            EditorGUIUtils.DrawNotEditableProperty(position, property, label);
                        }
                    }
                }
//                else
//                {
//                    Rect labelRect = position;
//                    labelRect.height = EditorGUIUtils.LineHeight;
//                    EditorGUI.LabelField(labelRect, label.text, "Invalid attribute.");
//                }
            }

            private bool ShouldDisplayProperty(ShowIfAttribute showIfAttribute, SerializedProperty property)
            {
                if (string.IsNullOrEmpty(showIfAttribute.Condition))
                {
                    return showIfAttribute.ConditionMatch;
                }

                try
                {
                    if (SerializedPropertyUtils.TryGetParentLevel(property, out object contextLevel))
                    {
                        //last parent of this property as object value
                        if (ReflectionUtils.HasMember(contextLevel, showIfAttribute.Condition, out MemberInfo member))
                        {
                            if (ReflectionUtils.TryGetMemberValue<bool>(contextLevel, member, out bool b))
                            {
                                return showIfAttribute.ConditionMatch == b;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError($"Failed to check condition: {e.Message}.");
                    return false;
                }

                return false;
            }
        }
#endif
        #endregion
    }
}