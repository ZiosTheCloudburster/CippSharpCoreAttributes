using System;

namespace CippSharp.Core.Attributes.Extensions
{
    public static class CodeExtensions
    {
        /// <summary>
        /// Tries to call an action from the given element
        /// </summary>
        /// <param name="element"></param>
        /// <param name="action"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static bool Try(this object element, Action action, out string error)
        {
            return CodeUtils.Try(element, action, out error);
        }
        
        /// <summary>
        /// Tries to call an action from the given element
        /// </summary>
        /// <param name="element"></param>
        /// <param name="action"></param>
        public static bool Try(this object element, Action action)
        {
            return CodeUtils.Try(element, action);
        }

        /// <summary>
        /// Tries to call an action from the given element
        /// </summary>
        /// <param name="element"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool Try<T>(this T element, Action<T> action)
        {
            return CodeUtils.Try(element, action);
        }
        
        /// <summary>
        /// Tries to call an action from the given element
        /// </summary>
        /// <param name="element"></param>
        /// <param name="action"></param>
        /// <param name="error"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool Try<T>(this T element, Action<T> action, out string error)
        {
            return CodeUtils.Try(element, action, out error);
        }

        /// <summary>
        /// Perform an action on element
        /// </summary>
        /// <param name="element"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Do<T>(this T element, Action<T> action)
        {
            return CodeUtils.Do(element, action);
        }
    }
}
