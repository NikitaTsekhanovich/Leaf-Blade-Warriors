using System;
using GameControllers.NetworkEntities;
using Photon.Pun;
using UnityEngine;

namespace GameControllers.Enemies
{
    public class HealthEnemy : HealthHandler
    {
        [SerializeField] private GameObject _bonusPrefab;

        public static Action OnIncreaseLocalScore;
        public static Action OnIncreaseCoins;

        public override void DealDamage(PhotonView damageTakerView, PhotonView damageDealerView)
        {
            OnIncreaseLocalScore.Invoke();
            OnIncreaseCoins.Invoke();

            if (damageTakerView == null && damageDealerView == null)
            {
                Instantiate(_bonusPrefab, transform.localPosition, Quaternion.identity);
                Destroy(gameObject);
                return;
            }

            damageDealerView.RPC("RequestDeathEnemy", RpcTarget.Others);
            damageTakerView.RPC("SpawnBonuses", RpcTarget.MasterClient);

            if (PhotonNetwork.IsMasterClient)
                DestroyNetworkEnemy();
            else
                damageTakerView.RPC("RequestDestroy", RpcTarget.MasterClient);
        }

        private void DestroyNetworkEnemy()
        {
            PhotonNetwork.Destroy(gameObject);
        }

        [PunRPC]
        private void SpawnBonuses()
        {
            PhotonNetwork.Instantiate(_bonusPrefab.name, transform.localPosition, Quaternion.identity);
        }

        [PunRPC]
        private void RequestDestroy()
        {
            DestroyNetworkEnemy();
        }
    }
}

