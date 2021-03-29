using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private int health = 5;
    private Text _healthText;

    // Start is called before the first frame update
    void Start()
    {
        _healthText = GetComponent<Text>();
        UpdateDisplay();
    }

    public void TakeDamage()
    {
        health--;
        UpdateDisplay();
        if (health <= 0)
        {
            FindObjectOfType<LevelLoader>().LoadLoseScreen();
        }
    }
    private void UpdateDisplay()
    {
        _healthText.text = Mathf.Max(health,0).ToString();
    }
    
}