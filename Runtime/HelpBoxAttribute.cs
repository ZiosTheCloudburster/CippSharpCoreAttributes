
namespace CippSharp.Core
{
    public class HelpBoxAttribute : AHelpBoxAttribute
    {
        public HelpBoxAttribute()
        {
            
        }

        public HelpBoxAttribute(string message, HelpBoxPosition position = HelpBoxPosition.After, HelpBoxMessageType messageType = HelpBoxMessageType.Info)
        {
            this.Message = message;
            this.Position = position;
            this.MessageType = messageType;
        }
        
        public HelpBoxAttribute(string message, string condition, HelpBoxPosition position = HelpBoxPosition.After, HelpBoxMessageType messageType = HelpBoxMessageType.Info)
        {
            this.UseCondition = !string.IsNullOrEmpty(condition);
            this.Condition = condition;
            this.Message = message;
            this.Position = position;
            this.MessageType = messageType;
        }
    }
}
