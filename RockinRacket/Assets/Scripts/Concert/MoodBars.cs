using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoodBars : MonoBehaviour
{
    [Header("Sliders References")]

    public Slider hypeSlider;
    public Slider comfortSlider;
    [SerializeField] private Image hypeSliderFill;
    [SerializeField] private Image comfortSliderFill;
    [SerializeField] private Color hypeLowColor = Color.yellow;
    [SerializeField] private Color hypeMidColor = new Color(1f, 0.5f, 0f);
    [SerializeField] private Color hypeHighColor = Color.red;

    [SerializeField] private Color comfortLowColor = Color.red;
    [SerializeField] private Color comfortMidColor = Color.yellow;
    [SerializeField] private Color comfortHighColor = Color.green;

    private float maxHype = 1000f;
    private float maxComfort = 1000f;
    [SerializeField] private float hypeLerpSpeed = 5f;
    [SerializeField] private float comfortLerpSpeed = 5f;
    
    [SerializeField] private float lowHypeThresholdPercent = 20f; //  percentage
    [SerializeField] private float highHypeThresholdPercent = 50f; 
    [SerializeField] private float lowComfortThresholdPercent = 20f; 
    [SerializeField] private float highComfortThresholdPercent = 75f; 

    private float lowHypeThreshold;
    private float highHypeThreshold;
    private float lowComfortThreshold;
    private float highComfortThreshold;

    private void Start()
    {
        InitializeSliders();
        GameStateEvent.OnGameStateStart += HandleGameStateStart;
        GameStateEvent.OnGameStateEnd += HandleGameStateEnd;
    }

    private void Update()
    {
        UpdateBars();
        UpdateBarColors();
    }

    private void CalculateThresholds()
    {
        lowHypeThreshold = MinigameStatusManager.Instance.maxHype * (lowHypeThresholdPercent / 100f);
        highHypeThreshold = MinigameStatusManager.Instance.maxHype * (highHypeThresholdPercent / 100f);
        lowComfortThreshold = MinigameStatusManager.Instance.maxComfort * (lowComfortThresholdPercent / 100f);
        highComfortThreshold = MinigameStatusManager.Instance.maxComfort * (highComfortThresholdPercent / 100f);
    }

    private void InitializeSliders()
    {
        // Ensure we have the right references
        if (hypeSlider == null || comfortSlider == null)
        {
            Debug.LogError("sliders not assigned in the inspector");
            return;
        }

        maxHype = MinigameStatusManager.Instance.maxHype;
        maxComfort = MinigameStatusManager.Instance.maxComfort;

        CalculateThresholds();

        hypeSlider.maxValue = maxHype;
        comfortSlider.maxValue = maxComfort;

        hypeSlider.value = MinigameStatusManager.Instance.hype;
        comfortSlider.value = MinigameStatusManager.Instance.comfort;
    }

    private void UpdateBars()
    {
        float targetHypeValue = Mathf.Clamp(MinigameStatusManager.Instance.hype, 0, maxHype);
        float targetComfortValue = Mathf.Clamp(MinigameStatusManager.Instance.comfort, 0, maxComfort);

        hypeSlider.value = Mathf.Lerp(hypeSlider.value, targetHypeValue, Time.deltaTime * hypeLerpSpeed);
        comfortSlider.value = Mathf.Lerp(comfortSlider.value, targetComfortValue, Time.deltaTime * comfortLerpSpeed);
    }

    private void UpdateBarColors()
    {
        // Update Hype Color
        if (hypeSlider.value <= lowHypeThreshold)
        {
            hypeSliderFill.color = Color.Lerp(hypeLowColor, hypeMidColor, hypeSlider.value / lowHypeThreshold);
        }
        else if (hypeSlider.value <= highHypeThreshold)
        {
            hypeSliderFill.color = Color.Lerp(hypeMidColor, hypeHighColor, (hypeSlider.value - lowHypeThreshold) / (highHypeThreshold - lowHypeThreshold));
        }
        else
        {
            hypeSliderFill.color = hypeHighColor;
        }

        // Update Comfort Color
        if (comfortSlider.value <= lowComfortThreshold)
        {
            comfortSliderFill.color = Color.Lerp(comfortLowColor, comfortMidColor, comfortSlider.value / lowComfortThreshold);
        }
        else if (comfortSlider.value <= highComfortThreshold)
        {
            comfortSliderFill.color = Color.Lerp(comfortMidColor, comfortHighColor, (comfortSlider.value - lowComfortThreshold) / (highComfortThreshold - lowComfortThreshold));
        }
        else
        {
            comfortSliderFill.color = comfortHighColor;
        }
    }

    
    void OnDestroy()
    {
        GameStateEvent.OnGameStateStart -= HandleGameStateStart;
        GameStateEvent.OnGameStateEnd -= HandleGameStateEnd;
    }

    public void HandleGameStateStart(object sender, GameStateEventArgs e)
    {
        //Debug.Log("State Started: " + e.stateType);
        switch(e.stateType)
        {
            case GameModeType.Song:
                InitializeSliders();
                break;
            default:
                break;
        }
    }
    
    private void HandleGameStateEnd(object sender, GameStateEventArgs e)
    {
        //Debug.Log("Game state ended: " + e.state.GameType);
        switch(e.stateType)
        {
            case GameModeType.Song:
                InitializeSliders();
                break;
            default:
                break;
        }
    }

}
