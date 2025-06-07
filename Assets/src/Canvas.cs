using System;
public static class CanvasGrid
{
    public static int WalleX = 0;
    public static int WalleY = 0;
    public static int Size = 1;
    public static int Color = 1;
    public static int horizontal = 10;
    public static int vertical = 10;
    public static int[,] pixels = new int[10, 10]
    {
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
    };
    public static event Action<int, int, int> draw_event;
    public static void Reset()
    {
        WalleX = 0;
        WalleY = 0;
        Size = 1;
        Color = 1;
    }
    public static void SetWalleX(int x)
    {

        WalleX = x;
    }
    /// <summary>
    /// Cambia el tamaño del canvas e inicializa todos los píxeles con valor 7 (Black)
    /// </summary>
    /// <param name="newWidth">Nuevo ancho del canvas</param>
    /// <param name="newHeight">Nuevo alto del canvas</param>
    public static void ResizeCanvas(int newWidth, int newHeight)
    {
        // Validar los nuevos tamaños
        if (newWidth <= 0 || newHeight <= 0)
        {
            throw new ArgumentException("El tamaño del canvas debe ser mayor que 0");
        }

        // Crear nueva matriz con el nuevo tamaño
        pixels = new int[newHeight, newWidth];

        // Inicializar todos los píxeles con valor 7 (Black)
        for (int y = 0; y < newHeight; y++)
        {
            for (int x = 0; x < newWidth; x++)
            {
                pixels[y, x] = 8; // Valor para White
            }
        }

        // Actualizar las dimensiones y la matriz de píxeles
        horizontal = newWidth;
        vertical = newHeight;

        // Ajustar la posición de WALL·E si está fuera de los nuevos límites
        WalleX = Math.Min(WalleX, newWidth - 1);
        WalleY = Math.Min(WalleY, newHeight - 1);
    }
    public static void Draw(int X, int Y)
    {
        int centerX = X;
        int centerY = Y;
        int size = Size;
        int radius = size / 2; // Radio del área a pintar



        // Calcular los límites del área a pintar
        int startX = Math.Max(0, centerX - radius);
        int endX = Math.Min(horizontal - 1, centerX + radius);
        int startY = Math.Max(0, centerY - radius);
        int endY = Math.Min(vertical - 1, centerY + radius);

        // Pintar el área cuadrada alrededor de Walle
        for (int y = startY; y <= endY; y++)
        {
            for (int x = startX; x <= endX; x++)
            {
                // Validar que estamos dentro de los límites del canvas
                if (x >= 0 && x < horizontal && y >= 0 && y < vertical)
                {
                    pixels[y, x] = Color;
                    draw_event?.Invoke(y, x, pixels[y, x]);

                }
            }
        }
    }
    public static void Draw()
    {
        int centerX = WalleX;
        int centerY = WalleY;
        int size = Size;
        int radius = size / 2; // Radio del área a pintar



        // Calcular los límites del área a pintar
        int startX = Math.Max(0, centerX - radius);
        int endX = Math.Min(horizontal - 1, centerX + radius);
        int startY = Math.Max(0, centerY - radius);
        int endY = Math.Min(vertical - 1, centerY + radius);

        // Pintar el área cuadrada alrededor de Walle
        for (int y = startY; y <= endY; y++)
        {
            for (int x = startX; x <= endX; x++)
            {
                // Validar que estamos dentro de los límites del canvas
                if (x >= 0 && x < horizontal && y >= 0 && y < vertical)
                {
                    pixels[y, x] = Color;
                    draw_event?.Invoke(y, x, pixels[y, x]);

                }
            }
        }
    }
    public static void SetWalle(int x, int y)
    {
        //agregar comprobacion
        WalleX = x;
        WalleY = y;
    }
    public static void SetWalleY(int y)
    {
        WalleY = y;
    }
    public static int GetPixelColor(int x, int y)
    {
        return pixels[x, y];
    }


}