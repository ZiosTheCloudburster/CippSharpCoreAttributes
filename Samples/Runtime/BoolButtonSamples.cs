#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CippSharp.Core.Attributes.Samples
{
#pragma warning disable 649
#pragma warning disable 414
    internal class BoolButtonSamples : MonoBehaviour
    {
        [Serializable]
        public struct CustomData
        {
            public int prankingYouCount;
            [BoolButtonCallback(nameof(PrintAndIncreasePranksCount))]
            public bool printImPrankingYou;
            [BoolButtonCallback(nameof(ClearPrankingCount))]
            public bool clearPrankingCount;
            
            private void PrintAndIncreasePranksCount()
            {
                prankingYouCount++;
                Debug.Log("I'm pranking you.");
            }

            private void ClearPrankingCount()
            {
                prankingYouCount = 0;
            }
        }
        
        #region Inheritance 
        
        [Serializable]
        public class NestData0
        {
            protected virtual void PrintSomething()
            {
                Debug.Log("Something");
            }
        }
        
        [Serializable]
        public class NestData1 : NestData0
        {
            [BoolButtonCallback(nameof(PrintSomething))]
            public bool printSomething = false;
        }
        
        #endregion
        
        [Serializable]
        public class Madness0
        {
            [ButtonDecorator("Print if I'm Crazy", nameof(PrintYes))]
            public bool ImThatCrazy = false;

            private void PrintYes()
            {
                Debug.Log("if i can print this, it's a success!");
            }
        }
        
        [TextArea(1, 3)]
        public string tooltip0 = "Example of BoolButtonCallback (minibutton) with press behaviour, and displayed boolean value.";
        
        [BoolButtonCallback(nameof(PrintHelloWorld), ABoolButtonAttribute.Behaviour.Press, true, GUIButtonStyle.MiniButton)]
        public bool printHelloWorld = false;

        public void PrintHelloWorld()
        {
            Debug.Log("Hello World", this);
        }

        [Space(6)]
        public string tooltip1 = "Example of BoolButtonToggle with callback.";
        [BoolButtonToggle(nameof(SetThisGameObjectActive))] 
        public bool toggleGameObjectActiveStatus = false;
        
        public void SetThisGameObjectActive(bool value)
        {
            gameObject.SetActive(value);
            Debug.Log($"Toggle gameObject active status to: {value}");
        }
        
        
        [Space(6)]
        public string tooltip2 = "Example of Nested Types with BoolButtonCallback.";
        public CustomData customData0 = new CustomData();
        public NestData1 nestData1 = new NestData1();
        
        [Space(6)]
        public string tooltip3 = "Example of ButtonDecorator";
//        [ButtonDecorator("-", nameof(DecreaseUselessCount))]
//        [ButtonDecorator("+", nameof(IncreaseUselessCount))]
        [ButtonDecorator(new []{"Decrease (-),"+nameof(DecreaseUselessCount), "Increase (+),"+nameof(IncreaseUselessCount)})]
        public int uselessCount = 0;

        private void IncreaseUselessCount()
        {
            uselessCount++;
        }

        private void DecreaseUselessCount()
        {
            uselessCount--;
        }
        
        //Lets see if it is
        public Madness0 thisIsMadness = new Madness0();
    }
#pragma warning restore 414
#pragma warning restore 649
}
#endif
