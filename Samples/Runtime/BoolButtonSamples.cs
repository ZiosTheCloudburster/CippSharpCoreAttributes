#if UNITY_EDITOR
using UnityEngine;

namespace CippSharp.Core.Attributes.Samples
{
#pragma warning disable 414
    public class BoolButtonSamples : MonoBehaviour
    {
        [TextArea(1, 3)]
        public string tooltip0 = "Example of BoolButtonCallback (minibutton) with press behaviour, and displayed boolean value.";
        
        [BoolButtonCallback(nameof(PrintHelloWorld), ABoolButtonAttribute.Behaviour.Press, true, GUIButtonStyle.MiniButton)]
        public bool printHelloWorld = false;

        public void PrintHelloWorld()
        {
            Debug.Log("Hello World", this);
        }

        [Space(6)]
        public string tooltip1 = "Example of BoolButtonToggle with callback.";
        [BoolButtonToggle(nameof(SetThisGameObjectActive))] 
        public bool toggleGameObjectActiveStatus = false;
        
        public void SetThisGameObjectActive(bool value)
        {
            gameObject.SetActive(value);
        }
        
    }
#pragma warning restore 414
}
#endif
