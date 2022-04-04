
namespace CippSharp.Core.Attributes
{
    public class DisplayNameSuffixAttribute : ADisplayNameAttribute
    {
        private DisplayNameSuffixAttribute()
        {
            
        }
        
        public DisplayNameSuffixAttribute(string suffix) : this ()
        {
            this.Suffix = suffix;
            this.AddSuffix = true;
        }
    }
}

