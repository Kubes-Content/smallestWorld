using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class ActorMouseOverride : MonoBehaviour
{
    private Actor actor;
    
    // Start is called before the first frame update
    void Start()
    {
        actor = GetComponent<Actor>();
        if (actor) return;
        
        Debug.LogError("Actor script not found.");
        Destroy(this);
    }

    private void Update()
    {
        var mouse = Mouse.current;
        if (mouse.rightButton.wasReleasedThisFrame)
        {
            RaycastHit[] results = {default, default};

            var viewportPointToRay = Camera.main.ScreenPointToRay(mouse.position.ReadValue());
            Debug.DrawRay(viewportPointToRay.origin, viewportPointToRay.direction);
            var raycastHitCount = Physics.RaycastNonAlloc(viewportPointToRay, results);
            if (raycastHitCount <= 0) return;

            var target = results[0];
            if (transform.root == target.transform.root)
            {
                if (raycastHitCount == 1) return;

                target = results[1];
            }

            

            if (!TryGetComponent(out NavMeshAgent navMeshAgent)) return;

            // ignore if clicking something way above navmesh?
            
            /*if (navMeshAgent.CanReach(target.point, out _))
            {
                navMeshAgent.destination = target.point;
            }
            else
            {*/
            var targetPoint = target.point;
            //var targetTransform = target.transform;
            const float maxNavMeshDistance = 10;
                if (NavMesh.SamplePosition(targetPoint, out NavMeshHit navMeshHit, maxNavMeshDistance, NavMesh.AllAreas))
                {
                    targetPoint = navMeshHit.position;

                    const float minMoveDistance = 1.04f;
                    var distanceFromTarget = Vector3.Distance(targetPoint, transform.position);
                    if (distanceFromTarget <= minMoveDistance) return;

                    Debug.Log($"Distance from target = {distanceFromTarget}");
                    
                    
                    // no Transform :/
                    navMeshAgent.destination = targetPoint;
                }
                
                var markerT = Instantiate(DebugManager.Instance.debugValues.modelMissingPrefab, targetPoint,
                    Quaternion.identity);

                markerT.SetParent(target.transform, true);
                LimitedLifespan.Limit(markerT.gameObject, 2);
            //}
        }

        if (mouse.middleButton.isPressed)
        {
            // rotate camera
        }
    }
}