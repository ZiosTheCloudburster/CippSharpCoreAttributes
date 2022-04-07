
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace CippSharp.Core.Attributes
{
    using ScaleMode = UnityEngine.ScaleMode;
    using Settings = APreviewAttribute.Settings;
    
    /// <summary>
    /// Draws custom texture as preview
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class PreviewCustomTextureDecoratorAttribute : PreviewDecoratorAttribute
    {
        /// <summary>
        /// Texture path in resources or in asset database
        /// </summary>
        public string TexturePath { get; protected set; } = "";
        
        protected PreviewCustomTextureDecoratorAttribute()
        {
            
        }
        
        public PreviewCustomTextureDecoratorAttribute(string fieldName, string texturePath) : this()
        {
            this.FieldNameOrIdentifier = fieldName;
            this.TexturePath = texturePath;
        }
        
        public PreviewCustomTextureDecoratorAttribute(string fieldName, string texturePath, bool editable = true, ScaleMode scaleMode = ScaleMode.ScaleToFit, int measure = 128, string shaderPath = "", bool foldable = false)
            : this(fieldName, texturePath)
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
        [CustomPropertyDrawer(typeof(PreviewCustomTextureDecoratorAttribute))]
        public class PreviewCustomTextureDecoratorAttributeDrawer : PreviewDecoratorAttributeDrawer
        {
             public override float GetHeight()
            {
                float height = EditorGUIUtils.LineHeight;
                
                if (this.attribute is PreviewCustomTextureDecoratorAttribute previewCustomTextureDecoratorAttribute)
                {
                    Settings previewSettings = previewCustomTextureDecoratorAttribute.PreviewSettings;

                    property = DecoratorDrawersUtils.GetPropertiesWithAttribute<PreviewCustomTextureDecoratorAttribute>(AttributePredicate).FirstOrDefault();
                    bool AttributePredicate(PreviewCustomTextureDecoratorAttribute c)
                    {
                        return previewCustomTextureDecoratorAttribute.FieldNameOrIdentifier == c.FieldNameOrIdentifier 
                               && previewCustomTextureDecoratorAttribute.TexturePath == c.TexturePath 
                               && previewCustomTextureDecoratorAttribute.PreviewSettings.Equals(c.PreviewSettings);
                    }
                    if (property != null)
                    {
                        currentPreview = AssetPreview.GetAssetPreview(AssetPreview.GetAssetPreview(AssetDatabaseUtils.LoadTargetAsset<Object>(previewCustomTextureDecoratorAttribute.TexturePath)));
                        bool useCustomMaterial = !string.IsNullOrEmpty(previewSettings.shaderPath);
                        currentMaterial = useCustomMaterial ? previewCustomTextureDecoratorAttribute.GetMaterial() : UIDefault;
                      
                        if (previewSettings.foldable)
                        {
                            if (currentPreview == null)
                            {
                                return height;
                            }
                            if (previewCustomTextureDecoratorAttribute.isExpanded)
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
        }
#endif
        #endregion
    }
}
