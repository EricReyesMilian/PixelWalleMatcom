public class GetActualX : IntFunction
{
    public GetActualX() : base("GetActualX", 0) { }
    public override int Execute(int[] arr)
    {
        CheckParam(arr);
        return _GetActualX();
    }

    private int _GetActualX()
    {
        return CanvasGrid.WalleX;
    }
}