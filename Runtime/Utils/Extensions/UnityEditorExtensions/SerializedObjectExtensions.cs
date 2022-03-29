#if UNITY_EDITOR
using CippSharp.Core;
using UnityEditor;

namespace CippSharpEditor.Core.Extensions
{
    public static class SerializedObjectExtensions
    {
        /// <summary>
        /// To override the inspector of a SerializedObject
        ///
        /// Usage:
        /// serializedObject.Update();
        /// serializedObject.DrawCascadeInspector(callback);
        /// serializedObject.ApplyModifiedProperties(); 
        /// </summary>
        /// <param name="serializedObject"></param>
        /// <param name="drawPropertyDelegate"></param>
        /// <returns></returns>
        public static bool DrawCascadeInspector(this SerializedObject serializedObject, DrawSerializedPropertyDelegate drawPropertyDelegate)
        {
            return SerializedObjectUtils.DrawInspector(serializedObject, drawPropertyDelegate);
        }
    }
}
#endif
