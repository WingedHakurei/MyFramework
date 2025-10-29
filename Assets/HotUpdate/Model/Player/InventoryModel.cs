using System.Collections.Generic;
using HotUpdate.Utility;
using QFramework;

namespace HotUpdate.Model.Player
{
    public class InventoryModel : AbstractModel
    {
        public Dictionary<int, int> ItemIdToCount { get; } = new();
        
        protected override void OnInit()
        {
            var dataTables = this.GetUtility<DataTableUtility>().Root;

            var tbInventory = dataTables.TbInventory.DataList;
            foreach (var inventory in tbInventory)
            {
                ItemIdToCount[inventory.ItemId] = inventory.Count;
            }
        }
    }
}