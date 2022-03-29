//
// Author: Alessandro Salani (Cippo)
//

#if UNITY_EDITOR
using System.Collections.Generic;
using CippSharp.Core;
using UnityEditor;

namespace CippSharpEditor.Core
{
    public static class SerializedPropertyExtensions
    {
	    /// <summary>
	    /// It retrieves all serialized properties from <param name="serializedObject"></param> iterator.
	    /// </summary>
	    /// <param name="serializedObject"></param>
	    /// <returns></returns>
	    public static SerializedProperty[] GetAllProperties(this SerializedObject serializedObject)
	    {
		    return SerializedPropertyUtils.GetAllProperties(serializedObject);
	    }

	    public static IEnumerable<SerializedProperty> GetChildren(this SerializedProperty property)
	    {
		    return SerializedPropertyUtils.GetChildren(property);
	    }

	    /// <summary>
		/// Retrieve a brother property of the interested one.
		/// </summary>
		/// <param name="property"></param>
		/// <param name="brotherPropertyName"></param>
		/// <returns></returns>
	    public static SerializedProperty FindBrotherProperty(this SerializedProperty property, string brotherPropertyName)
		{
			return SerializedPropertyUtils.FindBrotherProperty(property, brotherPropertyName);
		}
	    
	    /// <summary>
	    /// Retrieve a serialized property that is an array as an array of serialized properties.
	    /// </summary>
	    /// <param name="property"></param>
	    /// <returns></returns>
	    public static SerializedProperty[] ToArray(this SerializedProperty property)
	    {
		    return SerializedPropertyUtils.ToArray(property);
	    }
	 
		#region Serialized Property generic Get Set of Values
   
	    /// <summary>
	    /// Retrieve an array from serialized property array.
	    /// </summary>
	    /// <param name="property"></param>
	    /// <typeparam name="T"></typeparam>
	    /// <returns></returns>
	    public static List<T> ToValueList<T>(this SerializedProperty property)
	    {
		    return SerializedPropertyUtils.GetList<T>(property);
	    }
		
		/// <summary>
		/// Retrieve value from serialized property relative if possible.
		/// </summary>
		/// <param name="property"></param>
		/// <param name="propertyRelative"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T GetValue<T>(this SerializedProperty property, string propertyRelative)
		{
			return SerializedPropertyUtils.GetValue<T>(property, propertyRelative);
		}

		/// <summary>
		/// Retrieve value from serialized property if possible.
		/// </summary>
		/// <param name="property"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T GetValue<T>(this SerializedProperty property)
		{
			return SerializedPropertyUtils.GetValue<T>(property);
		}
	
		/// <summary>
		/// Overwrite array element values.
		/// </summary>
		/// <param name="property"></param>
		/// <param name="objects"></param>
		/// <typeparam name="T"></typeparam>
		public static void SetValues<T>(this SerializedProperty property, T[] objects)
		{
			SerializedPropertyUtils.SetValues<T>(property, objects);
		}
		
		/// <summary>
		/// Set value to serialized property relative if possible.
		/// </summary>
		/// <param name="property"></param>
		/// <param name="propertyRelative"></param>
		/// <param name="value"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static void SetValue<T>(this SerializedProperty property, string propertyRelative, T value)
		{
			SerializedPropertyUtils.SetValue<T>(property, propertyRelative, value);
		}
		
		/// <summary>
		/// Set value to serialized property if possible.
		/// </summary>
		/// <param name="property"></param>
		/// <param name="value"></param>
		/// <typeparam name="T"></typeparam>
		public static void SetValue<T>(this SerializedProperty property, T value)
		{
			SerializedPropertyUtils.SetValue<T>(property, value);
		}

		#endregion
    }
}
#endif
