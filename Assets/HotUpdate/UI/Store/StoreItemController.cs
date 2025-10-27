using System;
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

        private int _itemId;
        
        public void Init(int itemId, string itemName, int cost, int count)
        {
            _itemId = itemId;
            _nameText.text = itemName;
            _costText.text = cost.ToString();
            _countText.text = count.ToString();
        }

        public void RegisterBuyButton(Action<int> listener)
        {
            _buyButton.onClick.AddListener(() =>
            {
                listener?.Invoke(_itemId);
            });
        }

        public void RegisterSellButton(Action<int> listener)
        {
            _sellButton.onClick.AddListener(() =>
            {
                listener?.Invoke(_itemId);
            });
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
    }
}