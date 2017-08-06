using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T tInstance = default(T);

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

    private void Awake()
    {
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
}
