using DG.Tweening;
using GameControllers.Player;
using Photon.Pun;
using UnityEngine;

namespace GameControllers.NetworkEntities
{
    public class BonusPower : MonoBehaviour
    {
        [SerializeField] private EntityNetworkData _entityNetworkData;
        [SerializeField] private float _bonusValue;
        [SerializeField] private Vector3 _scale;
        private const float _delayAppear = 0.3f;

        private void Start()
        {
            Appear();
        }

        private void Appear()
        {
            DOTween.Sequence()
                .Append(transform.DOScale(_scale, _delayAppear));
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent<PowerHandler>(out var bonusHandler))
            {
                bonusHandler.TakeBonus(_bonusValue);

                if (!PhotonNetwork.IsConnected)
                {
                    Destroy(gameObject);
                    return;
                }

                _entityNetworkData.PlayerView.RPC("SendDestroyBonus", RpcTarget.MasterClient);
            }
        }

        [PunRPC]
        private void SendDestroyBonus()
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}

