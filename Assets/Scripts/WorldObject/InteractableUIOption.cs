using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace WorldObject
{
    public class InteractableUIOption : SerializedMonoBehaviour
    {
        [ShowInInspector] private InteractableOption option;

        [ShowInInspector] private InteractableUI ui;

        public static InteractableUIOption Spawn(InteractableUI ui, InteractableOption option)
        {
            throw new NotImplementedException();
        }
    }
}
