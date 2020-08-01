using UnityEngine;

namespace CharacterDesigner.Scripts
{
    [CreateAssetMenu(fileName = "ClothingMeshDefinition", menuName = "Repnox/ClothingMeshDefinition", order = 1)]
    public class ClothingMeshDefinition : ScriptableObject
    {
        public bool isHuman;

        public bool isMale;

        public bool isFemale;

        public Mesh mesh;

        public int[] hiddenTris;


    }
}
