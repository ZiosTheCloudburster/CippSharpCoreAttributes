using UnityEngine;

namespace CippSharp.Core.Attributes.Samples
{
    public class ClampValueSamples : MonoBehaviour
    {
        public string tooltip0 = "Clamp min & max value drawers samples.";
        [ClampMinValue(0)]
        public int minValueClampedAt0 = 1;
        [ClampMaxValue(2.43f)]
        public float maxValueClampedAt2dot43 = 1;
        [ClampValue(0.0f, 1.0f)]
        public float between0And1 = 0.5f;
    }
}
