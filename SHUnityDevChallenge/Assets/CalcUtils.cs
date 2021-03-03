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

public class DataManager
{
    public static string OperationToString(InputOperation op)
    {
        switch (op)
        {
            case InputOperation.ADD: return "+";
            case InputOperation.SUB: return "-";
            case InputOperation.MUL: return "*";
            case InputOperation.DIV: return "/";
            case InputOperation.POW: return "POW";
            case InputOperation.SQRT: return "SQRT";
            default: return "";
        }
    }
}