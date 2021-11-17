using UnityEngine;

[CreateAssetMenu(menuName = "Statics/Debug", fileName = "Debug")]
public class DebugStaticVariables : ScriptableObject
{
    [SerializeField] private Transform modelMissingPrefab;

    private Transform markerParent;

    private static Transform GetMarkerParent()
    {
        var instance = StaticManager.Values.DebugStaticVars;
        
        if (instance.markerParent) return instance.markerParent;
        
        return instance.markerParent = new GameObject("Debug Markers").transform;
    }
    
    public Transform ModelMissingPrefab => modelMissingPrefab;
    
    public static Transform SpawnModelMissingMarker(Vector3 targetPoint)
    {
        var markerT = Instantiate(StaticManager.Values.DebugStaticVars.ModelMissingPrefab, targetPoint, Quaternion.identity, GetMarkerParent());
        LimitedLifespan.Limit(markerT.gameObject, 2);
        
        return markerT;
    }
}