using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatalogManager : MonoBehaviour
{

    // TODO make a better organization where all items are in one list
    [SerializeField] private List<ItemOption> stageItems;
    [SerializeField] private List<ItemOption> instrumentItems;

    public void Awake()
    {
        List<string> savedItemNames = ItemInventory.Load();
        //List<ItemOption> itemOptions = new(stageItems.AddRange(instrumentItems));
        //new List<ItemOption>(stageItems.Count + instrumentItems.Count);


        foreach (ItemOption itemOption in stageItems)
        {
            if (savedItemNames.Contains(itemOption.item.name))
                itemOption.BuyItem();
        }
        foreach (ItemOption itemOption in instrumentItems)
        {
            if (savedItemNames.Contains(itemOption.item.name))
                itemOption.BuyItem();
        }
    }

    public void ResetItems()
    {
        ItemInventory.ResetInventory();
        foreach (ItemOption itemOption in stageItems)
            itemOption.ResetItem();
        foreach (ItemOption itemOption in instrumentItems)
            itemOption.ResetItem();
    }
}
