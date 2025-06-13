public class Spawn : VoidFunction
{
    public Spawn() : base("Spawn", 2) { }
    public override void Execute(int[] arr)
    {
        _Spawn(CheckX(arr[0]), CheckY(arr[1]));
    }
    private void _Spawn(int x, int y)
    {
        CanvasGrid.SetWalleX(x);
        CanvasGrid.SetWalleY(y);
    }


}