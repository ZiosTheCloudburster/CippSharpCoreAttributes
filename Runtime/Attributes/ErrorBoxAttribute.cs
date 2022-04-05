
namespace CippSharp.Core.Attributes
{
    public class ErrorBoxAttribute : HelpBoxAttribute
    {
        public ErrorBoxAttribute()
        {
            this.Type = MessageType.Error;
        }
        
        public ErrorBoxAttribute(string message, ShowOptions position = ShowOptions.After) : this ()
        {
            this.Message = message;
            this.Show = position;
        }
        
        public ErrorBoxAttribute(string message, string condition, ShowOptions position = ShowOptions.After) : this (message, position)
        {
            this.Condition = condition;
            this.UseCondition = !string.IsNullOrEmpty(condition);
        }
    }
}
