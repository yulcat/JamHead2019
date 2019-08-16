using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T Instance { get; private set; }

    public static bool IsInitialized
    {
        get { return Instance != null; }
    }

    protected virtual void Awake()
    {
        if (Instance != null)
        {
#if UNITY_EDITOR
            if (Application.isPlaying)
                Debug.LogError("Trying to make secondary singleton object.");
#endif
        }
        else
        {
            Instance = (T)this;
        }
    }

    protected virtual void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}