using UnityEngine;
#if !UNITY_EDITOR
using System.IO;
using System.Reflection;
#endif

namespace MyFramework
{
    public class GameMain : MonoBehaviour
    {
        private void Start()
        {
#if !UNITY_EDITOR
            var hotUpdateAss = Assembly.Load(File.ReadAllBytes($"{Application.streamingAssetsPath}/HotUpdate/HotUpdate.dll.bytes"));
#endif
            
        }
    }
}