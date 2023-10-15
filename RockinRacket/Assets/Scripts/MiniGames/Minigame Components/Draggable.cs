using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector]
    public Vector3 startPosition;

    public int SlotID;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        //canvasGroup.blocksRaycasts = false; // object might block its own ray during dragging
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position; // Update position while dragging
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; // Make it detectable by raycast again
        transform.position = startPosition; // This will be changed if dropped on a valid Drop zone
    }
}
