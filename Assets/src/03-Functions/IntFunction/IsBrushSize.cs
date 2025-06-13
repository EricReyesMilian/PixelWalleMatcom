public class IsBrushSize : IntFunction
{
    public IsBrushSize() : base("IsBrushSize", 1, new int[] { 1 }) { }
    public override int Execute(int[] arr)
    {
        CheckParam(arr);
        return _IsBrushSize(arr[0]);
    }
    private int _IsBrushSize(int size)
    {
        if (CanvasGrid.Size == size)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }


}