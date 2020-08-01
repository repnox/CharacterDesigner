using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MaterialDefinition", menuName = "Repnox/MaterialDefinition", order = 1)]
public class MaterialDefinition : ScriptableObject
{
    public Shader shader;
    
    public Color baseColor;

    public float baseSmoothness;

    public float baseMetallic;

    private Material cachedMaterial;

    public Material GetMaterial()
    {
        if (!cachedMaterial)
        {
            cachedMaterial = new Material(shader);
            Refresh();
        }
        return cachedMaterial;
    }

    public void Refresh()
    {
        GetMaterial();
        cachedMaterial.SetColor("_Color", baseColor);
        cachedMaterial.SetFloat("_Glossiness", baseSmoothness);
        cachedMaterial.SetFloat("_Metallic", baseMetallic);
    }
}
