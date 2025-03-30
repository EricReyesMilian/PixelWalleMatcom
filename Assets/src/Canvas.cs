using System.Collections.Generic;
public class Canvas
{
    List<List<Cell>> grid = new List<List<Cell>>();


    public void EraseCanvas()
    {
        for (int i = 0; i < grid.Count; i++)
        {
            for (int j = 0; j < grid[0].Count; j++)
            {
                grid[i][j].SetColor(Colors.White);
            }
        }
    }

}
