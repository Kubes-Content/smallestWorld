using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using WorldObject;

public class StaticManagerVars : ScriptableObject
{
    [SerializeField, InlineEditor] private ActorStaticVariables actorStaticVars;
    public ActorStaticVariables ActorStaticVars => actorStaticVars;
    
    [SerializeField, InlineEditor] private DebugStaticVariables debugStaticVars;
    public DebugStaticVariables DebugStaticVars => debugStaticVars;
}