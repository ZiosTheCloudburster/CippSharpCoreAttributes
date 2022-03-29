
namespace CippSharp.Core.Attributes
{
    public abstract class ANotEditableAttribute : AFieldAttribute
    {
        public enum ShowMode : sbyte
        {
            HideInInspector,
            ReadOnly
        }
    }
}
