using TMPro;
using UnityEngine;

namespace GameControllers.PlayerCanvasControllers
{
    public class UIScore : MonoBehaviour
    {
        [SerializeField] private TMP_Text _totalScoreText;
        [SerializeField] private TMP_Text _localPlayerScoreText;

        public void ChangeTotalScore(int totalScore)
        {
            _totalScoreText.text = $"{totalScore}";
        }

        public void ChangeLocalScore(int localPlayerScore)
        {
            _localPlayerScoreText.text = $"{localPlayerScore}";
        }
    }
}

