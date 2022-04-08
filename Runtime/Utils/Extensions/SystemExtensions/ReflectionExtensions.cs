using System;
using System.Reflection;

namespace CippSharp.Core.Attributes.Extensions
{
    public static class ReflectionExtensions
    {
        /// <summary>
        /// Is field info
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public static bool IsFieldInfo(this MemberInfo member)
        {
            return ReflectionUtils.IsFieldInfo(member);
        }

        /// <summary>
        /// Is field info
        /// </summary>
        /// <param name="member"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static bool IsFieldInfo(this MemberInfo member, out FieldInfo field)
        {
            return ReflectionUtils.IsFieldInfo(member, out field);
        }
        
        /// <summary>
        /// Is property info 
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public static bool IsPropertyInfo(this MemberInfo member)
        {
            return ReflectionUtils.IsPropertyInfo(member);
        }

        /// <summary>
        /// Is property info 
        /// </summary>
        /// <param name="member"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public static bool IsPropertyInfo(this MemberInfo member, out PropertyInfo property)
        {
            return ReflectionUtils.IsPropertyInfo(member, out property);
        }

        /// <summary>
        /// Is member info
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public static bool IsMethodInfo(this MemberInfo member)
        {
            return ReflectionUtils.IsMethodInfo(member);
        }

        /// <summary>
        /// Is member info
        /// </summary>
        /// <param name="member"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static bool IsMethodInfo(this MemberInfo member, out MethodInfo method)
        {
            return ReflectionUtils.IsMethodInfo(member, out method);
        }
        
        #region Attributes

        /// <summary>
        /// Gets an attribute on an enum field value
        /// </summary>
        /// <typeparam name="T">The type of the attribute you want to retrieve</typeparam>
        /// <param name="enumVal">The enum value</param>
        /// <returns>The attribute of type T that exists on the enum value</returns>
        /// <example><![CDATA[string desc = myEnumVariable.GetAttributeOfType<DescriptionAttribute>().Description;]]></example>
        public static T GetAttributeOfType<T>(this Enum enumVal) where T : Attribute
        {
            return ReflectionUtils.GetAttributeOfType<T>(enumVal);
        }

        /// <summary>
        /// Gets attributes on an enum field value
        /// </summary>
        /// <typeparam name="T">The type of the attribute you want to retrieve</typeparam>
        /// <param name="enumVal">The enum value</param>
        /// <returns>The attribute of type T that exists on the enum value</returns>
        /// <example><![CDATA[string desc = myEnumVariable.GetAttributeOfType<DescriptionAttribute>().Description;]]></example>
        public static T[] GetAttributesOfType<T>(this Enum enumVal) where T : Attribute
        {
            return ReflectionUtils.GetAttributesOfType<T>(enumVal);
        }
        
        #endregion
    }
}
