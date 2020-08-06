using CharacterDesigner.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterDesigner.UI.Scripts
{
    public class ClothingConfigurationHelper : MonoBehaviour
    {

        public Character character;

        public ClothingConfigurator shirtsConfigurator;

        public ClothingConfigurator pantsConfigurator;


        public void SetTargetCharacter(Character character)
        {
            this.character = character;
            connectUI();
        }


        private void connectUI()
        {
            shirtsConfigurator.character = character;
            shirtsConfigurator.Init();
            pantsConfigurator.character = character;
            pantsConfigurator.Init();
        }

    }
}