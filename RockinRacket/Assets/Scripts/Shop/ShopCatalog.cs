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
    public void ResetItemOptions()
    {
        foreach (ItemOption itemOption in itemOptions)
            itemOption.Show(false);
    }
    // TODO REPLACE OLD FUNCTION
    public void DisplayItemsByCategory(ItemTest[] completeListOfItems, Bandmate itemType, ShopReceipt shopReceipt)
    {
        int index = 0;
        foreach (ItemTest item in completeListOfItems)
            if (item.itemType == itemType)
            {
                if (index < itemOptions.Length)
                {
                    DisplayItemOld(item, index, shopReceipt.IsInCart(item), !ItemInventory.ContainsItem(item), ItemInventory.IsEquipped(item));
                    index++;
                }
                else
                    Debug.Log("You have more items than can be displayed for this category");
            }
    }

    // called from CatalogManager when bookmark pressed
    public void DisplayItemsByBandmate(ItemTest[] items, ShopReceipt shopReceipt)
    {
        foreach (ItemTest item in items)
            DisplayItem(item, shopReceipt.IsInCart(item), !ItemInventory.ContainsItem(item), ItemInventory.IsEquipped(item));
    }

    // called from CatalogManager when items bought or added to cart to update the catalog options visually
    public void UpdateItemOptions(ShopReceipt shopReceipt)
    {
        foreach (ItemOption itemOption in itemOptions)
            itemOption.UpdateOption(shopReceipt.IsInCart(itemOption.item), !ItemInventory.ContainsItem(itemOption.item), ItemInventory.IsEquipped(itemOption.item));
    }

    // TODO REPLACE OLD FUNCTION
    private void DisplayItemOld(ItemTest item, int index, bool forSale, bool isInCart, bool equipped)
    {
        itemOptions[index].SetItem(item, isInCart, forSale, equipped);
    }

    // called by DisplayItemsByBandmate
    private void DisplayItem(ItemTest item, bool forSale, bool isInCart, bool equipped)
    {
        itemOptions[item.ShopIndex].SetItem(item, isInCart, forSale, equipped);
    }
}
