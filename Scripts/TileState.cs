using UnityEngine;


[CreateAssetMenu (menuName = "Tile State")]
public class TileState : ScriptableObject
{
    public Color TileBackColor; // tile's background color
    public Color TileTextColor; // tile's text color
    public int TileNumber; // size of text
}
