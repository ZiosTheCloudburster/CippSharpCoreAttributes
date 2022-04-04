#if UNITY_EDITOR

using System;
using UnityEngine;

namespace CippSharp.Core.Attributes.Samples
{
    internal class OtherAttributesSamples : MonoBehaviour
    {
        [Flags]
        public enum FlagExample
        {
            Option1,
            Option2,
            Option3,
            Option4
        }

        //BitMask drawer
        [BitMask]
        public FlagExample bitmaskDrawer = FlagExample.Option1;

        //Display name drawer
        [Space(5)] 
        [DisplayName("Property with a less Ugly Name")]
        public int propertyWithUglyName = 1;
    }
}
#endif