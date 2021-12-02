using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Unity.VisualScripting;
using UnityEngine;

namespace WorldObject
{
    public class Interactable : SerializedMonoBehaviour
    {
        [OdinSerialize] private List<InteractableOption> options = new();

        public IEnumerable<InteractableOption> GetOptions(Transform interactor) => options; //.Where(_ => true);

        private void Start()
        {
            foreach (var o in options)
            {
                Debug.Assert(o.IsValid(), "Invalid interactable-option.");
                Debug.Log($"Option: {o.GetDisplayText(this, null)}");
            }
        }

        public static Interactable Initialize(Transform target, IEnumerable<InteractableOption> newOptions)
        {
            Debug.Assert(!target.GetComponent<Interactable>(), "Interactable trying to be added to an already interactable object");

            var newInteractable = target.AddComponent<Interactable>();
            newInteractable.options.AddRange(newOptions);

            return newInteractable;
        }
    }
    
}