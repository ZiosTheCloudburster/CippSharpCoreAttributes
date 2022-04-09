#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using CippSharp.Core.Attributes.Extensions;
using UnityEditor;
using UnityEngine;

namespace CippSharp.Core.Attributes
{
    using UnityMessageType = UnityEditor.MessageType;
    
    /// <summary>
    ///
    /// 20201/08/14 → Added 'by refs' methods to reduce memory heaps
    /// and sometimes to edit variables
    /// </summary>
    public static partial class EditorGUIUtils
    {
        #region Draw Button

        /// <summary>
        /// Draws a button
        /// </summary>
        /// <param name="position"></param>
        /// <param name="name"></param>
        /// <param name="style"></param>
        public static void DrawButton(Rect position, string name, GUIStyle style = null)
        {
            if (style == null)
            {
                if (GUI.Button(position, name))
                {
                    
                }
            }
            else
            {
                if (GUI.Button(position, name, style))
                {
                    
                }
            }
        }

        /// <summary>
        /// Draws a button with callback
        /// </summary>
        /// <param name="position"></param>
        /// <param name="name"></param>
        /// <param name="clickCallback"></param>
        /// <param name="style"></param>
        public static void DrawButtonWithCallback(Rect position, string name, Action clickCallback, GUIStyle style = null)
        {
            DrawButtonWithCallback(ref position, name, clickCallback, style);
        }

        /// <summary>
        /// Ref Draws a Button with callback
        /// </summary>
        /// <param name="position"></param>
        /// <param name="name"></param>
        /// <param name="clickCallback"></param>
        /// <param name="style"></param>
        public static void DrawButtonWithCallback(ref Rect position, string name, Action clickCallback, GUIStyle style = null)
        {
            if (style == null)
            {
                if (GUI.Button(position, name))
                {
                    clickCallback.Invoke();
                }
            }
            else
            {
                if (GUI.Button(position, name, style))
                {
                    clickCallback.Invoke();
                }
            }
        }

        /// <summary>
        /// Draws a Button with callbacks
        /// </summary>
        /// <param name="position"></param>
        /// <param name="name"></param>
        /// <param name="clickCallback"></param>
        /// <param name="notClickedCallback"></param>
        /// <param name="style"></param>
        public static void DrawButtonWithCallback(Rect position, string name, Action clickCallback, Action notClickedCallback, GUIStyle style = null)
        {
            DrawButtonWithCallback(ref position, name, clickCallback, notClickedCallback, style);
        }
        
        /// <summary>
        /// Ref draws a Button with callbacks
        /// </summary>
        /// <param name="position"></param>
        /// <param name="name"></param>
        /// <param name="clickCallback"></param>
        /// <param name="notClickedCallback"></param>
        /// <param name="style"></param>
        public static void DrawButtonWithCallback(ref Rect position, string name, Action clickCallback, Action notClickedCallback, GUIStyle style = null)
        {
            if (style == null)
            {
                if (GUI.Button(position, name))
                {
                    clickCallback.Invoke();
                }
                else
                {
                    notClickedCallback.Invoke();
                }
            }
            else
            {
                if (GUI.Button(position, name, style))
                {
                    clickCallback.Invoke();
                }
                else
                {
                    notClickedCallback.Invoke();
                }
            }
        }

        #endregion

        #region Draw Foldout
        
        /// <summary>
        /// Draws an isExpanded foldout for the passed property.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="property"></param>
        public static void DrawFoldout(Rect position, SerializedProperty property)
        {
            DrawFoldout(ref position, property);
        }

        /// <summary>
        /// Draws an isExpanded foldout for the passed property.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="property"></param>
        /// <param name="rect">the edited position value</param>
        public static void DrawFoldout(Rect position, SerializedProperty property, out Rect rect)
        {
            rect = position;
            rect.height = SingleLineHeight;
            property.isExpanded = EditorGUI.Foldout(rect, property.isExpanded, property.displayName);
            rect.y += LineHeight;
        }

        /// <summary>
        /// Draws an isExpanded foldout for the passed property.
        /// </summary>
        /// <param name="position">will be edited on height to SingleLineHeight and in Y by adding the LineHeight</param>
        /// <param name="property"></param>
        public static void DrawFoldout(ref Rect position, SerializedProperty property)
        {
            position.height = SingleLineHeight;
            property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, property.displayName);
            position.y += LineHeight;
        }

        #endregion
        
        #region Draw Help Box

//        /// <summary>
//        /// The height of an help box based on his text message.
//        /// </summary>
//        /// <param name="helpBoxMessage"></param>
//        /// <returns></returns>
//        public static float GetHelpBoxHeight(string helpBoxMessage)
//        {
//            return GetHelpBoxHeight(ref helpBoxMessage);
//        }
        
        /// <summary>
        /// The height of an help box based on his text message.
        /// </summary>
        /// <param name="helpBoxMessage"></param>
        /// <returns></returns>
        public static float GetHelpBoxHeight(string helpBoxMessage)
        {
            return GetHelpBoxHeight(helpBoxMessage, Screen.width);
//            GUIStyle style = EditorStyles.helpBox;
//            GUIContent descriptionWrapper = new GUIContent(helpBoxMessage);
//            return style.CalcHeight(descriptionWrapper, Screen.width);
        }

        public static float GetHelpBoxHeight(string helpBoxMessage, float width)
        {
            GUIStyle style = EditorStyles.helpBox;
            GUIContent descriptionWrapper = new GUIContent(helpBoxMessage);
            return style.CalcHeight(descriptionWrapper, width);
        }

        /// <summary>
        /// Draw an help box with the passed rect and text.
        /// It doesn't matter about his height/resizing..
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="text"></param>
        /// <param name="messageType"></param>
        public static void DrawHelpBox(Rect rect, string text, UnityMessageType messageType = UnityMessageType.Info)
        {
            EditorGUI.HelpBox(rect, text, messageType);
        }
        
        /// <summary>
        /// Draw an help box with the passed rect and text.
        /// It doesn't matter about his height/resizing..
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="text"></param>
        /// <param name="messageType"></param>
        public static void DrawHelpBox(ref Rect rect, ref string text, ref UnityMessageType messageType)
        {
            EditorGUI.HelpBox(rect, text, messageType);
        }

        /// <summary>
        /// Draws an help box with the passed rect and text.
        /// </summary>
        /// <param name="inputRect"></param>
        /// <param name="helpBoxMessage"></param>
        /// <param name="textHeight">The computed height of the description.</param>
        /// <param name="messageType"></param>
        public static void DrawHelpBox(Rect inputRect, string helpBoxMessage, out float textHeight, UnityMessageType messageType = UnityMessageType.Info)
        {
            DrawHelpBox(ref inputRect, ref helpBoxMessage, out textHeight, ref messageType);
        }

        /// <summary>
        /// Draws an help box with the passed rect and text.
        /// </summary>
        /// <param name="inputRect"></param>
        /// <param name="helpBoxMessage"></param>
        /// <param name="textHeight">The computed height of the description.</param>
        /// <param name="messageType"></param>
        public static void DrawHelpBox(ref Rect inputRect, ref string helpBoxMessage, out float textHeight, ref UnityMessageType messageType)
        {
            GUIStyle style = EditorStyles.helpBox;
            GUIContent descriptionWrapper = new GUIContent(helpBoxMessage);
            textHeight = style.CalcHeight(descriptionWrapper, inputRect.width);
            inputRect.height = textHeight;
            inputRect.y += LineHeight;
            DrawHelpBox(ref inputRect, ref helpBoxMessage, ref messageType);
            inputRect.y += textHeight;
        }

        #endregion
        
        #region Draw Labels
        
//        /// <summary>
//        /// Draw a label with the passed text
//        /// </summary>
//        /// <param name="rect"></param>
//        /// <param name="text">doesn't draw if text is null or empty</param>
//        public static void DrawHeader(Rect rect, string text)
//        {
//            DrawHeader(ref rect, ref text);
//        }

        /// <summary>
        /// Draw a label with the passed text
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="text">doesn't draw if text is null or empty</param>
        public static void DrawHeader(Rect rect, string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                EditorGUI.LabelField(rect, new GUIContent(text), EditorStyles.boldLabel);
            }
        }

        #endregion

        #region Draw Pop Up

        /// <summary>
        /// None Element is at options 0
        /// Draws a popup of options for the string property.
        /// Automatically calculates the index.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="stringProperty"></param>
        /// <param name="options"></param>
        public static bool DrawOptionsPopUpForStringProperty(Rect position, SerializedProperty stringProperty, List<string> options)
        {
            return DrawOptionsPopUpForStringProperty(position, stringProperty.displayName, stringProperty, options);
        }

        /// <summary>
        /// None Element is at options 0
        /// Draws a popup of options for the string property.
        /// Automatically calculates the index.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="labelText"></param>
        /// <param name="stringProperty"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static bool DrawOptionsPopUpForStringProperty(Rect position, string labelText, SerializedProperty stringProperty, List<string> options)
        {
            const string invalidSuffix = " (invalid)";
            int index = -1;
            string stringValue = stringProperty.stringValue;
            if (string.IsNullOrEmpty(stringValue))
            {
                index = 0;
            }
            else if (options.Any(s => s == stringValue, out int tmpIndex))
            {
                index = tmpIndex;
            }
            else
            {
                index = options.Count;
                options.Add($"{stringValue}{invalidSuffix}");
            }
          
            EditorGUI.BeginChangeCheck();
            index = EditorGUI.Popup(position, labelText, index, options.ToArray());
            
            if (index == 0)
            {
                stringProperty.stringValue = string.Empty;
            }
            else if (ArrayUtils.IsValidIndex(index, options))
            {
                stringProperty.stringValue = options[index].Replace(invalidSuffix, string.Empty);
            }
            else
            {
                stringProperty.stringValue = string.Empty;
            }

            bool endCheck = EditorGUI.EndChangeCheck();
            return endCheck;
        }

     
        #endregion
    }
}
#endif