public class Cell
{
    public Colors cellColor { get; private set; }

    public void SetColor(Colors brushColor)
    {
        cellColor = brushColor;
    }
}