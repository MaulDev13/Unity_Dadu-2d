using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropdownHandler : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private NormalDice controller;

    public void Init(int value)
    {
        // value is max dice value

        dropdown.options.Clear();

        List<string> items = new List<string>();
        for (int i = 0; i < value; i++)
        {
            items.Add($"Dice {i + 1}");
        }

        // Fill dropdown with items
        foreach(var item in items)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData() { text = item });
        }

        DropdownItemSelected(dropdown);

        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
    }

    void DropdownItemSelected(TMP_Dropdown dropdown)
    {
        var index = dropdown.value;

        controller.ChangeCurrentDice(index);

        Debug.Log($"Change value {index}");
    }
}
