using System;
using System.Collections.Generic;

public class Walle
{

    public Colors currentColor { get; private set; }
    public int size { get; private set; }
    public (int, int) wallEPos { get; private set; }
    public int CanvasSizeX { get; private set; }
    public int CanvasSizeY { get; private set; }


    public void Spawn(int x, int y)
    {
        wallEPos = new(x, y);

    }
    public void Color(string color)
    {
        try
        {
            // Convertir el string a enum (case insensitive)

            Colors newColor = (Colors)System.Enum.Parse(typeof(Colors), color, true);
            currentColor = newColor;
            ErrorHandler.errorHandler.Info($"Color cambiado a: {currentColor}");

        }
        catch (System.ArgumentException)
        {
            ErrorHandler.errorHandler.Error($"Color '{color}' no válido. Opciones disponibles: {string.Join(", ", System.Enum.GetNames(typeof(Colors)))}");
        }
    }
    public void Size(int k)
    {
        if (k > 0)
        {
            if (k % 2 != 0)
            {
                size = k;
            }
            else
            {
                size = k - 1;
            }
        }
        else
        {
            ErrorHandler.errorHandler.Info("Inserte un valor mayor que cero");
        }
    }
    public void DrawLine(int dirx, int diry, int distance)
    {

    }
    public void DrawCircle(int dirx, int diry, int radius)
    {

    }
    public void DrawRectangle(int dirx, int diry, int distance, int width, int height)
    {

    }
    public void Fill()
    {

    }
    #region Functions
    public int GetActualX()
    {
        return wallEPos.Item1;
    }
    public int GetActualY()
    {
        return wallEPos.Item2;

    }
    public int GetCanvasSize()
    {
        return 10;
    }
    public int GetColorCount(string color, int x1, int y1, int x2, int y2)
    {
        return 0;
    }
    public int IsBrushColor(string color)
    {
        Colors checkColor = (Colors)System.Enum.Parse(typeof(Colors), color, true);
        return checkColor == currentColor ? 1 : 0;

    }
    public int IsBrushSize(int size)
    {
        return size == this.size ? 1 : 0;
    }
    public int IsCanvasColor(string color, int vertical, int horizontal)
    {
        return -1;
    }
    #endregion


}