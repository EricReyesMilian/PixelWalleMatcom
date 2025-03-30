using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GridCoord : MonoBehaviour
{
    public GridDisplay gridDisplay;
    public GridLayoutGroup gridLayout;
    public int count;
    public GameObject num;
    public Transform numWrapper;
    public bool isVertical;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateCellSize();
    }

    /// <summary>
    /// Updates the grid layout based on current parameters
    /// </summary>
    void UpdateCellSize()
    {
        count = gridDisplay.X;
        gridLayout.spacing = gridDisplay.gridLayout.spacing;
        Erase();
        Create();
        if (isVertical)
        {
            gridLayout.cellSize = new Vector2(16f, gridDisplay.gridLayout.cellSize.x);

        }
        else
        {
            gridLayout.cellSize = new Vector2(gridDisplay.gridLayout.cellSize.x, 16f);

        }
    }

    /// <summary>
    /// Destroys all child objects of numWrapper
    /// </summary>
    void Erase()
    {
        while (numWrapper.childCount > 0)
        {
            DestroyImmediate(numWrapper.GetChild(0).gameObject);
        }
    }

    /// <summary>
    /// Creates grid cells to fill the numWrapper
    /// </summary>
    void Create()
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(num, numWrapper);

        }
        for (int i = 0; i < count; i++)
        {

            numWrapper.GetChild(i).gameObject.GetComponent<TextMeshProUGUI>().text = $"{i}";


        }
    }
}




