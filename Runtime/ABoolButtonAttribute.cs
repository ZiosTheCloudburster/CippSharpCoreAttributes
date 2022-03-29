/*
 *  Author: Alessandro Salani (Cippman)
 */

using System;
using UnityEngine;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using System.Reflection;
using CippSharp.Core;
using CippSharp.Core.Extensions;
using UnityEditor;
#endif

namespace CippSharp.Core
{
    public enum BoolButtonBehaviour
    {
        Press,
        Toggle
    }
    
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public abstract class ABoolButtonAttribute : PropertyAttribute
    {
        public bool ShowValue { get; protected set; } = false;
        public BoolButtonBehaviour AttributeBehaviour { get; protected set; } = BoolButtonBehaviour.Press;
        public GUIButtonStyle GraphicStyle { get; protected set; } = GUIButtonStyle.MiniButton;
        
        public bool UseCallback { get; protected set; } = false;
        public string MethodCallback { get; protected set; } = string.Empty;
    }
}

#if UNITY_EDITOR
namespace CippSharpEditor.Core
{
    [CustomPropertyDrawer(typeof(ABoolButtonAttribute), true)]
    public class BoolButtonDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.Boolean)
            {
                EditorGUI.BeginChangeCheck();
                
                
                ABoolButtonAttribute abstractButtonAttribute = attribute as ABoolButtonAttribute;
                if (abstractButtonAttribute == null)
                {
                    EditorGUI.LabelField(position, label.text, "Invalid button attribute.");
                }
                else
                {
                    BoolButtonBehaviour attributeBehaviour = abstractButtonAttribute.AttributeBehaviour; 
                    if (abstractButtonAttribute is BoolButtonToggleAttribute)
                    {
                        attributeBehaviour = BoolButtonBehaviour.Toggle;
                    }
                    string propertyName = (abstractButtonAttribute.ShowValue) ? $"{property.displayName}: {property.boolValue.ToString()}" : property.displayName;
                    bool isMiniButton = abstractButtonAttribute.GraphicStyle == GUIButtonStyle.MiniButton;
                    GUIStyle style = (isMiniButton) ? EditorStyles.miniButton : (GUIStyle) null;
                    bool propertyBooleanValue = property.boolValue;
                    switch (attributeBehaviour)
                    {
                        case BoolButtonBehaviour.Press:
                            EditorGUIUtils.DrawButtonWithCallback(position, propertyName, () => { property.boolValue = true;}, () => {property.boolValue = false;}, style);
                            break;
                        case BoolButtonBehaviour.Toggle:
                            EditorGUIUtils.DrawButtonWithCallback(position, propertyName, () => { property.boolValue = !property.boolValue;}, style);
                            break;
                        default:
                            EditorGUI.LabelField(position, label.text, "Argument out of range exception.");
                            break;
                    }
                    
                    if (propertyBooleanValue != property.boolValue)
                    {
                        Debug.Log($"Property {property.displayName} has changed.");
                    }
                }

                if (EditorGUI.EndChangeCheck())
                {
                    #region Invoke Callback and Ensure References
                    
                    if (abstractButtonAttribute != null && abstractButtonAttribute.UseCallback)
                    {
                        property.serializedObject.Update();
                        
                        if (SerializedPropertyUtils.TryEditLastParentLevel(property, (ref object o) => Callback(ref o, abstractButtonAttribute.MethodCallback, property.boolValue)))
                        {
                            
                        }
                        
                        property.serializedObject.ApplyModifiedProperties();
                    }
                    
                    #endregion
                }
            }
            else
            {
                EditorGUI.LabelField(position, label.text, "Use BoolButtonAttribute with bools fields.");
            }
        }

        private void Callback(ref object context, string methodName, bool value)
        {
            MethodInfo methodInfo = null;
            if (ReflectionUtils.HasMethod(context, methodName, out methodInfo))
            {
                ParameterInfo[] parameters = methodInfo.GetParameters();
                if (parameters.IsNullOrEmpty())
                {
                    if (ReflectionUtils.TryCallMethod(context, methodName, out object result))
                    {
                        //Debug.Log("Method Called!", context as Object);
                    }
                }
                else if (parameters.Length == 1 && parameters[0].ParameterType == typeof(bool))
                {
                    if (ReflectionUtils.TryCallMethod(context, methodName, out object result, new[] {(object) value}))
                    {
                        //Debug.Log("Method Called!", context as Object);
                    }
                }
            }
            else
            {
                Debug.Log($"Method {methodName} not found. Context {context.ToString()}", context as Object);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtils.GetPropertyHeight(property, label);
        }
    }
}
#endif