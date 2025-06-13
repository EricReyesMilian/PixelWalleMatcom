using System;
public class RunTimeException : Exception
{
    public string ErrorDetails { get; }

    public RunTimeException(string message)
        : base($"Error en tiempo de ejecucion : {message}")
    {
        ErrorDetails = message;
    }
}