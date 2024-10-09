using Photon.Pun;
using UnityEngine;

namespace GameControllers.NetworkLogicControllers
{
    public class RoomController : MonoBehaviourPunCallbacks
    {
        public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
        {
            Debug.Log("Игрок покинул комнату: " + otherPlayer.NickName);
        }

        public override void OnLeftRoom()
        {
            LoadingScreenController.Instance.ChangeScene("Menu");
        }

        public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
        {
            Debug.Log("Новый MasterClient: " + newMasterClient.NickName);
            PhotonNetwork.LeaveRoom();
        }
    }
}

