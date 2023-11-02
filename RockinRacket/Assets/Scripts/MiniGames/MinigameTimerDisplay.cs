using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameTimerDisplay : MonoBehaviour
{
    [SerializeField] private MiniGame miniGame;
    [SerializeField] private Slider timerSlider;

    private void Awake()
    {
        if (timerSlider == null)
        {
            Debug.LogError("Timer Slider is not assigned!");
            enabled = false;
        }

        if (miniGame == null)
        {
            Debug.LogError("MiniGame is not assigned!");
            enabled = false;
        }
    }

    private void Update()
    {
        if (!miniGame.isActiveEvent)
        {
            return;
        }

        UpdateTimerSlider(miniGame.duration, miniGame.remainingDuration);
    }

    private void UpdateTimerSlider(float duration, float remainingDuration)
    {
        timerSlider.maxValue = duration;
        timerSlider.value = remainingDuration;
    }
}