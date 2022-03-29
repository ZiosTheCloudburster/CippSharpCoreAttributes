using UnityEngine;

namespace CippSharp.Core
{
    /// <summary>
    /// Read directly the string field as label to display.
    /// </summary>
    public class HelpBoxLabelAttribute : AHelpBoxAttribute
    {
        public HelpBoxLabelAttribute()
        {
            Position = HelpBoxPosition.HelpBoxOnly;
        }
        
        public HelpBoxLabelAttribute(HelpBoxMessageType messageType = HelpBoxMessageType.Info) : this ()
        {
            this.MessageType = messageType;
        }
        
        public HelpBoxLabelAttribute(string message, bool reflectedMessage, HelpBoxMessageType messageType = HelpBoxMessageType.Info) : this ()
        {
            this.Message = message;
            this.IsReflectedMessage = reflectedMessage;
            this.MessageType = messageType;
        }
        
        public HelpBoxLabelAttribute(string condition, HelpBoxMessageType messageType = HelpBoxMessageType.Info) : this ()
        {
            this.UseCondition = !string.IsNullOrEmpty(condition);
            this.Condition = condition;
            this.MessageType = messageType;
        }
        
        public HelpBoxLabelAttribute(string message, bool reflectedMessage, string condition, HelpBoxMessageType messageType = HelpBoxMessageType.Info) : this ()
        {
            this.Message = message;
            this.IsReflectedMessage = reflectedMessage;
            this.UseCondition = !string.IsNullOrEmpty(condition);
            this.Condition = condition;
            this.MessageType = messageType;
        }
    }
}
