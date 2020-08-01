using System;
using Unity.Collections;
using UnityEngine;

namespace CharacterDesigner.Scripts
{
    [CreateAssetMenu(fileName = "ClothingDefinition", menuName = "Repnox/ClothingDefinition", order = 1)]
    public class ClothingDefinition : ScriptableObject
    {
        public string clothingName;

        public ClothingSlot clothingSlot;
    
        public ClothingMeshDefinition clothingMeshDefinition;

        public Texture2D textureLayer;

        public MaterialDefinition materialDefinition;

        public bool HasMeshComponent()
        {
            return clothingMeshDefinition;
        }

        public bool HasTextureComponent()
        {
            return textureLayer;
        }

        public SkinnedMeshRenderer BuildMeshComponent()
        {
            if (clothingMeshDefinition)
            {
                var gameObject = new GameObject(clothingName);
                SkinnedMeshRenderer skinnedMeshRenderer = gameObject.AddComponent<SkinnedMeshRenderer>();
                skinnedMeshRenderer.sharedMesh = clothingMeshDefinition.mesh;
                var material = materialDefinition.GetMaterial();
                skinnedMeshRenderer.sharedMaterial = material;
                return skinnedMeshRenderer;
            }
            else
            {
                return null;
            }
        }

        public Texture2D BuildTextureComponent(float alpha = 1f)
        {
            if (textureLayer)
            {
                Color[] colors = textureLayer.GetPixels();
                for (int i = 0; i < colors.Length; i++)
                {
                    Color profileColor = materialDefinition.baseColor;
                    profileColor.a = colors[i].a * alpha;
                    colors[i] = profileColor;
                }
                
                var output = new Texture2D(textureLayer.width, textureLayer.height, TextureFormat.ARGB32, true);
                output.SetPixels(colors);
                output.Apply(true, true);
                return output;
            }
            else
            {
                return null;
            }
        }
    }
}
