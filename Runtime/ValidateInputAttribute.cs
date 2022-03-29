using UnityEngine;

namespace CippSharp.Core
{
    public class ValidateInputAttribute : AValidateInputAttribute
    {
        /// <summary>
        /// Method's return type must be of bool value
        /// 
        /// Method's may contains a parameter of same type as field. 
        /// </summary>
        public string MethodName { get; private set; }

        /// <summary>
        /// Message is printed if condition's of Method is false 
        /// </summary>
        public string Message { get; private set; }

        public ValidateInputMessageType MessageType { get; private set; }

        public ValidateInputAttribute(string methodName, string message = "", ValidateInputMessageType messageType = ValidateInputMessageType.Warning)
        {
            this.MethodName = methodName;
            this.Message = message;
            this.MessageType = messageType;
        }
    }
}
