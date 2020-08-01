using UnityEngine;

namespace CharacterDesigner.Scripts
{
    [CreateAssetMenu(fileName = "ClothingCollection", menuName = "Repnox/ClothingCollection", order = 1)]
    public class ClothingCollection : ScriptableObject
    {

        public ClothingDefinition[] clothingDefinitions;

    }
}
