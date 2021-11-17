using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using WorldObject;

public class StaticManagerVars : ScriptableObject
{
    // TODO: preview contents in editor
    [SerializeField] private ActorStaticVariables actorStaticVars;
    
    public ActorStaticVariables ActorStaticVars => actorStaticVars;
    
    // TODO: create separate class for debug values
    public Transform modelMissingPrefab;
}
