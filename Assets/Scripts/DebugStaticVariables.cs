using UnityEngine;

//[CreateAssetMenu(menuName = "Statics/Debug", fileName = "Debug")]
public class DebugStaticVariables : ScriptableObject
{
    private static DebugStaticVariables Instance => StaticManager.Values.DebugStaticVars;

    [SerializeField] private Transform modelMissingPrefab;

    private Transform markerParent;

    private static Transform GetMarkerParent()
    {
        if (Instance.markerParent) return Instance.markerParent;
        
        return Instance.markerParent = new GameObject("Debug Markers").transform;
    }
    
    public Transform ModelMissingPrefab => modelMissingPrefab;
    
    public static Transform SpawnModelMissingMarker(Vector3 targetPoint)
    {
        var markerT = Instantiate(Instance.ModelMissingPrefab, targetPoint, Quaternion.identity, GetMarkerParent());
        LimitedLifespan.Limit(markerT.gameObject, 2);
        
        return markerT;
    }
}