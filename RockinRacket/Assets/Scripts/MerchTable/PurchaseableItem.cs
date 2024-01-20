using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This custom class is a data storage container for the items customers can purchase at the Merch Table. It stores all of
 * the relevant data to the product.
 */

public class PurchaseableItem
{
    public int itemID;
    public string itemName;
    public Sprite itemIcon;
    public GameObject itemPrefab;

    public PurchaseableItem(int itemID, string itemName, Sprite itemIcon)
    {
        this.itemID = itemID;
        this.itemName = itemName;
        this.itemIcon = itemIcon;
    }

    public PurchaseableItem(int itemID, string itemName, Sprite itemIcon, GameObject itemPrefab)
    {
        this.itemID = itemID;
        this.itemName = itemName;
        this.itemIcon = itemIcon;
        this.itemPrefab = itemPrefab;
    }
}
