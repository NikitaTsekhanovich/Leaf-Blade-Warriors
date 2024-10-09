using System;
using System.Collections;
using GameControllers.GameLogic;
using Photon.Pun;
using TMPro;
using UnityEngine;

namespace GameControllers.Player
{
    public class TimerRespawn : MonoBehaviourPun
    {
        [SerializeField] private TMP_Text _timerRespawnText;
        private const float timerStartValue = 10f;
        private float _currentTime;
        private Coroutine _timerCoroutine = null;

        public static Action OnRespawnPlayer;

        private void OnEnable()
        {
            GameStateController.OnStartTimerRespawn += InitTimer;
            GameStateController.OnStopTimerRespawm += StopTimer;
        }

        private void OnDisable()
        {
            GameStateController.OnStartTimerRespawn -= InitTimer;
            GameStateController.OnStopTimerRespawm -= StopTimer;
        }

        private void InitTimer()
        {
            if (photonView.IsMine)
            {
                _timerRespawnText.enabled = true;
                _timerRespawnText.text = $"Time until revival: {timerStartValue}";
                _currentTime = timerStartValue;
                _timerCoroutine = StartCoroutine(StartTimerRespawn());
            }
        }

        private void StopTimer()
        {
            if (_timerCoroutine != null)
            {
                StopCoroutine(_timerCoroutine);
            }

            _timerRespawnText.enabled = false;
            _timerCoroutine = null;
        }

        private IEnumerator StartTimerRespawn()
        {
            while(_currentTime > 0)
            {
                yield return new WaitForSeconds(1f);
                _currentTime--;
                _timerRespawnText.text = $"Time until revival: {_currentTime}";
            }

            StopTimer();
            photonView.RPC("RespawnPlayer", RpcTarget.All);

            OnRespawnPlayer.Invoke();
        }
    }
}

