using UnityEngine;

public class Defender : MonoBehaviour
{
    [SerializeField] private int starCost = 100;
    private StarDisplay _starDisplay;

    private void Start()
    {
        _starDisplay = FindObjectOfType<StarDisplay>();
    }

    public void AddStars(int amount)
    {
        _starDisplay.AddStars(amount);
    }

    public int GetStarCost()
    {
        return starCost;
    }
}
