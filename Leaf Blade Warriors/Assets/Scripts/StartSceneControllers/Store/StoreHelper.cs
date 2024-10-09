using System.Collections.Generic;
using StartSceneControllers.Store.Properties;
using UnityEngine;

namespace StartSceneControllers.Store
{
    public class StoreHelper<T> : IStore
        where T : IStore, IItem
    {
        private List<T> _currentData = new ();

        public StoreHelper(List<T> data)
        {
            _currentData = new List<T>(data);
        }

        public int GetCountData()
        {
            return _currentData.Count;
        }

        public Sprite GetIconData(int index)
        {
            return _currentData[index].Icon;
        }

        public string GetPrice(int index)
        {
            return $"{_currentData[index].Price}";
        }

        public TypeStateStoreItem GetTypeState(int index)
        {
            return _currentData[index].TypeState;
        }

        public IItem GetItem(int index)
        {
            return _currentData[index];
        }
    }
}

