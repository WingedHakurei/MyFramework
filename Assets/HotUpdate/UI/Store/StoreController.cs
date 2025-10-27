using System.Collections.Generic;
using HotUpdate.Model.Player;
using HotUpdate.Model.Store;
using HotUpdate.Utility;
using QFramework;
using UnityEngine;

namespace HotUpdate.UI.Store
{
    public class StoreController : MonoBehaviour, IController
    {
        #region View
        [SerializeField] private StoreItemController _itemPrefab;
        [SerializeField] private Transform _itemParent;
        private Dictionary<int, StoreItemController> _storeItems = new();
        #endregion
        
        #region Model
        private StoreModel _storeModel;
        private InventoryModel _inventoryModel;
        #endregion

        private void Start()
        {
            _storeModel = this.GetModel<StoreModel>();
            _inventoryModel = this.GetModel<InventoryModel>();
            var dataTables = this.GetUtility<DataTableUtility>().Root;
            var tbItem = dataTables.TbItem;
            foreach (var (itemId, count) in _storeModel.ItemToCount)
            {
                var storeItem = Instantiate(_itemPrefab, _itemParent);
                var item = tbItem.Get(itemId);
                storeItem.Init(itemId, item.Name, item.Cost, count);
            }
        }

        public void OnBuyButtonClick(int itemId)
        {
            const int coinId = 1001;
            var inventoryCoins = _inventoryModel.ItemIdToCount[coinId];
            var storeItem = _storeItems[itemId];
            
        }

        public void OnSellButtonClick(int itemId)
        {
            
        }

        public IArchitecture GetArchitecture()
        {
            return GameArchitecture.Interface;
        }
    }
}