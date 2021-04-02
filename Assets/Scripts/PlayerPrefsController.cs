using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{
    private const string MasterVolumeKey = "MasterVolume";
    private const float DefaultMasterVolumeValue = 0.5f;
    private const float MinimumMasterVolumeValue = 0f;
    private const float MaximumMasterVolumeValue = 1f;

    private const string DifficultyKey = "Difficulty";
    private const int DefaultDifficultyValue = 2;
    private const int MinimumDifficultyValue = 1;
    private const int MaximumDifficultyValue = 3;

    public static float MasterVolume
    {
        get => PlayerPrefs.GetFloat(MasterVolumeKey, DefaultMasterVolumeValue);
        set => PlayerPrefs.SetFloat(MasterVolumeKey,
            Mathf.Clamp(value, MinimumMasterVolumeValue, MaximumMasterVolumeValue));
    }

    public static int Difficulty
    {
        get => PlayerPrefs.GetInt(DifficultyKey, DefaultDifficultyValue);
        set => PlayerPrefs.SetInt(DifficultyKey, Mathf.Clamp(value, MinimumDifficultyValue, MaximumDifficultyValue));
    }

    public static float GetDefaultMasterVolume()
    {
        return DefaultMasterVolumeValue;
    }
    
    public static float GetDefaultDifficulty()
    {
        return DefaultDifficultyValue;
    }
}