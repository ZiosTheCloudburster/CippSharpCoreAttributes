
namespace CippSharp.Core.Attributes
{
    /// <summary>
    /// Display a bool as a button in inspector that works as a toggle
    /// </summary>
    public class BoolButtonToggleAttribute : BoolButtonAttribute
    {
        public BoolButtonToggleAttribute()
        {
            this.Mode = Behaviour.Toggle;
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
