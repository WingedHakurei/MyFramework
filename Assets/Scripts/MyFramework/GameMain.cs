using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using HybridCLR;
using MyUtils;
using UnityEngine;
using UnityEngine.AddressableAssets;
#if !UNITY_EDITOR
using System.Reflection;
#endif

namespace MyFramework
{
    public class GameMain : MonoBehaviour
    {
        private void Start()
        {
            StartAsync().Forget();            
        }

        private static async UniTask StartAsync()
        {
            LoadMetadataForAOTAssemblies();
#if !UNITY_EDITOR
            var bytes = await Addressables.LoadAssetAsync<TextAsset>("Assets/HotUpdateAssets/HotUpdate/HotUpdate.dll.bytes").Task.AsUniTask();
            Assembly.Load(bytes.bytes);
#endif
            MyLogger.Info("Load DLL finished.");
            var menu = await Addressables.LoadSceneAsync("Assets/HotUpdateAssets/Scenes/Menu.unity").Task.AsUniTask();
            MyLogger.Info("Load Scene finished.");
            await menu.ActivateAsync();
        }

        private static void LoadMetadataForAOTAssemblies()
        {
            var aotDllList = new List<string>
            {
                "mscorlib.dll",
                "System.dll",
                "System.Core.dll",
                "QFramework.dll",
                "UniTask.dll",
                "Unity.Addressables.dll"
            };

            foreach (var aotDllName in aotDllList)
            {
                var dllBytes = File.ReadAllBytes(Path.Combine(Application.streamingAssetsPath, "AOT", aotDllName + ".bytes"));
                var error = RuntimeApi.LoadMetadataForAOTAssembly(dllBytes, HomologousImageMode.SuperSet);
                //MyLogger.Info($"LoadMetadataForAOTAssemblies: {aotDllName}. ret: {error}. ");
            }
        }
    }
}