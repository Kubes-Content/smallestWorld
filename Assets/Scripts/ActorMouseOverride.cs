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
    private Vector2 cursorPosition;


    // Start is called before the first frame update
    void Start()
    {
        if (!TryGetComponent(out actor))
        {
            Debug.LogError("Actor script not found.");
            Destroy(this);
            return;
        }

        if (!TryGetComponent(out navMeshAgent))
        {
            Debug.LogError("NavMeshAgent script not found.");
            Destroy(this);
            return;
        }

        var inputActions = GetComponent<PlayerInput>().actions ? GetComponent<PlayerInput>().actions : throw new NullReferenceException();
        const string moveCursorId = "MoveCursor";
        const string rightClickId = "OnCursorAltClick0";
        const string middleClickId = "OnCursorAltClick1";
        inputActions.FindAction(moveCursorId, true).performed += OnCursorMove;
        inputActions.FindAction(rightClickId, true).performed += OnRightClickWorld;
    }

    // private void Update() { }

    private void OnCursorMove(InputAction.CallbackContext context) => cursorPosition = context.ReadValue<Vector2>();

    private void OnRightClickWorld(InputAction.CallbackContext _)
    {
        if (!tryGetClickHit(out RaycastHit target)) return; // nothing under mouse hit

        
        const float maxDistanceFromNavMesh = 10; // TODO: add to a library, not dependent on this class
        var pointReachable = NavMesh.SamplePosition(target.point, out NavMeshHit navMeshHit, maxDistanceFromNavMesh, NavMesh.AllAreas);
        if (!pointReachable)
        {
            SpawnClickMarker(target.point);
            return;
        }

        var initialClickPositionMarker = SpawnClickMarker(target.point);
        initialClickPositionMarker.GetComponent<Recolorable>().Set(Color.yellow);
        initialClickPositionMarker.localScale /= 2;

        // relevant position on navMesh
        SpawnClickMarker(navMeshHit.position)
            .GetComponent<Recolorable>().Set(TryMoveTo(navMeshHit.position) ? Color.green : Color.red);
        
        #region Local Functions
        
        bool tryGetClickHit(out RaycastHit targetLocal)
        {
            var cam = Camera.main; if (!cam) throw new NullReferenceException("Camera.main null.");
            var mouse = Mouse.current;
            RaycastHit[] results = {default, default};

            var viewportPointToRay = cam.ScreenPointToRay(mouse.position.ReadValue());
            Debug.DrawRay(viewportPointToRay.origin, viewportPointToRay.direction);
            var raycastHitCount = Physics.RaycastNonAlloc(viewportPointToRay, results);
            if (raycastHitCount == 0)
            {
                targetLocal = default;
                return false;
            }


            targetLocal = results[0];
            if (transform.root != targetLocal.transform.root) return true;
        
        
            if (raycastHitCount == 1) return false;
        

            targetLocal = results[1];
            return transform.root != targetLocal.transform.root;
        }
        
        #endregion Local Functions
    }

    // TODO: move to a library
    // not relevant to class
    private static Transform SpawnClickMarker(Vector3 targetPoint)
    {
        var markerT = Instantiate(DebugManager.Instance.debugValues.modelMissingPrefab, targetPoint, Quaternion.identity);
        LimitedLifespan.Limit(markerT.gameObject, 2);
        
        return markerT;
    }

    // TODO: move to a library
    // not relevant to class
    private bool TryMoveTo(Vector3 targetPoint)
    {
        const float minMoveDistance = 1.04f; // TODO: move const to a library
        if (Vector3.Distance(targetPoint, transform.position) <= minMoveDistance) return false;
        
        
        navMeshAgent.destination = targetPoint;
        return true;
    }
}