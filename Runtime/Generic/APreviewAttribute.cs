
namespace CippSharp.Core.Attributes
{
    using ScaleMode = UnityEngine.ScaleMode;
    using Material = UnityEngine.Material;
    using Shader = UnityEngine.Shader;
    
    /// <summary>
    /// Abstract class for preview attributes. In this case custom editor are specific for each inherited class
    /// </summary>
    public abstract class APreviewAttribute : ACustomPropertyAttribute
    {
        #region class Settings
        
        [System.Serializable]
        public class Settings : System.IEquatable<Settings>
        {
            public static readonly Settings Default = new Settings()
            {
                editable = true,
                scaleMode = ScaleMode.ScaleToFit,
                measure = 128,
                shaderPath = "",
                foldable = false,
            };
            
            /// <summary>
            /// This property should be editable?
            /// </summary>
            public bool editable = true;
            /// <summary>
            /// <see cref="UnityEngine.ScaleMode"/> for more details.
            /// </summary>
            public ScaleMode scaleMode = ScaleMode.ScaleToFit;
            /// <summary>
            /// Square by measure*measure
            /// </summary>
            public int measure = 128;
            /// <summary>
            /// Do you want to override default shader?
            /// </summary>
            public string shaderPath = string.Empty;
            /// <summary>
            /// Add a foldout when drawing the preview?
            /// </summary>
            public bool foldable = false;

            public Settings()
            {
                
            }

            #region Equality Members

            public bool Equals(Settings other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return editable == other.editable && scaleMode == other.scaleMode && measure == other.measure && string.Equals(shaderPath, other.shaderPath) && foldable == other.foldable;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((Settings) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = editable.GetHashCode();
                    hashCode = (hashCode * 397) ^ (int) scaleMode;
                    hashCode = (hashCode * 397) ^ measure;
                    hashCode = (hashCode * 397) ^ (shaderPath != null ? shaderPath.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ foldable.GetHashCode();
                    return hashCode;
                }
            }
            
            #endregion
        }
        
        #endregion
        
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
    }
}
