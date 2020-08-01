using System;
using System.Collections;
using System.Collections.Generic;
using CharacterDesigner.Scripts;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Dropdown))]
public class ClothingConfigurator : MonoBehaviour
{
    public Character character;

    public ClothingSlot clothingSlot;

    public ClothingCollection clothingCollection;

    private Dropdown dropdown;

    private void Awake()
    {
        dropdown = GetComponent<Dropdown>();
        dropdown.onValueChanged.AddListener(ChangeSelectedIndex);
        dropdown.options = ToOptions(clothingCollection.clothingDefinitions);
    }

    private void Start()
    {
        if (!character)
        {
            character = FindObjectOfType<Character>();
        }

        Init();
    }

    public void Init()
    {
        dropdown.value = 0;
        ChangeSelectedIndex(0);
    }

    private List<Dropdown.OptionData> ToOptions(ClothingDefinition[] clothingDefinitions)
    {
        var options = new List<Dropdown.OptionData>();
        options.Add(new Dropdown.OptionData("None"));
        foreach (var clo in clothingDefinitions)
        {
            options.Add(new Dropdown.OptionData(clo.clothingName));
        }

        return options;
    }

    private void ChangeSelectedIndex(int i)
    {
        if (i == 0)
        {
            character.ClearSlot(clothingSlot);
        }
        else
        {
            character.SetClothing(clothingCollection.clothingDefinitions[i-1]);
        }
    }
}
