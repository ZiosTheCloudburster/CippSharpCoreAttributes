
namespace CippSharp.Core.Attributes
{
    public class HelpBoxAttribute : AHelpBoxAttribute
    {
        protected HelpBoxAttribute()
        {
            
        }

        public HelpBoxAttribute(string message, ShowOptions position = ShowOptions.After, MessageType messageType = MessageType.Info) 
            : this ()
        {
            this.Message = message;
            this.Show = position;
            this.Type = messageType;
        }
        
        public HelpBoxAttribute(string message, string condition, ShowOptions position = ShowOptions.After, MessageType messageType = MessageType.Info) 
            : this (message, position, messageType)
        {
            this.Condition = condition;
            this.UseCondition = !string.IsNullOrEmpty(condition);
        }

        public HelpBoxAttribute(string message, bool isReflectedMessage, string condition, ShowOptions position = ShowOptions.After, MessageType messageType = MessageType.Info) 
            : this(message, condition, position, messageType)
        {
            this.IsReflectedMessage = isReflectedMessage;
            
        }
    }
}
