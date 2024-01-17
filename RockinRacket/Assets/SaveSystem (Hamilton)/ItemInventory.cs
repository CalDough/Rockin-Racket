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
    private static string equippedSaveFileName = "EquippedItems.txt";
    // TODO get complete list of items by path at runtime
    //private static string itemPath = "Items/";

    private static ItemTest[] allItems;
    private static List<ItemTest> ownedItems = new();
    private static Dictionary<Bandmate, ItemTest> equippedItem = new();
    //private static Dictionary<Bandmate, GameObject> minigameByType;

    public static void AddItem(ItemTest item) { ownedItems.Add(item); }
    public static void AddItems(ItemTest[] items) { ownedItems.AddRange(items); }
    public static void RemoveItem(ItemTest item) { ownedItems.Remove(item); }
    public static List<ItemTest> GetItems() { return ownedItems; }
    public static bool ContainsItem(ItemTest item) { return ownedItems.Contains(item); }
    public static bool IsEquipped(ItemTest item) { return equippedItem.Values.Contains(item); }
    public static ItemTest[] GetAllItems() { return allItems; }

    private static readonly Dictionary<Bandmate, ItemTest[]> BandmateItems = new();
    // called by ShopCatalog
    public static ItemTest[] GetItemsByBandmate(Bandmate bandmate) { return BandmateItems[bandmate]; }

    // TODO: set to true to build with saving, false to build without saving
    private static readonly bool buildHasSaving = true;
    // TODO: ALSO SET IN GameSaver

    public static void Initialize(ItemTest[] completeListOfItems)
    {
        allItems = completeListOfItems;
        AddItems(LoadItems());
        AddBandmateItems();
        LoadEquippedItems();
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

    //public static void GetBandmateItems()
    //{

    //}

    public static ItemTest GetBandmateMinigame(Bandmate bandmate)
    {
        return equippedItem[bandmate];
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
        foreach (ItemTest item in ownedItems)
        {
            //Debug.Log(item);
            itemStrings.Add(item.name);
        }

        File.WriteAllLines(filePath, itemStrings);
        return itemStrings.Count;
    }
    // called by Save
    private static int SaveEquippedItems()
    {
        string filePath = saveFolderPath + equippedSaveFileName;

        if (!File.Exists(filePath))
            File.WriteAllText(filePath, "");

        List<string> equippedItemStrings = new();
        foreach (ItemTest item in equippedItem.Values)
        {
            Debug.Log("equipped item: " + item.name);
            equippedItemStrings.Add("" + item.ShopIndex);
        }
        Debug.Log("equipped items: " + equippedItem.Values.Count);
        Debug.Log("equipped items to save: " + equippedItemStrings.Count);
        //itemStrings.Add("" + GetBandmateEquippedItem(Bandmate.MJ).ShopIndex);
        //itemStrings.Add("" + GetBandmateEquippedItem(Bandmate.Kurt).ShopIndex);
        //itemStrings.Add("" + GetBandmateEquippedItem(Bandmate.Ace).ShopIndex);
        //itemStrings.Add("" + GetBandmateEquippedItem(Bandmate.Haley).ShopIndex);

        File.WriteAllLines(filePath, equippedItemStrings);
        return equippedItemStrings.Count;
    }
    // called by CatalogManager when equip btn pressed
    public static void EquipItem(Bandmate bandmate, ItemTest item)
    {
        equippedItem[bandmate] = item;
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

    // TODO call LoadItems and Load EquippedItems
    private static void Load()
    {

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

    private static ItemTest[] LoadItems()
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
    // TODO reset equipped items
    public static void ResetInventory()
    {
        if (buildHasSaving)
        {
            Debug.Log("Inventory Cleared");
            string filePath = saveFolderPath + saveFileName;
            File.WriteAllText(filePath, "");
            ownedItems = new();
        }
    }
}