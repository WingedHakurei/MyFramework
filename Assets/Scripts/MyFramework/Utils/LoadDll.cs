using System.Linq;
using UnityEngine;
#if !UNITY_EDITOR
using System.IO;
using System.Reflection;
#endif

namespace MyFramework.Utils
{
    public class LoadDll : MonoBehaviour
    {
        private void Start()
        {
            // Editor环境下，HotUpdate.dll.bytes已经被自动加载，不需要加载，重复加载反而会出问题。
#if !UNITY_EDITOR
            var hotUpdateAss = Assembly.Load(File.ReadAllBytes($"{Application.streamingAssetsPath}/HotUpdate/HotUpdate.dll.bytes"));
#else
            // Editor下无需加载，直接查找获得HotUpdate程序集
            var hotUpdateAss = System.AppDomain.CurrentDomain.GetAssemblies().First(a => a.GetName().Name == "HotUpdate");
#endif
            var type = hotUpdateAss.GetType("HotUpdate.Hello");
            type.GetMethod("Run")?.Invoke(null, null);
        }
    }
}