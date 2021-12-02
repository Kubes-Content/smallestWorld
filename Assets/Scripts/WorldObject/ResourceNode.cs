using UnityEngine;

namespace WorldObject
{
    public class ResourceNode : MonoBehaviour
    {
        // drop chances // default drop is usually 100% drop


        public bool TryHarvest(Actor harvester, object tool)
        {
            Debug.Log($"{name} Harvesting.......");
            
            return false;
        }
    }
}