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

    public static void Save(ItemTest[] newItems)
    {
        Directory.CreateDirectory(saveFolderPath);

        string filePath = saveFolderPath + saveFileName;

        if (!File.Exists(filePath))
            File.WriteAllText(filePath, "");
        
        List<string> itemStrings = new();
        foreach (ItemTest newItem in newItems)
            items.Add(newItem);
        foreach (ItemTest item in items)
            itemStrings.Add(item.name);
        File.WriteAllLines(filePath, itemStrings);

        Debug.Log($"Inventory saved successfully. {items.Count} items saved.");
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