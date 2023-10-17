using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;
using System.Linq;
using UnityEditor;

public static class ItemInventory
{
    // CHANGE TO THIS FOR FINAL BUILD
    // private string saveFolderPath = "Player/SaveFiles/";
    private static string saveFolderPath = "Assets/SaveFiles/";
    private static string saveFileName = "Items.txt";

    private static List<ItemTest> items = new();
    private static List<ItemTest> eqippedItems = new();

    public static void AddItem(ItemTest item) { items.Add(item); }
    public static void RemoveItem(ItemTest item) { items.Remove(item); }
    public static List<ItemTest> GetItems() { return items; }
    public static bool ContainsItem(ItemTest item) { return items.Contains(item); }
    private static Dictionary<ItemTest.MinigameType, GameObject> minigamesByType;


    public static GameObject GetMinigameByName(ItemTest.MinigameType type)
    {
        return minigamesByType[type];
    }

    // TODO only get EQUIPPED items
    public static void GetEquippedItems()
    {
        ItemTest.MinigameType[] minigameTypesIncludingNone = (ItemTest.MinigameType[])Enum.GetValues(typeof(ItemTest.MinigameType));
        ItemTest.MinigameType[] minigameTypes = (ItemTest.MinigameType[])minigameTypesIncludingNone.Skip(1);

        foreach (ItemTest item in items)
        {
            //if (item.Minigame_Type != ItemTest.MinigameType.NONE)
            //    minigamesByType.
            //else
            //    noneTypeMinigames.Add(item.MinigameObject);
        }
    }

    public static void Save()
    {
        Directory.CreateDirectory(saveFolderPath);

        string filePath = saveFolderPath + saveFileName;

        if (!File.Exists(filePath))
            File.WriteAllText(filePath, "");
        
        List<string> itemStrings = new();
        foreach (ItemTest item in items)
            itemStrings.Add(item.name);

        File.WriteAllLines(filePath, itemStrings);

        Debug.Log($"Inventory saved successfully. {items.Count} items saved.");
    }

    public static List<string> Load()
    {
        Directory.CreateDirectory(saveFolderPath);

        string filePath = saveFolderPath + saveFileName;

        List<string> itemStrings = new();
        itemStrings = new(File.ReadAllLines(filePath));

        Debug.Log($"Inventory loaded successfully. {itemStrings.Count} items loaded.");
        return itemStrings;
    }

    public static void ResetInventory()
    {
        string filePath = saveFolderPath + saveFileName;
        File.WriteAllText(filePath, "");
    }
}

//[System.Serializable]
//public class SerializableItemList
//{
//    public List<ItemTest> items;

//    public SerializableItemList(List<ItemTest> itemObjects)
//    {
//        //items = itemObjects;
//    }
//}
/*
    This script is a singleton.
    Inventory which manages the following:
    1. Checking for items
    2. Checking for items types
    3. Adding items to player inventory
    4. Selling items to NPC
    5. Removing items
    6. Getting list of all items based on type
    The other script InventorySaver will manage reloading/saving the inventory to help shorten this one.

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private List<Item> items;
    public List<Item> Items { get => items; set => items = value; }

    //Default Items
    public ItemData DefaultVocals;
    public ItemData DefaultStrings;
    public ItemData DefaultKeyboards;
    public ItemData DefaultPercussions;
    public ItemData DefaultBrass;
    public ItemData DefaultWoodwind;
    public ItemData DefaultRepair;
    public ItemData DefaultManagement;







    public static InventoryManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void LoadDefaultItems()
    {
        AddDefaultItem(DefaultVocals);
        AddDefaultItem(DefaultStrings);
        AddDefaultItem(DefaultKeyboards);
        AddDefaultItem(DefaultPercussions);
        AddDefaultItem(DefaultBrass);
        AddDefaultItem(DefaultWoodwind);
        AddDefaultItem(DefaultRepair);
        AddDefaultItem(DefaultManagement);
    }

    private void AddDefaultItem(ItemData itemData)
    {
        if (itemData != null)
        {
            AddItem(itemData);
        }
    }

    public bool HasItem(string itemName)
    {
        return items.Exists(item => item.ItemName == itemName);
    }

    public bool HasItems(List<string> itemNames)
    {
        bool hasAll = true;
        foreach (string itemName in itemNames)
        {
            if (HasItem(itemName) == false)
            {
                hasAll = false;
            }
        }
        return hasAll;
    }

    public bool HasItemType(Attribute itemType)
    {
        return items.Exists(item => item.ItemTypes.Contains(itemType));
    }

    public void AddItem(ItemData itemData, int amount = 1)
    {
        Item existingItem = items.Find(item => item.ID == itemData.ID);

        if (existingItem != null)
        {
            existingItem.Amount += amount;
        }
        else
        {
            items.Add(new Item(itemData, amount));
        }
        CleanUpInventory();
    }

    public void SellItem(int ID, int amount = 1)
    {
        Item item = items.Find(i => i.ID == ID);

        if (item != null)
        {
            int removedAmount = Mathf.Min(amount, item.Amount);
            // Add code to update the player's Global Money script here.
            GameManager.Instance.globalMoney += removedAmount;
            item.Amount -= removedAmount;
        }

        CleanUpInventory();
    }

    public void RemoveItem(string itemName, int amount = 1, bool removeKeyItem = false)
    {
        Item item = items.Find(i => i.ItemName == itemName && (removeKeyItem || !i.IsKeyItem));

        if (item != null)
        {
            int removedAmount = Mathf.Min(amount, item.Amount);
            item.Amount -= removedAmount;
        }

        CleanUpInventory();
    }

    public List<Item> GetItemsByType(Attribute itemType)
    {
        return items.FindAll(item => item.ItemTypes.Contains(itemType));
    }

    private void CleanUpInventory()
    {
        items.RemoveAll(item => item.Amount <= 0);
    }


}
*/