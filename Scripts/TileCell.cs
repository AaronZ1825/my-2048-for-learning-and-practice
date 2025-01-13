using UnityEngine;

public class TileCell : MonoBehaviour
{
    public Vector2Int coordinates; // coordinates of the cell now
    public Tile tile; 

    public bool IsEmpty()
    {
        return tile == null;
    }

    public bool IsOccupied()
    {
        return tile != null;
    }
}
