using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 
using FMODUnity; 
using System.IO;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
/*
    This handles the User's interaction with the game settings such as Contrast, Brightness, Gamma, Audio Levels, and FPS.
    It has functions to save and load these settings.
    Other scripts will read from the saved settings to modify the sound levels, UI size, or screen settings.
*/
public class AudioSettings : MonoBehaviour
{
    private string saveFolderPath = "Player/";
    private string saveFileName = "AudioSettings.json";
    private string fullSavePath;

    public Slider masterVolumeSlider;

    private FMOD.Studio.VCA masterVCA;
    private FMOD.Studio.VCA musicVCA;
    private FMOD.Studio.VCA voiceVCA;
    private FMOD.Studio.VCA soundeffectVCA;
    private FMOD.Studio.VCA ambientVCA;

    //This is a serialized script with data that we read and write from
    //It is below this script
    private AudioSettingsData currentSettings = new AudioSettingsData(); 

    private void Awake()
    {
        fullSavePath = Path.Combine(Application.persistentDataPath, saveFolderPath);
        Directory.CreateDirectory(fullSavePath);
        fullSavePath = Path.Combine(fullSavePath, saveFileName);

    }
    
    private void Start()
    {
        
        LoadSettings();
        
        masterVCA = FMODUnity.RuntimeManager.GetVCA("vca:/Master");
        //musicVCA = FMODUnity.RuntimeManager.GetVCA("vca:/Music");
        //voiceVCA = FMODUnity.RuntimeManager.GetVCA("vca:/Voice");
        //soundeffectVCA = FMODUnity.RuntimeManager.GetVCA("vca:/SoundEffects");
        //ambientVCA = FMODUnity.RuntimeManager.GetVCA("vca:/Ambient");
        if (masterVolumeSlider != null)
        {
            masterVolumeSlider.minValue = 0f; 
            masterVolumeSlider.maxValue = 1f; 
            
            masterVolumeSlider.value = currentSettings.masterVolume;
            masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        }
        ApplySettings();
    }

    public void SaveSettings()
    {
        currentSettings.masterVolume = masterVolumeSlider.value;

        string json = JsonUtility.ToJson(currentSettings);
        File.WriteAllText(fullSavePath, json);
        ApplySettings();
    }

    public void LoadSettings()
    {
        if (File.Exists(fullSavePath))
        {
            string json = File.ReadAllText(fullSavePath);
            currentSettings = JsonUtility.FromJson<AudioSettingsData>(json);
        }
        else
        {
            SetDefaultSettings();
        }
    }

    private void SetMasterVolume(float volume)
    {
        currentSettings.masterVolume = volume;
        ApplySettings();
    }

    public void ApplySettings()
    {

        if (masterVCA.isValid())
        {
            masterVCA.setVolume(currentSettings.masterVolume);
        }
        //musicVCA.setVolume(currentSettings.musicVolume);
        //voiceVCA.setVolume(currentSettings.voiceVolume);
        //soundeffectVCA.setVolume(currentSettings.soundEffectsVolume);
        //ambientVCA.setVolume(currentSettings.ambientVolume);
    }


    public void ResetToDefault()
    {
        SetDefaultSettings();
        ApplySettings();
        SaveSettings(); 
    }

    private void SetDefaultSettings()
    {
        currentSettings = new AudioSettingsData
        {
            masterVolume = 1.0f,
        };
    }

}

[System.Serializable]
public class AudioSettingsData
{
    public float masterVolume;

}