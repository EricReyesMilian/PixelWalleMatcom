using System.Collections.Generic;
using System;
public static class FunctionManager
{
    public static Dictionary<string, int> variables = new Dictionary<string, int>();
    public static Dictionary<string, int> labels = new Dictionary<string, int>();

    public static List<IntFunction> functions = new List<IntFunction>()
    {
        new GetActualX(),
        new GetActualY(),
        new GetCanvasSize(),
        new GetColorCount(),
        new IsBrushColor(),
        new IsBrushSize(),
        new IsCanvasColor(),
    };

    public static List<VoidFunction> instruction = new List<VoidFunction>()
    {
        new Spawn(),
        new DrawLine(),
        new DrawRectangle(),
        new ColorM(),
        new Size(),
        new DrawCircle(),
        new Fill(),

    };
    //chequeo de tipos
    public static bool CheckParams(string f, int[] arr)
    {

        foreach (var fun in functions)
        {
            if (fun.GetName().Equals(f))
            {
                if (arr.Length != fun.parCod.Length)
                {
                    return false;
                }
                else
                {
                    for (int i = 0; i < fun.parCod.Length; i++)
                    {
                        if (arr[i] != fun.parCod[i])
                        {
                            return false;
                        }
                    }
                }
                break;
            }
        }
        foreach (var fun in instruction)
        {
            if (fun.GetName().Equals(f))
            {
                if (arr.Length != fun.parCod.Length)
                {
                    return false;
                }
                else
                {
                    for (int i = 0; i < fun.parCod.Length; i++)
                    {
                        if (arr[i] != fun.parCod[i])
                        {
                            return false;
                        }
                    }
                }
                break;
            }
        }
        return true;
    }

    public static int CheckInt<T>(T x)
    {
        if (x is int intValue)
        {
            return intValue;
        }
        throw new ArgumentException(x switch
        {
            bool => "Se recibió un valor booleano cuando se esperaba un entero",
            null => "No se puede convertir null a entero",
            _ => $"No se puede convertir {x.GetType().Name} a entero"
        }, nameof(x));
    }
    public static bool CheckBool<T>(T x)
    {
        if (x is bool boolValue)
        {
            return boolValue;
        }
        throw new ArgumentException($"Se esperaba un valor booleano pero se recibió {x?.GetType().Name ?? "null"}");
    }

    public static bool IsIntFunction(FunctionNode function)
    {
        string f = function.token.Value;
        foreach (var fun in functions)
        {
            if (fun.GetName().Equals(f))
            {
                return true;
            }
        }
        return false;
    }
    public static int GetIntFunction(string f, int[] arr)
    {
        foreach (var fun in functions)
        {
            if (fun.GetName().Equals(f))
            {
                return fun.Execute(arr);
            }
        }
        throw new RunTimeException($"Funcion {f} de retorno entero no detectada");
    }
    public static void GetVoidFunction(string f, int[] arr)
    {
        bool found = false;
        foreach (var fun in instruction)
        {
            if (fun.GetName() == f)
            {
                fun.Execute(arr);
                found = true;
                break;
            }
        }
        if (!found)
            throw new RunTimeException($"Funcion {f} de retorno vacio no detectada");


    }

}