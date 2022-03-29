using System;

namespace CippSharp.Core
{
    public static class CodeUtils
    {
        #region Try Generic

        /// <summary>
        /// Tries to call an action from the given element
        /// </summary>
        /// <param name="element"></param>
        /// <param name="action"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static bool Try(object element, Action action, out string error)
        {
            error = string.Empty;
            try
            {
                action.Invoke();
                return true;
            }
            catch (Exception e)
            {
                error = e.Message;
                return false;
            }
        }

        /// <summary>
        /// Tries to call an action from the given element
        /// </summary>
        /// <param name="element"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool Try(object element, Action action)
        {
            try
            {
                action.Invoke();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Try 'T' Reference
         
        /// <summary>
        /// Tries to call an action from the given element
        /// </summary>
        /// <param name="element"></param>
        /// <param name="action"></param>
        /// <param name="error"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool Try<T>(T element, Action<T> action, out string error)
        {
            error = string.Empty;
            try
            {
                action.Invoke(element);
                return true;
            }
            catch (Exception e)
            {
                error = e.Message;
                return false;
            }
        }

        /// <summary>
        /// Tries to call an action from the given element
        /// </summary>
        /// <param name="element"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool Try<T>(T element, Action<T> action)
        {
            try
            {
                action.Invoke(element);
                return true;
            }
            catch
            {
                return false;
            }
        }
       
        #endregion
        
        #region Do
        
        /// <summary>
        /// Perform an action on an element.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Do<T>(T element, Action<T> action)
        {
            action.Invoke(element);
            return element;
        }
        
        /// <summary>
        /// Perform an action on an ref element.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Do<T>(T element, RefAction<T> action)
        {
            action.Invoke(ref element);
            return element;
        }
        
        #endregion
    }
}
