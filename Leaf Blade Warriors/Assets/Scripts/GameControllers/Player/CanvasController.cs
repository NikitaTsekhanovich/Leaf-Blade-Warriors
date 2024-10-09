using GameControllers.NetworkEntities;
using UnityEngine;
using StartSceneControllers;

namespace GameControllers.Player
{
    public class CanvasController : MonoBehaviour
    {
        [SerializeField] private GameObject _joystick;
        [SerializeField] private GameObject _playerCanvas;
        [SerializeField] private EntityNetworkData _entityNetworkData;

        private void Start()
        {
            if (_entityNetworkData.PlayerView.IsMine || GameSettings.ModeGame == ModeGame.Single)
                _playerCanvas.SetActive(true);
            else
                _playerCanvas.SetActive(false);
        }
    }
}

