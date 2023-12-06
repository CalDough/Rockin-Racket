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
    private static ItemTest[] allItems;
    private static Dictionary<Bandmate, ItemTest> equippedItems = new();
    private static Dictionary<Bandmate, GameObject> minigamesByType;

    public static void AddItem(ItemTest item) { ownedItems.Add(item); }
    public static void AddItems(ItemTest[] items) { ownedItems.AddRange(items); }
    public static void RemoveItem(ItemTest item) { ownedItems.Remove(item); }
    public static List<ItemTest> GetItems() { return ownedItems; }
    public static bool ContainsItem(ItemTest item) { return ownedItems.Contains(item); }
    public static bool IsEquipped(ItemTest item) { return equippedItems.Values.Contains(item); }
    public static ItemTest[] GetAllItems() { return allItems; }

    private static readonly Dictionary<Bandmate, ItemTest[]> BandmateItems = new();
    // called by ShopCatalog
    public static ItemTest[] GetItemsByBandmate(Bandmate bandmate) { return BandmateItems[bandmate]; }

    // TODO: set to true to build with saving, false to build without saving
    private static readonly bool buildHasSaving = false;
    // TODO: ALSO SET IN GameSaver

    public static void Initialize(ItemTest[] completeListOfItems)
    {
        allItems = completeListOfItems;
        AddItems(Load());
        AddBandmateItems();
        AddEquippedItems();
    }

    public static GameObject GetMinigameByName(Bandmate type)
    {
        return minigamesByType[type];
    }

    // TODO only get EQUIPPED items
    public static void GetEquippedItems()
    {
        Bandmate[] minigameTypesIncludingNone = (Bandmate[])Enum.GetValues(typeof(Bandmate));
        Bandmate[] minigameTypes = (Bandmate[])minigameTypesIncludingNone.Skip(1);

        //foreach (ItemTest item in ownedItems)
        //{
        //    if (item.Minigame_Type != ItemTest.MinigameType.NONE)
        //        minigamesByType.
        //    else
        //        noneTypeMinigames.Add(item.MinigameObject);
        //}
    }

    public static void GetBandmateItems()
    {

    }

    public static void Save()
    {
        if (buildHasSaving)
        {
            Directory.CreateDirectory(saveFolderPath);

            string filePath = saveFolderPath + saveFileName;

            if (!File.Exists(filePath))
                File.WriteAllText(filePath, "");

            HashSet<string> itemStrings = new();
            //if (newItems != null)
            //    foreach (ItemTest newItem in newItems)
            //        ownedItems.Add(newItem);
            foreach (ItemTest item in ownedItems)
            {
                Debug.Log(item);
                itemStrings.Add(item.name);
            }
            
            File.WriteAllLines(filePath, itemStrings);

            Debug.Log($"Inventory saved {itemStrings.Count} items");
        }
    }

    public static void EquipItem(Bandmate bandmate, ItemTest item)
    {
        equippedItems[bandmate] = item;
    }

    private static void AddBandmateItems()
    {
        BandmateItems.Add(Bandmate.MJ, new ItemTest[4]);
        BandmateItems.Add(Bandmate.Kurt, new ItemTest[4]);
        BandmateItems.Add(Bandmate.Ace, new ItemTest[4]);
        BandmateItems.Add(Bandmate.Haley, new ItemTest[4]);
        //BandmateItems.Add(Bandmate.Harvey, new ItemTest[4]);

        foreach (ItemTest item in allItems)
            switch (item.itemType)
            {
                case Bandmate.MJ: BandmateItems[Bandmate.MJ][item.ShopIndex] = item; break;
                case Bandmate.Kurt: BandmateItems[Bandmate.Kurt][item.ShopIndex] = item; break;
                case Bandmate.Ace: BandmateItems[Bandmate.Ace][item.ShopIndex] = item; break;
                case Bandmate.Haley: BandmateItems[Bandmate.Haley][item.ShopIndex] = item; break;
                //case Bandmate.Harvey: BandmateItems[Bandmate.Harvey][item.ShopIndex] = item; break;
            }
    }

    private static void AddEquippedItems()
    {
        foreach (Bandmate bandmate in BandmateItems.Keys)
        {
            equippedItems[bandmate] = BandmateItems[bandmate][0];
            AddItem(BandmateItems[bandmate][0]);
        }
            
    }

    private static ItemTest[] Load()
    {
        if (buildHasSaving)
        {
            Directory.CreateDirectory(saveFolderPath);

            string filePath = saveFolderPath + saveFileName;

            List<string> itemNames = new(File.ReadAllLines(filePath));
            //foreach (string itemName in itemNames)
            //    Debug.Log(itemName);
            ItemTest[] loadedItems = new ItemTest[itemNames.Count];

            int successfullyLoaded = 0;
            for (int i = 0; i < itemNames.Count; i++)
                foreach (ItemTest item in allItems)
                    if (itemNames[i] == item.name)
                    {
                        //Debug.Log($"String: {itemNames[i]} || Item Name: {item.name}");
                        loadedItems[i] = item;
                        successfullyLoaded++;
                    }

            //foreach (string item in itemNames)
            //    Debug.Log(item);
            //foreach (ItemTest item in allItems)
            //    Debug.Log(item.name);

            Debug.Log($"Inventory loaded {successfullyLoaded} items loaded out of {itemNames.Count}");
            //foreach (ItemTest item in loadedItems)
            //    Debug.Log(item.name);
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