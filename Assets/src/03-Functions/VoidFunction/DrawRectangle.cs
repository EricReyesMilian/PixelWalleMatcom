using System;
public class DrawRectangle : VoidFunction
{
    public DrawRectangle() : base("DrawRectangle", 5) { } // Parámetros: dirX, dirY, distance, width, height

    public override void Execute(int[] arr)
    {
        _DrawRectangle(arr[0], arr[1], arr[2], arr[3], arr[4]);
    }

    private void _DrawRectangle(int dirX, int dirY, int distance, int width, int height)
    {
        // 1. Calcular nueva posición del centro
        int newX = CanvasGrid.WalleX + dirX * distance;
        int newY = CanvasGrid.WalleY + dirY * distance;

        // 2. Verificar si el centro está fuera del canvas
        if (newX < 0 || newY < 0 || newX >= CanvasGrid.horizontal || newY >= CanvasGrid.vertical)
        {
            throw new InvalidOperationException("El centro del rectángulo queda fuera del canvas");
        }

        // 3. Mover físicamente a WALL·E al centro
        CanvasGrid.SetWalle(newX, newY);

        // 4. Dibujar el rectángulo
        DrawRectangleAroundCenter(width, height);
    }

    private void DrawRectangleAroundCenter(int width, int height)
    {
        int centerX = CanvasGrid.WalleX;
        int centerY = CanvasGrid.WalleY;
        int halfWidth = width / 2;
        int halfHeight = height / 2;

        // Coordenadas del rectángulo
        int left = centerX - halfWidth;
        int right = centerX + halfWidth;
        int top = centerY - halfHeight;
        int bottom = centerY + halfHeight;

        // Dibujar los bordes
        DrawHorizontalLine(top, left, right);
        DrawHorizontalLine(bottom, left, right);
        DrawVerticalLine(left, top, bottom);
        DrawVerticalLine(right, top, bottom);
    }

    private void DrawHorizontalLine(int y, int xStart, int xEnd)
    {
        for (int x = xStart; x <= xEnd; x++)
        {
            TryDrawPixel(x, y);
        }
    }

    private void DrawVerticalLine(int x, int yStart, int yEnd)
    {
        for (int y = yStart; y <= yEnd; y++)
        {
            TryDrawPixel(x, y);
        }
    }

    private void TryDrawPixel(int x, int y)
    {
        // Solo dibujar píxeles dentro del canvas
        if (x >= 0 && x < CanvasGrid.horizontal && y >= 0 && y < CanvasGrid.vertical)
        {
            int originalX = CanvasGrid.WalleX;
            int originalY = CanvasGrid.WalleY;

            CanvasGrid.SetWalle(x, y);
            CanvasGrid.Draw();

            CanvasGrid.SetWalle(originalX, originalY);
        }
    }
}