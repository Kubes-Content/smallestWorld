using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

public class Recolorable : MonoBehaviour
{
    // if unlit use _BaseColor ; standard uses _Color ?
    private static readonly int ColorId = Shader.PropertyToID("_BaseColor");
    
    [Button, DisableInEditorMode]
    public void Set(Color color)
    {
        foreach (var childRenderer in GetComponentsInChildren<Renderer>())
            foreach (var material in childRenderer.materials)
                material.SetColor(ColorId, color);
    }
}