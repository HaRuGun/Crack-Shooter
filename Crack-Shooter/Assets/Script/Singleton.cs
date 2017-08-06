using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T tInstance = default(T);

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (tInstance == null)
        {
            tInstance = this as T;
            tInstance.Init();
        }
    }

    public virtual void Init() { }

    private void OnApplicationQuit()
    {
        tInstance = null;
    }

    // -- Get --

    public static T GetInstance()
    {
        if (tInstance == null)
        {
            tInstance = GameObject.FindObjectOfType(typeof(T)) as T;
            if (tInstance == null)
                Debug.Log("There is no Activated Manager.");
        }

        return tInstance;
    }
}
