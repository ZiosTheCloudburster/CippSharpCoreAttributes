//
// Author: Alessandro Salani (Cippo)
//

namespace CippSharp.Core.Extensions
{
	public static class CharExtensions
	{
		/// <summary>
		/// Converts a latin character to the corresponding letter's index in the standard Latin alphabet
		/// </summary>
		/// <param name="value">An upper- or lower-case Latin character</param>
		/// <param name="index"></param>
		/// <returns>The 0-based index of the letter in the Latin alphabet</returns>
		public static bool TryGetIndexInAlphabet(this char value, out int index)
		{
			return CharUtils.TryGetIndexInAlphabet(value, out index);
		}
	}
}
