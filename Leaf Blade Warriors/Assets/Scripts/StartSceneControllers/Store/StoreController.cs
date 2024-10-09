using System.Collections.Generic;
using PlayerDataControllers;
using StartSceneControllers.Store.Properties;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StartSceneControllers.Store
{
    public class StoreController : MonoBehaviour
    {
        [SerializeField] private Transform _parentItems;
        [SerializeField] private StoreItem _storeItem;
        [SerializeField] private TMP_Text _titleStoreText;
        [SerializeField] private TMP_Text _coinsText;
        [SerializeField] private GameObject _actionBlock;
        [SerializeField] private TMP_Text _actionButtonText;
        [SerializeField] private Image _playerSkin;
        [SerializeField] private Image _playerWeapon;
        [SerializeField] private AudioSource _purchaseSound;

        private List<StoreItem> _storeItems = new();
        private IStore _storeDataItems;
        private IItem _currentItem;
        private int _currentCoins;
        private int _currentIndexStoreItem;

        public void ChooseWeaponStore()
        {   
            _storeDataItems = new StoreHelper<WeaponData>(StoreItemsContainer.WeaponsData);
            _titleStoreText.text = "Weapons";
            
            SetCurrentData();
        }

        public void ChooseSkinStore()
        {
            _storeDataItems = new StoreHelper<SkinData>(StoreItemsContainer.SkinsData);
            _titleStoreText.text = "Skins";

            SetCurrentData();
        }

        private void SetCurrentData()
        {
            _currentCoins = PlayerPrefs.GetInt(PlayerDataKeys.CoinsKey);
            UpdateCoinsText();

            for (var i = 0; i < _storeDataItems.GetCountData(); i++)
            {
                var newItem = Instantiate(_storeItem, _parentItems);
                newItem.Init(_storeDataItems.GetPrice(i), _storeDataItems.GetIconData(i), i, ChooseItem, _storeDataItems.GetTypeState(i));

                _storeItems.Add(newItem);
            }
        }

        private void UpdateCoinsText()
        {
            _coinsText.text = $"{_currentCoins}";
        }

        public void ClearStore()
        {
            while (_parentItems.childCount > 0) 
                DestroyImmediate(_parentItems.GetChild(0).gameObject);

            _storeItems.Clear();
            UpdatePlayerSkins();
        }

        public void UpdatePlayerSkins()
        {
            var indexSkin = PlayerPrefs.GetInt(StateStoreItemDataKeys.IndexChosenSkinKey);
            var indexWeapon = PlayerPrefs.GetInt(StateStoreItemDataKeys.IndexChosenWeaponKey);
            _playerSkin.sprite = StoreItemsContainer.SkinsData[indexSkin].GameSprite;
            _playerWeapon.sprite = StoreItemsContainer.WeaponsData[indexWeapon].GameSprite;
        }

        public void ChooseItem(int index)
        {
            _storeItems[_currentIndexStoreItem].ChangeClickStateItem(false);
            _currentIndexStoreItem = index;
            _storeItems[_currentIndexStoreItem].ChangeClickStateItem(true);
            
            _actionBlock.SetActive(true);
            _currentItem = _storeDataItems.GetItem(index);

            if (_currentItem.TypeState == TypeStateStoreItem.Selected)
            {
                _actionButtonText.text = $"Selected";
            }
            else if (_currentItem.TypeState == TypeStateStoreItem.Bought)
            {
                _actionButtonText.text = $"Select";
            }
            else if (_currentItem.TypeState == TypeStateStoreItem.NotBought) 
            {
                _actionButtonText.text = $"Buy";
            }
        }

        public void BuyOrSelectItem()
        {
            if (_currentItem.TypeState == TypeStateStoreItem.Bought || _currentItem.TypeState == TypeStateStoreItem.Selected)
            {
                SelectItem();
            }
            else if (_currentItem.TypeState == TypeStateStoreItem.NotBought) 
            {
                if (_currentCoins - _currentItem.Price >= 0)
                {
                    _purchaseSound.Play();
                    _currentCoins -= _currentItem.Price;
                    PlayerPrefs.SetInt(PlayerDataKeys.CoinsKey, _currentCoins);
                    UpdateCoinsText();
                    _storeItems[_currentItem.Index].HidePriceText();
                    SelectItem();
                }
            }
        }

        private void SelectItem()
        {
            _actionButtonText.text = $"Selected";
            
            var chosenItemIndex = PlayerPrefs.GetInt(_currentItem.IndexChosenItemKey);
            var previousItem = _storeDataItems.GetItem(chosenItemIndex);

            PlayerPrefs.SetInt(previousItem.StateItemKey, (int)TypeStateStoreItem.Bought);
            _storeItems[chosenItemIndex].ChangeChosenState(false);

            PlayerPrefs.SetInt(_currentItem.IndexChosenItemKey, _currentItem.Index);

            _storeItems[_currentItem.Index].ChangeChosenState(true);
            PlayerPrefs.SetInt(_currentItem.StateItemKey, (int)TypeStateStoreItem.Selected);
        }
    }
}

