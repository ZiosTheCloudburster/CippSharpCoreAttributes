#if UNITY_EDITOR
using System;
using UnityEngine;

namespace CippSharp.Core.Attributes.Samples
{
#pragma warning disable 649
    internal class NotEditableSamples : MonoBehaviour
    {
        [Serializable]
        public struct Nested1
        {
            public byte biteMe;
            public bool seriously;
        }
        
        [Serializable]
        public struct CustomData
        {
            public float value0;
            public float value1;
            public Nested1 nestOre;
        }
        
        [Serializable]
        public struct CustomData1
        {
            public bool editValue0;
            
            [ShowIf(nameof(CanShowValue), true, ANotEditableAttribute.ShowMode.ReadOnly)]
            public int value0;

            [ShowIf(nameof(CanShowValue), false, ANotEditableAttribute.ShowMode.ReadOnly)]
            public float value1;

            private bool CanShowValue()
            {
                return editValue0;
            }
        }
        
        public string tooltip0 = "Not Editable Attribute.";
        [NotEditable]
        public float value0 = 1.2f;
        [Space(6)]
        public string tooltip1 = "Not Editable Decorator Attribute.";
        [NotEditableDecorator]
        public float value1 = 3.5f;
        public string tooltip2 = "Nothing.";
        public float value2 = 9.7f;
        [Space(6)]
        [NotEditable]
        public CustomData withNotEditable = new CustomData();
        public string tooltip3 = "Decorator affect only single line.";
        [NotEditableDecorator]
        public CustomData withNotEditableDecorator = new CustomData();
        [Space(6)]
        public Nested1 unNested = new Nested1();
        public string tooltip4 = "Show if attribute. Check also the code for the sample";
        public bool showValue4 = false;
        [ShowIf(nameof(showValue4))] 
        public float value4 = 4.2f;
        [Space(6)]
        public CustomData1 customData1 = new CustomData1();
    }
#pragma warning restore 649
}
#endif