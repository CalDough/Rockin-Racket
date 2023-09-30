using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoodBar : MonoBehaviour
{
    [SerializeField] Slider slider;

    public void SetValue(int amount)
    {
        slider.value = amount;
    }

    public void SetMaxValue(int amount)
    {
        slider.maxValue = amount;
        slider.value = 0;
    }
}
