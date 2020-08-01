using CharacterDesigner.Scripts;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CharacterDesigner.UI.Scripts
{
    public class TextureConfigurationHelper : MonoBehaviour
    {

        public Character character;
    
        public Slider SkinTint;

        public Slider SkinHue;

        public ClothingConfigurator HairStyle;

        public ClothingConfigurator EyebrowStyle;

        public Slider HairTint;

        public Slider HairHue;

        public void SetTargetCharacter(Character character)
        {
            this.character = character;
            connectUI();
        }

        private void connectUI()
        {
            resetSlider(SkinTint, character.SetSkinTint);
            resetSlider(SkinHue, character.SetSkinHue);

            HairStyle.character = character;
            HairStyle.Init();
            EyebrowStyle.character = character;
            EyebrowStyle.Init();

            resetSlider(HairTint, character.SetHairTint);
            resetSlider(HairHue, character.SetHairHue);
        }

        private void resetSlider(Slider slider, UnityAction<float> unityAction)
        {
            slider.onValueChanged.RemoveAllListeners();
            slider.onValueChanged.AddListener(unityAction);
        }
        
    }
}
