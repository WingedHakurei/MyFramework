using System.Diagnostics;
using System.IO;
using MyUtils;
using UnityEditor;
using UnityEngine;

namespace Editor.Luban
{
    public static class LubanEditor
    {
        [MenuItem("MyFramework/Luban/Correct Namespace of the Generated Codes")]
        public static void CorrectNamespace()
        {
            var sw = new Stopwatch();
            sw.Start();
            Helper(Application.dataPath + "/HotUpdate/DataTable");
            MyLogger.Info($"Namespace of the Luban generated codes has been corrected with { sw.Elapsed }.");
            return;

            static void Helper(string directory)
            {
                if (!Directory.Exists(directory))
                {
                    return;
                }

                foreach (var file in Directory.GetFiles(directory))
                {
                    if (!file.EndsWith(".cs"))
                    {
                        continue;
                    }
                    
                    var text = File.ReadAllText(file);
                    text = text
                        .Replace("namespace cfg", "namespace HotUpdate.DataTable")
                        .Replace("global::cfg", "HotUpdate.DataTable");
                    File.WriteAllText(file, text);
                }

                foreach (var sub in Directory.GetDirectories(directory))
                {
                    Helper(sub);
                }
            }
        }

        [MenuItem("MyFramework/Luban/Copy Data Tables to Editor StreamingAssets")]
        public static void CopyDataTablesToEditorStreamingAssets()
        {
            CopyDataTables(PathDefines.EditorStreamingAssets);
            AssetDatabase.Refresh();
            MyLogger.Info("Data tables copied to Editor StreamingAssets.");
        }

        [MenuItem("MyFramework/Luban/Copy Data Tables to Build StreamingAssets")]
        public static void CopyDataTablesToBuildStreamingAssets()
        {
            CopyDataTables(PathDefines.BuildStreamingAssets);
            MyLogger.Info("Data tables copied to Build StreamingAssets.");
        }
        
        [MenuItem("MyFramework/Luban/Copy Data Tables to HotUpdateAssets")]
        public static void CopyDataTablesToHotUpdateAssets()
        {
            CopyDataTables(PathDefines.HotUpdateAssets);
            AssetDatabase.Refresh();
            MyLogger.Info("Data tables copied to HotUpdateAssets.");
        }

        private static void CopyDataTables(string destination)
        {
            const string dataTables = "DataTables/output";
            var sourceRootPath = Path.Combine(Application.dataPath, dataTables);
            if (!Directory.Exists(sourceRootPath))
            {
                MyLogger.Error("不存在DataTables/output目录");
                return;
            }

            var destinationRootPath = Path.Combine(destination, dataTables);
            
            if (Directory.Exists(destinationRootPath))
            {
                Directory.Delete(destinationRootPath, true);
            }
            CopyDirectory(sourceRootPath, destinationRootPath);
        }
        
        private static void CopyDirectory(string sourceDir, string destinationDir)
        {
            var dir = new DirectoryInfo(sourceDir);
            if (!dir.Exists)
            {
                return;
            }
            Directory.CreateDirectory(destinationDir);
            
            foreach (var file in dir.GetFiles())
            {
                if (file.Name.EndsWith(".meta"))
                {
                    continue;
                }
                var targetFilePath = Path.Combine(destinationDir, file.Name);
                file.CopyTo(targetFilePath, true);
            }
    
            foreach (var subDir in dir.GetDirectories())
            {
                var newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                CopyDirectory(subDir.FullName, newDestinationDir);
            }
        }
    }
}