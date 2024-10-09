using GameControllers.NetworkEntities;
using Photon.Pun;
using UnityEngine;
using System;

namespace GameControllers.Player
{
    public class HealthPlayer : HealthHandler
    {
        [SerializeField] private SpriteRenderer _lilySprite;
        [SerializeField] private SpriteRenderer _weaponSprite;
        [SerializeField] private SpriteRenderer _characterSprite;
        [SerializeField] private Sprite _deathSprite;
        [SerializeField] private Sprite _aliveSprite;
        [SerializeField] private BoxCollider2D _attackCollider;
        [SerializeField] private CircleCollider2D _collisionCollider;

        public static Action OnDeathPlayer;

        public override void DealDamage(PhotonView damageTakerView, PhotonView damageDealerView)
        {
            if (damageTakerView == null && damageDealerView == null)
            {
                DeathPlayer();
                return;
            }
            
            damageTakerView.RPC("DeathPlayer", RpcTarget.All);
        }

        [PunRPC]
        private void DeathPlayer()
        {
            _lilySprite.sprite = _deathSprite;
            _lilySprite.sortingOrder = 2;
            _weaponSprite.enabled = false;
            _attackCollider.enabled = false;
            gameObject.layer = LayerMask.NameToLayer("Death");
            _characterSprite.enabled = false;
            OnDeathPlayer.Invoke();
        }

        [PunRPC]
        private void RespawnPlayer()
        {
            _lilySprite.sprite = _aliveSprite;
            _lilySprite.sortingOrder = 3;
            _weaponSprite.enabled = true;
            _attackCollider.enabled = true;
            gameObject.layer = LayerMask.NameToLayer("Default");
            _characterSprite.enabled = true;
        }
    }
}

