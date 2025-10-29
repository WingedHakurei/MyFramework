using Cysharp.Threading.Tasks;
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
#if !UNITY_EDITOR
            var bytes = await Addressables.LoadAssetAsync<TextAsset>("Assets/HotUpdateAssets/HotUpdate/HotUpdate.dll.bytes").Task.AsUniTask();
            Assembly.Load(bytes.bytes);
#endif
            MyLogger.Info("Load DLL finished.");
            var menu = await Addressables.LoadSceneAsync("Assets/HotUpdateAssets/Scenes/Menu.unity").Task.AsUniTask();
            MyLogger.Info("Load Scene finished.");
            await menu.ActivateAsync();
        }
    }
}