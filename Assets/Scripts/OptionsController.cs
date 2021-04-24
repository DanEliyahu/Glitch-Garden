using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider difficultySlider;
    private MusicPlayer _musicPlayer;

    // Start is called before the first frame update
    void Start()
    {
        volumeSlider.value = PlayerPrefsController.MasterVolume;
        difficultySlider.value = PlayerPrefsController.Difficulty;
        _musicPlayer = FindObjectOfType<MusicPlayer>();
    }

    public void SetVolume()
    {
        if (_musicPlayer)
        {
            _musicPlayer.SetVolume(volumeSlider.value);
        }
    }

    public void SaveAndExit()
    {
        PlayerPrefsController.MasterVolume = volumeSlider.value;
        PlayerPrefsController.Difficulty = (int)difficultySlider.value;
        FindObjectOfType<LevelLoader>().LoadMainMenu();
    }

    public void SetDefaults()
    {
        volumeSlider.value = PlayerPrefsController.GetDefaultMasterVolume();
        difficultySlider.value = PlayerPrefsController.GetDefaultDifficulty();
    }
}