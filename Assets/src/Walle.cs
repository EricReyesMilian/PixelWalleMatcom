using System;
using System.Collections.Generic;
public enum Colors
{
    Transparent,
    Red,
    Green,
    Yellow,
    Orange,
    Purple,
    Black,
    White,
}
public class Walle
{

    public Colors currentColor { get; private set; }
    public int size { get; private set; }
    public (int, int) wallEPos { get; private set; }
    public int CanvasSizeX { get; private set; }
    public int CanvasSizeY { get; private set; }
    static Dictionary<Colors, (float, float, float, float)> brushColor = new Dictionary<Colors, (float, float, float, float)>()
    {
        { Colors.Transparent, (0,0,0,0) },
        { Colors.Red, (1,0,0,1) },          // Correct
        { Colors.Green, (0,1,0,1) },        // Fixed - Green should be (0,1,0,1)
        { Colors.Yellow, (1,1,0,1) },       // Fixed - Yellow is red+green (1,1,0,1)
        { Colors.Orange, (1,0.5f,0,1) },    // Correct orange
        { Colors.Purple, (0.5f,0,0.5f,1) }, // Correct purple
        { Colors.Black, (0,0,0,1) },        // Correct
        { Colors.White, (1,1,1,1) }         // Correct
    };

    public void Spawn(int x, int y)
    {


    }
    public void Color(string color)
    {
        try
        {
            // Convertir el string a enum (case insensitive)
            Colors newColor = (Colors)System.Enum.Parse(typeof(Colors), color, true);

            if (brushColor.ContainsKey(newColor))
            {
                currentColor = newColor;
                Console.WriteLine($"Color cambiado a: {currentColor}");
            }
            else
            {
                Console.WriteLine($"Color '{color}' no encontrado en el diccionario");
            }
        }
        catch (System.ArgumentException)
        {
            Console.WriteLine($"Color '{color}' no válido. Opciones disponibles: {string.Join(", ", System.Enum.GetNames(typeof(Colors)))}");
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
            Console.WriteLine("Inserte un valor mayor que cero");
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
    public (int, int) GetCanvasSize()
    {
        return (CanvasSizeX, CanvasSizeY);
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