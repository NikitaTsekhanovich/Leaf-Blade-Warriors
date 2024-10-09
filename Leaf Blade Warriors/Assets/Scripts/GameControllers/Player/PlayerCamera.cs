using Cinemachine;
using UnityEngine;

namespace GameControllers.Player
{
    public class PlayerCamera : MonoBehaviour
    {
        public static CinemachineVirtualCamera CinemachineVirtual { get; private set; }

        private void Awake()
        {
            CinemachineVirtual = GetComponent<CinemachineVirtualCamera>();
        }
    }
}

