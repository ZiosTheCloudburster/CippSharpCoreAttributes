#if UNITY_EDITOR
using UnityEngine;

namespace CippSharp.Core.Attributes.Samples
{
    internal class HelpBoxSamples : MonoBehaviour
    {
        [TextArea(1, 3)]
        public string tooltip0 = "HelpBox Examples. Remember drawers like this doesn't support any other editor customization.";
        [HelpBox("Hello, I'm an help box!", nameof(boolWithHelpBox), AHelpBoxAttribute.ShowOptions.Before)]
        public bool boolWithHelpBox = false;
        [HelpBox("This property field is hidden and only this helpBox is displayed.", AHelpBoxAttribute.ShowOptions.HelpBoxOnly)]
        public string stringHelpBoxOnly = "";

        [Space(6)]
        [WarningBox("You have activated the warning box!", nameof(toggleWarningBox))]
        public bool toggleWarningBox = false;
        [ErrorBox("Its over 10!", nameof(IsOverTen))]
        public float errorBoxWhenOver10 = 9.0f;

        private bool IsOverTen()
        {
            return errorBoxWhenOver10 > 10.0f;
        }

        [Space(6)] 
        [TextArea(1, 3)]
        public string tooltip1 = "HelpBox Decorators. Have fun with them. Just remember to specify the field name.";

        public bool enableErrorBoxOnWithMultipleDecoratorsBox = false;
        [HelpBoxDecorator(nameof(withMultipleDecoratorsBox), "Finally you wrote something!", nameof(IsNotNullOrEmptyWithMultipleDecoratorsBox), true)]
        [WarningBoxDecorator(nameof(withMultipleDecoratorsBox), "This string is null or empty!", nameof(IsNotNullOrEmptyWithMultipleDecoratorsBox), false)]
        [ErrorBoxDecorator(nameof(withMultipleDecoratorsBox), "This is an error box! lol", nameof(enableErrorBoxOnWithMultipleDecoratorsBox))]
        public string withMultipleDecoratorsBox = "Ciao!";

        private bool IsNotNullOrEmptyWithMultipleDecoratorsBox => !string.IsNullOrEmpty(withMultipleDecoratorsBox);
    }
}
#endif
