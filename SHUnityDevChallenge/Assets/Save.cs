using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    // We just need to save the inputs
    public List<CalculatorController.CInput> inputs;
    public int position;


    public Save(List<CalculatorController.CInput> _inputs, int _position)
    {
        inputs = _inputs;
        position = _position;
    }
}