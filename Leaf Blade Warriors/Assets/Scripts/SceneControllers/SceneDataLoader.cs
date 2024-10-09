using System;
using PlayerDataControllers;
using StartSceneControllers;
using StartSceneControllers.Store;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneControllers
{
    public class SceneDataLoader : MonoBehaviour
    {
        private ModeGame _modeGame;
        private Sprite _playerSkin;
        private Sprite _playerWeapon;

        public static Action<ModeGame> OnInitGameSettings;

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            StartSceneController.OnStashModeGame += StashModeGame;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            StartSceneController.OnStashModeGame -= StashModeGame;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.name == "StartScene")
            {
                CheckStateLaunch();
                StoreItemsContainer.LoadSkinsData();
                StoreItemsContainer.LoadWeaponsData();
            }
            else if (scene.name == "Menu")
            {

            }
            else if (scene.name == "Game")
            {
                OnInitGameSettings.Invoke(_modeGame);
            }
        }

        private void StashModeGame(ModeGame modeGame)
        {
            _modeGame = modeGame;
        }

        private void CheckStateLaunch()
        {
            if (PlayerPrefs.GetInt(PlayerDataKeys.IsFirstLaunchGameKey) == (int)ModeLaunch.IsFirst)
            {
                PlayerPrefs.SetInt(StateStoreItemDataKeys.IndexChosenSkinKey, 0);
                PlayerPrefs.SetInt(StateStoreItemDataKeys.IndexChosenWeaponKey, 0);
                PlayerPrefs.SetInt($"{StateStoreItemDataKeys.StateSkinKey}{0}", (int)TypeStateStoreItem.Selected);
                PlayerPrefs.SetInt($"{StateStoreItemDataKeys.StateWeaponKey}{0}", (int)TypeStateStoreItem.Selected);
                PlayerPrefs.SetInt(PlayerDataKeys.IsFirstLaunchGameKey, (int)ModeLaunch.IsNotFirst);
            }
        }
    }
}

