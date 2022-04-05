
namespace CippSharp.Core.Attributes
{
    public class WarningBoxAttribute : HelpBoxAttribute
    {
        protected WarningBoxAttribute()
        {
            this.Type = MessageType.Warning;
        }
        
        public WarningBoxAttribute(string message, ShowOptions position = ShowOptions.After) : this ()
        {
            this.Message = message;
            this.Show = position;
        }
        
        public WarningBoxAttribute(string message, string condition, ShowOptions position = ShowOptions.After) : this (message, position)
        {
            this.Condition = condition;
            this.UseCondition = !string.IsNullOrEmpty(condition);
        }
    }
}
