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
    // TODO get complete list of items by path at runtime
    //private static string itemPath = "Items/";

    private static List<ItemTest> ownedItems = new();
    private static List<ItemTest> equippedItems = new();
    private static ItemTest[] allItems;
    private static Dictionary<ItemTest.MinigameType, GameObject> minigamesByType;

    public static void AddItem(ItemTest item) { ownedItems.Add(item); }
    public static void AddItems(ItemTest[] items) { ownedItems.AddRange(items); }
    public static void RemoveItem(ItemTest item) { ownedItems.Remove(item); }
    public static List<ItemTest> GetItems() { return ownedItems; }
    public static bool ContainsItem(ItemTest item) { return ownedItems.Contains(item); }
    public static bool IsEquipped(ItemTest item) { return equippedItems.Contains(item); }
    public static ItemTest[] GetAllItems() { return allItems; }

    // TODO: set to true to build with saving, false to build without saving
    private static readonly bool buildHasSaving = false;
    // TODO: ALSO SET IN GameSaver

    public static void Initialize(ItemTest[] completeListOfItems)
    {
        allItems = completeListOfItems;
        AddItems(Load());
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
        if (buildHasSaving)
        {
            Directory.CreateDirectory(saveFolderPath);

            string filePath = saveFolderPath + saveFileName;

            if (!File.Exists(filePath))
                File.WriteAllText(filePath, "");

            List<string> itemStrings = new();
            if (newItems != null)
                foreach (ItemTest newItem in newItems)
                    ownedItems.Add(newItem);
            foreach (ItemTest item in ownedItems)
                itemStrings.Add(item.name);
            File.WriteAllLines(filePath, itemStrings);

            Debug.Log($"Inventory saved {itemStrings.Count} items");
        }
    }

    private static ItemTest[] Load()
    {
        if (buildHasSaving)
        {
            Directory.CreateDirectory(saveFolderPath);

            string filePath = saveFolderPath + saveFileName;

            List<string> itemNames = new(File.ReadAllLines(filePath));
            ItemTest[] loadedItems = new ItemTest[itemNames.Count];

            int successfullyLoaded = 0;
            for (int i = 0; i < itemNames.Count; i++)
                foreach (ItemTest item in allItems)
                    if (itemNames[i] == item.name)
                    {
                        loadedItems[i] = allItems[i];
                        successfullyLoaded++;
                    }

            //foreach (string item in itemNames)
            //    Debug.Log(item);
            //foreach (ItemTest item in allItems)
            //    Debug.Log(item.name);

            Debug.Log($"Inventory loaded {successfullyLoaded} items loaded out of {itemNames.Count}");
            return loadedItems;
        }
        return new ItemTest[0];
    }

    public static void ResetInventory()
    {
        if (buildHasSaving)
        {
            string filePath = saveFolderPath + saveFileName;
            File.WriteAllText(filePath, "");
            ownedItems = new();
        }
    }
}