using System;
using System.Collections;
using System.Collections.Generic;
using CharacterDesigner.UI.Scripts;
using UnityEngine;

[ExecuteInEditMode]
public class FaceConfiguratorHelper : MonoBehaviour
{
    
    public SkinnedMeshRenderer skinnedMeshRenderer;

    public bool updateNow;

    private void Update()
    {
#if (UNITY_EDITOR)
        if (updateNow)
        {
            updateNow = false;
            connectUI();
        }
#endif
    }

    public void SetTargetCharacter(SkinnedMeshRenderer character)
    {
        skinnedMeshRenderer = character;
        connectUI();
    }

    private void connectUI()
    {
        var faceConfigurators = GetComponentsInChildren<FaceConfigurator>();
        foreach (var faceConfigurator in faceConfigurators)
        {
            faceConfigurator.skinnedMeshRenderer = skinnedMeshRenderer;
        }
    }


}
