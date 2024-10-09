using System;
using Photon.Pun;
using UnityEngine;

namespace GameControllers.Player
{
    public class PowerHandler : MonoBehaviourPun
    {
        [SerializeField] private AudioSource _takeBonusSound;
        private const float maximumPower = 2.5f;
        private float _currentPower;

        public static Action<float> OnUpdateHisPower;
        public static Action<float> OnUpdateLocalPower;

        public void TakeBonus(float bonusValue)
        {
            if (PhotonNetwork.IsConnected && photonView.IsMine)
            {
                if (_currentPower <= maximumPower)
                {
                    _takeBonusSound.Play();
                    _currentPower += bonusValue;
                    photonView.RPC("SendTakeBouns", RpcTarget.All, bonusValue);
                    photonView.RPC("SendProgressPower", RpcTarget.Others, bonusValue);
                    OnUpdateLocalPower.Invoke(bonusValue / maximumPower);
                }
            }
            else if (!PhotonNetwork.IsConnected)
            {
                if (_currentPower <= maximumPower)
                {
                    _takeBonusSound.Play();
                    _currentPower += bonusValue;
                    OnUpdateLocalPower.Invoke(bonusValue / maximumPower);
                    transform.localScale += new Vector3(bonusValue, bonusValue, bonusValue);
                }
            }
        }

        [PunRPC]
        private void SendTakeBouns(float bonusValue)
        {
            transform.localScale += new Vector3(bonusValue, bonusValue, bonusValue);
        }

        [PunRPC]
        public void SendProgressPower(float bonusValue)
        {
            OnUpdateHisPower.Invoke(bonusValue / maximumPower);
        }
    }
}

