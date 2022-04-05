
namespace CippSharp.Core.Attributes
{
    using MessageType = AHelpBoxAttribute.MessageType;

    public class ErrorBoxDecoratorAttribute : HelpBoxDecoratorAttribute
    {
        protected ErrorBoxDecoratorAttribute()
        {
            this.Type = MessageType.Error;
        }
        
        public ErrorBoxDecoratorAttribute(string fieldName, string message) 
            : this ()
        {
            this.FieldNameOrIdentifier = fieldName;
            this.Message = message;
        }
        
        public ErrorBoxDecoratorAttribute(string fieldName, string message, string condition) 
            : this (fieldName, message)
        {
            this.Condition = condition;
            this.UseCondition = !string.IsNullOrEmpty(condition);
        }
        
        public ErrorBoxDecoratorAttribute(string fieldName, string message, string condition, bool conditionMatch) 
            : this (fieldName, message, condition)
        {
            this.ConditionMatch = conditionMatch;
        }
        
        //With reflected message
        public ErrorBoxDecoratorAttribute(string fieldName, string message, bool isReflectedMessage) 
            : this(fieldName, message)
        {
            this.IsReflectedMessage = isReflectedMessage;
        }
        
        public ErrorBoxDecoratorAttribute(string fieldName, string message, bool isReflectedMessage, string condition)
            : this(fieldName, message, isReflectedMessage)
        {
            this.Condition = condition;
            this.UseCondition = !string.IsNullOrEmpty(condition);
        }
        
        public ErrorBoxDecoratorAttribute(string fieldName, string message, bool isReflectedMessage, string condition, bool conditionMatch)
            : this(fieldName, message, isReflectedMessage, condition)
        {
            this.ConditionMatch = conditionMatch;
        }
    }
}
