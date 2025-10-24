using System.IO;
using MyUtils;
using SimpleJSON;
using UnityEngine;

namespace HotUpdate
{
    public class Program
    {
        public static void Main()
        {
            MyLogger.Info("Hello World");
            
            var dataTableDir = Path.Combine(Application.streamingAssetsPath, "DataTables/output");
            var tables = new DataTable.Tables(file => JSON.Parse(File.ReadAllText($"{dataTableDir}/{file}.json")));
            var item = tables.TbItem.Get(1002);
            MyLogger.Info($"item: {item}");
            
            MyLogger.Info("Hot Update");
        }
    }
}