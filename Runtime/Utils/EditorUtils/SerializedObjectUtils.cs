#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CippSharp.Core.Attributes
{
    internal static class SerializedObjectUtils
    {
        /// <summary>
        /// Retrieve all targets objects from a <see cref="Editor"/>
        /// </summary>
        /// <param name="editor"></param>
        /// <returns></returns>
        public static Object[] GetTargetObjects(Editor editor)
        {
            return GetTargetObjects(editor.serializedObject);
        }

        /// <summary>
        /// Retrieve all targets objects from a <see cref="SerializedObject"/>
        /// </summary>
        /// <param name="serializedObject"></param>
        /// <returns></returns>
        public static Object[] GetTargetObjects(SerializedObject serializedObject)
        {
            if (serializedObject == null)
            {
                return null;
            }

            Object target = serializedObject.targetObject;
            Object[] targets = serializedObject.targetObjects;
            List<Object> allObjects = new List<Object>();
            if (target != null)
            {
                allObjects.Add(target);
            }
            
            if (ArrayUtils.IsNullOrEmpty(targets))
            {
                return allObjects.ToArray();
            }
            
            foreach (var o in targets)
            {
                if (o == null)
                {
                    continue;
                }

                if (allObjects.Contains(o))
                {
                    continue;
                }
					
                allObjects.Add(o);
            }

            return allObjects.ToArray();
        }

        /// <summary>
        /// Get Pairs between editor and edited objects
        /// </summary>
        /// <returns></returns>
        public static KeyValuePair<Editor, Object[]>[] GetActiveEditorTargetsObjectsPairs()
        {
            Editor[] editors = ActiveEditorTracker.sharedTracker.activeEditors;
            KeyValuePair<Editor, Object[]>[] pairs = new KeyValuePair<Editor, Object[]>[editors.Length];
            for (int i = 0; i < editors.Length; i++)
            {
                Editor editor = editors[i];
                Object[] editorTargets = GetTargetObjects(editor);
                pairs[i] = new KeyValuePair<Editor, Object[]>(editor, editorTargets);
            }
            return pairs;
        }
    }
}
#endif