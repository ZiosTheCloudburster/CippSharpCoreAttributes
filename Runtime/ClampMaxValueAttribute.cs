using System;
using UnityEngine;

namespace CippSharp.Core
{
    /// <summary>
    /// Force a number to not go over a value.
    /// </summary>
    public class ClampMaxValueAttribute : ClampValueAttribute
    {
        public ClampMaxValueAttribute()
        {
            
        }
        
        public ClampMaxValueAttribute(int value)
        {
            this.IntegerMaxValue = value;
        }

        public ClampMaxValueAttribute(float value)
        {
            this.FloatMaxValue = value;
        }
    }
}
