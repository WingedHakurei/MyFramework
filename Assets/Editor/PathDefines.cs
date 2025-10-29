using System.IO;
using UnityEngine;

namespace Editor
{
    public static class PathDefines
    {
        public static string EditorStreamingAssets => Application.streamingAssetsPath;
        public static string BuildStreamingAssets => Path.Combine(Application.dataPath, "../Build/StandaloneWindows64", Application.productName + "_Data", "StreamingAssets/");
        public static string HotUpdateAssets => Path.Combine(Application.dataPath, "HotUpdateAssets/");
    }
}