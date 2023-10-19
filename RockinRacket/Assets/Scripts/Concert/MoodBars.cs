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
        if (hypeSlider.value / maxHype <= 0.5f)
        {
            hypeSliderFill.color = Color.Lerp(hypeLowColor, hypeMidColor, hypeSlider.value / (0.5f * maxHype));
        }
        else if (hypeSlider.value / maxHype <= 0.9f)
        {
            hypeSliderFill.color = Color.Lerp(hypeMidColor, hypeHighColor, (hypeSlider.value - 0.5f * maxHype) / (0.4f * maxHype));
        }
        else
        {
            hypeSliderFill.color = hypeHighColor;
        }

        // Update Comfort Color
        if (comfortSlider.value / maxComfort <= 0.5f)
        {
            comfortSliderFill.color = Color.Lerp(comfortLowColor, comfortMidColor, comfortSlider.value / (0.5f * maxComfort));
        }
        else if (comfortSlider.value / maxComfort <= 0.9f)
        {
            comfortSliderFill.color = Color.Lerp(comfortMidColor, comfortHighColor, (comfortSlider.value - 0.5f * maxComfort) / (0.4f * maxComfort));
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
