public class DrawCircle : VoidFunction
{
    public DrawCircle() : base("DrawCircle", 3) { } // Parámetros: centerX, centerY, radius

    public override void Execute(int[] arr)
    {
        CheckParam(arr);
        _DrawCircle(CheckX(arr[0]), CheckY(arr[1]), arr[2]); // centerX, centerY, radius
    }

    private void _DrawCircle(int centerX, int centerY, int radius)
    {
        int canvasWidth = CanvasGrid.horizontal;
        int canvasHeight = CanvasGrid.vertical;



        int x = 0;
        int y = radius;
        int d = 3 - 2 * radius; // Algoritmo de Bresenham



        while (x <= y)
        {
            // Dibuja los 8 puntos simétricos
            DrawCirclePoints(centerX, centerY, x, y, canvasWidth, canvasHeight);

            if (d <= 0)
            {
                d = d + 4 * x + 6;
            }
            else
            {
                d = d + 4 * (x - y) + 10;
                y--;
            }
            x++;
        }

        // Restauramos la posición original de WALL·E
        CanvasGrid.SetWalle(centerX, centerY);
    }

    // Dibuja los 8 puntos simétricos del círculo
    private void DrawCirclePoints(int centerX, int centerY, int x, int y, int canvasWidth, int canvasHeight)
    {
        // Octante 1 (x, y)
        SetPixelIfValid(centerX + x, centerY + y, canvasWidth, canvasHeight);
        // Octante 2 (y, x)
        SetPixelIfValid(centerX + y, centerY + x, canvasWidth, canvasHeight);
        // Octante 3 (-y, x)
        SetPixelIfValid(centerX - y, centerY + x, canvasWidth, canvasHeight);
        // Octante 4 (-x, y)
        SetPixelIfValid(centerX - x, centerY + y, canvasWidth, canvasHeight);
        // Octante 5 (-x, -y)
        SetPixelIfValid(centerX - x, centerY - y, canvasWidth, canvasHeight);
        // Octante 6 (-y, -x)
        SetPixelIfValid(centerX - y, centerY - x, canvasWidth, canvasHeight);
        // Octante 7 (y, -x)
        SetPixelIfValid(centerX + y, centerY - x, canvasWidth, canvasHeight);
        // Octante 8 (x, -y)
        SetPixelIfValid(centerX + x, centerY - y, canvasWidth, canvasHeight);
    }

    // Dibuja un píxel si está dentro del canvas
    private void SetPixelIfValid(int x, int y, int canvasWidth, int canvasHeight)
    {
        if (x >= 0 && y >= 0 && x < canvasWidth && y < canvasHeight)
        {
            // Movemos WALL·E temporalmente para dibujar el punto
            //CanvasGrid.SetWalle(x, y);
            CanvasGrid.Draw(x, y);
            // Nota: No restauramos aquí la posición para mejorar rendimiento
            // La restauración se hace al final en _DrawCircle
        }
    }
}