using UnityEngine;

namespace IsakUtils
{    
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T instance { get; private set; }

        private void Awake()
        {
            if (instance != null) Destroy(gameObject);
            instance = this as T;

        }

        private void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }
        }
    }
}

