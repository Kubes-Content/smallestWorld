using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class StaticManager : MonoBehaviour
{
    private static StaticManager instance;

    [SerializeField] private StaticManagerVars staticManagerVars;

    public static StaticManagerVars Values => instance.staticManagerVars;

    // Start is called before the first frame update
    void Start()
    {

        if (instance)
        {
            Debug.LogError("A DebugManager has spawned while one already exists.");
            Destroy(this);
            return;
        }

        if (!AssetCached(staticManagerVars))
        {
            return;
        }

        instance = this;
    }

    private bool AssetCached<TAsset>(TAsset asset) where TAsset : Object
    {
        if (asset) return true;
        
        
        Debug.LogError($"A {GetType()} has spawned with a missing {typeof(TAsset)}");
        Destroy(this);

        return false;
    }
}
