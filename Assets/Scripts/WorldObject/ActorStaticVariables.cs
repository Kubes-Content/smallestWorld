using UnityEngine;

//[CreateAssetMenu(menuName = "Statics", fileName = "Actor")]
namespace WorldObject
{
    public class ActorStaticVariables : ScriptableObject
    {
        [SerializeField] private float minimumMoveDistance = 0.3f;

        public float MinimumMoveDistance => minimumMoveDistance;
    }
}