using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatalogManager : MonoBehaviour
{
    // TODO make a better organization where all items are in one list
    [SerializeField] private ItemTest[] items;
    [SerializeField] private ItemOption[] itemOptions;
    private int itemOptionIndex;

    private void resetItemoptions()
    {
        itemOptionIndex = 0;
        foreach (ItemOption itemOption in itemOptions)
            itemOption.Show(false);
    }

    public void DisplayItemsByCategory(ItemTest.ItemType itemType)
    {
        resetItemoptions();
        foreach (ItemTest item in items)
            if (item.itemType == itemType)
                DisplayItem(item);
    }

    public void DisplayItem(ItemTest item)
    {
        if (itemOptionIndex < itemOptions.Length)
        {
            itemOptions[itemOptionIndex].SetItem(item);
            itemOptionIndex++;
        }
        else
            Debug.Log("You have more items than can be displayed for this category");
    }

    public void Awake()
    {
        List<string> savedItemNames = ItemInventory.Load();
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
