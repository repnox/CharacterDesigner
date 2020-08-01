using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace CharacterDesigner.Scripts
{
    public class HideVertices : MonoBehaviour
    {

        public bool preview = true;

        public SkinnedMeshRenderer skinnedMeshRenderer;

        public ClothingMeshDefinition clothingMesh;

        public GameObject collidersRoot;

        private Mesh originalMesh;

        private Mesh modifiedMesh;

        void Start()
        {
            var colliders = collidersRoot.GetComponentsInChildren<Collider>();
            originalMesh = skinnedMeshRenderer.sharedMesh;
            modifiedMesh = Instantiate(originalMesh);

            var vertices = modifiedMesh.vertices;
            var tris = modifiedMesh.triangles;
            var hiddenTris = new List<int>();
            for (int i = 0; i < tris.Length; i+=3)
            {
                foreach (var coll in colliders)
                {
                    if (isWithinCollider(coll, vertices[tris[i]])
                        && isWithinCollider(coll, vertices[tris[i+1]])
                        && isWithinCollider(coll, vertices[tris[i+2]])
                        )
                    {
                        hiddenTris.Add(i);
                        tris[i] = 0;
                        tris[i + 1] = 0;
                        tris[i + 2] = 0;
                        break;
                    }
                }
            }

            clothingMesh.hiddenTris = hiddenTris.ToArray();

            if (preview)
            {
                modifiedMesh.triangles = tris;
                modifiedMesh.UploadMeshData(false);
                skinnedMeshRenderer.sharedMesh = modifiedMesh;
            }
        }

        private bool isWithinCollider(Collider coll, Vector3 point)
        {
            var direction = coll.bounds.center - point;
            var ray = new Ray(point, direction);
            var hit = coll.Raycast(ray, out RaycastHit hitInfo, direction.magnitude);
            return !hit;
        }
    }
}
