
using System.Reflection;
using CippSharp.Core.Extensions;
using UnityEditor;
using UnityEngine;

namespace CippSharp.Core.Attributes
{   
    /// <summary>
    /// Purpose: calls the methods if EditorGUI.EndChangeCheck was called.
    ///
    /// Usage: use this to do checks or ensure that a property is correctly updated.
    /// If you'd like to print some messages or not.
    /// </summary>
    public class ValidateAttribute : AValidatePropertyAttribute
    {
        /// <summary>
        /// Method's return type must be of bool value
        /// 
        /// Method's may contains a parameter of same type as field. 
        /// </summary>
        public string MethodName { get; protected set; }

        /// <summary>
        /// Message is printed if condition's of Method is false 
        /// </summary>
        public string Message { get; protected set; }

        public MessageType MessageType { get; protected set; }

        protected ValidateAttribute()
        {
            
        }

        public ValidateAttribute(string methodName, string message = "", MessageType messageType = MessageType.Info)
            : this()
        {
            this.MethodName = methodName;
            this.Message = message;
            this.MessageType = messageType;
        }

        #region Custom Editor
#if UNITY_EDITOR
        [CustomPropertyDrawer(typeof(ValidateAttribute))]
        public class ValidateAssertionAttributeDrawer : PropertyDrawer
        {
            public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            {
                return EditorGUIUtils.GetPropertyHeight(property, label);
            }

            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                ValidateAttribute validateAttribute = attribute as ValidateAttribute;
                if (validateAttribute == null)
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
                        if (validateAttribute is ValidateAttribute v)
                        {
                            if (!string.IsNullOrEmpty(v.MethodName))
                            {
                                if (SerializedPropertyUtils.TryEditLastParentLevel(property, (ref object o) => Callback(ref o, v)))
                                {
                                    
                                }
                            }
                        }
                    }

                    property.serializedObject.ApplyModifiedProperties();
                }
            }

            private void Callback(ref object context, ValidateAttribute validateAttribute)
            {
                MethodInfo methodInfo = null;
                string methodName = validateAttribute.MethodName;
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
                                LogMessageFromAttribute(context, validateAttribute);
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
                                LogMessageFromAttribute(context, validateAttribute);
                            }
                        }
                    }
                }
                else
                {
                    Debug.Log($"Method {methodName} not found. Context {context.ToString()}", context as UnityEngine.Object);
                }
            }

            private void LogMessageFromAttribute(object context, ValidateAttribute validateAttribute)
            {
                string logName = StringUtils.LogName(context);
                Object @object = context as UnityEngine.Object;
                switch (validateAttribute.MessageType)
                {
                    case MessageType.Info:
                        Debug.Log(logName + validateAttribute.Message, @object);
                        break;
                    case MessageType.Warning:
                        Debug.LogWarning(logName + validateAttribute.Message, @object);
                        break;
                    case MessageType.Error:
                        Debug.LogError(logName + validateAttribute.Message, @object);
                        break;
                }
            }
        }
#endif
        #endregion
    }
}
