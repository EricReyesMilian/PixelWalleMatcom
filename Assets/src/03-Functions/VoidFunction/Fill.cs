using System.Collections.Generic;
public class Fill : VoidFunction
{
    public Fill() : base("Fill", 0) { }

    public override void Execute(int[] arr)
    {
        CheckParam(arr);
        _Fill();
    }

    private void _Fill()
    {
        int startX = CanvasGrid.WalleX;
        int startY = CanvasGrid.WalleY;
        int targetColor = CanvasGrid.GetPixelColor(startX, startY);
        int fillColor = CanvasGrid.Color;

        if (targetColor == fillColor) return;

        Queue<(int x, int y)> queue = new Queue<(int x, int y)>();
        bool[,] visited = new bool[CanvasGrid.horizontal, CanvasGrid.vertical];

        queue.Enqueue((startX, startY));
        visited[startX, startY] = true;

        // Guardamos la posición original de WALL·E
        int originalX = CanvasGrid.WalleX;
        int originalY = CanvasGrid.WalleY;

        while (queue.Count > 0)
        {
            var (x, y) = queue.Dequeue();

            // Verificamos el color actual (por si cambió durante el proceso)
            if (CanvasGrid.GetPixelColor(x, y) != targetColor)
                continue;

            // Pintamos el píxel sin mover físicamente a WALL·E
            CanvasGrid.pixels[x, y] = fillColor; // Método directo para cambiar el color

            // Explorar vecinos (4-direcciones)
            ExploreNeighbor(x + 1, y, queue, visited, targetColor);
            ExploreNeighbor(x - 1, y, queue, visited, targetColor);
            ExploreNeighbor(x, y + 1, queue, visited, targetColor);
            ExploreNeighbor(x, y - 1, queue, visited, targetColor);
        }

        // Restauramos la posición original de WALL·E (por si acaso)
        CanvasGrid.SetWalle(originalX, originalY);
    }

    private void ExploreNeighbor(int x, int y, Queue<(int x, int y)> queue,
                               bool[,] visited, int targetColor)
    {
        if (x >= 0 && y >= 0 && x < CanvasGrid.horizontal && y < CanvasGrid.vertical &&
            !visited[x, y] && CanvasGrid.GetPixelColor(x, y) == targetColor)
        {
            visited[x, y] = true;
            queue.Enqueue((x, y));
        }
    }
}