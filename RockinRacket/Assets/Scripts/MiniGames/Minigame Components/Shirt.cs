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

    }
}
