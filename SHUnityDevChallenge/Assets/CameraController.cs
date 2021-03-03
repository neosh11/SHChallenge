using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Most of this code is referenced from my own previously build components
public class CameraController : MonoBehaviour
{
    static readonly Color DARK = new Color(0.247f, 0.275f, 0.282f);
    static readonly Color LIGHT = new Color(0.569f, 0.635f, 0.725f);
    private Camera cam;


    // Sensitivities for mouse inputs
    public float sensitivityHor = 9.0f;
    public float sensitivityVert = 9.0f;


    // Limits on vertical + mouse vert angle
    private float _rotationX = 0;
    public float minimumVert = -45.0f;
    public float maximumVert = 45.0f;

    // Show cross hair or not
    private bool showX;
    private static int crossHairSize = 20;
    private static GUIStyle guiStyle = new GUIStyle();

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        // Set light theme by default
        cam.backgroundColor = LIGHT;
        showX = false;
        guiStyle.normal.textColor = Color.red;


    }

    void OnGUI()
    {
        if (showX)
        {
            // Calculated here to not be affected by screensize changes
            float posX = cam.pixelWidth / 2 - crossHairSize / 4;
            float posY = cam.pixelHeight / 2 - crossHairSize / 2;
            GUI.Label(new Rect(posX, posY, crossHairSize, crossHairSize), "X", guiStyle);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("MouseMove"))
        {
            // show crosshair
            showX = true;

            // GetAxis is rate independent
            // Calc and clamp vertical rotation 
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);
            // Calc and pres horizontal rotaion
            float delta = Input.GetAxis("Mouse X") * sensitivityHor;
            float rotationY = transform.localEulerAngles.y + delta;

            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
        else if (showX)
        {
            showX = false;
        }
    }

    public void DarkMode(bool on)
    {
        if (on) cam.backgroundColor = DARK;
        else cam.backgroundColor = LIGHT;
    }
}
