using System;
using System.Collections.Generic;
public class SemanticException
{
    public List<string> Errors { get; } = new List<string>();

    public void Report(string error, int line, int column)
    {
        Errors.Add($"Error semantico en Línea {line}, Columna {column}: {error}");
    }

    public bool HasErrors => Errors.Count > 0;
}
