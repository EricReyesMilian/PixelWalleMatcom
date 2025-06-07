public class DrawLine : VoidFunction
{
    public DrawLine() : base("DrawLine", 3) { }
    public override void Execute(int[] arr)
    {
        CheckParam(arr);
        _DrawLine(arr[0], arr[1], arr[2]);
    }
    private void _DrawLine(int xDir, int yDir, int dist)
    {
        int currentX = CanvasGrid.WalleX;
        int currentY = CanvasGrid.WalleY;
        int canvasWidth = CanvasGrid.horizontal;
        int canvasHeight = CanvasGrid.vertical;

        while (dist > 0)
        {
            // Calcular la próxima posición
            int newX = currentX + xDir;
            int newY = currentY + yDir;

            // Verificar si la nueva posición está fuera de los límites
            if (newX < 0 || newY < 0 || newX >= canvasWidth || newY >= canvasHeight)
            {
                break; // Salir si estamos fuera de los límites
            }

            // Actualizar posición y dibujar
            CanvasGrid.SetWalle(newX, newY);
            CanvasGrid.Draw();

            // Actualizar posición actual y reducir distancia
            currentX = newX;
            currentY = newY;
            dist--;
        }
    }
}


