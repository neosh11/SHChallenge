using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DarkModeToggle : MonoBehaviour
{
    // Because of the set up,
    // 0 = off
    // 1 = on
    public void OnValChange(float val)
    {
        int on = (int)val;
        // switch (on)
        // {
        //     case 1: darkModeText.text = "ON"; break;
        //     default: darkModeText.text = "OFF"; break;
        // }
    }
}
