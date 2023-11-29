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
        //if (StateManager.Instance == null || StateManager.Instance.CurrentState == null || StateManager.Instance.CurrentState.stateType != StateType.Song)
        //{
        //    return;
        //}

        if (StateManager.Instance.CurrentState.stateType == StateType.Song || StateManager.Instance.CurrentState.stateType == StateType.Intermission)
        {
            float duration = StateManager.Instance.stateDuration;
            float currentTime = StateManager.Instance.stateRemainder;

            UpdateTimerSlider(duration, currentTime);
        } 
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
