using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class ButtonClicks : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler
{
    [SerializeField] private AudioManager audioManager;
    public void OnPointerEnter(PointerEventData eventData)
    {

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        audioManager.PlayButtonDown();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        audioManager.PlayButtonUp();
    }
}
