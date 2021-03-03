using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraController : MonoBehaviour
{
    static readonly Color DARK = new Color(0.247f, 0.275f, 0.282f);
    static readonly Color LIGHT = new Color(0.569f, 0.635f, 0.725f);

    private Camera cam;


    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        // Set light theme by default
        cam.backgroundColor = LIGHT;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DarkMode(bool on)
    {
        if (on) cam.backgroundColor = DARK;
        else cam.backgroundColor = LIGHT;
    }
}
