using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderSpawner : MonoBehaviour
{
    private Defender _defender;

    private void OnMouseDown()
    {
        SpawnDefender(GetSquareClick());
    }

    public void SetSelectedDefender(Defender defenderToSelect)
    {
        _defender = defenderToSelect;
    }
    private Vector2 GetSquareClick()
    {
        var clickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        var worldPos = Camera.main.ScreenToWorldPoint(clickPos);
        return SnapToGrid(worldPos);
    }

    private Vector2 SnapToGrid(Vector2 rawWorldPos)
    {
        float newX = Mathf.RoundToInt(rawWorldPos.x);
        float newY = Mathf.RoundToInt(rawWorldPos.y);
        return new Vector2(newX, newY);
    }

    private void SpawnDefender(Vector2 worldPos)
    {
        var newDefender = Instantiate(_defender, worldPos, Quaternion.identity);
    }
}