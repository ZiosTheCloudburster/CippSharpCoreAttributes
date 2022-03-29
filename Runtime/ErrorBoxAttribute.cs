using UnityEngine;

namespace CippSharp.Core
{
    public class ErrorBoxAttribute : HelpBoxAttribute
    {
        public ErrorBoxAttribute()
        {
            this.MessageType = HelpBoxMessageType.Error;
        }
        
        public ErrorBoxAttribute(string message, HelpBoxPosition position = HelpBoxPosition.After) : this ()
        {
            this.Message = message;
            this.Position = position;
        }
        
        public ErrorBoxAttribute(string message, string condition, HelpBoxPosition position = HelpBoxPosition.After) : this ()
        {
            this.UseCondition = !string.IsNullOrEmpty(condition);
            this.Condition = condition;
            this.Message = message;
            this.Position = position;
        }
    }
}
