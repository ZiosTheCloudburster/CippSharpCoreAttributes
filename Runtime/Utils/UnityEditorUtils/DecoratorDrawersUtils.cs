#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CippSharp.Core.Extensions;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CippSharp.Core.Attributes
{
    internal static class DecoratorDrawersUtils
    {
        public delegate void PropertiesWithAttributeDelegate(Object target, SerializedObject serializedObject, SerializedProperty[] properties);
        
        /// <summary>
        /// Iterate all properties with attribute
        /// </summary>
        /// <param name="attributePredicate"></param>
        /// <param name="delegate"></param>
        /// <typeparam name="T"></typeparam>
        public static void IteratePropertiesWithAttribute<T>(Predicate<T> attributePredicate, PropertiesWithAttributeDelegate @delegate) where T : PropertyAttribute
        {
            SerializedObjectUtils.GetActiveEditorTargetsObjectsPairs().ForEach(Filter);
            void Filter(KeyValuePair<Editor, Object[]> pair)
            {
                foreach (var o in pair.Value)
                {
                    SerializedObject serializedObject = new SerializedObject(o);
                    SerializedProperty[] properties = GetPropertiesWithAttribute(serializedObject, attributePredicate);
                    if (ArrayUtils.IsNullOrEmpty(properties))
                    {
                        continue;
                    }
                    
                    @delegate.Invoke(o, serializedObject, properties);
                }
            }
        }

        /// <summary>
        /// Retrieve all properties with attribute
        /// </summary>
        /// <param name="attributePredicate"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static SerializedProperty[] GetPropertiesWithAttribute<T>(Predicate<T> attributePredicate) where T : PropertyAttribute
        {
            List<SerializedProperty> properties = new List<SerializedProperty>();
            SerializedObjectUtils.GetActiveEditorTargetsObjectsPairs().ForEach(Filter);
            void Filter(KeyValuePair<Editor, Object[]> pair)
            {
                foreach (var o in pair.Value)
                {
                    SerializedObject serializedObject = new SerializedObject(o);
                    properties.AddRange(GetPropertiesWithAttribute(serializedObject, attributePredicate));
                }
            }

            return properties.ToArray();
        }

        /// <summary>
        /// Get properties with attribute T on target serialized object
        /// </summary>
        /// <param name="serializedObject"></param>
        /// <param name="attributePredicate"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static SerializedProperty[] GetPropertiesWithAttribute<T>(SerializedObject serializedObject, Predicate<T> attributePredicate) where T : PropertyAttribute
        {
            List<SerializedProperty> propertiesWithAttribute = new List<SerializedProperty>();
            SerializedProperty[] allProperties = SerializedPropertyUtils.GetAllProperties(serializedObject);
            foreach (var property in allProperties)
            {
                if (HasAttribute(property, attributePredicate))
                {
                    propertiesWithAttribute.Add(property);
                }

                if (property.propertyType == SerializedPropertyType.Generic && (property.isExpanded && property.hasChildren))
                {
                    SearchInChildren(property, attributePredicate, ref propertiesWithAttribute);
                }
            }
                
            return propertiesWithAttribute.ToArray();
        }

        /// <summary>
        /// Iterate children properties recursively, but avoid not expanded or properties with no children
        /// </summary>
        /// <param name="property"></param>
        /// <param name="attributePredicate"></param>
        /// <param name="propertiesWithAttribute"></param>
        private static void SearchInChildren<T>(SerializedProperty property, Predicate<T> attributePredicate, ref List<SerializedProperty> propertiesWithAttribute)  where T : PropertyAttribute
        {
            List<SerializedProperty> tmpPropertiesWithAttribute = new List<SerializedProperty>();
            SerializedPropertyUtils.IterateAllChildren(property, OnIterate);
            void OnIterate(SerializedProperty childProperty)
            {
                if (HasAttribute(childProperty, attributePredicate))
                {
                    //You MUST use child property.Copy() to save the iteration in current state.
                    tmpPropertiesWithAttribute.Add(childProperty.Copy());
                }
            }
            
            propertiesWithAttribute.AddRange(tmpPropertiesWithAttribute);
        }

        /// <summary>
        /// Has attribute?
        /// </summary>
        /// <param name="property"></param>
        /// <param name="attributePredicate"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool HasAttribute<T>(SerializedProperty property, Predicate<T> attributePredicate) where T : PropertyAttribute
        {
            try
            {
                FieldInfo fieldInfo = MirroredScriptAttributeUtility.GetFieldInfoFromProperty(property, out Type type);
                List<PropertyAttribute> attributes = MirroredScriptAttributeUtility.GetPropertyAttributes(fieldInfo);
                return !ArrayUtils.IsNullOrEmpty(attributes) && attributes.Any(a => a is T attributeT && attributePredicate.Invoke(attributeT));
            }
            catch
            {
                //Ignored
                return false;
            }
        }
    }
}
#endif