using UnityEngine;
using UnityEngine.AI;

namespace WorldObject
{
    public static class ActorExtension
    {
        public static float GetMinimumMoveDistance(this Actor target) => ActorStaticVariables.MinimumMoveDistance;
    
        public static bool TryMoveTo(this Actor target, NavMeshAgent navMeshAgent, Vector3 targetPoint)
        {
            if (Vector3.Distance(targetPoint, navMeshAgent.destination) <= target.GetMinimumMoveDistance()) return false;
        
        
            navMeshAgent.destination = targetPoint;
            return true;
        }
    }
}