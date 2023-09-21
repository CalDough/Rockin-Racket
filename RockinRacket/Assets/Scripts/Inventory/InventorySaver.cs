using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
/*
    This script is a singleton, see InventoryManager for more clarification on items.
*/
public class InventorySaver : MonoBehaviour
{
    [SerializeField] private InventoryManager inventory;

    private string saveFolderPath = "Player/SaveFiles/";
    private string saveFileName = "InventoryData.json";

    //making this a singleton just cause, but this class would probably be a static?
    public static InventorySaver Instance { get; private set; }

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

    public void SaveInventory()
    {
        if (!Directory.Exists(saveFolderPath))
        {
            Directory.CreateDirectory(saveFolderPath);
        }

        List<Item> itemsToSave = inventory.Items;
        string json = JsonUtility.ToJson(new SerializableItemList(itemsToSave), prettyPrint: true);

        File.WriteAllText(saveFolderPath + saveFileName, json);

        Debug.Log($"Inventory saved successfully. {itemsToSave.Count} items saved.");
    }

    public void LoadInventory()
    {
        string filePath = saveFolderPath + saveFileName;

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            SerializableItemList serializableItemList = JsonUtility.FromJson<SerializableItemList>(json);

            //inventory.Items = serializableItemList.items;

            // Load sprites for each item
            foreach (Item item in inventory.Items)
            {
                item.LoadSprite();
            }

            Debug.Log($"Inventory loaded successfully. {inventory.Items.Count} items loaded.");
        }
        else
        {
            Debug.Log("No saved inventory found. Starting with an empty inventory.");
            inventory.Items = new List<Item>();
            
            InventoryManager.Instance.LoadDefaultItems();
        }
    }
}

[System.Serializable]
public class SerializableItemList
{
    public List<Item> items;

    public SerializableItemList(List<Item> itemObjects)
    {
        items = itemObjects;
    }
}
