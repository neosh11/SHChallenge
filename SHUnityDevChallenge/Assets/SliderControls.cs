using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class SliderControls : MonoBehaviour
{
    [SerializeField] private DropdownController dropdownController;
    [SerializeField] private TMP_Text leftText;
    [SerializeField] private TMP_Text rightText;

    void Start()
    {
        // Debug.Log()
        leftText.text = dropdownController.minNumber.ToString();
        rightText.text = dropdownController.maxNumber.ToString();
    }

    public void OnValChange(float min, float max)
    {
        dropdownController.minNumber = (int)min;
        dropdownController.maxNumber = (int)max;

        leftText.text = dropdownController.minNumber.ToString();
        rightText.text = dropdownController.maxNumber.ToString();

        dropdownController.reBuildDropdown();
    }
}
