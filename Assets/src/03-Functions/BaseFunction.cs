using System.Collections.Generic;
using System;
public abstract class BaseFunction
{
    string name;
    private int paramCount;
    public int[] parCod = { };//0 bool,1 num, 2 color
    public BaseFunction(string name, int paramCount, int[] parCod)
    {
        this.name = name;
        this.paramCount = paramCount;
        this.parCod = parCod;

    }
    public string GetName()
    {
        return name;
    }

    public bool CheckParam(int[] arr)
    {
        //agregar especificacion por funcion
        if (paramCount != arr.Length)
        {
            return false;

        }
        return true;

    }
    public int CheckColor(int color)
    {
        if (color < 9 && color >= 0)
        {
            return color;
        }
        else
        {
            throw new RunTimeException($"El parametro de {name} no es un color valido");
        }
    }
    public int CheckX(int x)
    {
        if (x < CanvasGrid.horizontal && x >= 0)
        {
            return x;
        }
        else
        {
            throw new RunTimeException($"El parametro X de {name} se sale de los limites del canvas");
        }
    }
    public int CheckY(int y)
    {
        if (y < CanvasGrid.horizontal && y >= 0)
        {
            return y;
        }
        else
        {
            throw new RunTimeException($"El parametro Y de {name} se sale de los limites del canvas");
        }
    }



}
