using UnityEngine;

namespace CippSharp.Core
{
    public class BoolButtonToggleAttribute : BoolButtonAttribute
    {
        public BoolButtonToggleAttribute()
        {
            this.AttributeBehaviour = BoolButtonBehaviour.Toggle;
            this.ShowValue = true;
        }
        
        public BoolButtonToggleAttribute(bool showValue = true, GUIButtonStyle graphicStyle = GUIButtonStyle.MiniButton) : this ()
        {
            this.ShowValue = showValue;
            this.GraphicStyle = graphicStyle;
        }
        
        public BoolButtonToggleAttribute(string methodCallback = "", bool showValue = true, GUIButtonStyle graphicStyle = GUIButtonStyle.MiniButton) : this ()
        {
            this.UseCallback = !string.IsNullOrEmpty(methodCallback);
            this.MethodCallback = methodCallback;
            
            this.ShowValue = showValue;
            this.GraphicStyle = graphicStyle;
        }
    }
}
