
namespace CippSharp.Core.Attributes
{
    /// <summary>
    /// Force a number to not go under a value.
    /// </summary>
    public class ClampMinValueAttribute : ClampValueAttribute
    {
        public ClampMinValueAttribute()
        {
            
        }
        
        public ClampMinValueAttribute(int value)
        {
            this.IntegerMinValue = value;
        }

        public ClampMinValueAttribute(float value)
        {
            this.FloatMinValue = value;
        }
    }
}
