using UnityEngine;

namespace StartSceneControllers.Store
{
    [CreateAssetMenu(fileName = "SkinData", menuName = "Store item data/ Skin data")]
    public class SkinData : StoreItemData
    {
        public override TypeStateStoreItem TypeState
        {
            get
            {
                return (TypeStateStoreItem)PlayerPrefs.GetInt($"{StateStoreItemDataKeys.StateSkinKey}{_index}");
            }
        }

        public override string StateItemKey => $"{StateStoreItemDataKeys.StateSkinKey}{_index}";
        public override string IndexChosenItemKey => StateStoreItemDataKeys.IndexChosenSkinKey;
    }
}

