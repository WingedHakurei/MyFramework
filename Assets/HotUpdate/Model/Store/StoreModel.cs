using System.Collections.Generic;
using HotUpdate.Utility;
using QFramework;

namespace HotUpdate.Model.Store
{
    public class StoreModel : AbstractModel
    {
        public Dictionary<int, int> ItemToCount { get; } = new();
        
        protected override void OnInit()
        {
            var dataTables = this.GetUtility<DataTableUtility>().Root;
            
            var tbStoreItem = dataTables.TbStoreItem.DataList;
            foreach (var storeItem in tbStoreItem)
            {
                ItemToCount[storeItem.ItemId] = storeItem.Count;
            }
        }
    }
}