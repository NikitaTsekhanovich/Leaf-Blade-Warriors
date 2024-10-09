using UnityEngine;

namespace GameObjectsStateControllers
{
    public class SaverGameObject : MonoBehaviour
    {
        private void Start()
        {
            var objs = GameObject.FindGameObjectsWithTag("GloabalControllers");

            if (objs.Length > 1)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }
    }
}

