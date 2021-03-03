using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DarkModeToggle : MonoBehaviour
{

    [SerializeField] CameraController cameraController;
    // Because of the set up,
    // 0 = off
    // 1 = on
    public void OnValChange(float val)
    {

        int on = (int)val;
        switch (on)
        {
            case 1: cameraController.DarkMode(true); break;
            default: cameraController.DarkMode(false); break;
        }
    }
}
