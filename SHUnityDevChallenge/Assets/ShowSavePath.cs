﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowSavePath : MonoBehaviour
{
    void Start()
    {
        GetComponent<TMP_Text>().text = Application.persistentDataPath + "/filesave.save";

    }
}
