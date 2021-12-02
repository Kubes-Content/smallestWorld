using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace WorldObject
{
    public class InteractableUI : MonoBehaviour
    {
        [ShowInInspector] private static InteractableUI prefab;
        
        private static InteractableUI instance;
        
        [ShowInInspector] private Interactable target;
        
        [ShowInInspector] private List<InteractableUIOption> options = new();

        public IEnumerable<InteractableUIOption> GetOptions() => options;

        public static InteractableUI Spawn(Interactable target, Transform interactor, Vector3 position, Canvas canvas)
        {
            if (instance) Destroy(instance.gameObject);

            instance = Instantiate(prefab, position, Camera.main.transform.rotation, canvas.transform); // TODO: inject camera

            instance.target = target;
            //target.GetOptions(interactor); // TODO: convert to UIOption's
            
            throw new NotImplementedException(); // TODO: finish

            return instance;
        }
    }
}