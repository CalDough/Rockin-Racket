using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/*
    This script is a test UI script for the Shop Scene.
*/

public class ShopUI : ScrollSelector<Item>
{
    [SerializeField] public List<ItemData> SetItems;

    public Item SelectedItem;
    public int currentIndex;
    public TextMeshProUGUI SelectedItemInfoBox;
    public InventoryUI inventoryUI;

    public void Start()
    {
        foreach(ItemData i in SetItems)
        {
            Items.Add(new Item(i,999));
        }
        CreateButtons();
    }

    public override void OnButtonClick(int index)
    {
        //Debug.Log("Clicked " + index);
        if(index >= 0 && index < Items.Count)
        {
            //Debug.Log("Opening " + index);
            SelectedItem = Items[index];
           
            DisplayItemInfo(SelectedItem);
        }
    }




    public void DisplayItemInfo(Item item)
    {
        string itemInfo = "";

        itemInfo += "Name: " + item.ItemName + "\n";
        itemInfo += "Description: " + item.Description + "\n";
        itemInfo += "Value: " + item.Value + "\n";
        itemInfo += "Amount: " + item.Amount + "\n";
        itemInfo += "Durability: " + item.Durability + "\n";
        itemInfo += "Mood Bonus: " + item.MoodBonus + "\n";

        // Display each Item Type
        foreach(Attribute type in item.ItemTypes)
        {
            itemInfo += "Type: " + type + "\n";
        }

        itemInfo += "Is Key Item: " + (item.IsKeyItem ? "Yes" : "No") + "\n";
        itemInfo += "Is Consumable: " + (item.IsConsumable ? "Yes" : "No") + "\n";

        // Display each Skill Bonus
        foreach(Skill skill in item.SkillBonus)
    {
        itemInfo += skill.SkillName + " +" + skill.Level + "\n";
    }

        SelectedItemInfoBox.text = itemInfo;
    }

}
