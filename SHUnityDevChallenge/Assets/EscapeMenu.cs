using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class EscapeMenu : MonoBehaviour
{

    [SerializeField] CalculatorController calculatorController;
    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void onSave()
    {
        Save save = new Save(calculatorController.inputs, calculatorController.position);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/filesave.save");
        bf.Serialize(file, save);
        file.Close();
    }

    public void onLoad()
    {
        // 1
        if (File.Exists(Application.persistentDataPath + "/filesave.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/filesave.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            // update save.inputs
            calculatorController.LoadInputs(save.inputs, save.position);
        }
        else
        {
            Debug.Log("No game saved!");
        }
    }


    public void onReset()
    {
        calculatorController.ResetCalculator();
    }


    public void ToggleOpenClose()
    {
        if (gameObject.activeSelf) Close();
        else Open();
    }
}
