using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitedLifespan : MonoBehaviour
{
    [SerializeField] private float lifespan;
    [SerializeField] private float timeSpawned;

    private void Update()
    {
        if (timeSpawned + lifespan < Time.time)
        {
            Destroy(gameObject);
        }
    }

    public static LimitedLifespan Limit(GameObject obj, float lifespan)
    {
        if (obj.GetComponent<LimitedLifespan>())
        {
            Debug.LogError("We have a conflict, boss.");
        }
        
        var newLimit = obj.AddComponent<LimitedLifespan>();
        newLimit.lifespan = lifespan;
        newLimit.timeSpawned = Time.time;

        return newLimit;
    }
}
