using UnityEngine;

namespace CippSharp.Core
{
    public static class IntUtils
    {
//        #region Rolls 
//        /// <summary>
//        /// Roll a d100 with integer numbers.
//        /// </summary>
//        /// <returns></returns>
//        public static int intD100()
//        {
//            return Random.Range(0, 101);
//        }
//
//        #endregion

        public static bool IsOdd(int i)
        {
            return i % 2 == 1;
        }

//        /// <summary>
//        /// How many odd numbers are in 'i'?
//        /// </summary>
//        /// <param name="i"></param>
//        /// <returns></returns>
//        public static int OddCount(int i)
//        {
//            return Mathf.CeilToInt(i * 0.5f);
//        }

//        /// <summary>
//        /// How many even numbers are in 'i'? 
//        /// </summary>
//        /// <param name="i"></param>
//        /// <returns></returns>
//        public static int EvenCount(int i)
//        {
//            return 1 + Mathf.FloorToInt(i * 0.5f);
//        }
    }
}
