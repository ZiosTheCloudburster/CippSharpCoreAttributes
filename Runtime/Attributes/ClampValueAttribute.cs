
namespace CippSharp.Core.Attributes
{
    public class ClampValueAttribute : AClampAttribute
    {
        public ClampValueAttribute()
        {
            
        }

        public ClampValueAttribute(int min, int max)
        {
            this.IntegerMinValue = min;
            this.IntegerMaxValue = max;
        }

        public ClampValueAttribute(float min, float max)
        {
            this.FloatMinValue = min;
            this.FloatMaxValue = max;
        }
    }
}
