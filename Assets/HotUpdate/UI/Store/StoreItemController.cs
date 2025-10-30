using HotUpdate.Model.Player;
using HotUpdate.Model.Store;
using HotUpdate.Utility;
using MyUtils;
using QFramework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HotUpdate.UI.Store
{
    public class StoreItemController : MonoBehaviour, IController
    {
        #region View
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _costText;
        [SerializeField] private TMP_Text _countText;
        
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _sellButton;
        #endregion
        
        #region Model
        private StoreModel _storeModel;
        private int _itemId;
        #endregion

        public void Init(int itemId)
        {
            _itemId = itemId;
            _storeModel = this.GetModel<StoreModel>();
            
            _buyButton.onClick.AddListener(() => this.SendCommand(new ItemBuyCommand(_itemId)));
            _sellButton.onClick.AddListener(() => this.SendCommand(new ItemSellCommand(_itemId)));

            this.RegisterEvent<ItemBoughtEvent>(_ => UpdateView()).UnRegisterWhenGameObjectDestroyed(gameObject);
            this.RegisterEvent<ItemSoldEvent>(_ => UpdateView()).UnRegisterWhenGameObjectDestroyed(gameObject);
            
            UpdateView();
        }

        private void UpdateView()
        {
            var dt = this.GetUtility<DataTableUtility>().Root;
            var item = dt.TbItem[_itemId];
            _nameText.text = item.Name;
            _costText.text = item.Cost.ToString();
            _countText.text = _storeModel.ItemToCount[_itemId].ToString();
        }

        private void OnDestroy()
        {
            _buyButton.onClick.RemoveAllListeners();
            _sellButton.onClick.RemoveAllListeners();
        }

        public IArchitecture GetArchitecture()
        {
            return GameArchitecture.Interface;
        }

        private class ItemBuyCommand : AbstractCommand
        {
            private const int CoinId = 1001;
            private readonly int _itemId;

            public ItemBuyCommand(int itemId)
            {
                _itemId = itemId;
            }

            protected override void OnExecute()
            {
                var inventoryModel = this.GetModel<InventoryModel>();
                var inventoryCoins = inventoryModel.ItemIdToCount[CoinId];

                var dt = this.GetUtility<DataTableUtility>().Root;
                var tbItem = dt.TbItem;
                var item = tbItem[_itemId];
            
                var itemName = item.Name;
                var cost = item.Cost;
                var storeModel = this.GetModel<StoreModel>();
                var count = storeModel.ItemToCount[_itemId];
            
                if (count <= 0)
                {
                    MyLogger.Info($"{itemName} is sold out.");
                    return;
                }

                if (inventoryCoins < cost)
                {
                    MyLogger.Info("Insufficient coins.");
                    return;
                }

                inventoryModel.ItemIdToCount[CoinId] -= cost;
                storeModel.ItemToCount[_itemId]--;
                inventoryModel.ItemIdToCount[_itemId]++;
                
                this.SendEvent<ItemBoughtEvent>();
                MyLogger.Info($"Successfully bought item {itemName}.");
            }
        }

        private class ItemSellCommand : AbstractCommand
        {
            private const int CoinId = 1001;
            private readonly int _itemId;
            
            public ItemSellCommand(int itemId)
            {
                _itemId = itemId;
            }

            protected override void OnExecute()
            {
                var inventoryModel = this.GetModel<InventoryModel>();
                var inventoryItems = inventoryModel.ItemIdToCount[_itemId];
            
                var dt = this.GetUtility<DataTableUtility>().Root;
                var tbItem = dt.TbItem;
                var item = tbItem[_itemId];
            
                var itemName = item.Name;
                var cost = item.Cost;
            
                if (inventoryItems <= 0)
                {
                    MyLogger.Info($"No more item {itemName}.");
                    return;
                }
            
                inventoryModel.ItemIdToCount[CoinId] += cost;
                var storeModel = this.GetModel<StoreModel>();
                storeModel.ItemToCount[_itemId]++;
                inventoryModel.ItemIdToCount[_itemId]--;
                
                this.SendEvent<ItemSoldEvent>();
                MyLogger.Info($"Successfully sold item {itemName}.");
            }
        }

        public struct ItemBoughtEvent {}

        public struct ItemSoldEvent {}
        
    }
}