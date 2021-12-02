using Sirenix.OdinInspector;

namespace WorldObject
{
    public class Interacting : SerializedMonoBehaviour
    {
        private Actor actor;

        [ShowInInspector] private IInteractionData data;
    }
}