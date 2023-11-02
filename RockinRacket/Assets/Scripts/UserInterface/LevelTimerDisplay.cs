using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTimerDisplay : MonoBehaviour
{
    [SerializeField] private Slider timerSlider;

    private void Awake()
    {
        if (timerSlider == null)
        {
            Debug.LogError("Timer slider is not assigned!");
            enabled = false;
        }
    }

    private void Update()
    {
        if (GameStateManager.Instance == null || GameStateManager.Instance.CurrentGameState == null)
        {
            return;
        }

        float duration = GameStateManager.Instance.CurrentGameState.Duration;
        float currentTime = GameStateManager.Instance.levelTime;

        UpdateTimerSlider(duration, currentTime);
    }

    private void UpdateTimerSlider(float duration, float currentTime)
    {
        if (duration <= 0)
        {
            //Debug.LogWarning("Duration is less than or equal to zero");
            return;
        }

        timerSlider.maxValue = duration;
        timerSlider.value = currentTime;
    }
}
