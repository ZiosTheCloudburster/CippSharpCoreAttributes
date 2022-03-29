using UnityEngine;

namespace CippSharp.Core
{
    public class DisplayNameSuffixAttribute : ADisplayNameAttribute
    {
        public DisplayNameSuffixAttribute(string suffix)
        {
            this.NewDisplayName = suffix;
        }
    }
}

