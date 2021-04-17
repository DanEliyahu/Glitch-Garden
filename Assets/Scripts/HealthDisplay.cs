using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    private int _health = 4;
    private Text _healthText;
    private bool _lost;

    // Start is called before the first frame update
    void Start()
    {
        _health -= PlayerPrefsController.Difficulty;
        _healthText = GetComponent<Text>();
        UpdateDisplay();
    }

    public void TakeDamage()
    {
        _health--;
        UpdateDisplay();
        if (_health <= 0 && !_lost)
        {
            _lost = true;
            FindObjectOfType<LevelController>().HandleLoseCondition();
        }
    }
    private void UpdateDisplay()
    {
        _healthText.text = Mathf.Max(_health,0).ToString();
    }

    public bool IsAlive()
    {
        return _health > 0;
    }
    
}