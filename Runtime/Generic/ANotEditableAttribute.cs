
namespace CippSharp.Core.Attributes
{
    /// <summary>
    /// Abstract class for not editable attributes
    /// </summary>
    public abstract class ANotEditableAttribute : ACustomPropertyAttribute
    {
        public enum ShowMode : sbyte
        {
            HideInInspector,
            ReadOnly,
        }
    }
}
