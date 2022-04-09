#if UNITY_EDITOR
//
// Author: Alessandro Salani (Cippo)
//
using System;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using Object = UnityEngine.Object;

namespace CippSharp.Core.Attributes
{
	public static partial class EditorGUILayoutUtils
	{
		public const string inspectorModePropertyName = "inspectorMode";
		public const string instanceIdLabelValue = "Instance ID";
		public const string identfierLabelValue = "Local Identfier in File";
		public const string selfLabelValue = "Self";
		public const string k_BackingField = SerializedPropertyUtils.k_BackingField;

		private const string PropertyIsNotArrayError = "Property isn't an array.";
		private const string PropertyIsNotValidArrayWarning = "Property isn't a valid array.";
		
	}
}
#endif
