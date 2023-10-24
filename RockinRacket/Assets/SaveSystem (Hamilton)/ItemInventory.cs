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
    private static string itemPath = "Items/";

    private static List<ItemTest> ownedItems = new();
    private static List<ItemTest> equippedItems = new();
    private static Dictionary<ItemTest.MinigameType, GameObject> minigamesByType;

    public static void AddItem(ItemTest item) { ownedItems.Add(item); }
    public static void AddItems(ItemTest[] items) { ownedItems.AddRange(items); }
    public static void RemoveItem(ItemTest item) { ownedItems.Remove(item); }
    public static List<ItemTest> GetItems() { return ownedItems; }
    public static bool ContainsItem(ItemTest item) { return ownedItems.Contains(item); }
    public static bool IsEquipped(ItemTest item) { return equippedItems.Contains(item); }

    public static void Initialize(ItemTest[] completeListOfItems)
    {
        ownedItems.AddRange(Load(completeListOfItems));
    }

    public static GameObject GetMinigameByName(ItemTest.MinigameType type)
    {
        return minigamesByType[type];
    }

    // TODO only get EQUIPPED items
    public static void GetEquippedItems()
    {
        ItemTest.MinigameType[] minigameTypesIncludingNone = (ItemTest.MinigameType[])Enum.GetValues(typeof(ItemTest.MinigameType));
        ItemTest.MinigameType[] minigameTypes = (ItemTest.MinigameType[])minigameTypesIncludingNone.Skip(1);

        foreach (ItemTest item in ownedItems)
        {
            //if (item.Minigame_Type != ItemTest.MinigameType.NONE)
            //    minigamesByType.
            //else
            //    noneTypeMinigames.Add(item.MinigameObject);
        }
    }

    public static void Save(ItemTest[] newItems)
    {
        Directory.CreateDirectory(saveFolderPath);

        string filePath = saveFolderPath + saveFileName;

        if (!File.Exists(filePath))
            File.WriteAllText(filePath, "");
        
        List<string> itemStrings = new();
        foreach (ItemTest newItem in newItems)
            ownedItems.Add(newItem);
        foreach (ItemTest item in ownedItems)
            itemStrings.Add(item.name);
        File.WriteAllLines(filePath, itemStrings);

        Debug.Log($"Inventory saved successfully. {ownedItems.Count} items saved.");
    }

    public static ItemTest[] Load(ItemTest[] completeListOfItems)
    {
        Directory.CreateDirectory(saveFolderPath);

        string filePath = saveFolderPath + saveFileName;

        List<string> itemNames = new(File.ReadAllLines(filePath));
        ItemTest[] loadedItems = new ItemTest[itemNames.Count];

        for (int i = 0; i < loadedItems.Length; i++)
            if (itemNames[i] == completeListOfItems[i].name)
            {
                loadedItems[i] = completeListOfItems[i];
            }
                
        Debug.Log($"Inventory loaded successfully. {itemNames.Count} items loaded.");
        return loadedItems;
    }

    public static void ResetInventory()
    {
        string filePath = saveFolderPath + saveFileName;
        File.WriteAllText(filePath, "");
    }
}