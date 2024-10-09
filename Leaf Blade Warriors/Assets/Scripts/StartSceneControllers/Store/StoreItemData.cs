using StartSceneControllers.Store.Properties;
using UnityEngine;

namespace StartSceneControllers.Store
{
    public class StoreItemData : ScriptableObject, IStore, IItem
    {
        [SerializeField] protected int _index;
        [SerializeField] private Sprite _icon;
        [SerializeField] private int _price;
        [SerializeField] private Sprite _gameSprite;

        public int Index => _index;
        public Sprite Icon => _icon;
        public int Price => _price;
        public Sprite GameSprite => _gameSprite;

        public virtual TypeStateStoreItem TypeState { get; }

        public virtual string StateItemKey { get; }

        public virtual string IndexChosenItemKey { get; }

        public int GetCountData()
        {
            throw new System.NotImplementedException();
        }

        public Sprite GetIconData(int index)
        {
            throw new System.NotImplementedException();
        }

        public IItem GetItem(int index)
        {
            throw new System.NotImplementedException();
        }

        public string GetPrice(int index)
        {
            throw new System.NotImplementedException();
        }

        public TypeStateStoreItem GetTypeState(int index)
        {
            throw new System.NotImplementedException();
        }
    }
}

