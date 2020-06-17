

using UnityEngine;
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public void Awake()
    {
        // Если в сцене уже есть объект с таким компонентом, то
        // он пропишет себя в _instance при инициализации
        if (!_instance)
        {
            _instance = gameObject.GetComponent<T>();
        }
        else
        {
            Debug.LogError("[Singleton] Second instance of '" + typeof(T) + "' created!");
        }
    }

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (T)FindObjectOfType(typeof(T));

                if (FindObjectsOfType(typeof(T)).Length > 1)
                {
                    Debug.LogError("[Singleton] multiple instances of '" + typeof(T) + "' found!");
                }

                if (_instance == null)
                {
                    // Если в сцене объектов с этим классом нет - создаём
                    // новый GameObject и лепим ему наш компонент
                    GameObject singleton = new GameObject();
                    _instance = singleton.AddComponent<T>();
                    singleton.name = "(singleton) " + typeof(T).ToString();
                    DontDestroyOnLoad(singleton);
                    Debug.Log("[Singleton] An instance of '" + typeof(T) + "' was created: " + singleton);
                }
                else
                {
                    Debug.Log("[Singleton] Using instance of '" + typeof(T) + "': " + _instance.gameObject.name);
                }
            }
            return _instance;
        }
    }
}
