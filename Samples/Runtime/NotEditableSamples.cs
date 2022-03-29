using System;
using UnityEngine;

namespace CippSharp.Core.Attributes.Samples
{
    public class NotEditableSamples : MonoBehaviour
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
    }
}
