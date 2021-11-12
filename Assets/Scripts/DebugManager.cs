using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    public static DebugManager Instance;

    public DebugValues debugValues;
    
    // Start is called before the first frame update
    void Start()
    {
        if (Instance || !debugValues)
        {
            Debug.LogError("A DebugManager has spawned while one already exists.");
            Destroy(this);
            return;
        }
        
        Instance = this;
    }
}
