using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class InputHistory : MonoBehaviour
{

    [SerializeField] private CalculatorController calculatorController;
    [SerializeField] private GameObject rowPrefab;
    [SerializeField] private Transform scrollPanel;
    [SerializeField] private TMP_Text total;


    private List<GameObject> rows;

    // Start is called before the first frame update
    void Start()
    {
        rows = new List<GameObject>();
    }

    void Awake()
    {
        Messenger.AddListener(GameEvent.UPDATE_HISTORY, UpdateScrollView);
    }
    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.UPDATE_HISTORY, UpdateScrollView);
    }


    void UpdateScrollView()
    {
        // TODO need to make more efficient
        Debug.LogFormat("Number of rows {0}", rows.Count);

        for (int i = 0; i < this.rows.Count; i++)
        {
            Debug.Log("Destroying");
            Destroy(this.rows[i]);
        }


        rows.Clear();

        List<CalculatorController.Input> inputs = calculatorController.inputs;
        Debug.LogFormat("Number of inputs {0}", inputs.Count);

        // foreach (CalculatorController.Input i in inputs)

        for (int i = inputs.Count - 1; i >= 0; i--)
        {
            GameObject row = Instantiate(rowPrefab);

            this.rows.Add(row);

            RowItem ri = row.GetComponent<RowItem>();
            if (inputs[i].type == InputDataType.OPERATION)
            {
                ri.entry = DataManager.OperationToString(inputs[i].operation);
            }
            else
            {
                ri.entry = inputs[i].number.ToString();
            }
            ri.value = inputs[i].value.ToString();
            if (calculatorController.position == i)
            {
                Debug.Log("Selected");
                ri.selected = true;
            }
            ri.UpdateValues();

            row.transform.SetParent(scrollPanel);

        }

        total.text = inputs[calculatorController.position].value.ToString();
    }


}
