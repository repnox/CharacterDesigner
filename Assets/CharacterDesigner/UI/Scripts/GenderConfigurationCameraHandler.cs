using System;
using CharacterDesigner.Scripts;
using UnityEngine;

namespace CharacterDesigner.UI.Scripts
{
    [RequireComponent(typeof(Animator))]
    public class GenderConfigurationCameraHandler : MonoBehaviour
    {

        public Character MaleCharacter;

        public Character FemaleCharacter;

        public FaceConfiguratorHelper faceConfiguratorHelper;

        public TextureConfigurationHelper textureConfigurationHelper;

        private Animator animator;
        private int FocusOnHumanMale = Animator.StringToHash("HumanMale");
        private int FocusOnHumanFemale = Animator.StringToHash("HumanFemale");

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void SetFocusIndex(int i)
        {
            if (i == 0)
            {
                ShowMale();
            } else if (i == 1)
            {
                ShowFemale();
            }
        }

        public void ShowMale()
        {
            animator.SetTrigger(FocusOnHumanMale);
            Show(MaleCharacter);

        }

        public void ShowFemale()
        {
            animator.SetTrigger(FocusOnHumanFemale);
            Show(FemaleCharacter);
        }

        private void Show(Character character)
        {
            faceConfiguratorHelper.SetTargetCharacter(character.body);
            textureConfigurationHelper.SetTargetCharacter(character);
        }
    }
}
