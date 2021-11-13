using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class Recolorable : MonoBehaviour
{
    private static readonly int ColorId = Shader.PropertyToID("_Color");
    
    [Button, DisableInEditorMode]
    public void Set(Color color)
    {
        // if not dynamic mat, replace it, if it is dynamic, just change the color
        
        foreach (var childRenderer in GetComponentsInChildren<Renderer>())
            foreach (var mat in childRenderer.GetMaterials())
                mat.SetColor(ColorId, color);
    }
}