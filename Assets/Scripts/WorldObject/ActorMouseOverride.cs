using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace WorldObject
{
    public class ActorMouseOverride : MonoBehaviour
    {
        private Actor actor;
        private NavMeshAgent navMeshAgent;
        private Vector2 cursorPosition;


        void Start()
        {
            if (!TryGetComponentOrDie(out actor) 
                || !TryGetComponentOrDie(out navMeshAgent) 
                || !TryGetComponentOrDie(out PlayerInput playerInput)) return;

        
            var inputActions = playerInput.actions ? playerInput.actions : throw new NullReferenceException();
            inputActions.FindAction(PlayerInputAction.ID.MoveCursor, true).performed += OnCursorMove;
            inputActions.FindAction(PlayerInputAction.ID.RightClick, true).performed += OnRightClickWorld;
            inputActions.FindAction(PlayerInputAction.ID.MiddleClick, true).performed += OnMiddleClick;
        }

        // TODO: Move to a library
        // not relevant to class
        private bool TryGetComponentOrDie<TComponent>(out TComponent component) where TComponent : Component
        {
            if (TryGetComponent(out component)) return true;

            Debug.LogError($"{typeof(TComponent).Name} script not found.");
            Destroy(this);

            return false;
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
                .GetComponent<Recolorable>().Set(actor.TryMoveTo(navMeshAgent, navMeshHit.position) ? Color.green : Color.red);
        
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

        private void OnMiddleClick(InputAction.CallbackContext _)
        {
            // TODO: rotate camera
        }

        // TODO: move to a library, DebugStaticVars ScriptableObject?
        // not relevant to class
        private static Transform SpawnClickMarker(Vector3 targetPoint)
        {
            var markerT = Instantiate(StaticManager.Values.modelMissingPrefab, targetPoint, Quaternion.identity);
            LimitedLifespan.Limit(markerT.gameObject, 2);
        
            return markerT;
        }
    }
}