using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private GameObject winLabel;
    [SerializeField] private GameObject loseLabel;
    private int _numOfAttackers;
    private bool _levelTimerFinished;
    private AudioSource _audioSource;
    private HealthDisplay _healthDisplay;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _healthDisplay = FindObjectOfType<HealthDisplay>();
        winLabel.SetActive(false);
        loseLabel.SetActive(false);
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
        winLabel.SetActive(true);
        _audioSource.Play();
        yield return new WaitForSeconds(_audioSource.clip.length);
        FindObjectOfType<LevelLoader>().LoadNextScene();
    }

    public void HandleLoseCondition()
    {
        loseLabel.SetActive(true);
        Time.timeScale = 0;
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