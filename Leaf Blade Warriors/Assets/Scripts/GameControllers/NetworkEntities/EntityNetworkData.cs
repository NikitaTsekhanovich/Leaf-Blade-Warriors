using System;
using Photon.Pun;
using UnityEngine;

namespace GameControllers.NetworkEntities
{
    public class EntityNetworkData : MonoBehaviour
    {
        private PhotonView _playerView;

        public PhotonView PlayerView => _playerView;

        public static Action OnIncreaseTotalScore;

        private void Awake()
        {
            _playerView = GetComponent<PhotonView>();
        }

        [PunRPC]
        public void RequestDeathEnemy()
        {
            OnIncreaseTotalScore.Invoke();
        }
    }
}

