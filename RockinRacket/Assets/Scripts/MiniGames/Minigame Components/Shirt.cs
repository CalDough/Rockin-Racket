using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*
 *  This class houses details about the shirt object used in the T-Shirt Cannon Minigame
 */
public class Shirt : MonoBehaviour
{
    [Header("Shirt Image(s)")]
    [SerializeField] Sprite shirtSprite;

    [Header("Logic Details")]
    [SerializeField] Transform destructionDestination;

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Shirt is being dragged");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("No longer being dragged");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointer Down");
    }
}
