using System.Text.RegularExpressions;

public static class VariableValidator
{
    private static readonly Regex ValidVariableRegex = new Regex(
        @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ][a-zA-ZáéíóúÁÉÍÓÚñÑ0-9-]*$",
        RegexOptions.Compiled);

    public static bool IsValidVariableName(string name)
    {
        return !string.IsNullOrEmpty(name) &&
               ValidVariableRegex.IsMatch(name) &&
               !char.IsDigit(name[0]) &&
               name[0] != '-';
    }
}