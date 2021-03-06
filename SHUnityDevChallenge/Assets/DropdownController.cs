﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DropdownController : MonoBehaviour
{
    // Currently set up in a dodgy manner
    // Operations menu uses a different prefab compared to numbers
    [SerializeField] private GameObject optionPrefab;
    [SerializeField] private InputDataType type;
    [SerializeField] private Transform menuPanel;
    private List<CalcInputs> options;

    // TODO need to make this modifiable for later activity
    public int minNumber = 0;
    public int maxNumber = 9;
    private List<GameObject> subPanels;


    // Start is called before the first frame update
    void Start()
    {
        BuildDropDown();
    }

    void BuildDropDown()
    {

        // Initialize
        subPanels = new List<GameObject>();

        if (type == InputDataType.OPERATION)
        {
            options = new List<CalcInputs>(){
            new CalcInputs()
            {
                operation = InputOperation.ADD,
                text = "+",
                type = InputDataType.OPERATION
            },
            new CalcInputs()
            {
                operation = InputOperation.SUB,
                text = "-",
                type = InputDataType.OPERATION
            },
            new CalcInputs()
            {
                operation = InputOperation.MUL,
                text = "*",
                type = InputDataType.OPERATION
            },
            new CalcInputs()
            {
                operation = InputOperation.DIV,
                text = "/",
                type = InputDataType.OPERATION
            },
            new CalcInputs()
            {
                operation = InputOperation.POW,
                text = "^",
                type = InputDataType.OPERATION
            },
            new CalcInputs()
            {
                operation = InputOperation.SQRT,
                text = "SQRT",
                type = InputDataType.OPERATION
            }
            };
        }
        else if (type == InputDataType.NUMERIC)
        {
            options = new List<CalcInputs>();
            for (int i = minNumber; i <= maxNumber; i++)
            {
                List<CalcInputs> subOptions = new List<CalcInputs>();

                for (int j = 0; j < 10; j++)
                {
                    subOptions.Add(new CalcInputs()
                    {
                        text = i.ToString() + j.ToString(),
                        type = InputDataType.NUMERIC,
                    });
                }

                options.Add(new CalcInputs()
                {
                    text = i.ToString(),
                    type = InputDataType.SUBMENU,
                    subOptions = subOptions
                });

            }
        }
        for (int i = 0; i < options.Count; i++)
        {



            GameObject optionButton = Instantiate(optionPrefab);
            CalcInputs op = options[i];

            optionButton.GetComponentInChildren<TMP_Text>().text = op.text;

            if (op.type == InputDataType.SUBMENU)
            {


                // Store panel
                GameObject panel = optionButton.transform.Find("Panel").gameObject;
                subPanels.Add(panel);

                SubMenuItem subButtonComp = optionButton.GetComponent<SubMenuItem>();
                subButtonComp.options = op.subOptions;
                subButtonComp.GenerateSubMenu();

            }
            // Unfortunatly this code is never called due to construction
            // Will only be useful if SubMenuItem code is merged here
            // TODO left due to time constraints
            // else if (op.type == InputDataType.NUMERIC)
            // {
            //     // Pass the value to calculator controller
            //     optionButton.GetComponent<Button>().onClick.AddListener(
            //         () =>
            //         {
            //             Messenger<int>.Broadcast(GameEvent.SEND_NUM, System.Convert.ToInt32(op.text));

            //         }
            //     );
            // }

            else if (op.type == InputDataType.OPERATION)
            {
                // Pass the value to calculator controller
                optionButton.GetComponent<Button>().onClick.AddListener(
                    () =>
                    {
                        // Broadcast message
                        Messenger<InputOperation>.Broadcast(GameEvent.SEND_OP, op.operation);
                    }
                );
            }
            optionButton.transform.SetParent(menuPanel);
        }
    }

    public void reBuildDropdown()
    {
        foreach (Transform child in menuPanel)
        {
            GameObject.Destroy(child.gameObject);
        }
        BuildDropDown();
    }


    public void onClick()
    {
        //Close all subPanels, will only run if type is submenu A.K.A within numbers
        if (subPanels != null && subPanels.Count > 0)
        {
            foreach (GameObject g in subPanels)
            {
                g.SetActive(false);
            }
        }
    }
}
