using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableMerch : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;
    public CustomerWants merchType;
    public MerchTableHandler merchHandler;
    public RectTransform destination;

    private void Awake()
    {
    }

    private void Start()
    {
        merchHandler = GameObject.FindObjectOfType<MerchTableHandler>();
        destination = merchHandler.destination;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("Dragging Trash");
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(destination, eventData.position))
        {
            //Debug.Log("reached destination");

            bool isRequired = merchHandler.CheckIfIsRequired(merchType);

            if (isRequired)
            {
                Debug.Log("Correct merch type - depositing in bag");
                merchHandler.UpdateCustomerCloud(merchType);
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Incorrect Merch type - throw in trash");
            }
        }

            // Check if we're over a dumpster
        //    foreach (RectTransform dumpsterRect in trashSorting.dumpsters)
        //{
        //    if (RectTransformUtility.RectangleContainsScreenPoint(dumpsterRect, eventData.position))
        //    {
        //        Dumpster dumpster = dumpsterRect.GetComponent<Dumpster>();

        //        if (dumpster && dumpster.acceptsType == trashType)
        //        {
        //            // Trash is of correct type for this dumpster
        //            trashSorting.TrashSorted(this);
        //            break;
        //        }
        //    }
        //}
    }
}
