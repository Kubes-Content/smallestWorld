using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace WorldObject
{
    public class InteractableUIOption : SerializedMonoBehaviour
    {
        [ShowInInspector] private InteractableOption option;

        private InteractableUI ui;

        // ref to interactable?
        // ref to interactableUI?
        // interactableUI can be used to retrieve ref to Interactable?
    }
}
