using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatalogManager : MonoBehaviour
{
    [SerializeField] private ShopSelection shopSelection;
    [SerializeField] private ShopReceipt shopReceipt;
    [SerializeField] private ItemTest[] items;
    [SerializeField] private ItemOption[] itemOptions;
    private int itemOptionIndex;

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
        //foreach (ItemTest item in items)
        //    print(item.name);
        ResetItemOptions();
        shopSelection.Reset();
        foreach (ItemTest item in items)
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

    public void BuyBtnHit()
    {
        shopSelection.UpdateSelection();
        ItemInventory.Save(shopReceipt.BuyItems());
        UpdateItemOptions();
    }

    public void Awake()
    {
        //items = ItemInventory.Load();
        //List<ItemOption> itemOptions = new(stageItems.AddRange(instrumentItems));
        //new List<ItemOption>(stageItems.Count + instrumentItems.Count);


        //foreach (ItemOption itemOption in stageItems)
        //{
        //    if (savedItemNames.Contains(itemOption.item.name))
        //        itemOption.BuyItem();
        //}
        //foreach (ItemOption itemOption in instrumentItems)
        //{
        //    if (savedItemNames.Contains(itemOption.item.name))
        //        itemOption.BuyItem();
        //}
        //foreach (ItemOption itemOption in crowdItems)
        //{
        //    if (savedItemNames.Contains(itemOption.item.name))
        //        itemOption.BuyItem();
        //}
    }

    public void ResetItems()
    {
        //ItemInventory.ResetInventory();
        //foreach (ItemOption itemOption in stageItems)
        //    itemOption.ResetItem();
        //foreach (ItemOption itemOption in instrumentItems)
        //    itemOption.ResetItem();
    }
}
