using UnityEngine;

namespace CippSharp.Core
{
    public class BoolButtonCallbackAttribute : BoolButtonAttribute
    {
        public BoolButtonCallbackAttribute(string methodCallback, BoolButtonBehaviour attributeBehaviour = BoolButtonBehaviour.Press, bool showValue = false, GUIButtonStyle graphicStyle = GUIButtonStyle.MiniButton)
        {
            this.AttributeBehaviour = attributeBehaviour;
            
            this.UseCallback = !string.IsNullOrEmpty(methodCallback);
            this.MethodCallback = methodCallback;
            
            this.ShowValue = showValue;
            this.GraphicStyle = graphicStyle;
        }
    }
}
