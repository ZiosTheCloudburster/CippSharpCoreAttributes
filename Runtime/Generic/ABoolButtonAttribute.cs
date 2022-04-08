/*
 *  Author: Alessandro Salani (Cippman)
 */

#if UNITY_EDITOR
using System.Reflection;
using CippSharp.Core.Attributes.Extensions;
using UnityEditor;
using UnityEngine;
#endif

namespace CippSharp.Core.Attributes
{
    /// <summary>
    /// Displays a bool field as a button.
    /// </summary>
    public abstract class ABoolButtonAttribute : ACustomPropertyAttribute
    {
        public enum Behaviour
        {
            /// <summary>
            /// Press mode counts only to be clicked but boolean property is not updated 
            /// </summary>
            Press,
            /// <summary>
            /// Boolean property is updated with click
            /// </summary>
            Toggle
        }
        
        /// <summary>
        /// Show boolean value
        /// </summary>
        public bool ShowValue { get; protected set; } = false;
        /// <summary>
        /// This button works as press or toggle?
        /// </summary>
        public Behaviour Mode { get; protected set; } = Behaviour.Press;
        /// <summary>
        /// Button Style
        /// </summary>
        public GUIButtonStyle GraphicStyle { get; protected set; } = GUIButtonStyle.MiniButton;
        
        /// <summary>
        /// Should call a method on button click?
        /// </summary>
        public bool UseCallback { get; protected set; } = false;
        /// <summary>
        /// Callback that should be invoked. Warning: the method must not have parameters or can have one but of type bool.
        /// </summary>
        public string MethodCallback { get; protected set; } = string.Empty;

        #region Custom Editor
#if UNITY_EDITOR
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
                        Behaviour mode = abstractButtonAttribute.Mode;
                        if (abstractButtonAttribute is BoolButtonToggleAttribute)
                        {
                            mode = Behaviour.Toggle;
                        }

                        string propertyName = (abstractButtonAttribute.ShowValue) ? $"{property.displayName}: {property.boolValue.ToString()}" : property.displayName;
                        bool isMiniButton = abstractButtonAttribute.GraphicStyle == GUIButtonStyle.MiniButton;
                        GUIStyle style = (isMiniButton) ? EditorStyles.miniButton : (GUIStyle) null;
                        bool propertyBooleanValue = property.boolValue;
                        switch (mode)
                        {
                            //Press means that is true just for the GUI frame when it's clicked.
                            case Behaviour.Press:
                                EditorGUIUtils.DrawButtonWithCallback(position, propertyName,
                                    () => { property.boolValue = true; }, () => { property.boolValue = false; }, style);
                                break;
                            //Toggle means that is true or false like a normal boolean.
                            case Behaviour.Toggle:
                                EditorGUIUtils.DrawButtonWithCallback(position, propertyName,
                                    () => { property.boolValue = !propertyBooleanValue; }, () => { property.boolValue = propertyBooleanValue; }, style);
                                break;
                            default:
                                EditorGUI.LabelField(position, label.text, "Argument out of range exception.");
                                break;
                        }

//                        if (propertyBooleanValue != property.boolValue)
//                        {
//                            //Debug.Log($"Property {property.displayName} has changed.");
//                        }
                    }

                    if (EditorGUI.EndChangeCheck())
                    {
                        #region Invoke Callback and Ensure References

                        if (abstractButtonAttribute != null && abstractButtonAttribute.UseCallback)
                        {
                            //apply this edited property
                            property.serializedObject.ApplyModifiedProperties();
                            
                            //update again
                            property.serializedObject.Update();
                            //invoke the callback
                            if (SerializedPropertyUtils.TryEditLastParentLevel(property, (ref object o) => Callback(ref o, abstractButtonAttribute.MethodCallback, property.boolValue)))
                            {

                            }
                            //then apply again if something was edited during the callback
                            property.serializedObject.ApplyModifiedProperties();
                        }

                        #endregion
                    }
                }
                else
                {
                    EditorGUI.LabelField(position, label.text, "Use BoolButtonAttribute with boolean fields.");
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
#endif
        #endregion
        
    }
}
