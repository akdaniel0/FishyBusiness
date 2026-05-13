using UnityEngine;

namespace Utils
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = typeof(T).Name;
                    _instance = go.AddComponent<T>();

                    // Not using this unless needed
                    //DontDestroyOnLoad(go);
                }

                return _instance;
            }
        }

        private void Awake()
        {
            Debug.Log($"Awake called for {this.gameObject.name}");

            if (_instance == null) _instance = this as T;
            else
            {
                Debug.Log($"Destroying {this.gameObject.name}", this);
                Destroy(this.gameObject);
            }
        }
    }
}