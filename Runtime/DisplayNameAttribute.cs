using UnityEngine;

namespace CippSharp.Core
{
    public class DisplayNameAttribute : ADisplayNameAttribute
    {
        public DisplayNameAttribute(string newDisplayName)
        {
            this.NewDisplayName = newDisplayName;
        }
    }
}
