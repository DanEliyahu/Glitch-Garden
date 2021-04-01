using UnityEngine;

public class DefenderSpawner : MonoBehaviour
{
    private Defender _defender;
    private GameObject _defenderParent;
    private const string DefenderParentName = "Defenders";

    private void Start()
    {
        CreateDefenderParent();
    }

    private void CreateDefenderParent()
    {
        _defenderParent = GameObject.Find(DefenderParentName);
        if (!_defenderParent)
        {
            _defenderParent = new GameObject(DefenderParentName);
        }
    }

    private void OnMouseDown()
    {
        AttemptToPlaceDefenderAt(GetSquareClick());
    }

    public void SetSelectedDefender(Defender defenderToSelect)
    {
        _defender = defenderToSelect;
    }

    private void AttemptToPlaceDefenderAt(Vector2 gridPos)
    {
        if (!_defender)
        {
            return;
        }
        var starDisplay = FindObjectOfType<StarDisplay>();
        int defenderCost = _defender.GetStarCost();
        if (starDisplay.HaveEnoughStars(defenderCost))
        {
            SpawnDefender(gridPos);
            starDisplay.SpendStars(defenderCost);
        }
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
        Instantiate(_defender, worldPos, Quaternion.identity,_defenderParent.transform);
    }
}