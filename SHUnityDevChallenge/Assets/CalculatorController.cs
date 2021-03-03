﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculatorController : MonoBehaviour
{

    // Internal structure to store inputs - might be useful for history?
    // Placed inside to avoid nameing overlap with Unity's input
    public class Input
    {
        public InputDataType type { get; set; }
        // Optional operation
        public InputOperation operation { get; set; }
        public int number { get; set; }
        public double value { get; set; }

    }


    // This is stored in order to turn off all the dropdown panels when the app starts
    [SerializeField] private List<GameObject> panels;
    public List<Input> inputs { get; private set; }
    // Used to store where in the input area the individual is
    // -1 means list is empty
    public int position { get; private set; }



    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject g in panels) g.SetActive(false);
        inputs = new List<Input>();
        position = -1;

    }

    // Listeners for the controller
    void Awake()
    {
        Messenger<int>.AddListener(GameEvent.SEND_NUM, OnNumberReceived);
        Messenger<InputOperation>.AddListener(GameEvent.SEND_OP, OnOpReceived);
        Messenger.AddListener(GameEvent.UNDO, Undo);
        Messenger.AddListener(GameEvent.REDO, Redo);
    }
    void OnDestroy()
    {
        Messenger<int>.RemoveListener(GameEvent.SEND_NUM, OnNumberReceived);
        Messenger<InputOperation>.RemoveListener(GameEvent.SEND_OP, OnOpReceived);
        Messenger.RemoveListener(GameEvent.UNDO, Undo);
        Messenger.RemoveListener(GameEvent.REDO, Redo);
    }

    void Undo()
    {
        // TODO BROKEN! NEED CHECKS
        position--;
        Messenger.Broadcast(GameEvent.UPDATE_HISTORY);
    }

    void Redo()
    {
        // TODO BROKEN! NEED CHECKS
        position++;
        Messenger.Broadcast(GameEvent.UPDATE_HISTORY);

    }


    void OnNumberReceived(int num)
    {
        // Most numbers require an operator or nothing before
        //  sqrt is a special case - 

        // Check if last item was nil or ( an operation but not squareroot)
        if (inputs.Count == 0)
        {
            // Add number
            inputs.Add(new Input()
            {
                value = num,
                type = InputDataType.NUMERIC,
                number = num
            });
            position = 0;
        }
        else if (
            inputs[position].type == InputDataType.OPERATION &&
            inputs[position].operation != InputOperation.SQRT
        )
        {

            // Remove all items after position if position is not count-1
            RemoveItemsAfterPos();
            // Evaluate value based on last operation
            UpdateValueAfterNumber(num, inputs[inputs.Count - 1].operation);
            position++;
        }
        else
        {
            // Remove all items after position if position is not count-1
            RemoveItemsAfterPos();

            // Reset value to nil
            inputs.Add(new Input()
            {
                value = num,
                type = InputDataType.NUMERIC,
                number = num
            });
            position++;
        }


        // After evaluation, update screen :o
        UpdateRepresentation();
    }

    void UpdateValueAfterNumber(int num, InputOperation operation)
    {
        if (operation == InputOperation.SQRT) return;
        double newVal = PerformOperation(inputs[inputs.Count - 1].value, num, operation);
        inputs.Add(new Input()
        {
            value = newVal,
            type = InputDataType.NUMERIC,
            number = num
        });
    }

    public static double PerformOperation(double v1, double v2, InputOperation operation)
    {
        switch (operation)
        {
            case InputOperation.ADD:
                return v1 + v2;
            case InputOperation.SUB:
                return v1 - v2;
            case InputOperation.MUL:
                return v1 * v2;
            case InputOperation.DIV:
                return v1 / v2;
            case InputOperation.POW:
                return System.Math.Pow(v1, v2);
            // SQRT NOT ALLOWED!
            // case InputOperation.SQRT:
            //     return;
            default:
                return v1;
        }
    }

    void OnOpReceived(InputOperation op)
    {
        // If no preveious inputs bad call!
        if (inputs.Count == 0)
        {
            Debug.Log("Bad call 0_");
        }
        // Check if last operaition was numer or square-root
        else if (inputs[position].type == InputDataType.NUMERIC ||
                    (
                        inputs[position].type == InputDataType.OPERATION &&
                        inputs[position].operation == InputOperation.SQRT)
                    )
        {
            // Remove all items after position if position is not count-1
            RemoveItemsAfterPos();

            // Allowed - add to input

            inputs.Add(new Input()
            {
                type = InputDataType.OPERATION,
                operation = op,
                value = inputs[inputs.Count - 1].value
            });
            position++;
        }
        else
        {
            //Bad call
            Debug.Log("Bad call 1_");
        }

        // After evaluation, update screen :o
        UpdateRepresentation();
    }

    // This is thinking about the future when undo and redo are implemented
    void RemoveItemsAfterPos()
    {
        if (position != inputs.Count - 1)
            inputs.RemoveRange(position + 1, inputs.Count - position - 1);
    }


    void UpdateRepresentation()
    {
        // Display value
        if (inputs.Count > 0)
        {
            Debug.LogFormat("Value: {0}", inputs[inputs.Count - 1].value);
        }
        else
        {
            Debug.Log("Value: 0");
        }
        Messenger.Broadcast(GameEvent.UPDATE_HISTORY);
    }
}
