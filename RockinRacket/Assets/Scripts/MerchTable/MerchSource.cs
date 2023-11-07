using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MerchSource : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] GameStateData currentGameState;
    [SerializeField] CustomerWants sourceType;

    /*
     * 
     */
    public void OnPointerDown(PointerEventData eventData)
    {
        currentGameState.currentlyHeldObject = sourceType;
        Debug.Log("Player Clicked on " + gameObject.name);
    }
}
