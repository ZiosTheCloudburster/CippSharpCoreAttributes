#if UNITY_EDITOR
using System.Reflection;
using CippSharp.Core.Attributes.Extensions;
using UnityEditor;
using UnityEngine;
#endif

namespace CippSharp.Core.Attributes
{   
    /// <summary>
    /// Purpose: redraws the property and calls the methods if EditorGUI.EndChangeCheck was called.
    ///
    /// Usage: use this to do assert that a property is correctly updated.
    /// If you'd like to print some messages or not.
    ///
    /// Warning: property editing is not allowed.
    /// </summary>
    public class AssertAttribute : AValidatePropertyAttribute
    {
        /// <summary>
        /// Method's return type can be of bool type
        /// 
        /// Method's may contains a parameter of same type as field. 
        /// </summary>
        public string MethodName { get; protected set; }

        /// <summary>
        /// Message is printed if condition's of Method is false 
        /// </summary>
        public string Message { get; protected set; }

        public MessageType MessageType { get; protected set; }

        protected AssertAttribute()
        {
            
        }

        public AssertAttribute(string methodName, string message = "", MessageType messageType = MessageType.Info)
            : this()
        {
            this.MethodName = methodName;
            this.Message = message;
            this.MessageType = messageType;
        }

        #region Custom Editor
#if UNITY_EDITOR
        [CustomPropertyDrawer(typeof(AssertAttribute))]
        public class ValidateAssertionAttributeDrawer : PropertyDrawer
        {
            public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            {
                return EditorGUIUtils.GetPropertyHeight(property, label);
            }

            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                AssertAttribute assertAttribute = attribute as AssertAttribute;
                if (assertAttribute == null)
                {
                    EditorGUI.LabelField(position, label.text, "Invalid attribute.");
                }
                else
                {
                    property.serializedObject.Update();
                    EditorGUI.BeginChangeCheck();
                    EditorGUIUtils.DrawProperty(position, property, label);
                    if (EditorGUI.EndChangeCheck())
                    {
                        if (!string.IsNullOrEmpty(assertAttribute.MethodName))
                        {
                            if (SerializedPropertyUtils.TryEditLastParentLevel(property, (ref object o) => Callback(ref o, assertAttribute)))
                            {
                                    
                            }
                        }
                    }

                    property.serializedObject.ApplyModifiedProperties();
                }
            }

            private void Callback(ref object context, AssertAttribute assertAttribute)
            {
                MethodInfo methodInfo = null;
                string methodName = assertAttribute.MethodName;
                object fieldValue = fieldInfo.GetValue(context);

                if (ReflectionUtils.HasMethod(context, methodName, out methodInfo))
                {
                    ParameterInfo[] parameters = methodInfo.GetParameters();
                    if (parameters.IsNullOrEmpty())
                    {
                        if (ReflectionUtils.TryCallMethod(context, methodName, out object result))
                        {
                            if (!(result is bool b))
                            {
                                return;
                            }

                            if (!b)
                            {
                                LogMessageFromAttribute(context, assertAttribute);
                            }
                        }
                    }
                    else if (parameters.Length == 1 && parameters[0].ParameterType == fieldInfo.FieldType)
                    {
                        if (ReflectionUtils.TryCallMethod(context, methodName, out object result, new[] {(object) fieldValue}))
                        {
                            if (!(result is bool b))
                            {
                                return;
                            }

                            if (!b)
                            {
                                LogMessageFromAttribute(context, assertAttribute);
                            }
                        }
                    }
                }
                else
                {
                    Debug.Log($"Method {methodName} not found. Context {context.ToString()}", context as UnityEngine.Object);
                }
            }

            private void LogMessageFromAttribute(object context, AssertAttribute assertAttribute)
            {
                string message = assertAttribute.Message;
                if (string.IsNullOrEmpty(message))
                {
                    return;
                }
                
                string logName = StringUtils.LogName(context);
                Object @object = context as UnityEngine.Object;
                
                switch (assertAttribute.MessageType)
                {
                    case MessageType.Info:
                        Debug.Log(logName + message, @object);
                        break;
                    case MessageType.Warning:
                        Debug.LogWarning(logName + message, @object);
                        break;
                    case MessageType.Error:
                        Debug.LogError(logName + message, @object);
                        break;
                }
            }
        }
#endif
        #endregion
    }
}
