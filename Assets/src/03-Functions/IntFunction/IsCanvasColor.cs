public class IsCanvasColor : IntFunction
{
    public IsCanvasColor() : base("IsCanvasColor", 3, new int[] { 2, 1, 1 }) { }
    public override int Execute(int[] arr)
    {
        CheckParam(arr);
        return _IsCanvasColor(CheckColor(arr[0]), CheckY(arr[1]), CheckX(arr[2]));
    }
    private int _IsCanvasColor(int color, int vertical, int horizontal)
    {
        if (CanvasGrid.pixels[horizontal, vertical] == color)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }


}