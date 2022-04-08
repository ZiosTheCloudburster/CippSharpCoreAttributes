/*
 *  Author: Alessandro Salani (Cippman)
 */

#if UNITY_EDITOR
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
#endif

namespace CippSharp.Core.Attributes
{
    public abstract class AHelpBoxAttribute : ACustomPropertyAttribute
    {
        public enum ShowOptions : byte
        {
            Before = 0,
            After = 1,
            HelpBoxOnly = 2,
        }
        
        public bool IsReflectedMessage { get; protected set; } = false;
        public string Message { get; protected set; } = string.Empty;
        public ShowOptions Show { get; protected set; } = ShowOptions.After;
        public MessageType Type { get; protected set; } = MessageType.Info;

        //HelpBox If
        public bool UseCondition { get; protected set; } = false;
        public string Condition { get; protected set; } = string.Empty;

        #region Custom Editor
#if UNITY_EDITOR
        [CustomPropertyDrawer(typeof(AHelpBoxAttribute), true)]
        public class HelpBoxDrawer : PropertyDrawer
        {
            private static BindingFlags flags = ReflectionUtils.Common | BindingFlags.FlattenHierarchy;
            private bool SingleLine = false;

            public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            {
                if (SingleLine)
                {
                    return EditorGUIUtils.LineHeight;
                }

                AHelpBoxAttribute helpBoxAttribute = attribute as AHelpBoxAttribute;
                string message = GetAttributeMessage(helpBoxAttribute, property);
                float AttributeHeight = GetAttributeHeight(message);
                if (ShowAttributeOnly(helpBoxAttribute))
                {
                    return Mathf.Max(EditorGUIUtils.LineHeight, AttributeHeight);
                }
                else
                {
                    if (ShouldDisplayAttribute(helpBoxAttribute, property))
                    {
                        return EditorGUIUtils.GetPropertyHeight(property, label) + AttributeHeight;
                    }
                    else
                    {
                        return EditorGUIUtils.GetPropertyHeight(property, label);
                    }
                }
            }

            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                SingleLine = false;

                AHelpBoxAttribute helpBoxAttribute = attribute as AHelpBoxAttribute;
                if (helpBoxAttribute == null)
                {
                    SingleLine = true;

                    Rect labelRect = position;
                    labelRect.height = EditorGUIUtils.LineHeight;
                    EditorGUI.LabelField(labelRect, label.text, "Invalid attribute.");
                }
                else
                {
                    Rect propertyRect = position;
                    Rect helpBoxRect = propertyRect;

                    string message = GetAttributeMessage(helpBoxAttribute, property);
                    helpBoxRect.height = GetAttributeHeight(message);

                    UnityEditor.MessageType messageType = (UnityEditor.MessageType) ((int) helpBoxAttribute.Type);
                    ShowOptions helpBoxPosition = helpBoxAttribute.Show;

                    if (ShouldDisplayAttribute(helpBoxAttribute, property))
                    {
                        if (helpBoxPosition == ShowOptions.HelpBoxOnly)
                        {
                            EditorGUIUtils.DrawHelpBox(helpBoxRect, message, messageType);
                        }
                        else
                        {
                            switch (helpBoxPosition)
                            {
                                case ShowOptions.Before:
                                    propertyRect.y += helpBoxRect.height;
                                    propertyRect.height = EditorGUIUtils.GetPropertyHeight(property, label);
                                    EditorGUIUtils.DrawHelpBox(helpBoxRect, message, messageType);
                                    EditorGUIUtils.DrawProperty(propertyRect, property, label);
                                    break;
                                case ShowOptions.After:
                                    propertyRect.height = EditorGUIUtils.GetPropertyHeight(property, label);
                                    helpBoxRect.y += propertyRect.height;
                                    EditorGUIUtils.DrawProperty(propertyRect, property, label);
                                    EditorGUIUtils.DrawHelpBox(helpBoxRect, message, messageType);
                                    break;
                                default:
                                    Rect labelRect = position;
                                    labelRect.height = EditorGUIUtils.LineHeight;
                                    EditorGUI.LabelField(labelRect, label.text, "Not supported position case");
                                    SingleLine = true;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        EditorGUIUtils.DrawProperty(position, property, label);
                    }
                }
            }

            private string GetAttributeMessage(AHelpBoxAttribute helpBoxAttribute, SerializedProperty property)
            {
                string s = string.Empty;
                if (helpBoxAttribute == null || property == null)
                {
                    return s;
                }

                if (helpBoxAttribute is HelpBoxLabelAttribute)
                {
                    if (property.propertyType != SerializedPropertyType.String)
                    {
                        s = "Invalid attribute for non-string field.";
                    }
                    else
                    {
                        s = property.stringValue;
                    }
                }
                else
                {
                    s = helpBoxAttribute.Message;
                }

                if (helpBoxAttribute.IsReflectedMessage)
                {
                    try
                    {
                        if (SerializedPropertyUtils.TryGetParentLevel(property, out object parent))
                        {
                            if (ReflectionUtils.HasMember(parent, helpBoxAttribute.Message, out MemberInfo member, flags))
                            {
                                if (ReflectionUtils.TryGetMemberValue<string>(parent, member, out string tmp))
                                {
                                    s = tmp;

                                    if (helpBoxAttribute is HelpBoxLabelAttribute && property.propertyType == SerializedPropertyType.String)
                                    {
                                        property.stringValue = s;
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e.Message);
                    }
                }

                return s;
            }

            private float GetAttributeHeight(string attributeMessage)
            {
                return Mathf.Max(EditorGUIUtils.LineHeight,
                    EditorGUIUtils.GetHelpBoxHeight(attributeMessage) + EditorGUIUtils.VerticalSpacing);
            }

            private bool ShouldDisplayAttribute(AHelpBoxAttribute helpBoxAttribute, SerializedProperty property)
            {
                if (helpBoxAttribute.UseCondition)
                {
                    try
                    {
                        if (SerializedPropertyUtils.TryGetParentLevel(property, out object parent))
                        {
                            if (ReflectionUtils.HasMember(parent, helpBoxAttribute.Condition, out MemberInfo member))
                            {
                                if (ReflectionUtils.TryGetMemberValue<bool>(parent, member, out bool b))
                                {
                                    return b;
                                }
                            }
                        }

                        return true;
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e.Message);
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }

            private bool ShowAttributeOnly(AHelpBoxAttribute helpBoxAttribute)
            {
                return helpBoxAttribute != null && helpBoxAttribute.Show == ShowOptions.HelpBoxOnly;
            }
        }
#endif
        #endregion
    }
}