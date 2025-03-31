using System;
public class Spawn : IFunction
{
    public string FunctionName => "Spawn";
    public object Execute(params object[] args)
    {
        try
        {
            int x = Convert.ToInt32(args[0]);
            int y = Convert.ToInt32(args[1]);
            GlobalVariables.walle = new(x, y);
            ErrorHandler.errorHandler.Info($"La posicion de Wall-e es ({x},{y})");
        }
        catch
        {
            ErrorHandler.errorHandler.Error("Debes introducir dos parametros  x, y tal que ambossean mayores o iguales que 0 y menores que la dimension del canvas");
        }
        return null;
    }
}