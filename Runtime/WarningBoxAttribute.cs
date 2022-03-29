using UnityEngine;

namespace CippSharp.Core
{
    public class WarningBoxAttribute : HelpBoxAttribute
    {
        public WarningBoxAttribute()
        {
            this.MessageType = HelpBoxMessageType.Warning;
        }
        
        public WarningBoxAttribute(string message, HelpBoxPosition position = HelpBoxPosition.After) : this ()
        {
            this.Message = message;
            this.Position = position;
        }
        
        public WarningBoxAttribute(string message, string condition, HelpBoxPosition position = HelpBoxPosition.After) : this ()
        {
            this.UseCondition = !string.IsNullOrEmpty(condition);
            this.Condition = condition;
            this.Message = message;
            this.Position = position;
        }
    }
}
