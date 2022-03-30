/*
 *  Author: Alessandro Salani (Cippman)
 */

namespace CippSharp.Core.Attributes
{
    /// <summary>
    /// Display a bool as a button in inspector.
    /// </summary>
    public class BoolButtonAttribute : ABoolButtonAttribute
    {
        public BoolButtonAttribute()
        {
            
        }
        
        public BoolButtonAttribute(bool showValue = false, GUIButtonStyle graphicStyle = GUIButtonStyle.MiniButton)
        {
            this.ShowValue = showValue;
            this.GraphicStyle = graphicStyle;
        }
        
        public BoolButtonAttribute(string methodCallback = "", bool showValue = false, GUIButtonStyle graphicStyle = GUIButtonStyle.MiniButton)
        {
            this.UseCallback = !string.IsNullOrEmpty(methodCallback);
            this.MethodCallback = methodCallback;
            
            this.ShowValue = showValue;
            this.GraphicStyle = graphicStyle;
        }
    }
}
