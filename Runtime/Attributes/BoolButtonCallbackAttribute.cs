
namespace CippSharp.Core.Attributes
{
    /// <summary>
    /// Display a bool as a button in inspector and invoke a callback during on click
    /// </summary>
    public class BoolButtonCallbackAttribute : BoolButtonAttribute
    {
        public BoolButtonCallbackAttribute(string methodCallback, Behaviour mode = Behaviour.Press, bool showValue = false, GUIButtonStyle graphicStyle = GUIButtonStyle.MiniButton)
        {
            this.Mode = mode;
            
            this.UseCallback = !string.IsNullOrEmpty(methodCallback);
            this.MethodCallback = methodCallback;
            
            this.ShowValue = showValue;
            this.GraphicStyle = graphicStyle;
        }
    }
}
