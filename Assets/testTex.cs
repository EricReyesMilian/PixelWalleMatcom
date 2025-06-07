using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class testTex : MonoBehaviour
{
    public RawImage rawImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Uso básico:
        Texture2D miTextura = CrearTexturaBasica(4, 4);
        miTextura.filterMode = FilterMode.Point;
        rawImage.texture = miTextura;
        miTextura.SetPixel(0, 0, Color.green);
        miTextura.Apply();



    }


    public Texture2D CrearTexturaBasica(int ancho, int alto)
    {
        // Crear una nueva textura con el formato por defecto
        Texture2D nuevaTextura = new Texture2D(ancho, alto);

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
