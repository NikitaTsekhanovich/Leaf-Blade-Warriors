using Photon.Pun;
using StartSceneControllers.Store;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameControllers.Player
{
    public class PlayerSettings : MonoBehaviourPun
    {
        [SerializeField] private SpriteRenderer _skin;
        [SerializeField] private SpriteRenderer _weapon;
        [SerializeField] private Image _localPlayerSkinIcon;
        [SerializeField] private Image _hisPlayerSkinIcon;
        [SerializeField] private Image _marker1;
        [SerializeField] private Sprite _marker2;
        [SerializeField] private TMP_Text _playerName;
        [SerializeField] private GameObject _hisBlockInfo;
        [SerializeField] private Image _localPlayerSkinIconLoseScreen;
        [SerializeField] private Image _hisPlayerSkinIconLoseScreen;


        private void Start()
        {
            if (!PhotonNetwork.IsConnected)
            {
                _marker1.enabled = false;
                _playerName.enabled = false;

                _skin.sprite = StoreItemsContainer.SkinsData[PlayerPrefs.GetInt(StateStoreItemDataKeys.IndexChosenSkinKey)].GameSprite;
                _weapon.sprite = StoreItemsContainer.WeaponsData[PlayerPrefs.GetInt(StateStoreItemDataKeys.IndexChosenWeaponKey)].GameSprite;
                _localPlayerSkinIcon.sprite = StoreItemsContainer.SkinsData[PlayerPrefs.GetInt(StateStoreItemDataKeys.IndexChosenSkinKey)].Icon;

                _hisBlockInfo.SetActive(false);

                return;
            }

            if (photonView.IsMine)
            {
                photonView.RPC("SendInitSettings", RpcTarget.All, 
                    PlayerPrefs.GetInt(StateStoreItemDataKeys.IndexChosenSkinKey), 
                    PlayerPrefs.GetInt(StateStoreItemDataKeys.IndexChosenWeaponKey));

                _localPlayerSkinIcon.sprite = StoreItemsContainer.SkinsData[PlayerPrefs.GetInt(StateStoreItemDataKeys.IndexChosenSkinKey)].Icon;

                photonView.RPC("SyncNickName", RpcTarget.All, PhotonNetwork.LocalPlayer.NickName);

                if (!GameSettings.IsFirstPlayer)
                    photonView.RPC("SyncMarker", RpcTarget.All);

                _localPlayerSkinIconLoseScreen.sprite = StoreItemsContainer.SkinsData[PlayerPrefs.GetInt(StateStoreItemDataKeys.IndexChosenSkinKey)].Icon;
            }
            else if (!photonView.IsMine)
            {
                photonView.RPC("SendSyncIconPlayer", RpcTarget.All, PlayerPrefs.GetInt(StateStoreItemDataKeys.IndexChosenSkinKey));
            }

            if (GameSettings.PlayerCount == 1)
            {
                _hisBlockInfo.SetActive(false);
            }
        }

        [PunRPC]
        private void SyncMarker()
        {
            _marker1.sprite = _marker2;
        }
    
        [PunRPC]
        private void SendSyncIconPlayer(int indexSkin)
        {   
            _hisPlayerSkinIcon.sprite = StoreItemsContainer.SkinsData[indexSkin].Icon;
            _hisPlayerSkinIconLoseScreen.sprite = StoreItemsContainer.SkinsData[indexSkin].Icon;
        }

        [PunRPC]
        private void SendInitSettings(int indexSkin, int indexWeapon)
        {
            InitSkinsSettings(indexSkin, indexWeapon);
        }

        [PunRPC]
        private void SyncNickName(string name)
        {
            _playerName.text = name;
        }

        private void InitSkinsSettings(int indexSkin, int indexWeapon)
        {
            _skin.sprite = StoreItemsContainer.SkinsData[indexSkin].GameSprite;
            _weapon.sprite = StoreItemsContainer.WeaponsData[indexWeapon].GameSprite;
        }
    }
}

