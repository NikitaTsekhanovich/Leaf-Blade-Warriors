using Photon.Pun;
using UnityEngine;

namespace GameControllers.Player
{
    public class PhysicsMovement : MonoBehaviour, IPunObservable
    {
        [SerializeField] private float _speed;
        [SerializeField] private Transform _sprites;
        [SerializeField] private AudioSource _moveSound;
        private float _startSpeed;
        private Rigidbody2D _rigidbody;
        private Vector3 _smooth;
        private Vector3 _movement;
        private float _horizontalJoystick;
        private float _verticalJoystick;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _startSpeed = _speed;
            PhotonNetwork.SendRate = 20;
            PhotonNetwork.SerializationRate = 15;
        }

        public void SetJoystickInfo(FixedJoystick joystick)
        {
            _horizontalJoystick = joystick.Horizontal;
            _verticalJoystick = joystick.Vertical;
            _movement = new Vector2(_horizontalJoystick * _speed, _verticalJoystick * _speed);
        }

        public void DoRotate()
        {
            if (_horizontalJoystick != 0 || _verticalJoystick != 0)
            {
                var angle = Mathf.Atan2(_movement.y, _movement.x) * Mathf.Rad2Deg;
                _sprites.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

                if (!_moveSound.isPlaying)
                    _moveSound.Play();
            }
        }

        public void DoMove()
        {
            _rigidbody.velocity = _movement;
        }

        public void SmoothMovement()
        {
            transform.position = Vector3.Lerp(transform.position, _smooth, Time.deltaTime * 10);
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(transform.position);
            }
            else if (stream.IsReading)
            {
                _smooth = (Vector3)stream.ReceiveNext();
            }
        }

        public void UseBoost(float valueBoost)
        {
            _speed = valueBoost;
        }

        public void StopBoost()
        {
            _speed = _startSpeed;
        }
    }
}

