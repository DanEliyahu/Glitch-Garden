using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    [Tooltip("Level timer in SECONDS")] [SerializeField]
    private float levelTime = 10;

    private Slider _slider;
    private LevelController _levelController;
    private bool _isLevelFinished = false;

    private void Start()
    {
        _levelController = FindObjectOfType<LevelController>();
        _slider = GetComponent<Slider>();
    }

    private void Update()
    {
        if (_isLevelFinished) return;
        _slider.value = Time.timeSinceLevelLoad / levelTime;
        var timerFinished = (Time.timeSinceLevelLoad >= levelTime);
        if (timerFinished)
        {
            _levelController.LevelTimerFinished();
            _isLevelFinished = true;
        }
    }
}