using System;
using UnityEngine;

namespace CippSharp.Core
{
    public static partial class StringUtils
    {
        #region Log Name

        /// <summary>
        /// Retrieve a more contextual name for logs, based on typeName.
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static string LogName(string typeName)
        {
            return string.Format("[{0}]: ", typeName);
        }
        
        /// <summary>
        /// Retrieve a more contextual name for logs, based on type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string LogName(Type type)
        {
            return string.Format("[{0}]: ", type.Name);
        }

        /// <summary>
        /// Retrieve a more contextual name for logs, based on type.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string LogName(Type type, Color color)
        {
            return string.Format("[{0}]: ", OfColor(type.Name, color));
        }

        /// <summary>
        /// Retrieve a more contextual name for logs, based on object.
        /// If object is null an empty string is returned.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string LogName(object context)
        {
            return ((object)context == null) ? string.Empty : LogName(context.GetType());
        }
        
        #endregion
    }
}
