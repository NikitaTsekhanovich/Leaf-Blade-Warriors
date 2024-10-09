using UnityEngine;

namespace StartSceneControllers.Store.Properties
{
    public interface IItem
    {
        public int Index { get; }
        public Sprite Icon { get; }
        public int Price { get; }
        public Sprite GameSprite { get; }
        public TypeStateStoreItem TypeState { get; }
        public string StateItemKey { get; }
        public string IndexChosenItemKey { get; }
    }
}

