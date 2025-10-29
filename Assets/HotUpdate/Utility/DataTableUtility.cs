using HotUpdate.DataTable;
using QFramework;
using SimpleJSON;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace HotUpdate.Utility
{
    public class DataTableUtility : IUtility
    {
        private const string FileRoot = "Assets/HotUpdateAssets/DataTables/output";
        public Tables Root { get; private set; }
        
        public DataTableUtility()
        {
            Root = new Tables( file =>
            {
                var text = Addressables.LoadAssetAsync<TextAsset>($"{FileRoot}/{file}.json").WaitForCompletion();
                return JSON.Parse(text.text);
            });   
        }
    }
}