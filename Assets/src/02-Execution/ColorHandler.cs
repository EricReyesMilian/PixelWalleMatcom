using System.Collections.Generic;
using System;

using UnityEngine;

public static class ColorHandler
{
    public static HashSet<string> _colors = new()
    { "\"Transparent\"", "\"Blue\"", "\"Red\"", "\"Green\"", "\"Yellow\"", "\"Orange\"", "\"Purple\"", "\"Black\"", "\"White\"","\"Cyan\"" };
    public static int GetColorNumber(Token token)
    {
        string colorName = token.Value;
        return colorName switch
        {
            "\"Transparent\"" => 0,
            "\"Blue\"" => 1,
            "\"Red\"" => 2,
            "\"Green\"" => 3,
            "\"Yellow\"" => 4,
            "\"Orange\"" => 5,
            "\"Purple\"" => 6,
            "\"Black\"" => 7,
            "\"White\"" => 8,
            "\"Cyan\"" => 9,
            _ => throw new RunTimeException($"Color no soportado: {token.Value}"),

        };
    }
    public static Color GetColor(int colorCode)
    {
        switch (colorCode)
        {
            case 0: return new Color(0, 0, 0, 0);
            case 1: return Color.blue;
            case 2: return Color.red;
            case 3: return Color.green;
            case 4: return Color.yellow;
            case 5: return new Color(1, 0.5f, 0, 1);
            case 6: return new Color(0.5f, 0, 0.5f, 1);
            case 7: return Color.black;
            case 8: return Color.white;
            case 9: return Color.cyan;
            default: return Color.white;
        }
    }


}