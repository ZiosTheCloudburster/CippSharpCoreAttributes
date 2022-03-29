using System;
using UnityEngine;
#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using CippSharp.Core;
using CippSharp.Core.Extensions;
using UnityEditor;
using UnityEditor.Callbacks;
#endif

namespace CippSharp.Core
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class SceneSelectorAttribute : PropertyAttribute
    {
        public bool ShowNotEnabledScenes { get; protected set; } = false;

        public SceneSelectorAttribute()
        {
            
        }

        public SceneSelectorAttribute(bool showNotEnabledScenes)
        {
            this.ShowNotEnabledScenes = showNotEnabledScenes;
        }
    }
}

#if UNITY_EDITOR
namespace CippSharpEditor.Core
{
    [CustomPropertyDrawer(typeof(SceneSelectorAttribute))]
    public class SceneSelectorPropertyDrawer : PropertyDrawer
    {
        #region Initialization
        
        private static KeyValuePair<string, bool>[] SceneNames = new KeyValuePair<string, bool>[0];
        
        [InitializeOnLoadMethod, DidReloadScripts]
        private static void Initialize()
        {
            UpdateSceneNames();
            
            EditorBuildSettings.sceneListChanged -= OnEditorBuildSettingsSceneListChanged;
            EditorBuildSettings.sceneListChanged += OnEditorBuildSettingsSceneListChanged;
        }

        private static void OnEditorBuildSettingsSceneListChanged()
        {
            UpdateSceneNames();
        }

        private static void UpdateSceneNames()
        {
            List<KeyValuePair<string, bool>> tmpSceneNames = new List<KeyValuePair<string, bool>>();
            tmpSceneNames.Add(new KeyValuePair<string, bool>("None", true));
            tmpSceneNames.AddRange(EditorBuildSettings.scenes.Select(s => new KeyValuePair<string, bool>(AssetDatabase.LoadAssetAtPath<SceneAsset>(s.path).name, s.enabled)).ToArray());
            SceneNames = tmpSceneNames.ToArray();
        }
        
        #endregion

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (SceneNames.IsNullOrEmpty())
            {
                UpdateSceneNames();
            }
            
            if (property.propertyType == SerializedPropertyType.String)
            {
                EditorGUI.BeginProperty(position, label, property);

                SceneSelectorAttribute sceneSelectorAttribute = this.attribute as SceneSelectorAttribute;
                if (sceneSelectorAttribute != null)
                {
                    List<KeyValuePair<string, bool>> currentPairs = SceneNames.Where(Predicate).ToList();
                    bool Predicate(KeyValuePair<string, bool> scene)
                    {
                        return sceneSelectorAttribute.ShowNotEnabledScenes || scene.Value;
                    }
                    
                    bool isLastDeprecated = false;
                    int currentIndex = 0;
                    string currentValue = property.stringValue;
                    if (string.IsNullOrEmpty(currentValue))
                    {
                        currentIndex = 0;
                    }
                    else if (currentPairs.Any(s => s.Key == currentValue, out int index))
                    {
                        currentIndex = index;
                    }
                    else
                    {
                        currentPairs.Add(new KeyValuePair<string, bool>(currentValue, true));
                        currentIndex = currentPairs.Count - 1;
                        isLastDeprecated = true;
                    }

                    string[] displayNames = currentPairs.Select(s => s.Key).ToArray();
                    if (isLastDeprecated)
                    {
                        displayNames[displayNames.Length - 1] += " (deprecated)";
                    }
                    currentIndex = EditorGUI.Popup(position, label.text, currentIndex, displayNames);

                    if (currentIndex == 0)
                    {
                        property.stringValue = string.Empty;
                    }
                    else
                    {
                        property.stringValue = currentPairs[currentIndex].Key;
                    }
                }

                EditorGUI.EndProperty();
            }
            else
            {
                EditorGUI.PropertyField(position, property, label);
            }
        }
    }
}
#endif