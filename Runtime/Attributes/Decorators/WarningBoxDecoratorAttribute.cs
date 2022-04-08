
namespace CippSharp.Core.Attributes
{
    public class WarningBoxDecoratorAttribute : HelpBoxDecoratorAttribute
    {
        protected WarningBoxDecoratorAttribute()
        {
            this.Type = MessageType.Warning;
        }
        
        public WarningBoxDecoratorAttribute(string fieldName, string message) 
            : this ()
        {
            this.FieldNameOrIdentifier = fieldName;
            this.Message = message;
        }
        
        public WarningBoxDecoratorAttribute(string fieldName, string message, string condition) 
            : this (fieldName, message)
        {
            this.Condition = condition;
            this.UseCondition = !string.IsNullOrEmpty(condition);
        }
        
        public WarningBoxDecoratorAttribute(string fieldName, string message, string condition, bool conditionMatch) 
            : this (fieldName, message, condition)
        {
            this.ConditionMatch = conditionMatch;
        }
        
        //With reflected message
        public WarningBoxDecoratorAttribute(string fieldName, string message, bool isReflectedMessage) 
            : this(fieldName, message)
        {
            this.IsReflectedMessage = isReflectedMessage;
        }
        
        public WarningBoxDecoratorAttribute(string fieldName, string message, bool isReflectedMessage, string condition)
            : this(fieldName, message, isReflectedMessage)
        {
            this.Condition = condition;
            this.UseCondition = !string.IsNullOrEmpty(condition);
        }
        
        public WarningBoxDecoratorAttribute(string fieldName, string message, bool isReflectedMessage, string condition, bool conditionMatch)
            : this(fieldName, message, isReflectedMessage, condition)
        {
            this.ConditionMatch = conditionMatch;
        }
    }
}
