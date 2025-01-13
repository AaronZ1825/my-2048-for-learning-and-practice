using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public TileState state { get; private set; }
    public TileCell cell { get; private set; }
    public bool locked { get; set; }

    public Image imgBackground;
    public TextMeshProUGUI txtNumber;

    private void Awake()
    {
    if (imgBackground == null)
    {
        imgBackground = GetComponent<Image>();
    }

    if (txtNumber == null)
    {
        txtNumber = GetComponentInChildren<TextMeshProUGUI>();
    }

    if (imgBackground == null || txtNumber == null)
    {
        Debug.LogError("Missing UI components in Tile prefab!");
    }
    }


    public void SetState(TileState state)
    {
        this.state = state;
        imgBackground.color = state.TileBackColor;
        txtNumber.color = state.TileTextColor;
        txtNumber.text = state.TileNumber.ToString();
    }

    public void Spawn(TileCell cell)
    {
        this.cell = cell;
        transform.position = cell.transform.position;
    }

    public void Merge(TileCell cell)
    {
        if (this.cell != null)
        {
            this.cell.tile = null;
        }

        this.cell = null;
        cell.tile.locked = true;

        StartCoroutine(MoveAnimate(cell.transform.position, true));
    }
    public void MoveTo(TileCell cell)
    {
    if (cell == null)
    {
        Debug.LogError("MoveTo() received a null TileCell. Cannot move the tile.");
        return;
    }

    if (this.cell != null)
    {
        this.cell.tile = null;  // 释放当前单元格引用
    }

    this.cell = cell;
    cell.tile = this;  // 将 Tile 绑定到新单元格
    cell.tile.locked = true;

    StartCoroutine(MoveAnimate(cell.transform.position, merging: false));
    }



    IEnumerator MoveAnimate(Vector3 posTo, bool merging)
    {
        float elapsed = 0f;
        float duration = 0.1f;
        Vector3 posFrom = transform.position;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(posFrom, posTo, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = posTo;

        if (merging)
        {
            Destroy(gameObject);
        }
    }
}
