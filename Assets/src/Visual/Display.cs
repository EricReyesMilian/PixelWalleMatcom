using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class Display : MonoBehaviour
{
    [Tooltip("Number of cells in the grid")]

    public RawImage canvasContainer;
    static public Texture2D canvas;
    void Start()
    {
        CanvasGrid.draw_event += Draw;
        UpdateCellSize();
    }
    void Update()
    {
    }

    static public void Draw(int x, int y, int color)
    {
        if (x < canvas.width && y < canvas.height)
        {
            Color colorPixel = ColorHandler.GetColor(color);

            if (color != 0)
            {
                canvas.SetPixel(x, y, colorPixel);
                canvas.Apply();

            }


        }
        else
        {
            throw new Exception($"coordenada ({x},{y}) fuera de los limites del canvas");
        }
    }
    /// <summary>
    /// Updates the grid layout based on current parameters
    /// </summary>
    public void UpdateCellSize()
    {
        int X = CanvasGrid.vertical;
        canvas = CrearTexturaBasica(X, X);
        canvasContainer.texture = canvas;


    }
    public Texture2D CrearTexturaBasica(int ancho, int alto)
    {
        // Crear una nueva textura con el formato por defecto
        Texture2D nuevaTextura = new Texture2D(ancho, alto);
        nuevaTextura.filterMode = FilterMode.Point;

        // Establecer todos los pixeles a un color inicial
        Color colorInicial = Color.white;
        for (int x = 0; x < ancho; x++)
        {
            for (int y = 0; y < alto; y++)
            {
                nuevaTextura.SetPixel(x, y, colorInicial);
            }
        }

        // Aplicar los cambios
        nuevaTextura.Apply();

        return nuevaTextura;
    }


}