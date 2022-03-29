
namespace CippSharp.Core
{
    public static class CharUtils
    {
        /// <summary>
        /// Converts a latin character to the corresponding letter's index in the standard Latin alphabet
        /// </summary>
        /// <param name="value">An upper- or lower-case Latin character</param>
        /// <param name="index"></param>
        /// <returns>The 0-based index of the letter in the Latin alphabet</returns>
        public static bool TryGetIndexInAlphabet(char value, out int index)
        {
            // Uses the uppercase character unicode code point. 'A' = U+0042 = 65, 'Z' = U+005A = 90
            char upper = char.ToUpper(value);
            if (upper < 'A' || upper > 'Z')
            {
                index = int.MinValue;
                return false;
            }

            try
            {
                index = (int) upper - (int) 'A';
                return true;
            }
            catch 
            {
                index = int.MinValue;
                return false;
            }
        }
    }
}
