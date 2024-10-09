using Photon.Pun;
using SceneControllers;
using StartSceneControllers;
using UnityEngine;

namespace GameControllers
{
    public class GameSettings : MonoBehaviour
    {
        public static ModeGame ModeGame { get; private set; }
        public static int PlayerCount { get; private set; }
        public static bool IsFirstPlayer { get; private set; }

        private void OnEnable()
        {
            SceneDataLoader.OnInitGameSettings += InitSettings;
        }

        private void OnDisable()
        {   
            SceneDataLoader.OnInitGameSettings -= InitSettings;
        }   

        private void InitSettings(ModeGame modeGame)
        {
            ModeGame = modeGame;

            if (modeGame == ModeGame.Multiplayer)
                PlayerCount = PhotonNetwork.CurrentRoom.PlayerCount;
            else if (modeGame == ModeGame.Single)
                PlayerCount = 1;

            var myActorNumber = PhotonNetwork.LocalPlayer.ActorNumber;

            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
            {
                if (myActorNumber < player.ActorNumber)
                    IsFirstPlayer = true;
            }
        }
    }
}

