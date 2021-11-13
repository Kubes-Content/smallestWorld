using System.Collections.Generic;
using UnityEngine;

public static class RendererExtensions
{
    public static List<Material> GetMaterials(this Renderer source)
    {
        var mats = new List<Material>();
        source.GetMaterials(mats);

        return mats;
    }
}