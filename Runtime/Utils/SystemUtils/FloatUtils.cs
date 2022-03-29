using System;
using UnityEngine;

namespace CippSharp.Core
{
    public static class FloatUtils
    {
        /// <summary>
        /// Returns true if a float is equal to target 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool IsEqualTo(float value, float target)
        {
            if (target == value)
            {
                return true;
            }
            else if (target < value)
            {
                return false;
            }
            else if (target > value)
            {
                return false;
            }
            else
            {
                Debug.Log($"{nameof(FloatUtils)}: what is this case?");
                return true;
            }
        }
        
        /// <summary>
        /// Returns true if the passed measure is equal to the distance with a tolerance
        /// </summary>
        /// <param name="measure"></param>
        /// <param name="distance"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static bool IsInRange(float measure, float distance, float tolerance)
        {
            return Math.Abs(measure - distance) < tolerance;
        }
        
        /// <summary>
        /// Retrieve a percentage float of passed one
        /// </summary>
        /// <returns></returns>
        public static float Perc(float value, float perc)
        {
            return value * (perc / 100.00f);
        }

        #region Rolls

        /// <summary>
        /// Roll a d100 with floating numbers.
        /// </summary>
        /// <returns></returns>
        public static float floatD100()
        {
            return UnityEngine.Random.Range(0.00f, 100.00f);
        }
        #endregion
        
        /// <summary>
        /// Round a measure to target x numbers after comma.
        /// </summary>
        /// <param name="measure"></param>
        /// <param name="places"></param>
        /// <returns></returns>
        public static float Round(float measure, int places = 1)
        {
            int amount = 10 * places;
            return (float)Math.Round((measure * amount) / amount);
        }

        /// <summary>
        /// Returns index of largest of two or more values.
        /// </summary>
        /// <returns></returns>
        public static int IndexOfMax(float[] values)
        {
            if (values == null || values.Length == 0)
            {
                return -1;
            }
            
            int length = values.Length;
            float num = values[0];
            int index = 0;
            for (int i = 1; i < length; ++i)
            {
                if ((double) values[i] > (double) num)
                {
                    num = values[i];
                    index = i;
                }
            }
            return index;
        }
    }
}
