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

    public void DisplayItem(ItemTest item, int index, bool forSale, bool isInCart, bool equipped)
    {
        itemOptions[index].SetItem(item, isInCart, forSale, equipped);
    }

    public void ResetItemOptions()
    {
        foreach (ItemOption itemOption in itemOptions)
            itemOption.Show(false);
    }
    // called from CatalogManager when bookmark clicked
    public void DisplayItemsByCategory(ItemTest[] completeListOfItems, ItemTest.ItemType itemType, ShopReceipt shopReceipt)
    {
        int index = 0;
        foreach (ItemTest item in completeListOfItems)
            if (item.itemType == itemType)
            {
                if (index < itemOptions.Length)
                {
                    DisplayItem(item, index, shopReceipt.IsInCart(item), !ItemInventory.ContainsItem(item), ItemInventory.IsEquipped(item));
                    index++;
                }
                else
                    Debug.Log("You have more items than can be displayed for this category");
            }
    }

    // called from CatalogManager when items bought or added to cart to update the catalog options visually
    public void UpdateItemOptions(ShopReceipt shopReceipt)
    {
        foreach (ItemOption itemOption in itemOptions)
            itemOption.UpdateOption(shopReceipt.IsInCart(itemOption.item), !ItemInventory.ContainsItem(itemOption.item), ItemInventory.IsEquipped(itemOption.item));
    }
}
