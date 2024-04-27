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
    private string saveFolderPath = "Player/SaveFiles/";
    private string saveFileName = "AudioSettings.json";
    private string fullSavePath = "";

    public Slider masterVolumeSlider;
    public Slider ambientVolumeSlider;
    public Slider musicVolumeSlider;

    private FMOD.Studio.VCA masterVCA;
    private FMOD.Studio.VCA musicVCA;
    private FMOD.Studio.VCA voiceVCA;
    private FMOD.Studio.VCA soundeffectVCA;
    private FMOD.Studio.VCA ambientVCA;

    //This is a serialized script with data that we read and write from
    //It is below this script
    public AudioSettingsData currentSettings = new AudioSettingsData(); 

    private void Awake()
    {
        fullSavePath = Path.Combine(Application.persistentDataPath, saveFolderPath);
        Directory.CreateDirectory(fullSavePath);
        fullSavePath = Path.Combine(fullSavePath, saveFileName);

    }
    
    private void Start()
    {
        masterVCA = FMODUnity.RuntimeManager.GetVCA("vca:/Master");
        musicVCA = FMODUnity.RuntimeManager.GetVCA("vca:/Music");
        ambientVCA = FMODUnity.RuntimeManager.GetVCA("vca:/Ambient");


        LoadSettings();
        Debug.Log("Settings Loaded Finished");
        SetUpSliders();
        Debug.Log("Sliders  Finished");
        ApplySettings();
        Debug.Log("Settings Applied  Finished");
    }


    public void SetUpSliders()
    {
        Debug.Log("Audio Settings: " + currentSettings.masterVolume + "," + currentSettings.ambientVolume + "," + currentSettings.musicVolume);


        if (masterVolumeSlider != null)
        {            
            //masterVolumeSlider.minValue = 0f;
            //masterVolumeSlider.maxValue = 1f;
            masterVolumeSlider.value = currentSettings.masterVolume;
            masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
            Debug.Log("Audio Set: " + currentSettings.masterVolume);
        }
        else
        {
            Debug.Log("Slider is null");
        }

        if (ambientVolumeSlider != null)
        {
            //ambientVolumeSlider.minValue = 0f;
            //ambientVolumeSlider.maxValue = 1f;
            ambientVolumeSlider.value = currentSettings.ambientVolume;
            ambientVolumeSlider.onValueChanged.AddListener(SetBackgroundVolume);
            Debug.Log("Audio Set: " + currentSettings.ambientVolume);
        }        
        else
        {
            Debug.Log("Slider is null");
        }

        if (musicVolumeSlider != null)
        { 
            //musicVolumeSlider.minValue = 0f;
            //musicVolumeSlider.maxValue = 1f;
            musicVolumeSlider.value = currentSettings.musicVolume;
            musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
            Debug.Log("Audio Set: " + currentSettings.musicVolume);
        }        
        else
        {
            Debug.Log("Slider is null");
        }
    }

    public void SaveSettings()
    {
        Debug.Log("Saving Audio Settings");
        string json = JsonUtility.ToJson(currentSettings);
        File.WriteAllText(fullSavePath, json);
        ApplySettings();
    }

    private void CreateDefaultSaveFile()
    {
        SetDefaultSettings();
        Debug.Log("Audio Set to default");
        string json = JsonUtility.ToJson(currentSettings);
        File.WriteAllText(fullSavePath, json);
    }

    public void LoadSettings()
    {
        if (File.Exists(fullSavePath))
        {
            string json = File.ReadAllText(fullSavePath);
            currentSettings = JsonUtility.FromJson<AudioSettingsData>(json);
            //Debug.Log("AUDIO LOADED VALUES: " + currentSettings.masterVolume + "," + currentSettings.ambientVolume + "," + currentSettings.musicVolume);

        }
        else
        {
            CreateDefaultSaveFile();
        }

    }

    public void ApplySettings()
    {
        if (masterVCA.isValid())
        {
            masterVCA.setVolume(currentSettings.masterVolume);
           // Debug.Log("Setting Master Volume: " + currentSettings.masterVolume);
        }
        if (ambientVCA.isValid())
        {
            ambientVCA.setVolume(currentSettings.ambientVolume);
           // Debug.Log("Setting Ambient Volume: " + currentSettings.ambientVolume);
        }
        if (musicVCA.isValid())
        {
            musicVCA.setVolume(currentSettings.musicVolume);
            //Debug.Log("Setting Music Volume: " + currentSettings.musicVolume);
        }
    }



    private void SetDefaultSettings()
    {
        currentSettings = new AudioSettingsData
        {
            masterVolume = 1.0f,
            ambientVolume = 1.0f,
            musicVolume = 1.0f
        };
    }

    private void SetBackgroundVolume(float volume)
    {
        
        currentSettings.ambientVolume = volume;
        if (ambientVCA.isValid())
        {
            ambientVCA.setVolume(currentSettings.ambientVolume);
            //Debug.Log("Setting Ambient Volume: " + currentSettings.ambientVolume);
        }
    }

    private void SetMusicVolume(float volume)
    {
        currentSettings.musicVolume = volume;
        if (musicVCA.isValid())
        {
            musicVCA.setVolume(currentSettings.musicVolume);
            //Debug.Log("Setting Music Volume: " + currentSettings.musicVolume);
        }
    }

    private void SetMasterVolume(float volume)
    {
        currentSettings.masterVolume = volume;
        if (masterVCA.isValid())
        {
            masterVCA.setVolume(currentSettings.masterVolume);
           // Debug.Log("Setting Master Volume: " + currentSettings.masterVolume);
        }
    }
}

[System.Serializable]
public class AudioSettingsData
{
    public float masterVolume;
    public float ambientVolume;
    public float musicVolume;
}