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
    public GridCoord coordX;
    public GridCoord coordY;

    /// <summary>
    /// Called when values are changed in the inspector
    /// </summary>
    void Start()
    {
        UpdateCellSize(X);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ErrorHandler.errorHandler.Error("esto funciona");
        }
    }
    /// <summary>
    /// Updates the grid layout based on current parameters
    /// </summary>
    public void UpdateCellSize(int X)
    {
        this.X = X;
        gridLayout.constraintCount = X;
        Erase();
        Create(X);//test
        float ratio;
        if (X > 50)
        {
            gridLayout.spacing = Vector2.zero;
            ratio = (400f) / gridLayout.constraintCount;

        }
        else
        {
            gridLayout.spacing = new Vector2(2, 2);

            ratio = (400 - (gridLayout.spacing.x * (X + 1))) / gridLayout.constraintCount;

        }
        gridLayout.cellSize = new Vector2(ratio, ratio);
        coordX.UpdateCellSize();
        coordY.UpdateCellSize();
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
    void Create(int X)
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