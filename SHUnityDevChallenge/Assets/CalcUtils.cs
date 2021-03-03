using System.Collections.Generic;


public enum InputDataType
{
    NUMERIC,
    OPERATION,
    SUBMENU
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