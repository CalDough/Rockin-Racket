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
public class SettingsManager : MonoBehaviour
{
    private string saveFolderPath = "Player/";
    private string saveFileName = "Settings.json";
    private string fullSavePath;

    public UniversalRenderPipelineAsset urpAsset;
    private Volume globalVolume; 
    private ColorAdjustments colorAdjustments;
    private LiftGammaGain liftGammaGain;

    public Toggle vsyncToggle;
    public Slider brightnessSlider;
    public Slider contrastSlider;
    public Slider gammaSlider;
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider voiceVolumeSlider;
    public Slider soundEffectsVolumeSlider;
    public Slider ambientVolumeSlider;
    public Slider textSizeSlider;

    //Im figuring out a better way for text scaling for other scenes
    //multiplying by a scaler value to change size is not good, its just to see if its possible
    private float previousTextSize = 1f;

    private FMOD.Studio.VCA masterVCA;
    private FMOD.Studio.VCA musicVCA;
    private FMOD.Studio.VCA voiceVCA;
    private FMOD.Studio.VCA soundeffectVCA;
    private FMOD.Studio.VCA ambientVCA;

    //This is a serialized script with data that we read and write from
    //It is below this script
    private GameSettings currentSettings = new GameSettings(); 

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
        musicVCA = FMODUnity.RuntimeManager.GetVCA("vca:/Music");
        voiceVCA = FMODUnity.RuntimeManager.GetVCA("vca:/Voice");
        soundeffectVCA = FMODUnity.RuntimeManager.GetVCA("vca:/SoundEffects");
        ambientVCA = FMODUnity.RuntimeManager.GetVCA("vca:/Ambient");


        ApplySettings();
        
        globalVolume = FindObjectOfType<Volume>();
        if (!globalVolume.profile.TryGet(out colorAdjustments))
        {
            Debug.Log("No Color Adjustments found on Volume");
        }
        
        if (!globalVolume.profile.TryGet(out liftGammaGain))
        {
            Debug.Log("No Lift, Gamma, Gain adjustments found on Volume");
        }
    }

    public void SaveSettings()
    {
        currentSettings.vsync = vsyncToggle.isOn;
        currentSettings.brightness = brightnessSlider.value;
        currentSettings.contrast = contrastSlider.value;
        currentSettings.gamma = gammaSlider.value;
        currentSettings.masterVolume = masterVolumeSlider.value;
        currentSettings.musicVolume = musicVolumeSlider.value;
        currentSettings.voiceVolume = voiceVolumeSlider.value;
        currentSettings.soundEffectsVolume = soundEffectsVolumeSlider.value;
        currentSettings.ambientVolume = ambientVolumeSlider.value;
        currentSettings.textSize = textSizeSlider.value;

        string json = JsonUtility.ToJson(currentSettings);
        File.WriteAllText(fullSavePath, json);
        ApplySettings();
    }

    public void LoadSettings()
    {
        if (File.Exists(fullSavePath))
        {
            string json = File.ReadAllText(fullSavePath);
            currentSettings = JsonUtility.FromJson<GameSettings>(json);
        }
        else
        {
            SetDefaultSettings();
        }

        UpdateUIWithSettings();
    }

    private void UpdateUIWithSettings()
    {
        vsyncToggle.isOn = currentSettings.vsync;
        brightnessSlider.value = currentSettings.brightness;
        contrastSlider.value = currentSettings.contrast;
        gammaSlider.value = currentSettings.gamma;
        masterVolumeSlider.value = currentSettings.masterVolume;
        musicVolumeSlider.value = currentSettings.musicVolume;
        voiceVolumeSlider.value = currentSettings.voiceVolume;
        soundEffectsVolumeSlider.value = currentSettings.soundEffectsVolume;
        ambientVolumeSlider.value = currentSettings.ambientVolume;
        textSizeSlider.value = currentSettings.textSize;
    }

    public void ApplySettings()
    {
        QualitySettings.vSyncCount = currentSettings.vsync ? 1 : 0;

        if (colorAdjustments != null)
        {
            colorAdjustments.postExposure.value = currentSettings.brightness;
            colorAdjustments.contrast.value = currentSettings.contrast;
            Debug.Log("Brightness/Contrast Changed");
        }

        if (liftGammaGain != null)
        {
            float gammaValue = currentSettings.gamma;
            liftGammaGain.gamma.value = new Vector4(gammaValue, gammaValue, gammaValue, gammaValue);
            Debug.Log("Gamma Changed");
        }

        masterVCA.setVolume(currentSettings.masterVolume);
        musicVCA.setVolume(currentSettings.musicVolume);
        voiceVCA.setVolume(currentSettings.voiceVolume);
        soundeffectVCA.setVolume(currentSettings.soundEffectsVolume);
        ambientVCA.setVolume(currentSettings.ambientVolume);


        TextMeshProUGUI[] allTexts = FindObjectsOfType<TextMeshProUGUI>();
        foreach (var text in allTexts)
        {
            text.fontSize *= currentSettings.textSize;
        }
    }

    public void TestSettings()
    {
        QualitySettings.vSyncCount = vsyncToggle.isOn ? 1 : 0;

        if (colorAdjustments != null)
        {
            colorAdjustments.postExposure.value = brightnessSlider.value;
            colorAdjustments.contrast.value = contrastSlider.value;
            Debug.Log("Brightness/Contrast Changed");
        }

        if (liftGammaGain != null)
        {
            float gammaValue = gammaSlider.value;
            liftGammaGain.gamma.value = new Vector4(gammaValue, gammaValue, gammaValue, gammaValue);
            Debug.Log("Gamma Changed");
        }

        masterVCA.setVolume(masterVolumeSlider.value);
        musicVCA.setVolume(musicVolumeSlider.value);
        voiceVCA.setVolume(voiceVolumeSlider.value);
        soundeffectVCA.setVolume(soundEffectsVolumeSlider.value);
        ambientVCA.setVolume(ambientVolumeSlider.value);

        TextMeshProUGUI[] allTexts = FindObjectsOfType<TextMeshProUGUI>();
        foreach (var text in allTexts)
        {
            text.fontSize *= (1f / previousTextSize); 
        }
        foreach (var text in allTexts)
        {
            text.fontSize *= textSizeSlider.value; 
        }
        previousTextSize = textSizeSlider.value;
    }

    public void ResetToDefault()
    {
        SetDefaultSettings();
        UpdateUIWithSettings();
        ApplySettings();
        SaveSettings(); 

        TextMeshProUGUI[] allTexts = FindObjectsOfType<TextMeshProUGUI>();
        foreach (var text in allTexts)
        {
            text.fontSize *= (1f / previousTextSize); 
        }

        foreach (var text in allTexts)
        {
            text.fontSize *= textSizeSlider.value; 
        }
        
        previousTextSize = textSizeSlider.value;
    }

    private void SetDefaultSettings()
    {
        currentSettings = new GameSettings
        {
            vsync = true,
            brightness = 0.0f,
            contrast = 0.0f,
            gamma = 0.0f,
            masterVolume = 1.0f,
            musicVolume = 1.0f,
            voiceVolume = 1.0f,
            soundEffectsVolume = 1.0f,
            ambientVolume = 1.0f,
            textSize = 1f
        };
    }

}

[System.Serializable]
public class GameSettings
{
    public bool vsync;
    public float brightness;
    public float contrast;
    public float gamma;
    public float masterVolume;
    public float musicVolume;
    public float voiceVolume;
    public float soundEffectsVolume;
    public float ambientVolume;
    public float textSize;
}