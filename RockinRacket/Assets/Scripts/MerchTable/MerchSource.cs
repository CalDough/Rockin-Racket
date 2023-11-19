using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MerchSource : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] GameStateData currentGameState;
    [SerializeField] CustomerWants sourceType;
    [SerializeField] bool isDestination;
    [SerializeField] Color highlightColor;
    [SerializeField] GameObject draggablePrefab;
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject shopAsParent;

    private Color baseColor;
    private RawImage imageRenderer;

    private void Start()
    {
        imageRenderer = gameObject.transform.GetComponent<RawImage>();
        baseColor = imageRenderer.color;
    }

    /*
     * 
     */
    public void OnPointerDown(PointerEventData eventData)
    {
        //currentGameState.currentlyHeldObject = sourceType;
        //Debug.Log("Player Clicked on " + gameObject.name);
        Instantiate(draggablePrefab, shopAsParent.transform);
    }

    public void OnMouseEnter()
    {
        imageRenderer.color = highlightColor;
    }

    private void OnMouseExit()
    {
        imageRenderer.color = baseColor;
    }
}
