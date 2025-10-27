using System.IO;
using HotUpdate.DataTable;
using QFramework;
using SimpleJSON;
using UnityEngine;

namespace HotUpdate.Utility
{
    public class DataTableUtility : IUtility
    {
        public Tables Root { get; private set; }
        
        public DataTableUtility()
        {
            var dataTableDir = Path.Combine(Application.streamingAssetsPath, "DataTables/output");
            Root = new Tables(file => JSON.Parse(File.ReadAllText($"{dataTableDir}/{file}.json")));   
        }
    }
}