using System.Diagnostics;
using System.IO;
using MyUtils;
using UnityEditor;
using UnityEngine;

namespace MyFramework.Editor.Luban
{
    public static class LubanEditor
    {
        [MenuItem("MyFramework/Luban/Correct Namespace of the Generated Codes")]
        public static void CorrectNamespace()
        {
            var sw = new Stopwatch();
            sw.Start();
            CorrectNamespace(Application.dataPath + "/Scripts/MyFramework/DataTable");
            MyLogger.Info($"Namespace of the Luban generated codes has been corrected with { sw.Elapsed }.");
        }

        private static void CorrectNamespace(string directory)
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
                    .Replace("namespace cfg", "namespace MyFramework.DataTable")
                    .Replace("global::cfg", "MyFramework.DataTable");
                File.WriteAllText(file, text);
            }

            foreach (var sub in Directory.GetDirectories(directory))
            {
                CorrectNamespace(sub);
            }
        }
    }
}