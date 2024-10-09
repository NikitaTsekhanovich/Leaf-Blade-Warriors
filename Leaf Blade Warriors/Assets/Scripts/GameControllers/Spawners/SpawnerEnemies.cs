using System.Collections;
using System.Collections.Generic;
using GameControllers.GameLogic;
using Photon.Pun;
using StartSceneControllers;
using UnityEngine;

namespace GameControllers.Spawners
{
    public class SpawnerEnemies : MonoBehaviour
    {
        [SerializeField] private List<Transform> _spawnPoints = new();
        [SerializeField] private List<GameObject> _enemy = new();
        private const float startDelay = 10f;
        private float _delay;
        private float _currentTime;
        private Coroutine _spawnerCoroutine = null;

        private void Start()
        {
            _delay = startDelay;

            if (GameSettings.ModeGame == ModeGame.Multiplayer && PhotonNetwork.IsMasterClient)
                _spawnerCoroutine = StartCoroutine(SpawnEnemies(GameSettings.ModeGame));
            else if (GameSettings.ModeGame == ModeGame.Single)
                _spawnerCoroutine = StartCoroutine(SpawnEnemies(GameSettings.ModeGame));
        }

        private void OnEnable()
        {
            GameStateController.OnStopSpawnEnemies += StopSpawmEnemies;
        }

        private void OnDisable()
        {
            GameStateController.OnStopSpawnEnemies -= StopSpawmEnemies;
        }

        private void StopSpawmEnemies()
        {
            if (_spawnerCoroutine != null)
                StopCoroutine(_spawnerCoroutine);
        }

        private IEnumerator SpawnEnemies(ModeGame modeGame)
        {
            while(true)
            {
                if (_currentTime >= 150f)
                    DoFourthWave(modeGame);
                else if (_currentTime >= 100f)
                    DoThirdWave(modeGame);  
                else if (_currentTime >= 50f)
                    DoSecondWave(modeGame);
                else if (_currentTime >= 0)
                    DoFirstWave(modeGame);

                yield return new WaitForSeconds(_delay);
                _currentTime += _delay;
            }
        }

        private void DoFirstWave(ModeGame modeGame)
        {
            if (modeGame == ModeGame.Multiplayer)
            {
                SpawnNetworkEnemy((0, 4));
            }
            else if (modeGame == ModeGame.Single)
            {
                SpawnLocalEnemy((0, 4));
            }
        }

        private void DoSecondWave(ModeGame modeGame)
        {
            _delay = 9f;

            if (modeGame == ModeGame.Multiplayer)
            {
                SpawnNetworkEnemy((4, 8));
                SpawnNetworkEnemy((0, 4));
            }
            else if (modeGame == ModeGame.Single)
            {
                SpawnLocalEnemy((4, 8));
                SpawnLocalEnemy((0, 4));
            }
        }

        private void DoThirdWave(ModeGame modeGame)
        {
            _delay = 6f;

            if (modeGame == ModeGame.Multiplayer)
            {
                SpawnNetworkEnemy((8, 12));
                SpawnNetworkEnemy((0, 8));
            }
            else if (modeGame == ModeGame.Single)
            {
                SpawnLocalEnemy((8, 12));
                SpawnLocalEnemy((0, 8));
            }
        }

        private void DoFourthWave(ModeGame modeGame)
        {
            _delay = 5f;

            if (modeGame == ModeGame.Multiplayer)
            {
                SpawnNetworkEnemy((12, 16));
                SpawnNetworkEnemy((0, 16));
            }
            else if (modeGame == ModeGame.Single)
            {
                SpawnLocalEnemy((12, 16));
                SpawnLocalEnemy((0, 16));
            }
        }

        private void SpawnNetworkEnemy((int, int) rangeIndexes)
        {
            var indexPoint = UnityEngine.Random.Range(0, _spawnPoints.Count);
            var indexEnemy = UnityEngine.Random.Range(rangeIndexes.Item1, rangeIndexes.Item2);
            PhotonNetwork.Instantiate(_enemy[indexEnemy].name, _spawnPoints[indexPoint].position, Quaternion.identity);
        }

        private void SpawnLocalEnemy((int, int) rangeIndexes)
        {
            var indexPoint = UnityEngine.Random.Range(0, _spawnPoints.Count);
            var indexEnemy = UnityEngine.Random.Range(rangeIndexes.Item1, rangeIndexes.Item2);
            Instantiate(_enemy[indexEnemy], _spawnPoints[indexPoint].position, Quaternion.identity);
        }
    }
}

