#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;
#endif

namespace CippSharp.Core.Attributes
{
    public class ClampValueDecoratorAttribute : ACustomPropertyAttribute, System.IEquatable<ClampValueDecoratorAttribute>
    {
        public enum Behaviour : sbyte
        {
            //Undefined, hidden
            //Undefined = -1,
            
            //Clamp min and max, hidden
            //Default = 0,
            
            /// <summary>
            /// Clamp min value only
            /// </summary>
            ClampMinOnly = 1,
            /// <summary>
            /// Clamp max value only
            /// </summary>
            ClampMaxOnly = 2,
        }
        
        public string FieldNameOrIdentifier { get; protected set; } = string.Empty;
        //initial value is undefined -1, default value to clamp min and max is 0
        public Behaviour Mode { get; protected set; } = (Behaviour)(sbyte)-1;
        
        public int IntegerMinValue { get; protected set; } = 0;
        public float FloatMinValue { get; protected set; } = 0;
        
        public int IntegerMaxValue { get; protected set; } = 1;
        public float FloatMaxValue { get; protected set; } = 1;

        /// <summary>
        /// Default constructor is private
        /// </summary>
        protected ClampValueDecoratorAttribute()
        {
            
        }
        
        public ClampValueDecoratorAttribute(string fieldName, int value, Behaviour behaviour) : this ()
        {
            this.FieldNameOrIdentifier = fieldName;
            this.Mode = behaviour;
            switch (this.Mode)
            {
                case Behaviour.ClampMinOnly:
                    this.IntegerMinValue = value;
                    break;
                case Behaviour.ClampMaxOnly:
                    this.IntegerMaxValue = value;
                    break;
            }   
        }

        public ClampValueDecoratorAttribute(string fieldName, float value, Behaviour behaviour) : this ()
        {
            this.FieldNameOrIdentifier = fieldName;
            this.Mode = behaviour;
            switch (this.Mode)
            {
                case Behaviour.ClampMinOnly:
                    this.FloatMinValue = value;
                    break;
                case Behaviour.ClampMaxOnly:
                    this.FloatMaxValue = value;
                    break;
            } 
        }

        public ClampValueDecoratorAttribute(string fieldName, int minValue, int maxValue) : this()
        {
            this.FieldNameOrIdentifier = fieldName;
            this.Mode = (Behaviour)(sbyte)0;
            this.IntegerMinValue = minValue;
            this.IntegerMaxValue = maxValue;
        }
        
        public ClampValueDecoratorAttribute(string fieldName, float minValue, float maxValue) : this()
        {
            this.FieldNameOrIdentifier = fieldName;
            this.Mode = (Behaviour)(sbyte)0;
            this.FloatMinValue = minValue;
            this.FloatMaxValue = maxValue;
        }

        #region Equality Members

        public bool Equals(ClampValueDecoratorAttribute other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && string.Equals(FieldNameOrIdentifier, other.FieldNameOrIdentifier) && Mode == other.Mode && IntegerMinValue == other.IntegerMinValue && FloatMinValue.Equals(other.FloatMinValue) && IntegerMaxValue == other.IntegerMaxValue && FloatMaxValue.Equals(other.FloatMaxValue);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ClampValueDecoratorAttribute) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ (FieldNameOrIdentifier != null ? FieldNameOrIdentifier.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int) Mode;
                hashCode = (hashCode * 397) ^ IntegerMinValue;
                hashCode = (hashCode * 397) ^ FloatMinValue.GetHashCode();
                hashCode = (hashCode * 397) ^ IntegerMaxValue;
                hashCode = (hashCode * 397) ^ FloatMaxValue.GetHashCode();
                return hashCode;
            }
        }

        #endregion
        
        #region Attribute Utils (editor Only)
#if UNITY_EDITOR
        /// <summary>
        /// Clamp integer
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private int ClampInteger(int value)
        {
            switch ((sbyte)Mode)
            {
                case 0:
                    return Mathf.Clamp(value, IntegerMinValue, IntegerMaxValue);
                case 1:
                    return value < IntegerMinValue ? IntegerMinValue : value;
                case 2:
                    return value > IntegerMaxValue ? IntegerMaxValue : value;
            }

            return value;
        }

        /// <summary>
        /// Clamp float
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private float ClampFloat(float value)
        {
            switch ((sbyte)Mode)
            {
                case 0:
                    return Mathf.Clamp(value, FloatMinValue, FloatMaxValue);
                case 1:
                    return value < FloatMinValue ? FloatMinValue : value;
                case 2:
                    return value > FloatMaxValue ? FloatMaxValue : value;
            }

            return value;
        }
#endif
        #endregion
        
        #region Custom Editor
#if UNITY_EDITOR
        [CustomPropertyDrawer(typeof(ClampValueDecoratorAttribute), true)]
        public class ClampValueDecoratorAttributeDrawer : DecoratorDrawer
        {
            public override float GetHeight()
            {
                return 0.0f;
            }

            public override void OnGUI(Rect position)
            {
                if (this.attribute is ClampValueDecoratorAttribute clampValueDecoratorAttribute)
                {
                    if (!string.IsNullOrEmpty(clampValueDecoratorAttribute.FieldNameOrIdentifier))
                    {
                       EverybodyDoTheClampCheck(clampValueDecoratorAttribute);
                    }
                }
            }

            /// <summary> 
            /// Dance: https://youtu.be/L5inD4XWz4U
            /// </summary>
            /// <param name="clampValueDecoratorAttribute"></param>
            private static void EverybodyDoTheClampCheck/*Flop*/(ClampValueDecoratorAttribute clampValueDecoratorAttribute)
            {
                DecoratorDrawersUtils.IteratePropertiesWithAttribute<ClampValueDecoratorAttribute>(AttributePredicate, CheckDelegate);
                bool AttributePredicate(ClampValueDecoratorAttribute c)
                {
                    return clampValueDecoratorAttribute.Equals(c);
                }
                void CheckDelegate(Object target, SerializedObject serializedObject, SerializedProperty[] properties)
                {
                    serializedObject.Update();
                    foreach (SerializedProperty property in properties)
                    {
                        switch (property.propertyType)
                        {
                            case SerializedPropertyType.Integer:
                                property.intValue = clampValueDecoratorAttribute.ClampInteger(property.intValue);
                                break;
                            case SerializedPropertyType.Float:
                                property.floatValue = clampValueDecoratorAttribute.ClampFloat(property.floatValue);
                                break;
                        }
                       
                    }
                    serializedObject.ApplyModifiedProperties();
                }
            }
        }
#endif
        #endregion
    }
}
