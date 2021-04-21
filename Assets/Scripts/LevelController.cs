using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [SerializeField] private GameObject winLabel;
    [SerializeField] private GameObject loseLabel;
    [SerializeField] private GameObject pauseLabel;
    [SerializeField] private GameObject introCanvas;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private Text introText;
    [TextArea(1, 3)] [SerializeField] private string[] introTextList;
    private int _numOfAttackers;
    private bool _levelTimerFinished;
    private AudioSource _audioSource;
    private HealthDisplay _healthDisplay;
    private int _activeIntro;
    private IEnumerator _textCoroutine;
    private string _sceneName;
    private static readonly int IsOpen = Animator.StringToHash("isOpen");

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _healthDisplay = FindObjectOfType<HealthDisplay>();
        _sceneName = SceneManager.GetActiveScene().name;
        winLabel.SetActive(false);
        loseLabel.SetActive(false);
        pauseLabel.SetActive(false);
        if (introCanvas != null)
        {
            introCanvas.SetActive(false);
            if (PlayerPrefs.GetInt(_sceneName, 0) == 0)
            {
                pauseButton.SetActive(false);
                SetUpIntro();
            }
        }
    }

    private void SetUpIntro()
    {
        introCanvas.SetActive(true);
        Time.timeScale = 0;
        introCanvas.GetComponent<Animator>().SetBool(IsOpen, true);
        NextIntro();
    }

    public void NextIntro()
    {
        if (_activeIntro == introTextList.Length)
        {
            StartCoroutine(EndTutorial());
        }
        else
        {
            if (_textCoroutine != null)
            {
                StopCoroutine(_textCoroutine);
            }

            _textCoroutine = TypeText(introTextList[_activeIntro]);
            StartCoroutine(_textCoroutine);
        }

        _activeIntro++;
    }

    private IEnumerator TypeText(string text)
    {
        introText.text = "";
        foreach (var letter in text)
        {
            introText.text += letter;
            yield return null;
        }
    }

    private IEnumerator EndTutorial()
    {
        introCanvas.GetComponent<Animator>().SetBool(IsOpen, false);
        PlayerPrefs.SetInt(_sceneName, 1);
        yield return new WaitForSecondsRealtime(0.5f);
        introCanvas.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1;
    }

    public void AttackerSpawned()
    {
        _numOfAttackers++;
    }

    public void AttackerDestroyed()
    {
        _numOfAttackers--;
        if (_numOfAttackers <= 0 && _levelTimerFinished && _healthDisplay.IsAlive())
        {
            StartCoroutine(HandleWinCondition());
        }
    }

    private IEnumerator HandleWinCondition()
    {
        if (winLabel)
        {
            winLabel.SetActive(true);
            _audioSource.Play();
            yield return new WaitForSeconds(_audioSource.clip.length);
            FindObjectOfType<LevelLoader>().LoadNextScene();
        }
    }

    public void HandleLoseCondition()
    {
        loseLabel.SetActive(true);
        Time.timeScale = 0;
    }

    public void PauseGame()
    {
        pauseLabel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        pauseLabel.SetActive(false);
        Time.timeScale = 1;
    }

    public void LevelTimerFinished()
    {
        _levelTimerFinished = true;
        StopSpawners();
    }

    private void StopSpawners()
    {
        var spawners = FindObjectsOfType<AttackerSpawner>();
        foreach (var spawner in spawners)
        {
            spawner.StopSpawning();
        }
    }
}