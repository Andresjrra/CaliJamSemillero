using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Configuración del Grid")]
    public int width = 20;
    public int height = 20;
    public float cellSize = 1f;
    public LayerMask obstacleLayer;
    public Vector2 gridOffset;

    private bool[,] grid;

    void Awake()
    {
        GenerarGrid();
    }

    void GenerarGrid()
    {
        grid = new bool[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 worldPos = GridToWorld(x, y);
                grid[x, y] = !Physics2D.OverlapCircle(worldPos, cellSize * 0.4f, obstacleLayer);
            }
        }
    }

    public bool IsWalkable(int x, int y)
    {
        return x >= 0 && x < width && y >= 0 && y < height && grid[x, y];
    }

    public Vector2 GridToWorld(int x, int y)
    {
        return new Vector2(x * cellSize, y * cellSize) + gridOffset;
    }

    public Vector2Int WorldToGrid(Vector2 worldPos)
    {
        worldPos -= gridOffset;
        return new Vector2Int(
            Mathf.Clamp(Mathf.FloorToInt(worldPos.x / cellSize), 0, width - 1),
            Mathf.Clamp(Mathf.FloorToInt(worldPos.y / cellSize), 0, height - 1)
        );
    }
}