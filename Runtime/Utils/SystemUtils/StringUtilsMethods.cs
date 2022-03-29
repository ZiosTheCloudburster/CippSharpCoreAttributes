using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CippSharp.Core
{
    public static partial class StringUtils
    {
        /// <summary>
        /// Add a string to another
        /// </summary>
        /// <returns></returns>
        public static string Add(string value, string other)
        {
            return string.Concat(value, other);
        }
        
        //Chars Utils
        
        /// <summary>
        /// Add spaces before capital letters
        /// https://stackoverflow.com/questions/272633/add-spaces-before-capital-letters
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string AddSpacesBeforeCapitalLetters(string value)
        {
            return Regex.Replace(value, "([a-z])([A-Z])", "$1 $2");
        }
        
        /// <summary>
        /// Put the first valid Char of a string to UpperCase after specific chars.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="chars"></param>
        /// <returns></returns>
        public static string CapitalizeAfter(string s, IEnumerable<char> chars)
        {
            var charsHash = new HashSet<char>(chars);
            StringBuilder sb = new StringBuilder(s);
            for (int i = 0; i < sb.Length - 2; i++)
            {
                if (charsHash.Contains(sb[i]) && sb[i + 1] == ' ')
                    sb[i + 2] = char.ToUpper(sb[i + 2]);
            }
            return sb.ToString();
        }
        
        /// <summary>
        /// Encode bytes in UTF8 string
        /// </summary>
        /// <param name="bytes">must be not null</param>
        /// <returns></returns>
        public static string EncodeBytes(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);;
        }
        
        /// <summary>
        /// Ensure that a string ends with a certain char.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static string EnsureEndingChar(string input, char c)
        {
            if (input.EndsWith(c.ToString()))
            {
                return input;
            }
            else
            {
                return (input + c);
            }
        }
          
        /// <summary>
        /// Return true if a string is equal to, or contains value
        /// </summary>
        /// <returns></returns>
        public static bool EqualOrContains(string input, string value)
        {
            return (input == value) || (input.Contains(value));
        }

        #region Firt Char to
        
        /// <summary>
        /// Put the first Char of a string to UpperCase
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FirstCharToUpper(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            return string.Format("{0}{1}", value.First().ToString().ToUpper(), value.Substring(1));
        }
        
        /// <summary>
        /// Put the first Char of a string to LowerCase
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FirstCharToLower(string input)
        {
            string newString = input;
            if (!string.IsNullOrEmpty(newString) && char.IsUpper(newString[0]))
            {
                newString = char.ToLower(newString[0]) + newString.Substring(1);
            }
            return newString;
        }
        
        #endregion
        
        /// <summary>
        /// If a string ends with number this code increments that
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string IncrementEndingNumber(string value)
        {
            var r = value;
            var name = value;
            string num = "";
            for (int k = 1; k < name.Length; k++)
            {
                if(int.TryParse(name.Substring(name.Length - k), out int n))
                {
                    num = name.Substring(name.Length - k);
                }
            }

            if (num != "")
            {
                r = r.Substring(0, name.Length - num.Length) + (int.Parse(num) + 1);
            }
            else
            {
                r += "1";
            }
            
            return r;
        }
        
        /// <summary>
        /// Is this string contained in string array?
        /// </summary>
        /// <param name="value"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool IsContained(string value, string[] values)
        {
            return values.Any(s => s == value);
        }
           
        /// <summary>
        /// Retrieve a line as multiline with tabs
        /// </summary>
        /// <param name="value"></param>
        /// <param name="tabLevel"></param>
        /// <param name="maxCharactersPerLine"></param>
        /// <returns></returns>
        public static string MultiLineTab(string value, int tabLevel, int maxCharactersPerLine = 50)
        {
            string tabs = string.Empty;
            for (int i = 0; i < tabLevel; i++)
            {
                tabs += "\t";
            }

            string finalValue = "";
            
            if (value.Length > maxCharactersPerLine)
            {
                string[] valueSplitResult = Split(value, maxCharactersPerLine).ToArray();
                for (int i = 0; i < valueSplitResult.Length; i++)
                {
                    string valueSplit = valueSplitResult[i];
                    finalValue += $"{tabs}{valueSplit}{Environment.NewLine}";
                }
            }
            else
            {
                finalValue = $"{tabs}{value}{Environment.NewLine}";
            }

            return finalValue;
        }
        
        /// <summary>
        /// Zero padding string.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static string PaddingFormat(int amount)
        {
            string s = "";
            for (int i = 0; i < amount; i++)
            {
                s += "0";
            }

            return s;
        }

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
        
        /// <summary>
        /// Split string into chunks of specified size.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="chunkSize"></param>
        /// <returns></returns>
        public static IEnumerable<string> Split(string str, int chunkSize = 50)
        { 
            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize));
        }

        /// <summary>
        /// Write a string array as flat string
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string ToFlatArray(IEnumerable<string> enumerable, string separator = "")
        {
            string last = enumerable.Last();
            return enumerable.Aggregate(string.Empty, (current, s) => current + $"{s}{(s != last ? separator : string.Empty)}");
        }

    }
}
