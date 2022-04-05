
namespace CippSharp.Core.Attributes
{
    /// <summary>
    /// Similar to <see cref="HelpBoxAttribute"/>, but this displays only the HelpBox message.
    ///
    /// Usage: - give this attribute to a string property to read that string property and to display his message.
    ///     - you can use a reference to a string member of the class. You have to specify when is a reflected message
    /// </summary>
    public class HelpBoxLabelAttribute : HelpBoxAttribute
    {
        /// <summary>
        /// To Display 'this' string property
        /// </summary>
        public HelpBoxLabelAttribute()
        {
            Show = ShowOptions.HelpBoxOnly;
        }
        
        /// <summary>
        /// To Display 'this' string property
        /// </summary>
        public HelpBoxLabelAttribute(MessageType messageType = MessageType.Info) : this()
        {
            this.Type = messageType;
        }
        
        /// <summary>
        /// To Display 'this' string property if condition is verified (true)
        /// </summary>
        public HelpBoxLabelAttribute(string condition, MessageType messageType = MessageType.Info) : this (messageType)
        {
            this.UseCondition = !string.IsNullOrEmpty(condition);
            this.Condition = condition;
        }
        
        
       /// <summary>
       /// Displays the message
       /// </summary>
       /// <param name="message"></param>
       /// <param name="reflectedMessage"></param>
       /// <param name="messageType"></param>
        public HelpBoxLabelAttribute(string message, bool reflectedMessage, MessageType messageType = MessageType.Info) : this (messageType)
        {
            this.Message = message;
            this.IsReflectedMessage = reflectedMessage;
        }
        
        /// <summary>
        /// Displays the message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="reflectedMessage"></param>
        /// <param name="condition"></param>
        /// <param name="messageType"></param>
        public HelpBoxLabelAttribute(string message, bool reflectedMessage, string condition, MessageType messageType = MessageType.Info) : this (message, reflectedMessage, messageType)
        {
            this.UseCondition = !string.IsNullOrEmpty(condition);
            this.Condition = condition;
        }
    }
}
