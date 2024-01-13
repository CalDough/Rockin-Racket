using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableMerch : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;
    public CustomerWants merchType;
    public int merchID;
    public string merchName;
    public Sprite merchSprite;
    //public MerchTableHandler merchHandler;
    public MerchTableUIHandler uiHandler;
    public RectTransform destination;

    private void Awake()
    {
    }

    private void Start()
    {
        //merchHandler = GameObject.FindObjectOfType<MerchTableHandler>();
        //destination = merchHandler.destination;
        uiHandler = GetComponent<MerchTableUIHandler>();
        destination = uiHandler.destination;
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


            //bool isRequired = merchHandler.CheckIfIsRequired(merchType);
            bool isRequired = uiHandler.CheckIfIsRequired(merchID);

            if (isRequired)
            {
                Debug.Log("Correct merch type - depositing in bag");
                //merchHandler.UpdateCustomerCloud(merchType);
                uiHandler.CustomerWantFulfilled(new PurchaseableItem(merchID, merchName, merchSprite));
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
