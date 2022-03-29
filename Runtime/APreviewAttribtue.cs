using System;
using UnityEngine;

namespace CippSharp.Core
{
    [Serializable]
    public class PreviewSettings
    {
        public ScaleMode scaleMode = ScaleMode.ScaleToFit;
        /// <summary>
        /// Square by measure*measure
        /// </summary>
        public int measure = 128;
        public string shaderPath = string.Empty;
        public bool foldable = false;
        public bool editable = true;
    }
    
    /// <summary>
    /// Abstract class for preview attributes. In this case custom editor are specific for each inherited class
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public abstract class APreviewAttribtue : PropertyAttribute
    {
        public PreviewSettings previewSettings = new PreviewSettings();
        
        protected Material materialBackingField = null;
        
        /// <summary>
        /// Used by editor
        /// </summary>
        public bool isExpanded = true;

        /// <summary>
        /// Retrieve current material for preview
        /// </summary>
        /// <returns></returns>
        public Material GetMaterial()
        {
            if (materialBackingField != null)
            {
                return materialBackingField;
            }
            
            materialBackingField = new Material(Shader.Find(previewSettings.shaderPath));
            return materialBackingField;
        }

    }
}
