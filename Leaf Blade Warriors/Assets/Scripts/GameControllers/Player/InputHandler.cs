using UnityEngine;
using StartSceneControllers;
using Photon.Pun;

namespace GameControllers.Player
{
    public class InputHandler : MonoBehaviourPun
    {
        [SerializeField] private PhysicsMovement _movement;
        [SerializeField] private FixedJoystick _joystick;

        private void Update()
        {
            if (photonView.IsMine || GameSettings.ModeGame == ModeGame.Single)
            {
                _movement.SetJoystickInfo(_joystick);
                _movement.DoRotate();
            }
        }

        private void FixedUpdate()
        {
            if (photonView.IsMine || GameSettings.ModeGame == ModeGame.Single)
            {
                _movement.DoMove();
            }
            else
            {
                _movement.SmoothMovement();
            }
        }
    }
}

