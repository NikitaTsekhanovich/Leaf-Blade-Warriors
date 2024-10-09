using MusicSystem;
using Photon.Pun;
using PlayerDataControllers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameControllers.Player
{
    public class InterfaceController : MonoBehaviourPun
    {
        [SerializeField] private TMP_Text _coinsPauseBlock;
        [SerializeField] private Button _restartButton;
        [SerializeField] private GameObject _restartText;
        [SerializeField] private Image _currentMusicImage;
        [SerializeField] private Image _currentEffectsImage;
        [SerializeField] private Sprite _musicOffImage;
        [SerializeField] private Sprite _effectsOffImage;

        private void Start()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                _restartButton.interactable = true;
                _restartText.SetActive(false);
            }

            if (!PhotonNetwork.IsConnected || photonView.IsMine)
            {
                if (PlayerPrefs.GetInt("MusicIsOn") == 1)
                    _currentMusicImage.sprite = _musicOffImage;
                if (PlayerPrefs.GetInt("EffectsIsOn") == 1)
                    _currentEffectsImage.sprite = _effectsOffImage;
            }
        }

        public void BackToMenu()
        {
            Time.timeScale = 1f;
            
            if (PhotonNetwork.IsConnected)
                PhotonNetwork.LeaveRoom();
            else 
                LoadingScreenController.Instance.ChangeScene("StartScene");
        }

        public void PauseGame()
        {
            if (!PhotonNetwork.IsConnected)
                Time.timeScale = 0f;

            _coinsPauseBlock.text = $"{PlayerPrefs.GetInt(PlayerDataKeys.CoinsKey)}";
        }

        public void ResumeGame()
        {
            if (!PhotonNetwork.IsConnected)
                Time.timeScale = 1f;
        }

        public void RestartGame()
        {
            if (!PhotonNetwork.IsConnected)
                LoadingScreenController.Instance.ChangeScene("Game");
            else
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    photonView.RPC("SendRestartGame", RpcTarget.All);
                }
            }
        }

        [PunRPC]
        private void SendRestartGame()
        {
            LoadingScreenController.Instance.ChangeScene("Game");
        }

        public void ChangeMusicState()
        {
            if (!PhotonNetwork.IsConnected || photonView.IsMine)
                MusicController.Instance.ChangeMusicState(_currentMusicImage);
        }

        public void ChangeEffectsState()
        {
            if (!PhotonNetwork.IsConnected || photonView.IsMine)
                MusicController.Instance.ChangeEffectsState(_currentEffectsImage);
        }
    }
}

