/*
 *  Author: Alessandro Salani (Cippman)
 */

using System;
using UnityEngine;
#if UNITY_EDITOR
using System.Reflection;
using CippSharp.Core;
using UnityEditor;
#endif

namespace CippSharp.Core
{
    public enum HelpBoxPosition
    {
        Before = 0,
        After = 1,
        HelpBoxOnly = 2,
    }
    
    public enum HelpBoxMessageType
    {
#if UNITY_EDITOR
        None = MessageType.None,
        Info = MessageType.Info,
        Warning = MessageType.Warning,
        Error = MessageType.Error,
#else
        None,
        Info,
        Warning,
        Error,
#endif
    }
    
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public abstract class AHelpBoxAttribute : PropertyAttribute
    {
        public bool IsReflectedMessage { get; protected set; } = false;
        public string Message { get; protected set; } = string.Empty;
        public HelpBoxPosition Position { get; protected set; } = HelpBoxPosition.After;
        public HelpBoxMessageType MessageType { get; protected set; } = HelpBoxMessageType.Info;

        public bool UseCondition { get; protected set; } = false;
        public string Condition { get; protected set; } = string.Empty;
        
    }
}

#if UNITY_EDITOR
namespace CippSharpEditor.Core
{
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
                
                MessageType messageType = (MessageType)((int)helpBoxAttribute.MessageType);
                HelpBoxPosition helpBoxPosition = helpBoxAttribute.Position;

                if (ShouldDisplayAttribute(helpBoxAttribute, property))
                {
                    if (helpBoxPosition == HelpBoxPosition.HelpBoxOnly)
                    {
                        EditorGUIUtils.DrawHelpBox(helpBoxRect, message, messageType);
                    }
                    else
                    {
                        switch (helpBoxPosition)
                        {
                            case HelpBoxPosition.Before:
                                propertyRect.y += helpBoxRect.height;
                                propertyRect.height = EditorGUIUtils.GetPropertyHeight(property, label);
                                EditorGUIUtils.DrawHelpBox(helpBoxRect, message, messageType);
                                EditorGUIUtils.DrawProperty(propertyRect, property, label);
                                break;
                            case HelpBoxPosition.After:
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
            return Mathf.Max(EditorGUIUtils.LineHeight, EditorGUIUtils.GetHelpBoxHeight(attributeMessage) + EditorGUIUtils.VerticalSpacing);
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
            return helpBoxAttribute != null && helpBoxAttribute.Position == HelpBoxPosition.HelpBoxOnly;
        }
    }
}
#endif
