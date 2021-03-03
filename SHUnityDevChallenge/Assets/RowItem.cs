using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
public class RowItem : MonoBehaviour
{
    public string entry;
    public string value;

    public bool selected;
    [SerializeField] private TMP_Text entryText;
    [SerializeField] private TMP_Text valueText;
    [SerializeField] private GameObject image;

    public void UpdateValues()
    {
        entryText.text = entry;
        valueText.text = value;
        if (selected)
        {
            image.SetActive(true);
        }
        else
        {
            entryText.color = Color.black;
            valueText.color = Color.black;
        }
    }
}
