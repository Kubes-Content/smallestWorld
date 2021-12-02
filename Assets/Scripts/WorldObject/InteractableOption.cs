using System;
using System.Collections.Generic;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.AI;

// 11/18/21 having a moment of "you must plan this out. take a step back. do this right." I think it's wasting more time than just attempting a few silly solutions

namespace WorldObject
{
    [Serializable]
    public struct InteractableOption
    {
        [OdinSerialize] private IInteractionData data;
        
        public void Execute(Interactable source, Transform interactor) => data.StartInteraction(source, interactor); // TODO

        public string GetDisplayText(Interactable source, Transform interactor) => data.GetDisplayText(source, interactor);

        internal bool IsValid() => data != null;
    }

    // TODO: SUPPORT vvvvvvvvv
    //
    // string -> dialogue
    // string -> interaction
    
}