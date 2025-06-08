using UnityEngine;
using TMPro;
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
public static class GlobalVariables
{

    public static (int, int) walle;
    public static List<string> colors = new List<string> {
         "\"Transparent\"",
            "\"Blue\"",
            "\"Red\"",
            "\"Green\"",
            "\"Yellow\"",
            "\"Orange\"",
            "\"Purple\"",
            "\"Black\"",
            "\"White\"",
    };
    public static Dictionary<Colors, (float, float, float, float)> brushColor = new Dictionary<Colors, (float, float, float, float)>()
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
}