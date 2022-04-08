
namespace CippSharp.Core.Attributes
{
    public abstract class AValidatePropertyAttribute : ACustomPropertyAttribute
    {
        public enum MessageType : sbyte
        {
#if UNITY_EDITOR
            None = UnityEditor.MessageType.None,
            Info = UnityEditor.MessageType.Info,
            Warning = UnityEditor.MessageType.Warning,
            Error = UnityEditor.MessageType.Error,
#else
            None,
            Info,
            Warning,
            Error,
#endif
        }
    }
}
