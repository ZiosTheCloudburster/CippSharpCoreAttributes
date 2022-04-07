#if UNITY_EDITOR

using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CippSharp.Core.Attributes.Samples
{
#pragma warning disable 414
    internal class OtherAttributesSamples : MonoBehaviour
    {
        [Flags]
        public enum FlagExample
        {
            Option1,
            Option2,
            Option3,
            Option4
        }

        //BitMask drawer
        [BitMask]
        public FlagExample bitmaskDrawer = FlagExample.Option1;

        //Display name drawer
        [Space(6)] 
        [DisplayName("Property with a less Ugly Name")]
        public int propertyWithUglyName = 1;

        [Space(6)] 
        [TagSelector]
        public string tagSelector = ""; 
        
        [Space(6)] 
        [SceneSelector]
        public string sceneSelector = "";

        [Space(16)] 
        [TextArea(1, 3)] 
        public string tooltip0 = "Previews the current field only. Basically any UnityEngine.Object supported by AssetPreview.";
        
        /// <summary>
        /// Previews the current field only
        /// </summary>
        [Preview(true, true)] 
        public Object unityObject = null;
        

        [Space(6)]
        [TextArea(1, 3)] 
        public string tooltip1 = "Displays a custom texture under this field. Just pass the 'filter' to search in AssetDatabase.";
        /// <summary>
        /// Displays a custom texture under this field 
        /// </summary>
        [PreviewCustomTexture("AttributesSamplesSubmarine720 t:Texture2D")]
        [ButtonDecorator("Open Image Url", nameof(OpenImageUrl))]
        public float value = 45;

        void OpenImageUrl()
        {
            Application.OpenURL("https://pixabay.com/illustrations/submarine-ocean-sea-underwater-7105870/");
        }

        [Space(6)] 
        private string tooltip2 = "Preview Decorator Attribute";
        
        /// <summary>
        /// Previews the current field only
        /// </summary>
        [PreviewDecorator(nameof(withDecoratorObject), true, true)] 
        public Object withDecoratorObject = null;

    }
#pragma warning restore 414
}
#endif