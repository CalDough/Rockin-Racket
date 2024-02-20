using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseButton : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler
{
    [SerializeField] private PauseManager pauseManager;
    public void OnPointerEnter(PointerEventData eventData)
    {

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        pauseManager.PlayButtonDown();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        pauseManager.PlayButtonUp();
    }
}
