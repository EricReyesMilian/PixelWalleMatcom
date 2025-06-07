public class Size : VoidFunction
{
    public Size() : base("Size", 1) { }
    public override void Execute(int[] arr)
    {
        CheckParam(arr);
        _Size(arr[0]);
    }
    private void _Size(int x)
    {
        if (x % 2 == 0)
        {
            if (x > 0)
                CanvasGrid.Size = x - 1;
            else
                CanvasGrid.Size = 1;
        }
        else
        {
            CanvasGrid.Size = x;

        }
    }


}