//
// Author: Alessandro Salani (Cippo)
//

namespace CippSharp.Core.Extensions
{
	public static class FloatExtensions
	{
		/// <summary>
		/// Returns true if the passed measure is equal to the distance with a tolerance
		/// </summary>
		/// <param name="measure"></param>
		/// <param name="distance"></param>
		/// <param name="tolerance"></param>
		/// <returns></returns>
		public static bool IsInRange(this float measure, float distance, float tolerance)
		{
			return FloatUtils.IsInRange(measure, distance, tolerance);
		}

		/// <summary>
		/// Round a measure to target x numbers after comma.
		/// </summary>
		/// <param name="measure"></param>
		/// <param name="places"></param>
		/// <returns></returns>
		public static float Round(this float measure, int places = 1)
		{
			return FloatUtils.Round(measure, places);
		}

		/// <summary>
		/// Retrieve a percentage float of passed one
		/// </summary>
		/// <returns></returns>
		public static float Perc(this float value, float perc)
		{
			return FloatUtils.Perc(value, perc);
		}
	}
}
