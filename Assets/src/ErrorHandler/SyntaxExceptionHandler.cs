using System;
using System.Collections.Generic;
public class SyntaxExceptionHandler
{
    public List<string> Errors { get; } = new List<string>();

    public void Report(string error, int line, int column)
    {
        string message = $"Error sintactico en Línea {line}, Columna {column}: {error} \n";
        ErrorHandler.errorHandler.AddError(message);
        Errors.Add(message);
    }

    public bool HasErrors => Errors.Count > 0;
}
