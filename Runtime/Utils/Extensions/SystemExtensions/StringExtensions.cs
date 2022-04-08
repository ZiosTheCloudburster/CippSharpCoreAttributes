//
// Author: Alessandro Salani (Cippo)
//

namespace CippSharp.Core.Attributes.Extensions
{
    public static class StringExtensions
    {
       #region Replacement
        
        /// <summary>
        /// Remove special characters from a string 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="replace"></param>
        /// <returns></returns>
        public static string RemoveSpecialCharacters(this string input, string replace = "")
        {
            return StringUtils.RemoveSpecialCharacters(input, replace);
        }
        
        /// <summary>
        /// Replace last occurrence of match.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="match"></param>
        /// <param name="replace"></param>
        /// <returns></returns>
        public static string ReplaceLastOccurrence(this string source, string match, string replace)
        {
            return StringUtils.ReplaceLastOccurrence(source, match, replace);
        }
        
        /// <summary>
        /// Replace empty lines.
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="replace">string.Empty</param>
        /// <returns></returns>
        public static string ReplaceEmptyLine(this string lines, string replace = "")
        {
            return StringUtils.ReplaceEmptyLine(lines, replace);
        }
        
        /// <summary>
        /// Replace new lines 
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="replace"></param>
        /// <returns></returns>
        public static string ReplaceNewLine(this string lines, string replace = "")
        {
            return StringUtils.ReplaceNewLine(lines, replace);
        }
        
        /// <summary>
        /// Replace any of the values with the replace value.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="values"></param>
        /// <param name="replace"></param>
        /// <returns></returns>
        public static string ReplaceAny(this string input, string[] values, string replace = "")
        {
            return StringUtils.ReplaceAny(input, values, replace);
        }

        #endregion
    }
}
