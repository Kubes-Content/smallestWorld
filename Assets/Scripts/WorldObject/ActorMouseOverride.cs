using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace WorldObject
{
    public class ActorMouseOverride : SerializedMonoBehaviour
    {
        private Actor actor;
        private NavMeshAgent navMeshAgent;
        private Vector2 cursorPosition;
        [OdinSerialize] private List<(IPlayerInputID, Action<InputAction.CallbackContext>)> actions = new();


        void Start()
        {
            if (!TryGetComponentOrAbort(out actor) 
                || !TryGetComponentOrAbort(out navMeshAgent) 
                || !TryGetComponentOrAbort(out PlayerInput playerInput)) return;

        
            bindInputs(); // TODO: unbind on destroy 

            #region Local Functions
            
            void bindInputs() // serialize inputs in editor
            {
                var inputActions = playerInput.actions ? playerInput.actions : throw new NullReferenceException();
                Debug.Assert(actions != null);
                Debug.Assert(actions.Count != 0);

                foreach (var (idProvider,callback) in actions)
                {
                    Debug.Assert(idProvider != null);
                    Debug.Assert(callback != null);

                    inputActions.FindAction(idProvider.GetID(), true).performed += callback;
                }
            }
            
            #endregion Local Functions
            
        }

        // TODO: Move to a library
        // not relevant to class
        private bool TryGetComponentOrAbort<TComponent>(out TComponent component) where TComponent : Component
        {
            if (TryGetComponent(out component)) return true;

            Debug.LogError($"{typeof(TComponent).Name} script not found.");
            Destroy(this);
            
            return false;
        }

        // private void Update() { }

        private void OnCursorMove(InputAction.CallbackContext context) => cursorPosition = context.ReadValue<Vector2>();
        
        private void OnRightClick(InputAction.CallbackContext _)
        {
            if (!tryGetClickHit(out RaycastHit target)) return; // nothing under mouse hit

            // TODO: check if an interactable object was clicked, show its options
            if (target.transform.root.TryGetComponent(out Interactable interactable)) 
            {
                // TODO: how are we getting/caching the world canvas?
                // spawn with this script on Start() for now
                
                //InteractableUI.Spawn(interactable, transform, target.point, );

                
                throw new NotImplementedException();
                
                return;
            }
            
            TryMoveActor(target.point);

            #region Local Functions
        
            bool tryGetClickHit(out RaycastHit targetLocal)
            {
                var cam = Camera.main; if (!cam) throw new NullReferenceException("Camera.main null.");
                var mouse = Mouse.current;
                RaycastHit[] results = {default, default};

                var viewportPointToRay = cam.ScreenPointToRay(cursorPosition);
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

        private void TryMoveActor(Vector3 targetLocalPoint)
        {
            var pointReachable = NavMesh.SamplePosition(targetLocalPoint, out NavMeshHit navMeshHit,
                NavMeshStaticVariables.MaximumRelevantDistanceFromMesh, NavMesh.AllAreas);
            if (!pointReachable)
            {
                DebugStaticVariables.SpawnModelMissingMarker(targetLocalPoint);
                return;
            }

            var initialClickPositionMarker = DebugStaticVariables.SpawnModelMissingMarker(targetLocalPoint);
            initialClickPositionMarker.GetComponent<Recolorable>().Set(Color.yellow);
            initialClickPositionMarker.localScale /= 2;

            // relevant position on navMesh
            DebugStaticVariables.SpawnModelMissingMarker(navMeshHit.position)
                .GetComponent<Recolorable>().Set(actor.TryMoveTo(navMeshAgent, navMeshHit.position) ? Color.green : Color.red);
        }

        private void OnMiddleClick(InputAction.CallbackContext _)
        {
            // TODO: rotate camera
        }
    }
}