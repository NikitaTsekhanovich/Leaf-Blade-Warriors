using UnityEngine;

namespace StartSceneControllers.Store.Properties
{
    public interface IStore
    {
        public int GetCountData();
        public Sprite GetIconData(int index);
        public string GetPrice(int index);
        public TypeStateStoreItem GetTypeState(int index);
        public IItem GetItem(int index);
    }
}

