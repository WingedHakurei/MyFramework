using System.IO;
using MyUtils;
using UnityEditor;
using UnityEngine;

namespace MyFramework.Editor.HybridCLR
{
    // ReSharper disable once InconsistentNaming
    public static class HybridCLREditor
    {
        [MenuItem("MyFramework/Move HotUpdate DLL to Editor StreamingAssets")]
        public static void MoveHotUpdateDllToEditorStreamingAssets()
        {
            var originalFilePath = Path.Combine(Application.dataPath,
                "../HybridCLRData/HotUpdateDlls/StandaloneWindows64/HotUpdate.dll");
            if (!File.Exists(originalFilePath))
            {
                MyLogger.Error("不存在HotUpdate.dll");
                return;
            }
            
            var targetFileRoot = Path.Combine(Application.streamingAssetsPath, "HotUpdate");
            var targetFilePath = Path.Combine(targetFileRoot, "HotUpdate.dll.bytes");
            
            if (!Directory.Exists(targetFileRoot))
            {
                Directory.CreateDirectory(targetFileRoot);
            }
            
            File.Copy(originalFilePath, targetFilePath, true);
            AssetDatabase.Refresh();
        }
        
        [MenuItem("MyFramework/Move HotUpdate DLL to Build StreamingAssets")]
        public static void MoveHotUpdateDllToBuildStreamingAssets()
        {
            var originalFilePath = Path.Combine(Application.dataPath,
                "../HybridCLRData/HotUpdateDlls/StandaloneWindows64/HotUpdate.dll");
            if (!File.Exists(originalFilePath))
            {
                MyLogger.Error("不存在HotUpdate.dll");
                return;
            }
            
            var targetFileRoot = Path.Combine(Application.dataPath, "../Build/StandaloneWindows64", Application.productName + "_Data", "StreamingAssets/HotUpdate");
            var targetFilePath = Path.Combine(targetFileRoot, "HotUpdate.dll.bytes");
            
            if (!Directory.Exists(targetFileRoot))
            {
                Directory.CreateDirectory(targetFileRoot);
            }
            
            File.Copy(originalFilePath, targetFilePath, true);
        }
    }
}