public class GetActualY : IntFunction
{
    public GetActualY() : base("GetActualY", 0, new int[] { }) { }
    public override int Execute(int[] arr)
    {
        CheckParam(arr);

        return _GetActualY();
    }

    private int _GetActualY()
    {
        return CanvasGrid.WalleX;
    }
}