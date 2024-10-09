using UnityEngine;

namespace StartSceneControllers.Store
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Store item data/ Weapon data")]
    public class WeaponData : StoreItemData
    {
        public override TypeStateStoreItem TypeState
        {
            get
            {
                return (TypeStateStoreItem)PlayerPrefs.GetInt($"{StateStoreItemDataKeys.StateWeaponKey}{_index}");
            }
        }

        public override string StateItemKey => $"{StateStoreItemDataKeys.StateWeaponKey}{_index}";
        public override string IndexChosenItemKey => StateStoreItemDataKeys.IndexChosenWeaponKey;
    }
}

