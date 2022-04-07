#if UNITY_2019_4_OR_NEWER
#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;
#endif

namespace CippSharp.Core.Attributes
{
    using ScaleMode = UnityEngine.ScaleMode;
    using Material = UnityEngine.Material;
    using Shader = UnityEngine.Shader;
    using Settings = APreviewAttribute.Settings;
    
    /// <summary>
    /// Displays a TexturePreview of the item, before the 'field'.
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class PreviewDecoratorAttribute : ACustomPropertyAttribute
    {
        public string FieldNameOrIdentifier { get; protected set; } = string.Empty;

        public Settings PreviewSettings { get; protected set; } = new Settings();

        #region Material
        
        protected Material materialBackingField = null;
        
        /// <summary>
        /// Retrieve current material for preview
        /// </summary>
        /// <returns></returns>
        public virtual Material GetMaterial()
        {
            if (materialBackingField != null)
            {
                return materialBackingField;
            }
            
            materialBackingField = new Material(Shader.Find(PreviewSettings.shaderPath));
            return materialBackingField;
        }
        
        #endregion
        
#if UNITY_EDITOR
        /// <summary>
        /// Used by editor only
        /// </summary>
        public bool isExpanded = true;
#endif
        
        protected PreviewDecoratorAttribute()
        {
            
        }

        public PreviewDecoratorAttribute(string fieldName) : this ()
        {
            this.FieldNameOrIdentifier = fieldName;
        }

        public PreviewDecoratorAttribute(string fieldName, bool editable = true, UnityEngine.ScaleMode scaleMode = UnityEngine.ScaleMode.ScaleToFit, int measure = 128, string shaderPath = "", bool foldable = false) 
            : this(fieldName)
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

        public PreviewDecoratorAttribute(string fieldName, bool editable = true, bool foldable = false, UnityEngine.ScaleMode scaleMode = UnityEngine.ScaleMode.ScaleToFit, int measure = 128, string shaderPath = "")
            : this(fieldName)
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
        [CustomPropertyDrawer(typeof(PreviewDecoratorAttribute))]
        public class PreviewDecoratorAttributeDrawer : DecoratorDrawer
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
            protected SerializedProperty property = null;
            
            public override float GetHeight()
            {
                float height = EditorGUIUtils.LineHeight;
                
                if (this.attribute is PreviewDecoratorAttribute previewDecoratorAttribute)
                {
                    Settings previewSettings = previewDecoratorAttribute.PreviewSettings;

                    property = DecoratorDrawersUtils.GetPropertiesWithAttribute<PreviewDecoratorAttribute>(AttributePredicate).FirstOrDefault();
                    bool AttributePredicate(PreviewDecoratorAttribute c)
                    {
                        return previewDecoratorAttribute.FieldNameOrIdentifier == c.FieldNameOrIdentifier && previewDecoratorAttribute.PreviewSettings.Equals(c.PreviewSettings);
                    }
                    if (property != null && property.propertyType == SerializedPropertyType.ObjectReference)
                    {
                        Object reference = property.objectReferenceValue;
                        currentPreview = AssetPreview.GetAssetPreview(reference);
                        bool useCustomMaterial = !string.IsNullOrEmpty(previewSettings.shaderPath);
                        currentMaterial = useCustomMaterial ? previewDecoratorAttribute.GetMaterial() : UIDefault;
                      
                        if (previewSettings.foldable)
                        {
                            if (currentPreview == null)
                            {
                                return height;
                            }
                            if (previewDecoratorAttribute.isExpanded)
                            {
                                height += previewSettings.measure + EditorGUIUtils.VerticalSpacing;
                            }
                        }
                        else
                        {
                            if (currentPreview != null)
                            {
                                height = previewSettings.measure + EditorGUIUtils.VerticalSpacing;
                            }
                            else
                            {
                                height = 0;
                            }
                        }
                    }
                    else
                    {
                        currentPreview = null;
                        currentMaterial = null;
                    }
                }

                return height;
            }

            public override void OnGUI(Rect position)
            {
                if (this.attribute is PreviewDecoratorAttribute previewDecoratorAttribute)
                {
                    Settings previewSettings = previewDecoratorAttribute.PreviewSettings;

                    bool guiStatus = GUI.enabled;
                    if (previewSettings.editable == false)
                    {
                        GUI.enabled = previewSettings.editable;
                    }

                    if (previewSettings.foldable)
                    {
                        if (currentPreview != null)
                        {
                            EditorGUI.indentLevel++;
                            Rect foldoutRect = position;
                            foldoutRect.height = EditorGUIUtils.SingleLineHeight;
                            previewDecoratorAttribute.isExpanded = EditorGUI.Foldout(foldoutRect, previewDecoratorAttribute.isExpanded, "Preview");
                            
                            if (previewDecoratorAttribute.isExpanded)
                            {
                                EditorGUI.indentLevel++;
                                Rect unEditedRect = foldoutRect;
                                unEditedRect.y += EditorGUIUtils.LineHeight;
                                DrawPreviewTexture(unEditedRect, previewSettings.measure, previewSettings.scaleMode);
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
                    EditorGUI.LabelField(position, "", "Invalid property or attribute!");
                }
            }
            
            protected void DrawPreviewTexture(Rect unEditedRect, float measure, ScaleMode scaleMode)
            {
                Rect previewRect = unEditedRect;
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