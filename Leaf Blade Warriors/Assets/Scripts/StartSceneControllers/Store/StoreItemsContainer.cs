using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StartSceneControllers.Store
{
    public class StoreItemsContainer
    {
        public static List<SkinData> SkinsData { get; private set; }
        public static List<WeaponData> WeaponsData { get; private set; }

        public static void LoadSkinsData()
        {
            SkinsData = Resources.LoadAll<SkinData>("ScriptableObjectData/SkinsData")
                .OrderBy(x => x.Index)
                .ToList();
        }

        public static void LoadWeaponsData()
        {
            WeaponsData = Resources.LoadAll<WeaponData>("ScriptableObjectData/WeaponsData")
                .OrderBy(x => x.Index)
                .ToList();
        }
    }
}

