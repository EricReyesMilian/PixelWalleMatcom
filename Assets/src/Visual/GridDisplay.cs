using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GridDisplay : MonoBehaviour
{
    [Tooltip("Number of cells in the grid")]
    public int X;

    [Tooltip("Parent transform that will contain all grid cells")]
    public Transform gridWrapper;

    [Tooltip("Prefab for individual grid cells")]
    public GameObject cellPref;

    [Tooltip("Grid Layout Group component that handles cell arrangement")]
    public GridLayoutGroup gridLayout;

    /// <summary>
    /// Called when values are changed in the inspector
    /// </summary>
    void Start()
    {
        UpdateCellSize();
    }

    /// <summary>
    /// Updates the grid layout based on current parameters
    /// </summary>
    void UpdateCellSize()
    {
        gridLayout.constraintCount = X;
        Erase();
        Create();//test
        float ratio = 100 / gridLayout.constraintCount;
        gridLayout.cellSize = new Vector2(ratio * 4, ratio * 4);
    }

    /// <summary>
    /// Destroys all child objects of gridWrapper
    /// </summary>
    void Erase()
    {
        while (gridWrapper.childCount > 0)
        {
            DestroyImmediate(gridWrapper.GetChild(0).gameObject);
        }
    }

    /// <summary>
    /// Creates grid cells to fill the gridWrapper
    /// </summary>
    void Create()
    {
        for (int i = 0; i < X; i++)
        {
            for (int j = 0; j < X; j++)
            {
                Instantiate(cellPref, gridWrapper);

            }
        }
        for (int i = 0; i < X * X; i++)
        {

            gridWrapper.GetChild(i).name = $"Cell {i / X}_{i % X}";


        }
    }
}