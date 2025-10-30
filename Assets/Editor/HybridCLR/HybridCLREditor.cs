using System.IO;
using MyUtils;
using UnityEditor;
using UnityEngine;

namespace Editor.HybridCLR
{
    // ReSharper disable once InconsistentNaming
    public static class HybridCLREditor
    {
        [MenuItem("MyFramework/HybridCLR/Copy HotUpdate DLL to Editor StreamingAssets")]
        public static void CopyHotUpdateDllToEditorStreamingAssets()
        {
            CopyHotUpdateDll(PathDefines.EditorStreamingAssets);
            AssetDatabase.Refresh();
            MyLogger.Info("HotUpdate DLL copied to Editor StreamingAssets.");
        }
        
        [MenuItem("MyFramework/HybridCLR/Copy HotUpdate DLL to HotUpdateAssets")]
        public static void CopyHotUpdateDllToHotUpdateAssets()
        {
            CopyHotUpdateDll(PathDefines.HotUpdateAssets);
            AssetDatabase.Refresh();
            MyLogger.Info("HotUpdate DLL copied to HotUpdateAssets.");
        }
        
        [MenuItem("MyFramework/HybridCLR/Copy HotUpdate DLL to Build StreamingAssets")]
        public static void CopyHotUpdateDllToBuildStreamingAssets()
        {
            CopyHotUpdateDll(PathDefines.BuildStreamingAssets);
            MyLogger.Info("HotUpdate DLL copied to Build StreamingAssets.");
        }

        private static void CopyHotUpdateDll(string destination)
        {
            var sourceFilePath = Path.Combine(Application.dataPath,
                "../HybridCLRData/HotUpdateDlls/StandaloneWindows64/HotUpdate.dll");
            if (!File.Exists(sourceFilePath))
            {
                MyLogger.Error("不存在HotUpdate.dll");
                return;
            }

            var destinationFileRoot = Path.Combine(destination, "HotUpdate");
            var destinationFilePath = Path.Combine(destinationFileRoot, "HotUpdate.dll.bytes");
            
            if (!Directory.Exists(destinationFileRoot))
            {
                Directory.CreateDirectory(destinationFileRoot);
            }
            
            File.Copy(sourceFilePath, destinationFilePath, true);
        }
    }
}