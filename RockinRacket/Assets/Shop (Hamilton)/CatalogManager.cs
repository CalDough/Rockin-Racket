using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatalogManager : MonoBehaviour
{
    [SerializeField] private ShopSelection shopSelection;
    [SerializeField] private ShopReceipt shopReceipt;
    [SerializeField] private ItemTest[] completeListOfItems;
    [SerializeField] private ItemOption[] itemOptions;
    private int itemOptionIndex;

    public void Awake()
    {
        ItemInventory.Save(ItemInventory.Load(completeListOfItems));
    }

    private void ResetItemOptions()
    {
        itemOptionIndex = 0;
        foreach (ItemOption itemOption in itemOptions)
            itemOption.Show(false);
    }

    public void UpdateItemOptions()
    {
        foreach (ItemOption itemOption in itemOptions)
            itemOption.UpdateOption(!ItemInventory.ContainsItem(itemOption.item), shopReceipt.IsInCart(itemOption.item));
    }

    public void DisplayItemsByCategory(ItemTest.ItemType itemType)
    {
        ResetItemOptions();
        shopSelection.ResetSelection();
        foreach (ItemTest item in completeListOfItems)
            if (item.itemType == itemType)
                DisplayItem(item);
    }

    public void DisplayItem(ItemTest item)
    {
        if (itemOptionIndex < itemOptions.Length)
        {
            itemOptions[itemOptionIndex].SetItem(item, !ItemInventory.ContainsItem(item), shopReceipt.IsInCart(item));
            itemOptionIndex++;
        }
        else
            Debug.Log("You have more items than can be displayed for this category");
    }

    public void BuyBtnPressed()
    {
        shopSelection.ResetSelection();
        ItemInventory.Save(shopReceipt.GetItemsToBuy());
        shopReceipt.ResetReceipt();
        UpdateItemOptions();
    }
    public void CartBtnPressed()
    {
        shopSelection.AddSelectedToCart();
        UpdateItemOptions();
    }

    public void ResetBtnPressed()
    {
        ItemInventory.ResetInventory();
        shopSelection.ResetSelection();
        shopReceipt.ResetReceipt();
        UpdateItemOptions();
    }
}
