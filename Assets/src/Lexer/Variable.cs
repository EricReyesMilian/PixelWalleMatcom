public enum VariableType { Number, Boolean, String, Undefined }

public class Variable
{
    public object Value { get; private set; }
    public VariableType Type { get; private set; }

    public void SetValue(object value)
    {
        if (value is bool)
        {
            Type = VariableType.Boolean;
        }
        else if (IsNumeric(value))
        {
            Type = VariableType.Number;
        }
        else
        {
            Type = VariableType.String;
            value = value.ToString();
        }

        Value = value;
    }

    private static bool IsNumeric(object value)
    {
        return value is int || value is double || value is float || value is decimal ||
               (value is string s && double.TryParse(s, out _));
    }
}