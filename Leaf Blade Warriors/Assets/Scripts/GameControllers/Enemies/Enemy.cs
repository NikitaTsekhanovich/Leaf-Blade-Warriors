using System.Collections.Generic;
using DG.Tweening;
using Photon.Pun;
using StartSceneControllers;
using UnityEngine;

namespace GameControllers.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Vector3 _scale;
        [SerializeField] private PhysicsMovement _physicsMovement;
        [SerializeField] private List<Collider2D> _enemyColliders = new();
        [SerializeField] private List<SpriteRenderer> _spriteRenderers = new();

        private const float _delayAppear = 1.6f;

        private void Start()
        {
            Appear();
        }

        private void Appear()
        {
            if (PhotonNetwork.IsMasterClient || GameSettings.ModeGame == ModeGame.Single)
            {
                var randomAngleZ = Random.Range(0f, 360f);
                transform.rotation = Quaternion.Euler(0, 0, randomAngleZ);
            }

            DOTween.Sequence()
                .Append(transform.DOScale(_scale, _delayAppear))
                .AppendCallback(() => _physicsMovement.enabled = true)
                .AppendCallback(OnColliders);

            foreach (var spriteRenderer in _spriteRenderers)
                OnAnimationSprites(spriteRenderer);
        }

        private void OnColliders()
        {
            foreach (var collider in _enemyColliders)
                collider.enabled = true;
        }

        private void OnAnimationSprites(SpriteRenderer spriteRenderer)
        {
            DOTween.Sequence()
                .Append(spriteRenderer.DOColor(Color.gray, 0.2f))
                .Append(spriteRenderer.DOColor(Color.white, 0.2f))
                .SetLoops(4, LoopType.Yoyo);
        }
    }
}

