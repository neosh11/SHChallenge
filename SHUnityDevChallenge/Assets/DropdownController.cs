using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum InputDataType
{
    NUMERIC,
    OPERATION
}

public enum InputOperation
{
    ADD,
    SUB,
    MUL,
    DIV,
    POW,
    SQRT
}


// Defines what an input is on a calculator
public class CalcInputs
{
    // If the type is an operation or a number
    public InputDataType type;
    // in the case of numeric - text = value
    public string text;
    // Optional only if type is operation
    public InputOperation operation;
    // Create a submenu from these
    public List<CalcInputs> subOptions;

}




public class DropdownController : MonoBehaviour
{
    [SerializeField] private GameObject optionPrefab;
    [SerializeField] private InputDataType type;
    [SerializeField] private Transform menuPanel;

    private List<CalcInputs> options;



    // Start is called before the first frame update
    void Start()
    {
        BuildDropDown();
    }

    void BuildDropDown()
    {
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



        Debug.Log(options.Count);
        for (int i = 0; i < options.Count; i++)
        {
            GameObject optionButton = Instantiate(optionPrefab);
            CalcInputs op = options[i];

            optionButton.GetComponentInChildren<TMP_Text>().text = op.text;

            Debug.Log("a");
            if (op.type == InputDataType.OPERATION)
            {
                optionButton.GetComponent<Button>().onClick.AddListener(

                    () =>
                    {
                        Debug.Log(op.operation);
                    }
                );
            }

            optionButton.transform.SetParent(menuPanel);

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
