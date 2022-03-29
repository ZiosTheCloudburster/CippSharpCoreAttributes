using System;
using System.Diagnostics;
using System.IO;

namespace CippSharp.Core
{
    using Debug = UnityEngine.Debug;
    using Object = UnityEngine.Object;
    
    public static class FileUtils
    {
        #region Ensure Directory
        
        /// <summary>
        /// Tries to ensure the existence of a folder
        /// </summary>
        /// <param name="directoryPath">full path of the directory</param>
        public static bool EnsureExistDirectory(string directoryPath)
        {
            return EnsureExistDirectory(directoryPath, null);
        }
        
        
        /// <summary>
        /// Tries to ensure the existence of a folder
        /// </summary>
        /// <param name="directoryPath">full path of the directory</param>
        /// <param name="debugContext"></param>
        public static bool EnsureExistDirectory(string directoryPath, Object debugContext)
        {
            try
            {
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                return true;
            }
            catch (Exception e)
            {
                string logName = StringUtils.LogName(debugContext);
                Debug.LogError(logName+$"{nameof(EnsureExistDirectory)} error: {e.Message}", debugContext);
                return false;
            }
        }
        
        #endregion
        
        /// <summary>
        /// Try to open folder
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="debugContext"></param>
        public static bool TryOpenFolder(string fullPath, Object debugContext = null)
        {
            try
            {
                Process.Start(@Path.GetDirectoryName(fullPath));
                return true;
            }
            catch (Exception e)
            {
                string logName = StringUtils.LogName(debugContext);
                Debug.LogError(logName + $"{nameof(TryOpenFolder)} error: {e.Message}", debugContext);
                return false;
            }
        }
        
        /// <summary>
        /// Try to delete a file.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="debugContext"></param>
        public static void TryDelete(string path, Object debugContext = null)
        {
            try
            {
                File.Delete(path);
            }
            catch (Exception e)
            {
                string logName = StringUtils.LogName(debugContext);
                Debug.LogError(logName + $"{nameof(TryDelete)} error: {e.Message}", debugContext);
            }
        }
        
        /// <summary>
        /// Try to move a file. It also work for file rename.
        /// </summary>
        /// <param name="oldPath"></param>
        /// <param name="newPath"></param>
        /// <param name="debugContext"></param>
        public static void TryMove(string oldPath, string newPath, Object debugContext = null)
        {
            try
            {
                File.Move(oldPath, newPath);
            }
            catch (Exception e)
            {
                string logName = StringUtils.LogName(debugContext);
                Debug.LogError(logName+$"{nameof(TryMove)} error: {e.Message}", debugContext);
            }
        }
    }
}