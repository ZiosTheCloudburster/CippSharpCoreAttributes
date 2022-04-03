
using System;
using System.Collections.Generic;
using System.Reflection;
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
        /// Ma non mi dire
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
        /// XLAM
        /// </summary>
        /// <param name="property"></param>
        /// <param name="fieldType"></param>
        /// <returns></returns>
        public static FieldInfo GetFieldInfoFromProperty(SerializedProperty property, out System.Type fieldType)
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
    }
}
