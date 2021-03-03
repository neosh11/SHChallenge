using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// This entire component seem unnecessary due to the amount of overlap with SropdownController
// Due to lack of time, this has been ignored for now
// TODO - get rid of this later

public class SubMenuItem : MonoBehaviour
{
    public List<CalcInputs> options;
    [SerializeField] private GameObject optionPrefab;
    [SerializeField] private Transform menuPanel;

    public void GenerateSubMenu()
    {

        // This is done on purpose because prefab was being modified after init
        // TODO need to debug actual reason why
        GameObject toClone = Instantiate(optionPrefab);

        for (int i = 0; i < options.Count; i++)
        {
            GameObject button = Instantiate(toClone);
            CalcInputs op = options[i];
            button.GetComponentInChildren<TMP_Text>().text = op.text;
            button.transform.SetParent(menuPanel);

            if (op.type == InputDataType.NUMERIC)
            {
                button.GetComponent<Button>().onClick.AddListener(
                    () =>
                    {
                        Debug.Log(System.Convert.ToInt32(op.text));
                    }
                );
            }
        }
        Destroy(toClone);
    }

}
