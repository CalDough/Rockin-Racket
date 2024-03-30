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
    private static readonly string saveFolderPath = "Player/SaveFiles/";
    private static readonly string saveFileName = "OwnedItems.txt";
    private static readonly string equippedSaveFileName = "EquippedItems.txt";
    // TODO get complete list of items by path at runtime
    //private static string itemPath = "Items/";

    private static Item[] allItems;
    private static List<Item> ownedItems = new();
    private static Dictionary<Bandmate, Item> equippedItem = new();
    //private static Dictionary<Bandmate, GameObject> minigameByType;

    public static void AddItem(Item item) { ownedItems.Add(item); }
    public static void AddItems(Item[] items) { ownedItems.AddRange(items); }
    public static void RemoveItem(Item item) { ownedItems.Remove(item); }
    public static List<Item> GetItems() { return ownedItems; }
    public static bool ContainsItem(Item item) { return ownedItems.Contains(item); }
    public static bool IsEquipped(Item item) { return equippedItem.Values.Contains(item); }
    public static Item[] GetAllItems() { return allItems; }

    private static readonly Dictionary<Bandmate, Item[]> BandmateItems = new();
    // called by ShopCatalog
    public static Item[] GetItemsByBandmate(Bandmate bandmate) { return BandmateItems[bandmate]; }

    // TODO: set to true to build with saving, false to build without saving
    private static readonly bool buildHasSaving = true;
    // TODO: ALSO SET IN GameSaver

    public static void Initialize(Item[] completeListOfItems)
    {
        allItems = completeListOfItems;
        AddBandmateItems();
        Load();
    }

    //public static GameObject GetMinigameByName(Bandmate type)
    //{
    //    return minigameByType[type];
    //}

    //public static ItemTest GetEquippedItems()
    //{
    //    //Bandmate[] minigameTypesIncludingNone = (Bandmate[])Enum.GetValues(typeof(Bandmate));
    //    //Bandmate[] minigameTypes = (Bandmate[])minigameTypesIncludingNone.Skip(1);

    //    //foreach (ItemTest item in ownedItems)
    //    //{
    //    //    if (item.Minigame_Type != ItemTest.MinigameType.NONE)
    //    //        minigamesByType.
    //    //    else
    //    //        noneTypeMinigames.Add(item.MinigameObject);
    //    //}
    //}

    public static Item GetBandmateEquippedItem(Bandmate bandmate)
    {
        return equippedItem[bandmate];
    }

    // Called to get the correct minigames for each bandmate at start of concert
    public static GameObject GetBandmateMinigame(Bandmate bandmate)
    {
        return equippedItem[bandmate].minigameObject;
    }

    public static void Save()
    {
        if (buildHasSaving)
        {
            Directory.CreateDirectory(saveFolderPath);

            int itemNum = SaveOwnedItems();
            int equippedItemNum = SaveEquippedItems();

            Debug.Log($"Inventory saved {itemNum} items, {equippedItemNum} equipped items");
        }
    }

    // called by Save
    private static int SaveOwnedItems()
    {
        string filePath = saveFolderPath + saveFileName;

        if (!File.Exists(filePath))
            File.WriteAllText(filePath, "");

        HashSet<string> itemStrings = new();
        foreach (Item item in ownedItems)
        {
            Debug.Log(item);
            Debug.Log(item.name);
            itemStrings.Add(item.name);
        }

        File.WriteAllLines(filePath, itemStrings);
        return itemStrings.Count;
    }
    // called by Save
    private static int SaveEquippedItems()
    {
        string filePath = saveFolderPath + equippedSaveFileName;

        //itemStrings.Add("" + GetBandmateEquippedItem(Bandmate.MJ).ShopIndex);
        //itemStrings.Add("" + GetBandmateEquippedItem(Bandmate.Kurt).ShopIndex);
        //itemStrings.Add("" + GetBandmateEquippedItem(Bandmate.Ace).ShopIndex);
        //itemStrings.Add("" + GetBandmateEquippedItem(Bandmate.Haley).ShopIndex);

        if (!File.Exists(filePath))
            File.WriteAllText(filePath, "");

        List<string> equippedItemStrings = new();
        foreach (Item item in equippedItem.Values)
        {
            Debug.Log("equipped item: " + item.name);
            equippedItemStrings.Add("" + item.ShopIndex);
        }
        Debug.Log("equipped items: " + equippedItem.Values.Count);
        Debug.Log("equipped items to save: " + equippedItemStrings.Count);

        File.WriteAllLines(filePath, equippedItemStrings);
        return equippedItemStrings.Count;
    }
    // called by CatalogManager when equip btn pressed
    public static void EquipItem(Bandmate bandmate, Item item)
    {
        equippedItem[bandmate] = item;
    }

    public static void Load()
    {
        AddItems(LoadItems());
        LoadEquippedItems();
    }

    private static void LoadEquippedItems()
    {
        foreach (Bandmate bandmate in BandmateItems.Keys)
        {
            equippedItem[bandmate] = BandmateItems[bandmate][0];
        }
        if (!File.Exists(saveFolderPath + equippedSaveFileName))
        {
            Debug.Log("FAILURE to load equipped items");
        }
        else
        {
            Directory.CreateDirectory(saveFolderPath);
            string filePath = saveFolderPath + equippedSaveFileName;
            string[] itemIndices = File.ReadAllLines(filePath);
            int index = 0;
            foreach (Bandmate bandmate in Enum.GetValues(typeof(Bandmate)))
            {
                equippedItem[bandmate] = BandmateItems[bandmate][Int32.Parse(itemIndices[index])];
                index++;
            }
        }
        Debug.Log("equipped Items on load: " + equippedItem.Values.Count);
    }

    private static Item[] LoadItems()
    {
        if (buildHasSaving)
        {
            Directory.CreateDirectory(saveFolderPath);

            string filePath = saveFolderPath + saveFileName;

            List<string> itemNames = new(File.ReadAllLines(filePath));
            //foreach (string itemName in itemNames)
            //    Debug.Log(itemName);
            Item[] loadedItems = new Item[itemNames.Count];

            int successfullyLoaded = 0;
            for (int i = 0; i < itemNames.Count; i++)
                foreach (Item item in allItems)
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
        return new Item[0];
    }

    public static void ResetInventory()
    {
        // owned items
        string filePath = saveFolderPath + saveFileName;
        string[] itemStrings = { "MJ_Basic", "Kurt_Basic", "Ace_Basic", "Haley_Basic" };
        File.WriteAllLines(filePath, itemStrings);
        LoadEquippedItems();

        // equipped items
        filePath = saveFolderPath + equippedSaveFileName;
        string[] equippedStrings = { "0", "0", "0", "0" };
        File.WriteAllLines(filePath, equippedStrings);
        LoadItems();

        Debug.Log("Item Inventory reset");
    }

    private static void AddBandmateItems()
    {
        BandmateItems.Add(Bandmate.MJ, new Item[3]);
        BandmateItems.Add(Bandmate.Kurt, new Item[3]);
        BandmateItems.Add(Bandmate.Ace, new Item[3]);
        BandmateItems.Add(Bandmate.Haley, new Item[3]);
        //BandmateItems.Add(Bandmate.Harvey, new ItemTest[4]);

        foreach (Item item in allItems)
            switch (item.itemType)
            {
                case Bandmate.MJ: BandmateItems[Bandmate.MJ][item.ShopIndex] = item; break;
                case Bandmate.Kurt: BandmateItems[Bandmate.Kurt][item.ShopIndex] = item; break;
                case Bandmate.Ace: BandmateItems[Bandmate.Ace][item.ShopIndex] = item; break;
                case Bandmate.Haley: BandmateItems[Bandmate.Haley][item.ShopIndex] = item; break;
                    //case Bandmate.Harvey: BandmateItems[Bandmate.Harvey][item.ShopIndex] = item; break;
            }
    }
}