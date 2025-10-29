using Cysharp.Threading.Tasks;
using HotUpdate.Model.Player;
using HotUpdate.Model.Store;
using MyUtils;
using QFramework;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace HotUpdate.UI.Store
{
    public class StorePanelController : MonoBehaviour, IController
    {
        #region View
        [SerializeField] private AssetReference _itemPrefab;
        [SerializeField] private Transform _itemParent;
        [SerializeField] private Button _inventoryButton;
        [SerializeField] private TMP_Text _coinsText;
        #endregion
        
        #region Model
        private InventoryModel _inventoryModel;
        #endregion

        private void Start()
        {
            StartAsync().Forget();
        }

        private async UniTask StartAsync()
        {
            _inventoryModel = this.GetModel<InventoryModel>();
            var storeModel = this.GetModel<StoreModel>();
            foreach (var itemId in storeModel.ItemToCount.Keys)
            {
                var itemGo = await _itemPrefab.InstantiateAsync(_itemParent);
                var storeItemController = itemGo.GetComponent<StoreItemController>();
                storeItemController.Inject(itemId);
            }
            _inventoryButton.onClick.AddListener(() => this.SendCommand(new OpenInventoryCommand()));
            
            this.RegisterEvent<StoreItemController.ItemBoughtEvent>(_ => UpdateView()).UnRegisterWhenGameObjectDestroyed(gameObject);
            this.RegisterEvent<StoreItemController.ItemSoldEvent>(_ => UpdateView()).UnRegisterWhenGameObjectDestroyed(gameObject);
            
            UpdateView();
        }

        private void UpdateView()
        {
            // TODO: 将coin id抽取到配置中
            const int coinId = 1001;
            var coins = _inventoryModel.ItemIdToCount[coinId];
            _coinsText.text = coins.ToString();
        }

        private void OnDestroy()
        {
            _inventoryButton.onClick.RemoveAllListeners();
        }

        public IArchitecture GetArchitecture()
        {
            return GameArchitecture.Interface;
        }

        private class OpenInventoryCommand : AbstractCommand
        {
            // TODO: 添加仓库面板
            protected override void OnExecute()
            {
                var inventoryModel = this.GetModel<InventoryModel>();
                
                MyLogger.Info(string.Join(',', inventoryModel.ItemIdToCount));
            }
        }
    }
}