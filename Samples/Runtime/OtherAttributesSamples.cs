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
        
        [MinMax(-1, 1)]
        public Vector2 minMaxDrawer = new Vector2(0, 1);

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
        [ButtonDecorator(nameof(value), "Open Image Url", nameof(OpenImageUrl))]
        public float value = 45;

        void OpenImageUrl()
        {
            Application.OpenURL("https://pixabay.com/illustrations/submarine-ocean-sea-underwater-7105870/");
        }

        [Space(6)] 
        [TextArea(1, 3)]
        public string tooltip2 = "Preview Decorator Attributes. The Good thing of decorators is that you can have multiple of them without affecting the drawing of the serialized property.";
        
        /// <summary>
        /// Previews the current field only
        /// </summary>
        [PreviewDecorator(nameof(withDecoratorObject), true, true)]
        [PreviewCustomTextureDecorator(nameof(withDecoratorObject), "AttributesSamplesSubmarine720 t:Texture2D")]
        public Object withDecoratorObject = null;

        [BeginColorDecorator(1, 0, 0, 0.5f)]
        public string highlightSample = "";
        [BeginColorDecorator(0, 1, 1, 0.5f)]
        public bool something = false;

    }
#pragma warning restore 414
}
#endif