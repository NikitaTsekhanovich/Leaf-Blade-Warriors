using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneControllers
{
    public class SceneLoader : MonoBehaviour
    {
        public static SceneLoader Instance = null;

        private void Start()
        {
            if (Instance == null) 
                Instance = this;  
            else if(Instance == this)
                Destroy(gameObject);
        }

        public void LoadScene(string nameScene, ModeLoadScene modeLoadScene)
        {
            switch (modeLoadScene)
            {
                case ModeLoadScene.Synchronous:
                {
                    SceneManager.LoadSceneAsync(nameScene);
                    break;
                }
                case ModeLoadScene.Asynchronous:
                {
                    SceneManager.LoadScene(nameScene);
                    break;
                }
            }
            
        }
    }
}

