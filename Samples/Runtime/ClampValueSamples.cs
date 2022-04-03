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

        [Space(5)]
        [TextArea(1, 3)]
        public string tooltip1 = "Clamp with decorator is possible? Yes, but you need to reference the property.";

        [ClampValueDecorator(nameof(minValueClampedAtMinusOne), -1, ClampValueDecoratorAttribute.Behaviour.ClampMinOnly)]
        public int minValueClampedAtMinusOne = -1;
        [ClampValueDecorator(nameof(maxValueClampedAt3dot1415), 3.1415f, ClampValueDecoratorAttribute.Behaviour.ClampMaxOnly)]
        public float maxValueClampedAt3dot1415 = 1;
        [ClampValueDecorator(nameof(betweenMinusOneAndTwo), -1.0f, 2.0f)]
        public float betweenMinusOneAndTwo = 1;
    }
}
