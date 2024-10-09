using System.Collections;
using Photon.Pun;
using UnityEngine;
using StartSceneControllers;

namespace GameControllers.Enemies
{
    public class PhysicsMovement : MonoBehaviour
    {
        [SerializeField] private Transform _targetMovement;
        [SerializeField] private float _speed;
        private float _delay = 5f;
        private const float speedRotation = 2f;
        private Coroutine _rotatableCoroutine = null;
        private Coroutine _smoothRotateCoroutine = null;

        private void Start()
        {
            if (PhotonNetwork.IsMasterClient || GameSettings.ModeGame == ModeGame.Single)
            {
                _rotatableCoroutine = StartCoroutine(ChooseDirection());
            }
        }

        private void FixedUpdate()
        {
            transform.position = Vector3.Lerp(_targetMovement.position, transform.position, Time.fixedDeltaTime * _speed);
        }

        private IEnumerator ChooseDirection()
        {
            while (true)
            {
                yield return new WaitForSeconds(_delay);

                var randomAngleZ = Random.Range(0f, 360f);

                var targetRotation = Quaternion.Euler(0, 0, randomAngleZ);

                _smoothRotateCoroutine = StartCoroutine(DoSmoothRotate(targetRotation));
            }
        }

        private IEnumerator DoSmoothRotate(Quaternion targetRotation)
        {
            while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, speedRotation * Time.deltaTime);
                yield return null;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (PhotonNetwork.IsMasterClient || GameSettings.ModeGame == ModeGame.Single)
            {
                if (collision.gameObject.CompareTag("HorizontalWall"))
                    ChangeDirection(360);
                else if (collision.gameObject.CompareTag("VerticalWall"))
                    ChangeDirection(180);
            }
        }

        private void ChangeDirection(float offset)
        {
            if (_rotatableCoroutine != null) StopCoroutine(_rotatableCoroutine);
            if (_smoothRotateCoroutine != null) StopCoroutine(_smoothRotateCoroutine);

            var targetRotation = Quaternion.Euler(0, 0, offset - transform.localEulerAngles.z);

            _smoothRotateCoroutine = StartCoroutine(DoSmoothRotate(targetRotation));
            _rotatableCoroutine = StartCoroutine(ChooseDirection());
        }
    }
}

