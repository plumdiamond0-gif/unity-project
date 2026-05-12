using UnityEngine;

public class SingletonObject<T> : MonoBehaviour where T : Component
{
    private static T _instance = null;  
    public static T instance
    {
        get
        {
            if( _instance == null )
            {
                T[] finds = FindObjectsByType<T>(FindObjectsSortMode.None);

                if (finds.Length > 0)
                {
                    if (finds.Length == 1)
                    { _instance = finds[0]; }

                    else if(finds.Length >= 2)
                    {
                        Debug.LogWarning("Class: " + typeof(T).Name + "finds.Length: " + finds.Length);
                    }
                }
                else
                {
                    string name = typeof(T).Name;   
                    GameObject go = new GameObject(name, typeof(T));
                    _instance = go.GetComponent<T>();

                }
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        Debug.Log($"SingletonObject {typeof(T).Name} Awake");
        if( _instance != null && _instance != this)
        {

            Destroy(this);
        }
        else
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
    }


}
