public class GetColorCount : IntFunction
{
    public GetColorCount() : base("GetColorCount", 5) { }
    public override int Execute(int[] arr)
    {
        CheckParam(arr);
        return _GetColorCount(CheckColor(arr[0]), arr[1], arr[2], arr[3], arr[4]);
    }
    private int _GetColorCount(int color, int x1, int y1, int x2, int y2)
    {
        if (x1 < 0 || y1 < 0 || x2 < 0 || y2 < 0 || x1 >= CanvasGrid.horizontal || x2 >= CanvasGrid.horizontal || y1 >= CanvasGrid.vertical || y2 >= CanvasGrid.vertical)
        {
            return 0;
        }
        else
        {
            int result = 0;
            for (int i = x1; i <= x2; i++)
            {
                for (int j = y1; j <= y2; j++)
                {
                    if (CanvasGrid.pixels[i, j] == color)
                    {
                        result++;
                    }
                }
            }
            return result;
        }
    }


}