public class GetCanvasSize : IntFunction
{
    public GetCanvasSize() : base("GetCanvasSize", 0, new int[] { }) { }
    public override int Execute(int[] arr)
    {
        CheckParam(arr);

        return _GetCanvasSize();
    }
    private int _GetCanvasSize()
    {
        return CanvasGrid.vertical;
    }


}