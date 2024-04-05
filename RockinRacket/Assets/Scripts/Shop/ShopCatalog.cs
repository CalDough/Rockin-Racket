using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    displays items with ItemOptions
    updates ItemOptions
    Resets ItemOptions
*/

public class ShopCatalog : MonoBehaviour
{
    [SerializeField] private ItemOption[] itemOptions;

    // called from CatalogManager when bookmark pressed
    public void DisplayItemsByBandmate(Item[] items, ShopReceipt shopReceipt)
    {
        foreach (ItemOption itemOption in itemOptions)
            itemOption.Show(false);
        foreach (Item item in items)
            DisplayItem(item, !ItemInventory.ContainsItem(item), shopReceipt.IsInCart(item), ItemInventory.IsEquipped(item));
    }

    // called from CatalogManager when items bought or added to cart to update the catalog options visually
    public void UpdateItemOptions(ShopReceipt shopReceipt)
    {
        foreach (ItemOption itemOption in itemOptions)
        {
            //Debug.Log($"item: {itemOption.GetItem()} || equipped: {ItemInventory.IsEquipped(itemOption.GetItem())}");
            itemOption.UpdateItem(!ItemInventory.ContainsItem(itemOption.GetItem()), shopReceipt.IsInCart(itemOption.GetItem()), ItemInventory.IsEquipped(itemOption.GetItem()));
        }
    }

    // called by DisplayItemsByBandmate
    private void DisplayItem(Item item, bool forSale, bool isInCart, bool equipped)
    {
        itemOptions[item.ShopIndex].SetItem(item, forSale, isInCart, equipped);
    }
}