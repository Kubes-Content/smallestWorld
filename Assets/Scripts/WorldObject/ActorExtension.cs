using UnityEngine;
using UnityEngine.AI;

namespace WorldObject
{
    public static class ActorExtension
    {
        public static float GetMinimumMoveDistance(this Actor target) => StaticManager.Values.ActorStaticVars.MinimumMoveDistance;
    
        public static bool TryMoveTo(this Actor target, NavMeshAgent navMeshAgent, Vector3 targetPoint)
        {
            Debug.Log($"Distance {Vector3.Distance(targetPoint, navMeshAgent.destination)}");
            if (Vector3.Distance(targetPoint, navMeshAgent.destination) <= target.GetMinimumMoveDistance()) return false;
        
        
            navMeshAgent.destination = targetPoint;
            return true;
        }
    }
}