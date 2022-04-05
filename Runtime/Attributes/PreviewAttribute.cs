#if UNITY_2019_4_OR_NEWER
using System;
using UnityEngine;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using CippSharp.Core;
using UnityEditor;
#endif

namespace CippSharp.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class PreviewAttribute : APreviewAttribute
    {
        public PreviewAttribute()
        {
            this.PreviewSettings = Settings.Default;
        }
        
        public PreviewAttribute(bool editable = true, ScaleMode scaleMode = ScaleMode.ScaleToFit, int measure = 128, string shaderPath = "", bool foldable = false)
        {
            this.PreviewSettings = new Settings()
            {
                editable = editable,
                scaleMode = scaleMode,
                measure = measure,
                shaderPath = shaderPath,
                foldable = foldable
            };
        }

        #region Custom Editor
#if UNITY_EDITOR
        [CustomPropertyDrawer(typeof(PreviewAttribute))]
        public class PreviewDrawer : PropertyDrawer
        {
            #region Default Material

            /// <summary>
            /// Backing field
            /// </summary>
            private static Material uiDefault = null;

            /// <summary>
            /// Unity's default ui material to draw the preview texture
            /// </summary>
            private static Material UIDefault
            {
                get
                {
                    if (uiDefault != null)
                    {
                        return uiDefault;
                    }

                    uiDefault = new Material(Shader.Find("UI/Default"));
                    return uiDefault;
                }
            }

            #endregion

            private Texture2D currentPreview = null;
            private Material currentMaterial = null;
            
//            private float additionalHeight = 0;

            public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            {
                float height = EditorGUIUtils.GetPropertyHeight(property, label);
                if (this.attribute is PreviewAttribute previewAttribute && property.propertyType == SerializedPropertyType.ObjectReference)
                {
                    Settings previewSettings = previewAttribute.PreviewSettings;
                    
                    //Did you know? unity calls get height before gui draws so, you can cache things here! :D
                    Object reference = property.objectReferenceValue;
                    currentPreview = AssetPreview.GetAssetPreview(reference);
                    bool useCustomMaterial = !string.IsNullOrEmpty(previewSettings.shaderPath);
                    currentMaterial = useCustomMaterial ? previewAttribute.GetMaterial() : UIDefault;
                    
                    if (previewSettings.foldable)
                    {
                        if (currentPreview != null)
                        {
                            height += EditorGUIUtils.LineHeight;
                            if (previewAttribute.isExpanded)
                            {
                                height += previewSettings.measure + EditorGUIUtils.LineHeight;
                            }
                        }
                    }
                    else
                    {
                        if (currentPreview != null)
                        {
                            height += previewSettings.measure + EditorGUIUtils.LineHeight;
                        }
                    }
                }

                return height;
            }

            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                if (this.attribute is PreviewAttribute previewAttribute && property.propertyType == SerializedPropertyType.ObjectReference)
                {
//                    Object reference = property.objectReferenceValue;
//                    preview = AssetPreview.GetAssetPreview(reference);

                    Settings previewSettings = previewAttribute.PreviewSettings;
//                    bool useCustomMaterial = !string.IsNullOrEmpty(previewSettings.shaderPath);
//                    mat = useCustomMaterial ? previewAttribute.GetMaterial() : UIDefault;
//
//                    additionalHeight = 0;

                    bool guiStatus = GUI.enabled;
                    if (previewSettings.editable == false)
                    {
                        GUI.enabled = previewSettings.editable;
                    }

                    Rect propertyRect = position;
                    propertyRect.height = EditorGUIUtils.SingleLineHeight;
                    EditorGUIUtils.DrawProperty(propertyRect, property, label);

                    if (previewSettings.foldable)
                    {
                        if (currentPreview != null)
                        {
                            EditorGUI.indentLevel++;
                            Rect foldoutRect = propertyRect;
                            foldoutRect.y += EditorGUIUtils.LineHeight;
                            previewAttribute.isExpanded = EditorGUI.Foldout(foldoutRect, previewAttribute.isExpanded, "Preview");
                            if (previewAttribute.isExpanded)
                            {
//                                EditorGUI.indentLevel++;
//                                Rect previewRect = position;
//                                previewRect.y += EditorGUIUtils.LineHeight;
//                                previewRect.x = (previewRect.width * 0.5f) - (previewSettings.measure * 0.5f);
//                                previewRect.width = previewSettings.measure;
//                                previewRect.height = previewSettings.measure;
//                                additionalHeight += previewSettings.measure + EditorGUIUtils.LineHeight;
                                DrawPreviewTexture(position, previewSettings.measure, previewSettings.scaleMode);
//                                EditorGUI.DrawPreviewTexture(previewRect, currentPreview, currentMaterial, previewSettings.scaleMode);

                                EditorGUI.indentLevel--;
                            }

                            EditorGUI.indentLevel--;
                        }
                    }
                    else
                    {
                        if (currentPreview != null)
                        {
//                            EditorGUI.indentLevel++;
//                            Rect previewRect = position;
//                            previewRect.y += EditorGUIUtils.LineHeight;
//                            previewRect.x = (previewRect.width * 0.5f) - (previewSettings.measure * 0.5f);
//                            previewRect.width = previewSettings.measure;
//                            previewRect.height = previewSettings.measure;
////                            additionalHeight += previewSettings.measure + EditorGUIUtils.LineHeight;
                            DrawPreviewTexture(position, previewSettings.measure, previewSettings.scaleMode);
//                            EditorGUI.DrawPreviewTexture(previewRect, currentPreview, currentMaterial, previewSettings.scaleMode);

                            EditorGUI.indentLevel--;
                        }
                    }

                    GUI.enabled = guiStatus;
                }
                else
                {
                    EditorGUI.LabelField(position, label.text, "Invalid property or attribute!");
                }
            }

            private void DrawPreviewTexture(Rect unEditedRect, float measure, ScaleMode scaleMode)
            {
                Rect previewRect = unEditedRect;
                previewRect.y += EditorGUIUtils.LineHeight;
                previewRect.x = (previewRect.width * 0.5f) - (measure * 0.5f);
                previewRect.width = measure;
                previewRect.height = measure;
                EditorGUI.DrawPreviewTexture(previewRect, currentPreview, currentMaterial, scaleMode);
            }
        }
#endif
        #endregion
    }
}
#endif