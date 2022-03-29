using System;
using UnityEngine;

namespace CippSharp.Core.Attributes
{
    /// <summary>
    /// Base class to derive custom property attributes from. Use this to create custom attributes for script variables.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public abstract class AFieldAttribute : PropertyAttribute
    {
       
    }
}
