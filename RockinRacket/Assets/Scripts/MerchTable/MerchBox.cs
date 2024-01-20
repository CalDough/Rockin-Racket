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

    public void Init(PurchaseableItem item, string itemName)
    {
        ownedItem = item;
        purchaseableItemName = itemName;
    }


    /*
     * The following method instantiates the corresponding PurchaseableItem prefab
     */
    public void OnPointerDown(PointerEventData eventData)
    {
        if (ownedItem != null && numToSpawn != 0)
        {
            Instantiate(ownedItem.itemPrefab, eventData.position, Quaternion.identity);
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
