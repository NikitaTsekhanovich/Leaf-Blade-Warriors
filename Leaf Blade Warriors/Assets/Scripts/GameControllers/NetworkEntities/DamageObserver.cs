using GameControllers.Properties;
using Photon.Pun;
using UnityEngine;

namespace GameControllers.NetworkEntities
{
    public class DamageObserver : MonoBehaviour
    {
        [SerializeField] private EntityNetworkData _entityNetworkData;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.TryGetComponent<ICanDealDamage>(out var canDealDamage))
                return;

            if (!PhotonNetwork.IsConnected && col.tag != gameObject.tag)
            {
                canDealDamage.DealDamage(null, null);
                return;
            }
            else if (_entityNetworkData.PlayerView.IsMine && 
                col.tag != gameObject.tag && 
                col.TryGetComponent<PhotonView>(out var targetPhotonView))
            {
                canDealDamage.DealDamage(targetPhotonView, _entityNetworkData?.PlayerView);
            }
        }
    }
}

