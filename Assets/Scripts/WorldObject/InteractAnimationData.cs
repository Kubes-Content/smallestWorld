using System;
using UnityEngine;

namespace WorldObject
{
    [Serializable]
    public struct InteractAnimationData : IInteractionData
    {
        [SerializeField] private string verb;
        
        // animation
        
        // pre-calculated products

        private int loopCount;

        public string GetDisplayText(Interactable source, Transform interactor) => $"{verb} [{source.name}]";

        public Interacting StartInteraction(Interactable interactable, Transform actor)
        {
            Debug.Assert(interactable && actor);
            Debug.Log($"{actor.name} is gathering ");

            throw new NotImplementedException();
        }
    }
}