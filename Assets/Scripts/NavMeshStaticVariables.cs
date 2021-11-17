using UnityEngine;

//[CreateAssetMenu(menuName = "Statics/Create NavMeshStaticVariables", fileName = "NavMeshStaticVariables", order = 0)]
public class NavMeshStaticVariables : ScriptableObject
{
    private static NavMeshStaticVariables Instance => StaticManager.Values.NavMeshStaticVars;
    
    [SerializeField] private float maximumRelevantDistanceFromMesh = 10f;
    public static float MaximumRelevantDistanceFromMesh => Instance.maximumRelevantDistanceFromMesh;
}