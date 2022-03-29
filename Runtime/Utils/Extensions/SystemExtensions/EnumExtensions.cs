//
// Author: Alessandro Salani (Cippo)
//

using System;

namespace CippSharp.Core.Extensions
{
	public static class EnumExtensions
	{
		/// <summary>
		/// Determines if an enum has the given flag defined bitwise.
		/// Fallback equivalent to .NET's Enum.HasFlag().
		/// </summary>
		public static bool HasFlag(this Enum value, Enum flag)
		{
			return EnumUtils.HasFlag(value, flag);
		}	
	}
}
