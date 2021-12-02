using System;
using UnityEngine;

[Serializable]
public struct ResourceDropChance
{
    // TODO: Resource scriptable class

    // TODO: hide in editor if resource is null
    [SerializeField] private float dropChance; // out of 100.0f

    // TODO: hide in editor if resource is null or stackable
    [SerializeField] private Range dropQuantityRange; // Min/Max of quantity that will be dropped // ignored for non-stackable items
}