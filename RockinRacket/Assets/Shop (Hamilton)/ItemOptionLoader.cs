using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOptionLoader : MonoBehaviour
{
    [SerializeField]
    private List<ItemOption> shopItems;

    public void Awake()
    {
        List<string> savedItemNames = ItemInventory.Load();

        foreach (ItemOption itemOption in shopItems)
        {
            if (savedItemNames.Contains(itemOption.item.name))
                itemOption.BuyItem();
        }
    }

    public void ResetItems()
    {
        ItemInventory.ResetItems();
        foreach (ItemOption itemOption in shopItems)
            itemOption.ResetItem();
    }
}
