using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = PlayerPrefsController.MasterVolume;
    }

    public void SetVolume(float volume)
    {
        _audioSource.volume = volume;
    }
}