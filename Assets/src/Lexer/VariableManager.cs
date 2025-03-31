using System.Collections.Generic;
public class VariableManager
{
    private Dictionary<string, Variable> _variables = new();

    public void Assign(string variableName, object value)
    {
        if (!VariableValidator.IsValidVariableName(variableName))
        {
            ErrorHandler.errorHandler.Error($"Nombre de variable inválido: '{variableName}'");
        }

        if (!_variables.TryGetValue(variableName, out var variable))
        {
            variable = new Variable();
            _variables[variableName] = variable;
        }

        variable.SetValue(value);
    }

    public object GetValue(string variableName)
    {
        if (_variables.TryGetValue(variableName, out var variable))
        {
            return variable.Value;
        }
        else
        {
            ErrorHandler.errorHandler.Error($"Variable no definida: '{variableName}'");
            return null;
        }
    }
}