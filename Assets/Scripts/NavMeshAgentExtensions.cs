using UnityEngine;
using UnityEngine.AI;

public static class NavMeshAgentExtensions
{
    public static bool CanReach(this NavMeshAgent thisNma, Vector3 destination, out NavMeshPath path)
    {
        path = new NavMeshPath();
        thisNma.CalculatePath(destination, path);

        return path.status == NavMeshPathStatus.PathComplete;

    }
}