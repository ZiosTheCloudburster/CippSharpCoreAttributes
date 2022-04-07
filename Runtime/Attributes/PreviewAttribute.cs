#if UNITY_2019_4_OR_NEWER
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;
#endif

namespace CippSharp.Core.Attributes
{
    public class PreviewAttribute : APreviewAttribute
    {
        public PreviewAttribute()
        {
            this.PreviewSettings = Settings.Default;
        }
        
        public PreviewAttribute(bool editable = true, UnityEngine.ScaleMode scaleMode = UnityEngine.ScaleMode.ScaleToFit, int measure = 128, string shaderPath = "", bool foldable = false)
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

        public PreviewAttribute(bool editable = true, bool foldable = false, UnityEngine.ScaleMode scaleMode = UnityEngine.ScaleMode.ScaleToFit, int measure = 128, string shaderPath = "")
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
        [CustomPropertyDrawer(typeof(PreviewAttribute), false)]
        public class PreviewAttributeDrawer : PropertyDrawer
        {
            #region Default Material

            /// <summary>
            /// Backing field
            /// </summary>
            private static Material uiDefault = null;

            /// <summary>
            /// Unity's default ui material to draw the preview texture
            /// </summary>
            protected static Material UIDefault
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

            protected Texture2D currentPreview = null;
            protected Material currentMaterial = null;

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
                if (this.attribute is PreviewAttribute previewAttribute && property.propertyType == SerializedPropertyType.ObjectReference)
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

            protected void DrawPreviewTexture(Rect unEditedRect, float measure, ScaleMode scaleMode)
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