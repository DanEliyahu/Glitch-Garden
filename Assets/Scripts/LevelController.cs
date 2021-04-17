using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [SerializeField] private GameObject winLabel;
    [SerializeField] private GameObject loseLabel;
    [SerializeField] private GameObject pauseLabel;
    [SerializeField] private GameObject introCanvas;
    [SerializeField] private Text introText;
    [TextArea(1,3)][SerializeField] private string[] introTextList;
    private int _numOfAttackers;
    private bool _levelTimerFinished;
    private AudioSource _audioSource;
    private HealthDisplay _healthDisplay;
    private int _activeIntro;
    private IEnumerator _textCoroutine;
    private static readonly int IsOpen = Animator.StringToHash("isOpen");

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _healthDisplay = FindObjectOfType<HealthDisplay>();
        winLabel.SetActive(false);
        loseLabel.SetActive(false);
        pauseLabel.SetActive(false);
        if (introCanvas)
        {
            introCanvas.SetActive(false);
            SetUpIntro();
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
        yield return new WaitForSecondsRealtime(0.5f);
        introCanvas.SetActive(false);
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