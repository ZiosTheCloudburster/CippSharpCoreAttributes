/* 
    Author: Alessandro Salani (Cippo) 
*/
#if UNITY_EDITOR
using System;
#endif
using UnityEngine;
#if UNITY_EDITOR
using System.IO;
using System.Linq;
using System.Collections.Generic;
using CippSharp.Core;
using UnityEditor;
#endif
using Object = UnityEngine.Object;


namespace CippSharp.Core.Attributes
{
    internal static class AssetDatabaseUtils
    {
        /// <summary>
        /// Retrieve the asset database path during editor.
        /// In build it retrieve "";
        /// </summary>
        /// <param name="asset"></param>
        /// <returns></returns>
        public static string GetAssetPath(Object asset)
        {
#if UNITY_EDITOR
            return AssetDatabase.GetAssetPath(asset);
#else
            return "";
#endif
        }
        
        /// <summary>
        /// Retrieve true if the path of the object is null or empty.
        /// </summary>
        /// <param name="interestedObject"></param>
        /// <returns></returns>
        public static bool IsObjectPathNullOrEmpty<T>(T interestedObject) where T : Object
        {
#if UNITY_EDITOR
            Object target = interestedObject;
            if (interestedObject is Component)
            {
                target = (Object)((interestedObject as Component).gameObject);
            }
            string assethPath = AssetDatabase.GetAssetPath(target);
            return string.IsNullOrEmpty(assethPath);
#else
			return false;
#endif
        }

        #region Load Target Asset
        
        /// <summary>
        /// This works only in editor
        /// </summary>
        /// <param name="filter"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T LoadTargetAsset<T>(string filter) where T : Object
        {
#if UNITY_EDITOR
            List<string> filteredPaths = new List<string>();
            string[] guids = AssetDatabase.FindAssets(filter);
            if (ArrayUtils.IsNullOrEmpty(guids))
            {
                return null;
            }

            foreach (var guid in guids)
            {
                filteredPaths.Add(AssetDatabase.GUIDToAssetPath(guid));
            }
            
            try
            {
                return AssetDatabase.LoadAssetAtPath<T>(filteredPaths.FirstOrDefault());
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }

            return null;
#else
            return Resources.Load<T>(assetName);
#endif
        }

        /// <summary>
        /// Try to load an asset using editor way, but at runtime it fallback in resources.load.
        /// </summary>
        /// <param name="assetName"></param>
        /// <param name="filter"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T LoadTargetAsset<T>(string assetName, string filter) where T : Object
        {
#if UNITY_EDITOR
            List<string> filteredPaths = new List<string>();
            string[] guids = AssetDatabase.FindAssets(filter);
            if (ArrayUtils.IsNullOrEmpty(guids))
            {
                Debug.LogWarning("No assets found.");
                return null;
            }

            foreach (var guid in guids)
            {
                filteredPaths.Add(AssetDatabase.GUIDToAssetPath(guid));
            }
            
            try
            {
                return AssetDatabase.LoadAssetAtPath<T>(filteredPaths.FirstOrDefault(f => Path.GetFileNameWithoutExtension(f) == assetName));
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }

            try
            {
                return Resources.Load<T>(assetName);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }

            return (T)null;
#else
            return Resources.Load<T>(assetName);
#endif
        }
        
        #endregion
  
    }
}
