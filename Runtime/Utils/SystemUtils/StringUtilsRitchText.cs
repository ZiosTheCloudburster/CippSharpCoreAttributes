using System.Text.RegularExpressions;

namespace CippSharp.Core
{
    using Color = UnityEngine.Color;
    using ColorUtility = UnityEngine.ColorUtility;
    
    public static partial class StringUtils
    {
        #region Unity Rich Text

         /// <summary>
        /// Add italic.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Italic(string value)
        {
            return string.Format("<i>{0}</i>", value);
        }

        /// <summary>
        /// Remove italic.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string UnItalic(string value)
        {
            return value.Replace("<i>", string.Empty).Replace("</i>", string.Empty);
        }

        /// <summary>
        /// Add bold
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Bold(string value)
        {
            return string.Format("<b>{0}</b>", value);
        }

        /// <summary>
        /// Remove bold
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string UnBold(string value)
        {
            return value.Replace("<b>", string.Empty).Replace("</b>", string.Empty);
        }

        /// <summary>
        /// Set the string to that color.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string OfColor(string value, Color color)
        {
            return string.Format("<color=#{1}>{0}</color>", value, ColorUtility.ToHtmlStringRGBA(color));
        }

        /// <summary>
        /// Remove the specific color from a string.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string UnColor(string value, Color color)
        {
            return value.Replace(string.Format("<color=#{0}>", ColorUtility.ToHtmlStringRGBA(color)), string.Empty).Replace("</color>", string.Empty);
        }

        /// <summary>
        /// Remove any color from a string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string UnColor(string value)
        {
            return Regex.Replace(value.Replace("</color>", string.Empty), "<color=#.*?>", string.Empty);
        }

        /// <summary>
        /// Set the char size to a specific value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string OfSize(string value, int size)
        {
            return string.Format("<size={1}>{0}</size>", value, size.ToString());
        }

        /// <summary>
        /// Remove the size from the string.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string UnSize(string value)
        {
            return Regex.Replace(value.Replace("</size>", string.Empty), "<size=.*?>", string.Empty);
        }

        /// <summary>
        /// Remove any rich text utility from string.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string UnRichTextString(string value)
        {
            return Regex.Replace(value, "<.*?>", string.Empty);
        }

        #endregion
    }
}
