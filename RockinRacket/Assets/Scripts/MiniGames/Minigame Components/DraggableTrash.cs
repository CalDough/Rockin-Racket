using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableTrash : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;
    public TrashType trashType;
    // Reference to the main TrashSorting script
    public TrashSorting trashSorting;

    private void Awake()
    {
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging Trash");
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Check if we're over a dumpster
        foreach (RectTransform dumpsterRect in trashSorting.dumpsters)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(dumpsterRect, eventData.position))
            {
                Dumpster dumpster = dumpsterRect.GetComponent<Dumpster>();

                if(dumpster && dumpster.acceptsType == trashType)
                {
                    // Trash is of correct type for this dumpster
                    trashSorting.TrashSorted(this);
                    break;
                }
            }
        }
    }
}
