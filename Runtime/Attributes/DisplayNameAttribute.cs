
namespace CippSharp.Core.Attributes
{
    public class DisplayNameAttribute : ADisplayNameAttribute
    {
        private DisplayNameAttribute()
        {
            
        }
        
        public DisplayNameAttribute(string newDisplayName) : this ()
        {
            this.NewDisplayName = newDisplayName;
            this.OverrideDisplayName = true;
        }

        public DisplayNameAttribute(string prefix = "", string suffix = "") : this ()
        {
            this.Prefix = prefix;
            this.AddPrefix = !string.IsNullOrEmpty(prefix);
            this.Suffix = suffix;
            this.AddSuffix = !string.IsNullOrEmpty(suffix);
        }
    }
}
