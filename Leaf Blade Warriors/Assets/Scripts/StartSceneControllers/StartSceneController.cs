using System;
using MusicSystem;
using PlayerDataControllers;
using UnityEngine;
using UnityEngine.UI;

namespace StartSceneControllers
{
    public class StartSceneController : MonoBehaviour
    {
        public static Action<ModeGame> OnStashModeGame;
        public static Action OnStartMultiplayer;

        private void Awake()
        {
            Screen.orientation = ScreenOrientation.LandscapeRight;
        }

        public void StartSingleGame()
        {   
            OnStashModeGame.Invoke(ModeGame.Single);
            LoadingScreenController.Instance.ChangeScene("Game");
        }

        public void StartMultiplayerGame()
        {
            OnStashModeGame.Invoke(ModeGame.Multiplayer);
            OnStartMultiplayer.Invoke();
        }

        public void ChangeMusicState(Image currentMusicImage)
        {
            MusicController.Instance.ChangeMusicState(currentMusicImage);
        }

        public void ChangeEffectsState(Image currentEffectsImage)
        {
            MusicController.Instance.ChangeEffectsState(currentEffectsImage);
        }
    }
}

