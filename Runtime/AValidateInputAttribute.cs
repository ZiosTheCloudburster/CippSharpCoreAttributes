using System;
using System.Reflection;
using CippSharp.Core.Extensions;
using UnityEngine;

#if UNITY_EDITOR
using CippSharp.Core;
using UnityEditor;
#endif

namespace CippSharp.Core
{
    public enum ValidateInputMessageType
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
    
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public abstract class AValidateInputAttribute : PropertyAttribute
    {
        
    }
}

#if UNITY_EDITOR
namespace CippSharpEditor.Core
{
    [CustomPropertyDrawer(typeof(AValidateInputAttribute))]
    public class AValidateInputAttributeDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtils.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            AValidateInputAttribute validateInputAttribute = attribute as  AValidateInputAttribute;
            if (validateInputAttribute == null)
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
                    if (validateInputAttribute is ValidateInputAttribute v)
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
        
        private void Callback(ref object context, ValidateInputAttribute validateInputAttribute)
        {
            MethodInfo methodInfo = null;
            string methodName = validateInputAttribute.MethodName;
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
                            LogMessageFromAttribute(context, validateInputAttribute);
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
                            LogMessageFromAttribute(context, validateInputAttribute);
                        }
                    }
                }
            }
            else
            {
                Debug.Log($"Method {methodName} not found. Context {context.ToString()}", context as UnityEngine.Object);
            }                   
        }

        private void LogMessageFromAttribute(object context, ValidateInputAttribute validateInputAttribute)
        {
            string logName = StringUtils.LogName(context);
            switch (validateInputAttribute.MessageType)
            {
                case ValidateInputMessageType.Info:
                    Debug.Log(logName+validateInputAttribute.Message, context as UnityEngine.Object);
                    break;
                case ValidateInputMessageType.Warning:
                    Debug.LogWarning(logName+validateInputAttribute.Message, context as UnityEngine.Object);
                    break;
                case ValidateInputMessageType.Error:
                    Debug.LogError(logName+validateInputAttribute.Message, context as UnityEngine.Object);
                    break;
            }
        }
    }
}
#endif