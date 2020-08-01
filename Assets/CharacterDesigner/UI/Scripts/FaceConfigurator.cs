using UnityEngine;
using UnityEngine.UI;

namespace CharacterDesigner.UI.Scripts
{
    [RequireComponent(typeof(Slider))]
    public class FaceConfigurator : MonoBehaviour
    {
        public SkinnedMeshRenderer skinnedMeshRenderer;

        public string blendShapeName;

        public float min = -100;

        public float max = 100;

        private Slider slider;

        private int blendShapeIndex;

        private void Start()
        {
            slider = GetComponent<Slider>();
            slider.onValueChanged.AddListener(ApplySliderValue);
        }

        public void ApplySliderValue(float sliderValue)
        {
            blendShapeIndex = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(blendShapeName);
            float weight = sliderValue * (max - min) + min;
            skinnedMeshRenderer.SetBlendShapeWeight(blendShapeIndex, weight);
        }

    }
}
