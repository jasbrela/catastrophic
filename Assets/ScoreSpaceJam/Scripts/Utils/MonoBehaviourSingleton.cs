// http://www.unitygeek.com/unity_c_singleton/
using UnityEngine;

public class MonoBehaviourSingleton<T> : MonoBehaviour where T : Component
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.Log("Trying to find singleton " + typeof(T).Name + "...");
                instance = FindObjectOfType<T>();

                if (instance == null)
                {
                    Debug.Log("Singleton " + typeof(T).Name + " not found; Creating new singleton...");
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    instance = obj.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    public virtual void Awake()
    {
        if (instance == null)
        {
            Debug.Log("Singleton " + typeof(T).Name + " successfully created on Awake.");
            instance = this as T;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            // TODO: If the object has child objects, those are destroyed along with the instance,
            // and this can lead to them disappearing from the scene.
            Destroy(this.gameObject);
            Debug.Log("Tried to override singleton " + typeof(T).Name + " on awake!");
        }
    }
}