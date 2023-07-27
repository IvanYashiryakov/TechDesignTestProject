using UnityEngine;

public class MonoSingleton<TSelf> : MonoBehaviour where TSelf : MonoBehaviour
{
    private static TSelf _instance;
    public static TSelf Instance => _instance;

    protected virtual void Awake()
    {
        _instance = this as TSelf;
    }
}
