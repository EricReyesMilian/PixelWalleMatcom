using System;
public class SemanticException : Exception
{
    public string ErrorDetails { get; }

    public SemanticException(string message)
        : base($"Error semantico : {message}")
    {
        ErrorDetails = message;
    }
}