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
    [SerializeField] private Sprite itemSprite;

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
            Debug.Log("<green>Correct PurchaseableItem Deposited</green>");
            MerchTableEvents.instance.e_itemDeposited.Invoke(itemSprite);
            Destroy(gameObject);
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
    public void SetItemSprite(Sprite sprite)
    {
        itemSprite = sprite;
    }
        
}
