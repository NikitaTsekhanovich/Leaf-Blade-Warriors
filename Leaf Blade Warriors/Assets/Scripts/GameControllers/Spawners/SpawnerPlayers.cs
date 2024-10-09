using System.Collections.Generic;
using GameControllers.Player;
using Photon.Pun;
using UnityEngine;
using StartSceneControllers;

namespace GameControllers.Spawners
{
    public class SpawnerPlayers : MonoBehaviour
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private List<Transform> _spawnPositions = new();

        private void Start()
        {
            SpawnPlayer();
        }

        private void SpawnPlayer()
        {
            if (GameSettings.ModeGame == ModeGame.Multiplayer)
            {
                Vector3 position;

                if (GameSettings.IsFirstPlayer)
                    position = GetSpawnPosition(0);
                else 
                    position = GetSpawnPosition(1);

                var newPLayer = PhotonNetwork.Instantiate(_player.name, position, Quaternion.identity);
                PlayerCamera.CinemachineVirtual.Follow = newPLayer.transform;
            }
            else if (GameSettings.ModeGame == ModeGame.Single)
            {
                var position = GetSpawnPosition(0);

                var newPLayer = Instantiate(_player, position, Quaternion.identity);
                PlayerCamera.CinemachineVirtual.Follow = newPLayer.transform;
            }
        }

        private Vector3 GetSpawnPosition(int indexPoint)
        {
            var spawnPoint = _spawnPositions[indexPoint];
            var position = new Vector3(
                spawnPoint.position.x, 
                spawnPoint.position.y, 
                0);

            return position;
        }
    }
}

