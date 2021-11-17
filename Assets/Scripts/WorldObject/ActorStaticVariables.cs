using UnityEngine;

//[CreateAssetMenu(menuName = "Statics/Actor", fileName = "Actor")]
namespace WorldObject
{
    public class ActorStaticVariables : ScriptableObject
    {
        private static ActorStaticVariables Instance => StaticManager.Values.ActorStaticVars;
        
        [SerializeField] private float minimumMoveDistance = 0.3f;

        public static float MinimumMoveDistance => Instance.minimumMoveDistance;
    }
}