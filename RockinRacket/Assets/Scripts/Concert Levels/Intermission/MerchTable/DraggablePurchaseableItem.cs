using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;

/*
 * The following script is used on the PurchaseableItem prefab and allows it to drag and find a destination
 */

public class DraggablePurchaseableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Destination Data")]
    [SerializeField] private RectTransform destination;
    [SerializeField] private string itemName;
    [SerializeField] private RectTransform screenBoundary;

    /*
     * The following three methods are implemented from the DragHandler interface set
     */
    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(destination, eventData.position))
        {
            Debug.Log("<color=green>Correct PurchaseableItem Deposited</color>");
            MerchTableEvents.instance.e_itemDeposited.Invoke(itemName);
            Destroy(gameObject);
        }

        if (!RectTransformUtility.RectangleContainsScreenPoint(screenBoundary, eventData.position))
        {
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        }
    }

    /*
     * The following method sets the destination the spawned purchaseableItem
     */
    public void SetDestination(RectTransform finalDestination)
    {
        destination = finalDestination;
    }
    
    /*
     * The following method sets the name of the item prefab
     */
    public void SetItemName(string name)
    {
        itemName = name;
    }

    /*
     *  The following method sets the boundary of the world
     */
    public void SetScreenBoundary(RectTransform screenSpace)
    {
        screenBoundary = screenSpace;
    }
        
}
