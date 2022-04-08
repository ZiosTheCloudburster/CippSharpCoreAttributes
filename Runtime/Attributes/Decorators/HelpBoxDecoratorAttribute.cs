#if UNITY_EDITOR
using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
#endif

namespace CippSharp.Core.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class HelpBoxDecoratorAttribute : ACustomPropertyAttribute, System.IEquatable<HelpBoxDecoratorAttribute>
    {
        public string FieldNameOrIdentifier { get; protected set; } = string.Empty;
        
        public bool IsReflectedMessage { get; protected set; } = false;
        public string Message { get; protected set; } = string.Empty;
        public MessageType Type { get; protected set; } = MessageType.Info;

        //HelpBox If
        public bool UseCondition { get; protected set; } = false;
        public string Condition { get; protected set; } = string.Empty;
        public bool ConditionMatch { get; protected set; } = true;

        protected HelpBoxDecoratorAttribute()
        {
            
        }
        
        public HelpBoxDecoratorAttribute(string fieldName, string message, MessageType messageType = MessageType.Info) 
            : this ()
        {
            this.FieldNameOrIdentifier = fieldName;
            this.Message = message;
            this.Type = messageType;
        }
        
        public HelpBoxDecoratorAttribute(string fieldName, string message, string condition, MessageType messageType = MessageType.Info) 
            : this (fieldName, message, messageType)
        {
            this.Condition = condition;
            this.UseCondition = !string.IsNullOrEmpty(condition);
        }
        
        public HelpBoxDecoratorAttribute(string fieldName, string message, string condition, bool conditionMatch = true, MessageType messageType = MessageType.Info) 
            : this (fieldName, message, condition, messageType)
        {
            this.ConditionMatch = conditionMatch;
        }
        
        //With reflected message
        public HelpBoxDecoratorAttribute(string fieldName, string message, bool isReflectedMessage, MessageType messageType = MessageType.Info) 
            : this(fieldName, message, messageType)
        {
            this.IsReflectedMessage = isReflectedMessage;
        }

        public HelpBoxDecoratorAttribute(string fieldName, string message, bool isReflectedMessage, string condition, MessageType messageType = MessageType.Info)
            : this(fieldName, message, isReflectedMessage, messageType)
        {
            this.Condition = condition;
            this.UseCondition = !string.IsNullOrEmpty(condition);
        }
        
        public HelpBoxDecoratorAttribute(string fieldName, string message, bool isReflectedMessage, string condition, bool conditionMatch = true, MessageType messageType = MessageType.Info)
            : this(fieldName, message, isReflectedMessage, condition, messageType)
        {
            this.ConditionMatch = conditionMatch;
        }
        
        #region Equality Members

        public bool Equals(HelpBoxDecoratorAttribute other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && string.Equals(FieldNameOrIdentifier, other.FieldNameOrIdentifier) && IsReflectedMessage == other.IsReflectedMessage && string.Equals(Message, other.Message) && Type == other.Type && UseCondition == other.UseCondition && string.Equals(Condition, other.Condition) && ConditionMatch == other.ConditionMatch;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((HelpBoxDecoratorAttribute) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ (FieldNameOrIdentifier != null ? FieldNameOrIdentifier.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ IsReflectedMessage.GetHashCode();
                hashCode = (hashCode * 397) ^ (Message != null ? Message.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int) Type;
                hashCode = (hashCode * 397) ^ UseCondition.GetHashCode();
                hashCode = (hashCode * 397) ^ (Condition != null ? Condition.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ ConditionMatch.GetHashCode();
                return hashCode;
            }
        }

        #endregion
        
        #region Custom Editor
#if UNITY_EDITOR
        [CustomPropertyDrawer(typeof(HelpBoxDecoratorAttribute), true)]
        public class HelpBoxDecoratorAttributeDrawer : DecoratorDrawer
        {
            private float currentMessageHeight = 0;
            private string currentMessage = string.Empty;
            private bool shouldDisplayHelpBox = false;
            
            public override float GetHeight()
            {
                if (!(this.attribute is HelpBoxDecoratorAttribute helpBoxDecoratorAttribute))
                {
                    return 0;
                }
                
                currentMessage = GetAttributeMessage(helpBoxDecoratorAttribute);
                currentMessageHeight = GetMessageHeight(currentMessage);
                shouldDisplayHelpBox = ShouldDisplayHelpBox(helpBoxDecoratorAttribute);
                if (shouldDisplayHelpBox)
                {
                    return currentMessageHeight + EditorGUIUtils.VerticalSpacing;
                }

                return 0;
            }
           
            public override void OnGUI(Rect position)
            {
                if (this.attribute is HelpBoxDecoratorAttribute helpBoxDecoratorAttribute)
                {
                    if (shouldDisplayHelpBox)
                    {
                        Rect helpBoxRect = position;
                        position.height = currentMessageHeight;
                        EditorGUIUtils.DrawHelpBox(helpBoxRect, currentMessage, ((UnityEditor.MessageType)(int)helpBoxDecoratorAttribute.Type));
                    }
                }
            }

            #region Get Attribute Message
          
            /// <summary>
            /// Do all required stuffs to get attribute message
            /// </summary>
            /// <param name="helpBoxDecoratorAttribute"></param>
            /// <returns></returns>
            private static string GetAttributeMessage(HelpBoxDecoratorAttribute helpBoxDecoratorAttribute)
            {
                string s = helpBoxDecoratorAttribute.Message;
                //If is not reflected message return s.
                if (!helpBoxDecoratorAttribute.IsReflectedMessage)
                {
                    return s;
                }
                
                SerializedProperty property = DecoratorDrawersUtils.GetPropertiesWithAttribute<HelpBoxDecoratorAttribute>(AttributePredicate).FirstOrDefault();
                bool AttributePredicate(HelpBoxDecoratorAttribute c)
                {
                    return helpBoxDecoratorAttribute.Equals(c);
                }

                //If property not found with this attribute, return s
                if (property == null)
                {
                    return s;
                }
                    
                try
                {
                    if (SerializedPropertyUtils.TryGetParentLevel(property, out object parent))
                    {
                        if (ReflectionUtils.HasMember(parent, s, out MemberInfo member))
                        {
                            if (ReflectionUtils.TryGetMemberValue<string>(parent, member, out string tmp))
                            {
                                s = tmp;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError($"Failed to retrieve message. Caught Exception: {e.Message}.", property.serializedObject.targetObject);
                }

                return s;
            }
            
            #endregion

            private static float GetMessageHeight(string message)
            {
                return Mathf.Max(EditorGUIUtils.SingleLineHeight, EditorGUIUtils.GetHelpBoxHeight(message));
            }

            #region Should Display Help Box Check
           
            private static bool ShouldDisplayHelpBox(HelpBoxDecoratorAttribute helpBoxDecoratorAttribute)
            {
                bool useCondition = helpBoxDecoratorAttribute.UseCondition;
                if (useCondition)
                {
                    SerializedProperty property = DecoratorDrawersUtils.GetPropertiesWithAttribute<HelpBoxDecoratorAttribute>(AttributePredicate).FirstOrDefault();
                    bool AttributePredicate(HelpBoxDecoratorAttribute c)
                    {
                        return helpBoxDecoratorAttribute.Equals(c);
                    }
                    
                    //If property not found with this attribute, return opposite of condition match
                    if (property == null)
                    {
                        return false;
                    }
                    
                    try
                    {
                        if (SerializedPropertyUtils.TryGetParentLevel(property, out object parent))
                        {
                            if (ReflectionUtils.HasMember(parent, helpBoxDecoratorAttribute.Condition, out MemberInfo member))
                            {
                                if (ReflectionUtils.TryGetMemberValue<bool>(parent, member, out bool b))
                                {
                                    //Member value must match the condition match value.
                                    return helpBoxDecoratorAttribute.ConditionMatch == b;
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"Failed to retrieve condition. Caught Exception: {e.Message}.", property.serializedObject.targetObject);
                    }
                }
                else
                {
                    return true;
                }

                return false;
            }
            
            #endregion
        }
#endif   
        #endregion
    }
}
