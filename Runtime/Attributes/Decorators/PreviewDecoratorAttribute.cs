namespace CippSharp.Core.Attributes
{
    /// <summary>
    /// Displays a TexturePreview of the item, before the 'field'.
    /// </summary>
    public class PreviewDecoratorAttribute : PreviewAttribute
    {
        public string FieldNameOrIdentifier { get; protected set; } = string.Empty;

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
        
        #region Equality Members

                

        #endregion

        #region Custom Editor
#if UNITY_EDITOR
        
#endif
        #endregion
    }
}
