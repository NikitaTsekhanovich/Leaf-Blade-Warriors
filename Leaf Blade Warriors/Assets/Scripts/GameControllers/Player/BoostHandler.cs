using System;
using System.Collections;
using Photon.Pun;
using UnityEngine;

namespace GameControllers.Player
{
    public class BoostHandler : MonoBehaviourPun
    {
        [SerializeField] private PhysicsMovement _physicsMovement;
        private float _currentBoostValue;
        private Coroutine _restorableCoroutine;
        private Coroutine _spendableCoroutine;
        private bool _boostUsed;
        private const float maxBoostValue = 10f;
        private const float delayRestore = 0.01f;
        private const float valueSpend = 0.3f;
        private const float delaySpend = 0.1f;
        private const float valueBoost = 15f;

        public static Action<float> OnUpdateHisBoost;
        public static Action<float> OnUpdateLcoalBoost;

        private void Start()
        {            
            if (!PhotonNetwork.IsConnected || photonView.IsMine)
                _restorableCoroutine = StartCoroutine(RestoreBoost());
        }

        private IEnumerator RestoreBoost()
        {
            while (true)
            {
                yield return new WaitForSeconds(delayRestore);

                if (_currentBoostValue < maxBoostValue)
                {
                    _currentBoostValue += delayRestore;
                    OnUpdateLcoalBoost.Invoke(delayRestore / maxBoostValue);

                    if (PhotonNetwork.IsConnected)
                        photonView.RPC("SendProgressBoost", RpcTarget.Others, delayRestore / maxBoostValue);
                }
            }
        }

        private IEnumerator SpendBoost()
        {
            while (true)
            {
                yield return new WaitForSeconds(delaySpend);

                if (_currentBoostValue > 0)
                {
                    _currentBoostValue -= valueSpend;
                    OnUpdateLcoalBoost.Invoke(- valueSpend / maxBoostValue);

                    if (PhotonNetwork.IsConnected)
                        photonView.RPC("SendProgressBoost", RpcTarget.Others, - valueSpend / maxBoostValue);
                }
                else
                {
                    StopUseBoost();
                }
            }
        }

        public void UseBoost()
        {
            if (!_boostUsed)
            {
                _boostUsed = true;

                _physicsMovement.UseBoost(valueBoost);
                StopCoroutine(_restorableCoroutine);
                _spendableCoroutine = StartCoroutine(SpendBoost());
            }
        }

        public void StopUseBoost()
        {
            if (_boostUsed)
            {
                _boostUsed = false;

                _physicsMovement.StopBoost();
                StopCoroutine(_spendableCoroutine);
                _restorableCoroutine = StartCoroutine(RestoreBoost());
            }
        }

        [PunRPC]
        public void SendProgressBoost(float bonusValue)
        {
            OnUpdateHisBoost.Invoke(bonusValue);
        }
    }
}

