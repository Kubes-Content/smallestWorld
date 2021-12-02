using UnityEngine;

namespace WorldObject
{
    public interface IInteractionData
    {
        Interacting StartInteraction(Interactable interactable, Transform actor);

        string GetDisplayText(Interactable source, Transform interactor);
    }
}