
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

        public static bool IsValidMirroredType
        {
            get { return MirroredType != null; }
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

        public static FieldInfo GetFieldInfoFromProperty(SerializedProperty property, out System.Type type)
        {
            Type mType = MirroredType;
            if (mType == null)
            {
                type = null;
                return null;
            }
            
            
        }
    }
}
