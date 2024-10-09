using GameControllers.Enemies;
using GameControllers.NetworkEntities;
using GameControllers.PlayerCanvasControllers;
using PlayerDataControllers;
using UnityEngine;

namespace GameControllers.GameLogic
{
    public class ScoreController : MonoBehaviour
    {
        [SerializeField] private UIScore _uiScore;
        private int _localPlayerScore;
        private int _hisPlayerScore;
        private int _bestScore;

        private void Start()
        {
            _bestScore = PlayerPrefs.GetInt(PlayerDataKeys.BestScoreKey);
        }

        private void OnEnable()
        {
            EntityNetworkData.OnIncreaseTotalScore += IncreaseHisScore;
            HealthEnemy.OnIncreaseLocalScore += IncreaseLocalScore;
            LoseScreen.OnGetLocalScore += GetLocalScore;
            LoseScreen.OnGetHisScore += GetHisScore;
        }

        private void OnDisable()
        {
            EntityNetworkData.OnIncreaseTotalScore -= IncreaseHisScore;
            HealthEnemy.OnIncreaseLocalScore -= IncreaseLocalScore;
            LoseScreen.OnGetLocalScore -= GetLocalScore;
            LoseScreen.OnGetHisScore -= GetHisScore;
        }

        private void IncreaseHisScore()
        {
            _hisPlayerScore++;
            _uiScore.ChangeTotalScore(_hisPlayerScore);
        }

        private void IncreaseLocalScore()
        {
            _localPlayerScore++;
            _uiScore.ChangeLocalScore(_localPlayerScore);

            if (_localPlayerScore > _bestScore)
            {
                _bestScore = _localPlayerScore;
                PlayerPrefs.SetInt(PlayerDataKeys.BestScoreKey, _bestScore);
            }
        }

        private int GetLocalScore() => _localPlayerScore;
        private int GetHisScore() => _hisPlayerScore;
    }
}

