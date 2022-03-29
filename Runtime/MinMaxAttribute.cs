
namespace CippSharp.Core
{
    public class MinMaxAttribute : AMinMaxAttribute
    {
        public MinMaxAttribute()
        {
            
        }
        
        public MinMaxAttribute(float min, float max, string firstProperty = Constants.x, string secondProperty = Constants.y) : this ()
        {
            this.Min = min;
            this.Max = max;
            this.firstPropertyName = firstProperty;
            this.secondPropertyName = secondProperty;
        }
    }
}
