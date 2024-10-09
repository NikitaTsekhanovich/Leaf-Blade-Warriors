using GameControllers.Enemies;
using PlayerDataControllers;
using UnityEngine;

namespace GameControllers.Player
{
    public class CoinsController : MonoBehaviour
    {
        private int _currentCoins;

        private void Start()
        {
            _currentCoins = PlayerPrefs.GetInt(PlayerDataKeys.CoinsKey);
        }

        private void OnEnable()
        {
            HealthEnemy.OnIncreaseCoins += IncreaseCoins;
        }

        private void OnDisable()
        {
            HealthEnemy.OnIncreaseCoins -= IncreaseCoins;
        }

        private void IncreaseCoins()
        {
            _currentCoins++;
            PlayerPrefs.SetInt(PlayerDataKeys.CoinsKey, _currentCoins);
        }
    }
}

