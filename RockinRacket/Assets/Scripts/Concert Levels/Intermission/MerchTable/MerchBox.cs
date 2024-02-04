using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*
 * The following class is used to handle and spawn MerchBox elements for the player to drag and use to fulfill the current order
 * 
 * 
 * The following class's initialization is controlled through the MerchTable class
 */

public class MerchBox : MonoBehaviour, IPointerDownHandler
{
    [Header("Merch Box Characteristics")]
    [SerializeField] private PurchaseableItem ownedItem;
    [SerializeField] private string purchaseableItemName;
    [SerializeField] int numToSpawn = 0;
    [SerializeField] RectTransform destination;

    public void Init(PurchaseableItem item, string itemName, RectTransform destin)
    {
        ownedItem = item;
        purchaseableItemName = itemName;
        destination = destin;
    }


    /*
     * The following method instantiates the corresponding PurchaseableItem prefab
     */
    public void OnPointerDown(PointerEventData eventData)
    {
        if (ownedItem != null && numToSpawn != 0)
        {
            GameObject item = Instantiate(ownedItem.itemPrefab, new Vector3(0, 200, 0), Quaternion.identity);
            Transform itemPos = item.transform;
            //item.transform.SetParent(GameObject.FindGameObjectWithTag("PurchaseableItemParent").transform, false);
            item.transform.SetParent(gameObject.transform, false);
            //item.transform.position = itemPos.position;
            item.GetComponent<DraggablePurchaseableItem>().SetItemName(ownedItem.itemName);
            item.GetComponent<DraggablePurchaseableItem>().SetDestination(destination);
            numToSpawn--;
        }
    }

    /*
     * The following method determines how many merch items of this particular type are left to spawn
     */
    public void UpdateMerchItemSpawnCounter(int num)
    {
        numToSpawn = num;
    }
}
