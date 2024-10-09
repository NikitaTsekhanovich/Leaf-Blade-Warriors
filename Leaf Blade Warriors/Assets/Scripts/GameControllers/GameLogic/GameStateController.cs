using System;
using GameControllers.Player;
using Photon.Pun;
using UnityEngine;

namespace GameControllers.GameLogic
{
    public class GameStateController : MonoBehaviourPun
    {
        [SerializeField] private AudioSource _loseSound;
        private int _playerCount;

        public static Action OnShowLoseScreen;
        public static Action OnStopSpawnEnemies;
        public static Action OnStartTimerRespawn;
        public static Action OnStopTimerRespawm;

        private void Start()
        {
            _playerCount = GameSettings.PlayerCount;
        }

        private void OnEnable()
        {
            HealthPlayer.OnDeathPlayer += DeathPlayer;
            TimerRespawn.OnRespawnPlayer += RespawnPlayer;
        }

        private void OnDisable()
        {
            HealthPlayer.OnDeathPlayer -= DeathPlayer;
            TimerRespawn.OnRespawnPlayer -= RespawnPlayer;
        }

        private void RespawnPlayer()
        {
            _playerCount++;
        }

        private void DeathPlayer()
        {
            _playerCount--;

            if (_playerCount <= 0)
            {
                _loseSound.Play();
                OnStopTimerRespawm.Invoke();
                OnShowLoseScreen.Invoke();
                OnStopSpawnEnemies.Invoke();
            }
            else 
            {
                OnStartTimerRespawn.Invoke();
            }
        }
    }
}

