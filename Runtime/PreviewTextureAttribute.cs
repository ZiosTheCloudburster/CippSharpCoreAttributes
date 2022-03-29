#if UNITY_2019_4_OR_NEWER
using System;
using UnityEngine;
#if UNITY_EDITOR
using CippSharp.Core;
using UnityEditor;
#endif

namespace CippSharp.Core
{
    public class PreviewTextureAttribute : APreviewAttribtue
    {
        public PreviewTextureAttribute(bool editable = true, ScaleMode scaleMode = ScaleMode.ScaleToFit, int measure = 128, string shaderPath = "", bool foldable = false)
        {
            this.previewSettings = new PreviewSettings()
            {
                editable = editable,
                scaleMode = scaleMode,
                measure = measure,
                shaderPath = shaderPath,
                foldable = foldable
            };
        }
    }
}

#if UNITY_EDITOR
namespace CippSharpEditor.Core
{
    [CustomPropertyDrawer(typeof(PreviewTextureAttribute))]
    public class PreviewTextureDrawer : PropertyDrawer
    {
        #region Default Material

        private static Material uiDefault = null;

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
        
        private float additionalHeight = 0;
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtils.GetPropertyHeight(property, label) + additionalHeight;
        }
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.ObjectReference)
            {
                Texture2D preview = null;
                Material mat = null;
                if (property.objectReferenceValue is Sprite sprite)
                {
                    preview = AssetPreview.GetAssetPreview(sprite);
                }
                else if (property.objectReferenceValue is Texture texture)
                {
                    preview = AssetPreview.GetAssetPreview(texture);
                }               
                else
                {
                    preview = null;
                    //Its a big trouble
                }
                
                PreviewTextureAttribute previewTextureAttribute = attribute as PreviewTextureAttribute;
                if (previewTextureAttribute != null)
                {
                    PreviewSettings previewSettings = previewTextureAttribute.previewSettings;
                    bool useCustomMaterial = !string.IsNullOrEmpty(previewSettings.shaderPath);
                    if (useCustomMaterial)
                    {
                        mat = previewTextureAttribute.GetMaterial();
                    }
                    else
                    {
                        mat = UIDefault;
                    }
                    additionalHeight = 0;

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
                        if (preview != null)
                        {
                            EditorGUI.indentLevel++;
                            Rect foldoutRect = propertyRect;
                            foldoutRect.y += EditorGUIUtils.LineHeight;
                            additionalHeight += EditorGUIUtils.LineHeight;
                            previewTextureAttribute.isExpanded = EditorGUI.Foldout(foldoutRect,
                                previewTextureAttribute.isExpanded, "Preview");
                            if (previewTextureAttribute.isExpanded)
                            {
                                EditorGUI.indentLevel++;
                                Rect previewRect = position;
                                previewRect.y += EditorGUIUtils.LineHeight;
                                previewRect.x = (previewRect.width * 0.5f) - (previewSettings.measure * 0.5f);
                                previewRect.width = previewSettings.measure;
                                previewRect.height = previewSettings.measure;
                                additionalHeight += previewSettings.measure + EditorGUIUtils.LineHeight;
                                EditorGUI.DrawPreviewTexture(previewRect, preview, mat, previewSettings.scaleMode);

                                EditorGUI.indentLevel--;
                            }

                            EditorGUI.indentLevel--;
                        }
                    }
                    else
                    {
                        if (preview != null)
                        {
                            EditorGUI.indentLevel++;
                            Rect previewRect = position;
                            previewRect.y += EditorGUIUtils.LineHeight;
                            previewRect.x = (previewRect.width * 0.5f) - (previewSettings.measure * 0.5f);
                            previewRect.width = previewSettings.measure;
                            previewRect.height = previewSettings.measure;
                            additionalHeight += previewSettings.measure + EditorGUIUtils.LineHeight;
                            EditorGUI.DrawPreviewTexture(previewRect, preview, mat, previewSettings.scaleMode);

                            EditorGUI.indentLevel--;
                        }
                    }
                    
                    GUI.enabled = guiStatus;
                }
            }
            else
            {
                EditorGUI.LabelField(position, label.text, $"Use {nameof(PreviewTextureAttribute)} with sprite or texture fields fields.");
            }
        }
    }
}
#endif
#endif
