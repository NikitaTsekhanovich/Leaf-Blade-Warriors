using UnityEngine;
using UnityEngine.UI;

namespace GameControllers.Player
{
    public class UICharacteristics : MonoBehaviour
    {
        [SerializeField] private Image _localProgressPower;
        [SerializeField] private Image _hisProgressPower;
        [SerializeField] private Image _boostLocalProgress;
        [SerializeField] private Image _boostHisProgress;

        private void OnEnable()
        {
            PowerHandler.OnUpdateHisPower += UpdateHisPower;
            PowerHandler.OnUpdateLocalPower += UpdateLocalPower;
            BoostHandler.OnUpdateLcoalBoost += UpdateLocalBoost;
            BoostHandler.OnUpdateHisBoost += UpdateHisBoost;
        }

        private void OnDisable()
        {
            PowerHandler.OnUpdateHisPower -= UpdateHisPower;
            PowerHandler.OnUpdateLocalPower -= UpdateLocalPower;
            BoostHandler.OnUpdateLcoalBoost -= UpdateLocalBoost;
            BoostHandler.OnUpdateHisBoost -= UpdateHisBoost;
        }

        private void UpdateHisPower(float powerValue)
        {
            _hisProgressPower.fillAmount += powerValue;
        }

        private void UpdateLocalPower(float powerValue)
        {
            _localProgressPower.fillAmount += powerValue;
        }

        private void UpdateHisBoost(float powerValue)
        {
            _boostHisProgress.fillAmount += powerValue;
        }

        private void UpdateLocalBoost(float powerValue)
        {
            _boostLocalProgress.fillAmount += powerValue;
        }
    }
}
