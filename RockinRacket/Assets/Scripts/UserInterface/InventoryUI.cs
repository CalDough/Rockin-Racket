using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/*
    This script is a test UI script for the Inventory Scene
*/
public class InventoryUI : ScrollSelector<Item>
{
    public Item SelectedItem;
    public int currentIndex;
    public ShopUI shopUI;

    public void Start()
    {
        Items = InventoryManager.Instance.Items;
        
        CreateButtons();
    }

    public override void OnButtonClick(int index)
    {
        //Debug.Log("Clicked " + index);
        if(index >= 0 && index < Items.Count)
        {
            //Debug.Log("Opening " + index);
            SelectedItem = Items[index];
            shopUI.DisplayItemInfo(SelectedItem);
        }
    }
}