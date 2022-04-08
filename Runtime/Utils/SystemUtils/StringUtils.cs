using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace CippSharp.Core
{
    internal static class StringUtils
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
        
        #region Replacement

        /// <summary>
        /// Remove special characters from a string 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="replace"></param>
        /// <returns></returns>
        public static string RemoveSpecialCharacters(string input, string replace = "")
        {
            Regex reg = new Regex("[*'\",_&#^@]");
            input = reg.Replace(input, replace);

            Regex reg1 = new Regex("[ ]");
            input = reg.Replace(input, "-");
            return input;
        }
        
        /// <summary>
        /// Replace last occurrence of match.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="match"></param>
        /// <param name="replace"></param>
        /// <returns></returns>
        public static string ReplaceLastOccurrence(string source, string match, string replace)
        {
            int place = source.LastIndexOf(match);

            if (place == -1)
            {
                return source;
            }

            string result = source.Remove(place, match.Length).Insert(place, replace);
            return result;
        }
        
        /// <summary>
        /// Replace empty lines
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="replace"></param>
        /// <returns></returns>
        public static string ReplaceEmptyLine(string lines, string replace = "")
        {
            return Regex.Replace(lines, @"^\s*$\n|\r", replace, RegexOptions.Multiline).TrimEnd();
        }
        
        /// <summary>
        /// Replace new lines
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="replace"></param>
        /// <returns></returns>
        public static string ReplaceNewLine(string lines, string replace = "")
        {
            return Regex.Replace(lines, @"\r\n?|\n", replace, RegexOptions.Multiline).TrimEnd();
        }
        
        
        /// <summary>
        /// Replace any of the values with the replace value.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="values"></param>
        /// <param name="replace"></param>
        /// <returns></returns>
        public static string ReplaceAny(string input, string[] values, string replace = "")
        {
            return values.Aggregate(input, (current, t) => current.Replace(t, replace));
        }

        #endregion
    }
}
