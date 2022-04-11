//
// Author: Alessandro Salani (Cippo)
//
#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CippSharp.Core.Attributes
{
    using Action = System.Action;
    using UnityMessageType = UnityEditor.MessageType;
    
    internal static class EditorGUIUtils
    {
        /// <summary>
        /// Wrap of unity's default single line height.
        /// </summary>
        public static readonly float SingleLineHeight = EditorGUIUtility.singleLineHeight;
  
        /// <summary>
        /// Wrap of unity's default vertical spacing between lines.
        /// </summary>
        public static readonly float VerticalSpacing = EditorGUIUtility.standardVerticalSpacing;
  
        /// <summary>
        /// Sum of <see cref="SingleLineHeight"/> + <seealso cref="VerticalSpacing"/>.
        /// </summary>
        public static readonly float LineHeight = SingleLineHeight + VerticalSpacing;

        /// <summary>
        /// Retrieve the original rect space divided in horizontal by length.
        /// By default a space of 2 is considered between each element.
        ///
        /// Count must be >= 1
        /// </summary>
        /// <returns></returns>
        public static Rect[] DivideSpaceHorizontal(Rect position, int count, float space = 2)
        {
            if (count < 1)
            {
                return null;
            }

            if (count == 1)
            {
                return new[] {position};
            }

            Rect[] subdivisions = new Rect[count];
            float startingX = position.x;
            float totalWidth = position.width;
            float lastX = startingX;
            for (int i = 0; i < count; i++)
            {
                Rect rI = position;
                float elementWidth = (totalWidth / count) - space * 0.5f;
                if (i != 0)
                {
                    rI.x = lastX + space * 0.5f;
                }
                if (i == count -1)
                {
                    elementWidth += space * 0.5f;
                }
                rI.width = elementWidth;
                lastX += rI.width;
                subdivisions[i] = rI;
            }

            return subdivisions;
        }
        
        #region Get Property Height

        /// <summary>
        /// Retrieve the height of property's rect.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static float GetPropertyHeight(SerializedProperty property)
        {
            return EditorGUI.GetPropertyHeight(property, property.isExpanded && property.hasChildren) + VerticalSpacing;
        }

        /// <summary>
        /// Retrieve the height of property's rect.
        /// </summary>
        /// <param name="property"></param>
        /// <param name="label"></param>
        /// <returns></returns>
        public static float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, property.isExpanded && property.hasChildren) + VerticalSpacing;;
        }

        #endregion
        
        #region Draw Property
        
        /// <summary>
        /// It draws the property only if its different from null.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="property"></param>
        /// <param name="label"></param>
        public static void DrawProperty(Rect rect, SerializedProperty property, GUIContent label = null)
        {
            if (property == null)
            {
                return;
            }
            
            if (label != null)
            {
                EditorGUI.PropertyField(rect, property, label, property.isExpanded && property.hasChildren);
            }
            else
            {
                EditorGUI.PropertyField(rect, property, property.isExpanded && property.hasChildren);
            }
        }
        
        #endregion
        
        #region Draw Not Editable Property
        
        /// <summary>
        /// It draws the property only in a readonly way only if its different from null.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="property"></param>
        /// <param name="label"></param>
        public static void DrawNotEditableProperty(Rect rect, SerializedProperty property, GUIContent label = null)
        {
            bool enabled =  GUI.enabled; 
            GUI.enabled = false;

            DrawProperty(rect, property, label);
            
            GUI.enabled = enabled;
        }
        
        #endregion
        
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
            property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, property.displayName);
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
        
        /// <summary>
        /// The height of an help box based on his text message.
        /// </summary>
        /// <param name="helpBoxMessage"></param>
        /// <returns></returns>
        public static float GetHelpBoxHeight(string helpBoxMessage)
        {
            return GetHelpBoxHeight(helpBoxMessage, Screen.width);
        }

        /// <summary>
        /// The height of an help box based on his text message.
        /// </summary>
        /// <param name="helpBoxMessage"></param>
        /// <param name="width"></param>
        /// <returns></returns>
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
        /// Draws an help box with the passed rect and text.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="text"></param>
        /// <param name="height">The computed height of the text.</param>
        /// <param name="messageType"></param>
        public static void DrawHelpBox(Rect rect, string text, out float height, UnityMessageType messageType)
        {
            GUIStyle style = EditorStyles.helpBox;
            GUIContent descriptionWrapper = new GUIContent(text);
            height = style.CalcHeight(descriptionWrapper, rect.width);
            rect.height = height;
            rect.y += LineHeight;
            DrawHelpBox(rect, text, messageType);
            rect.y += height;
        }

        #endregion
        
        #region Draw Labels

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
            else if (ArrayUtils.Any(options, s => s == stringValue, out int tmpIndex))
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
