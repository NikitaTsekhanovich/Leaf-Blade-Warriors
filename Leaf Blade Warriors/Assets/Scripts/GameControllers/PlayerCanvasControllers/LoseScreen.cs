using System;
using GameControllers.GameLogic;
using Photon.Pun;
using PlayerDataControllers;
using StartSceneControllers.Store;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameControllers.PlayerCanvasControllers
{
    public class LoseScreen : MonoBehaviourPun
    {
        [Header("Multiplayer screen")]
        [SerializeField] private GameObject _loseScreen;
        [SerializeField] private TMP_Text _coinsText;
        [SerializeField] private TMP_Text _localScoreText;
        [SerializeField] private TMP_Text _hisScoreText;

        [Header("Single player screen")]
        [SerializeField] private GameObject _loseSingleplayerSceen;
        [SerializeField] private TMP_Text _coinsSingleplayerText;
        [SerializeField] private TMP_Text _singleplayerScoreText;
        [SerializeField] private Image _iconSingleplayerSkin;

        public static Func<int> OnGetLocalScore;
        public static Func<int> OnGetHisScore;

        private void OnEnable()
        {
            GameStateController.OnShowLoseScreen += ShowLoseScreen;
        }

        private void OnDisable()
        {
            GameStateController.OnShowLoseScreen -= ShowLoseScreen;
        }

        private void ShowLoseScreen()
        {
            if (PhotonNetwork.IsConnected && GameSettings.PlayerCount != 1)
            {
                _loseScreen.SetActive(true);
                
                _coinsText.text = $"{PlayerPrefs.GetInt(PlayerDataKeys.CoinsKey)}";

                _localScoreText.text = $"{OnGetLocalScore.Invoke()}";
                _hisScoreText.text = $"{OnGetHisScore.Invoke()}";
            }
            else if (!PhotonNetwork.IsConnected || GameSettings.PlayerCount == 1)
            {
                _loseSingleplayerSceen.SetActive(true);

                _coinsSingleplayerText.text = $"{PlayerPrefs.GetInt(PlayerDataKeys.CoinsKey)}";

                _singleplayerScoreText.text = $"{OnGetLocalScore.Invoke()}";
                _iconSingleplayerSkin.sprite = StoreItemsContainer.SkinsData[PlayerPrefs.GetInt(StateStoreItemDataKeys.IndexChosenSkinKey)].Icon;
            }
        }
    }
}

