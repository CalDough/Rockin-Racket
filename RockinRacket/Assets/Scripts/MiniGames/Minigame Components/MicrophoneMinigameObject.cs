using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MicrophoneMinigameObject : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int cleanlinessLevel;
    private const int MaxCleanliness = 8;
    
    public bool IsFullyClean => cleanlinessLevel >= MaxCleanliness;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!IsFullyClean)
        {
            cleanlinessLevel++;
            UpdateVisuals();
        }
    }

    public void ResetCleanliness()
    {
        cleanlinessLevel = 0;
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        // update the visuals of the microphone based on the cleanliness level

    }
}
