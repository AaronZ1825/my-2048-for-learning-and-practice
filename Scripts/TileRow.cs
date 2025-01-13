using UnityEngine;

public class TileRow : MonoBehaviour
{
    public TileCell[] cells; // The cells governed by the current row
        private void Awake()
    {
        cells = GetComponentsInChildren<TileCell>();
    }
}
