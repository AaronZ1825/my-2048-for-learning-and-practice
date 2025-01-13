using UnityEngine;

public class TileGrid : MonoBehaviour
{
    public TileRow[] rows {get; private set; } // The row currently governed by the grid
    public TileCell[] cells {get; private set; } // All cells in the grid

    public int size => cells.Length; // Get the number of cells in the current grid
    public int height => rows.Length; // the row number
    public int width => size / height; // the width of the grid 

    private void Awake()
    {
        rows = GetComponentsInChildren <TileRow>();
        cells = GetComponentsInChildren <TileCell>();
    }

// Get cells by coordinates
    private void Start()
    {
        for (int i = 0; i < rows.Length; i++)
        {
            for (int j = 0; j < rows[i].cells.Length; j++)
            {
                rows[i].cells[j].coordinates = new Vector2Int(x:j , y:i);
            }
        }
    }

// Get cells by coordinates
    public TileCell GetCell(Vector2Int coord)
    {
            return GetCell(coord.x, coord.y);
    }

    public TileCell GetCell(int x, int y)
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            return rows[y].cells[x];
        }
        else
        {
            return null;
        }
    }

// Get the neighbors of a cell in a certain direction
    public TileCell GetAdjacentCell(TileCell cell, Vector2Int direction)
    {
        Vector2Int adjacentCell = cell.coordinates + direction;
        return GetCell(adjacentCell);
    }

// Provide a random empty cell
    public TileCell GetRandomEmptyCell()
    {
        int index = Random.Range(0, cells.Length);
        int startingIndex = index;

        while (cells[index].IsOccupied())
        {
            index++;

            if (index >= cells.Length) {
                index = 0;
            }

            // all cells are occupied
            if (index == startingIndex) {
                return null;
            }
        }

        return cells[index];
    }
}


