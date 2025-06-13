public class ColorM : VoidFunction
{
    public ColorM() : base("Color", 1) { }
    public override void Execute(int[] arr)
    {
        _Color(CheckColor(arr[0]));
    }
    private void _Color(int color)
    {
        CanvasGrid.Color = color;
    }


}