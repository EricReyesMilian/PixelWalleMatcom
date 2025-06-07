public class IsBrushColor : IntFunction
{
    public IsBrushColor() : base("IsBrushColor", 1) { }
    public override int Execute(int[] arr)
    {
        CheckParam(arr);
        return _IsBrushColor(CheckColor(arr[0]));
    }
    private int _IsBrushColor(int color)
    {
        if (CanvasGrid.Color == color)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }


}