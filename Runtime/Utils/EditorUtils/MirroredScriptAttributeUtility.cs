#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CippSharp.Core.Attributes.Extensions;
using UnityEditor;
using UnityEngine;

namespace CippSharp.Core.Attributes
{
    internal static class MirroredScriptAttributeUtility
    {
        #region Mirrored Type
        
        /// <summary>
        /// Mirrored type backing field
        /// </summary>
        private static Type mirroredType = null;
        
        /// <summary>
        /// Mirrored Type of this class
        /// </summary>
        /// <returns></returns>
        public static Type MirroredType
        {
            get
            {
                if (mirroredType != null)
                {
                    return mirroredType;
                }
                ReflectionUtils.FindType("UnityEditor.ScriptAttributeUtility", out Type foundType);
                mirroredType = foundType;
                return mirroredType;
            }
        }

        /// <summary>
        /// Is mirrored type != null?
        /// </summary>
        public static bool IsValidMirroredType => MirroredType != null;

        #endregion

        #region Instance

        /// <summary>
        /// Instance backing field
        /// </summary>
        private static object instance = null;

        /// <summary>
        /// Lazy Instance
        /// </summary>
        public static object Instance
        {
            get
            {
                if (instance != null)
                {
                    return instance;
                }

                var mType = MirroredType;
                if (mType != null)
                {
                    instance = Activator.CreateInstance(mType);
                }

                return instance;
            }
        }
        

        #endregion
        
        /// <summary>
        /// Get the PropertyAttributes of a Field
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static List<PropertyAttribute> GetPropertyAttributes(FieldInfo field)
        {
            Type mType = MirroredType;
            if (mType == null)
            {
                return null;
            }
            try
            {
                MethodInfo method = mType.GetMethod("GetFieldAttributes", ReflectionUtils.Common);
                return (List<PropertyAttribute>)method.Invoke(null, new[] {(object) field});
            }
            catch (Exception e)
            {
               Debug.Log($"Failed for {e.Message}");
            }

            return null;
        }

        /// <summary>
        /// Get the property relative FieldInfo and FieldType
        /// </summary>
        /// <param name="property"></param>
        /// <param name="fieldType"></param>
        /// <returns></returns>
        public static FieldInfo GetFieldInfoFromProperty(SerializedProperty property, out Type fieldType)
        {
            fieldType = null;
            if (property == null)
            {
                Debug.LogError("Property is null");
                return null;
            }
            
            Type mType = MirroredType;
            if (mType == null)
            {
                return null;}

            
            try
            {
                
                MethodInfo method = mType.GetMethod("GetFieldInfoFromProperty", ReflectionUtils.Common);
                //for out parameter, array[1] is 'empty'
                object[] parameters = new[] {(object) property, null,};
                FieldInfo targetField = null;
                targetField = (FieldInfo)method.Invoke(null, parameters);
                fieldType = parameters[1] as Type;
                return targetField;
            }
            catch (Exception e)
            {
                Debug.Log($"{nameof(GetFieldInfoFromProperty)}() failed on property {property.propertyPath} for exception: {e.Message}, stack: {e.StackTrace}.", property.serializedObject.targetObject);
            }
            
            return null;
        }

        /// <summary>
        /// Has attribute of type T?
        /// </summary>
        /// <param name="property"></param>
        /// <param name="attribute"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool HasAttribute<T>(SerializedProperty property, out T attribute) where T : PropertyAttribute
        {
            FieldInfo fieldInfo = GetFieldInfoFromProperty(property, out Type type);
            var attributes = GetPropertyAttributes(fieldInfo).Where(a => a is T).ToArray();
            attribute = (T) attributes.FirstOrDefault(a => a is T);
            return !ArrayUtils.IsNullOrEmpty(attributes) && attribute != null;
        }

        /// <summary>
        /// Has attributes of type T?
        /// </summary>
        /// <param name="property"></param>
        /// <param name="attributes"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool HasAttributes<T>(SerializedProperty property, out T[] attributes) where T : PropertyAttribute
        {
            FieldInfo fieldInfo = GetFieldInfoFromProperty(property, out Type type);
            attributes = GetPropertyAttributes(fieldInfo).SelectIf(a => a is T, a => (T)a).ToArray();
            return !ArrayUtils.IsNullOrEmpty(attributes);
        }
    }
}
#endif
