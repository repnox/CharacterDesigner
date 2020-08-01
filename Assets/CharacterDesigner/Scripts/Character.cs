using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace CharacterDesigner.Scripts
{
    public class Character : MonoBehaviour
    {
        public Transform armatureRoot;

        public SkinnedMeshRenderer body;

        public ClothingDefinition[] clothing;

        public MaterialDefinition hairMaterialDefinition;

        public ColorProfile hairColorProfile;

        public float hairTint = .5f;

        public float hairHue = 0;

        public bool refreshClothing = true;

        public bool refreshTexture = true;

        public ColorProfile skinColorProfile;

        public float skinTint = .5f;

        public float skinHue = 0;

        private bool refreshSkinColor = true;

        private GameObject[] currentClothing;

        private Dictionary<string, Transform> transformMap;

        private Mesh originalBodyMesh;

        public void SetHairTint(float v)
        {
            hairTint = v;
            hairMaterialDefinition.baseColor = hairColorProfile.Calculate(hairTint, hairHue);
            hairMaterialDefinition.Refresh();
            refreshTexture = true;
        }

        public void SetHairHue(float v)
        {
            hairHue = v;
            hairMaterialDefinition.baseColor = hairColorProfile.Calculate(hairTint, hairHue);
            hairMaterialDefinition.Refresh();
            refreshTexture = true;
        }

        public void SetSkinTint(float v)
        {
            skinTint = v;
            refreshSkinColor = true;
        }

        public void SetSkinHue(float v)
        {
            skinHue = v;
            refreshSkinColor = true;
        }

        private void Start()
        {
            originalBodyMesh = body.sharedMesh;
            Transform[] allTransforms = armatureRoot.GetComponentsInChildren<Transform>();
            transformMap = new Dictionary<string, Transform>();
            foreach (Transform t in allTransforms)
            {
                transformMap[t.name] = t;
            }

            UpdateSkinColor();
        }

        private void UpdateSkinColor()
        {
            body.sharedMaterial.color = skinColorProfile.Calculate(skinTint, skinHue);
        }

        void Update()
        {
            if (refreshClothing)
            {
                refreshClothing = false;
                refreshTexture = true;
                RefreshClothing();
            }

            if (refreshTexture && Time.frameCount % 20 == 0)
            {
                refreshTexture = false;
                RefreshTexture();
            }

            if (refreshSkinColor)
            {
                UpdateSkinColor();
            }
        }

        public void SetClothing(ClothingDefinition clothingDefinition)
        {
            refreshClothing = true;
            ClearSlot(clothingDefinition.clothingSlot);
            Array.Resize(ref clothing, clothing.Length+1);
            clothing[clothing.Length - 1] = clothingDefinition;
        }

        public void ClearSlot(ClothingSlot slot)
        {
            refreshClothing = true;
            clothing = new List<ClothingDefinition>(clothing)
                .FindAll(definition => definition.clothingSlot != slot)
                .ToArray();
        }

        private void RefreshClothing()
        {
            if (currentClothing != null)
            {
                foreach (var c in currentClothing)
                {
                    Destroy(c);
                }
            }

            currentClothing = new GameObject[clothing.Length];
            var hiddenTris = new List<int>();
            for (int j = 0; j < clothing.Length; j++)
            {
                var clothingDefinition = clothing[j];
                if (clothingDefinition.HasMeshComponent())
                {
                    hiddenTris.AddRange(clothingDefinition.clothingMeshDefinition.hiddenTris);
                    var smr = clothingDefinition.BuildMeshComponent();
                    AssignBones(smr);
                    currentClothing[j] = smr.gameObject;
                }
            }

            HideBodyTris(hiddenTris);
            hairMaterialDefinition.Refresh();
        }

        private void RefreshTexture()
        {
            Texture2D overlayTexture = null;
            for (int j = 0; j < clothing.Length; j++)
            {
                var clothingDefinition = clothing[j];
                if (clothingDefinition.HasTextureComponent())
                {
                    if (!overlayTexture)
                    {
                        overlayTexture = clothingDefinition.BuildTextureComponent();
                    }
                    else
                    {
                        BlendOverlays(overlayTexture, clothingDefinition.BuildTextureComponent());
                    }
                }
            }


            if (overlayTexture)
            {
                body.sharedMaterial.mainTexture = overlayTexture;
            }
            else
            {
                body.sharedMaterial.mainTexture = null;
            }
        }

        private void BlendOverlays(Texture2D tex1, Texture2D tex2)
        {
            if (tex1.dimension != tex2.dimension)
            {
                throw new Exception("All character textures must have the same dimensions.");
            }

            Color[] cs1 = tex1.GetPixels();
            Color[] cs2 = tex2.GetPixels();
            for (int i = 0; i < cs1.Length; i++)
            {
                var c1 = cs1[i];
                var c2 = cs2[i];
                var r = Mathf.Lerp(c1.r, c2.r, c2.a);
                var g = Mathf.Lerp(c1.g, c2.g, c2.a);
                var b = Mathf.Lerp(c1.b, c2.b, c2.a);
                var a = Mathf.Lerp(c1.a, 1, c2.a);
                cs1[i] = new Color(r, g, b, a);
            }
            tex1.SetPixels(cs1);
        }

        private void HideBodyTris(List<int> hiddenTris)
        {
            Mesh mesh = Instantiate(originalBodyMesh);
            var tris = mesh.triangles;
            foreach (int i in hiddenTris)
            {
                tris[i] = 0;
                tris[i + 1] = 0;
                tris[i + 2] = 0;
            }
            mesh.triangles = tris;
            mesh.UploadMeshData(true);
            body.sharedMesh = mesh;
        }

        private void AssignBones(SkinnedMeshRenderer smr)
        {
            smr.rootBone = body.rootBone;
            Transform[] existingBones = body.bones;
            Transform[] newBones = new Transform[existingBones.Length];

            for (int i = 0; i < existingBones.Length; i++)
            {
                Transform existingBone = existingBones[i];
                if (existingBone != null)
                {
                    if (transformMap.ContainsKey(existingBone.name))
                    {
                        newBones[i] = transformMap[existingBone.name];
                    }
                    else
                    {
                        Debug.LogError("Bone not found: " + existingBone.name);
                    }
                }
            }

            smr.bones = newBones;
        }
    }
}
