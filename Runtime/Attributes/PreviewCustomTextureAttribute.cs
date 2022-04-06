#if UNITY_2019_4_OR_NEWER
#if UNITY_EDITOR
using System;
using UnityEngine;
using CippSharp.Core;
using UnityEditor;
using Object = UnityEngine.Object;

#endif

namespace CippSharp.Core.Attributes
{
    public class PreviewCustomTextureAttribute : PreviewAttribute
    {
        /// <summary>
        /// Texture path in resources or in asset database
        /// </summary>
        public string TexturePath { get; protected set; } = "";

        protected PreviewCustomTextureAttribute()
        {
            
        }

        public PreviewCustomTextureAttribute(string texturePath) : this()
        {
            this.TexturePath = texturePath;
        }

        public PreviewCustomTextureAttribute(string texturePath, bool editable = true, UnityEngine.ScaleMode scaleMode = UnityEngine.ScaleMode.ScaleToFit, int measure = 128, string shaderPath = "", bool foldable = false)
            : this(texturePath)
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
        [CustomPropertyDrawer(typeof(PreviewCustomTextureAttribute))]
        public class PreviewCustomTextureAttributeDrawer : PreviewAttributeDrawer
        {
            public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            {
                float height = EditorGUIUtils.GetPropertyHeight(property, label);
                if (this.attribute is PreviewCustomTextureAttribute previewAttribute)
                {
                    Settings previewSettings = previewAttribute.PreviewSettings;
                    
                    //Did you know? unity calls get height before gui draws so, you can cache things here! :D
                    currentPreview = AssetPreview.GetAssetPreview(AssetDatabaseUtils.LoadTargetAsset<Object>(previewAttribute.TexturePath));
                    bool useCustomMaterial = !string.IsNullOrEmpty(previewSettings.shaderPath);
                    currentMaterial = useCustomMaterial ? previewAttribute.GetMaterial() : UIDefault;
                    
                    if (previewSettings.foldable)
                    {
                        if (currentPreview != null)
                        {
                            height += EditorGUIUtils.LineHeight;
                            if (previewAttribute.isExpanded)
                            {
                                height += previewSettings.measure;
                            }
                        }
                    }
                    else
                    {
                        if (currentPreview != null)
                        {
                            height += previewSettings.measure;
                        }
                    }
                }

                return height;
            }

            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                if (this.attribute is PreviewCustomTextureAttribute previewAttribute)
                {
                    Settings previewSettings = previewAttribute.PreviewSettings;

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
                                EditorGUI.indentLevel++;
                                DrawPreviewTexture(foldoutRect, previewSettings.measure, previewSettings.scaleMode);
                                EditorGUI.indentLevel--;
                            }
                            EditorGUI.indentLevel--;
                        }
                    }
                    else
                    {
                        if (currentPreview != null)
                        {
                            EditorGUI.indentLevel++;
                            DrawPreviewTexture(position, previewSettings.measure, previewSettings.scaleMode);
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
        }
#endif
        #endregion
    }
}
#endif
