using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class ActorMouseOverride : MonoBehaviour
{
    private Actor actor;
    private NavMeshAgent navMeshAgent;
    
    // Start is called before the first frame update
    void Start()
    {
        if (!TryGetComponent(out actor))
        {
            Debug.LogError("Actor script not found.");
            Destroy(this);
        }

        if (!TryGetComponent(out navMeshAgent))
        {
            Debug.LogError("NavMeshAgent script not found.");
            Destroy(this);
        }
    }

    private void Update()
    {
        // switch to binding instead of checking individual controls
        var mouse = Mouse.current;
        if (mouse.rightButton.wasReleasedThisFrame) OnRightClickUp();
        if (mouse.middleButton.isPressed) { /* rotate camera */ }
    }

    private void OnRightClickUp()
    {
        if (!TryGetClickHit(out RaycastHit target)) return; // nothing hit

        
        const float maxNavMeshDistance = 10; // TODO: add to a library, not dependent on this class
        if (NavMesh.SamplePosition(target.point, out NavMeshHit navMeshHit, maxNavMeshDistance, NavMesh.AllAreas))
        {
            if (!TryMoveTo(navMeshHit.position)) return; // destination too close too current position


            SpawnClickMarker(navMeshHit.position);
        }
        else /* target.point not reachable */
        {
            SpawnClickMarker(target.point);
        }
    }

    private bool TryGetClickHit(out RaycastHit target)
    {
        var cam = Camera.main; if (!cam) throw new NullReferenceException("Camera.main null.");
        var mouse = Mouse.current;
        RaycastHit[] results = {default, default};

        var viewportPointToRay = cam.ScreenPointToRay(mouse.position.ReadValue());
        Debug.DrawRay(viewportPointToRay.origin, viewportPointToRay.direction);
        var raycastHitCount = Physics.RaycastNonAlloc(viewportPointToRay, results);
        if (raycastHitCount == 0)
        {
            target = default;
            return false;
        }


        target = results[0];
        if (transform.root != target.transform.root) return true;
        
        
        if (raycastHitCount == 1) return false;
        

        target = results[1];
        return transform.root != target.transform.root;
    }

    // TODO: move to a library
    // not relevant to class
    private static void SpawnClickMarker(Vector3 targetPoint)
    {
        var markerT = Instantiate(DebugManager.Instance.debugValues.modelMissingPrefab, targetPoint,
            Quaternion.identity);
        LimitedLifespan.Limit(markerT.gameObject, 2);
    }

    private bool TryMoveTo(Vector3 targetPoint)
    {
        const float minMoveDistance = 1.04f;
        var distanceFromTarget = Vector3.Distance(targetPoint, transform.position);
        if (distanceFromTarget <= minMoveDistance) return false;

        Debug.Log($"Distance from target = {distanceFromTarget}");

        navMeshAgent.destination = targetPoint;
        return true;
    }
}