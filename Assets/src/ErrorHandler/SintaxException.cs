using System;
public class SyntaxException : Exception
{
    public string ErrorDetails { get; }

    public SyntaxException(string message)
        : base($"Error de sintaxis : {message}")
    {
        ErrorDetails = message;
    }
}