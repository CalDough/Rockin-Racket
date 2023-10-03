using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;
using UnityEditor;

public class StorySaver
{
    // CHANGE TO THIS FOR FINAL BUILD
    //private static string saveFolderPath = "Player/SaveFiles/";
    private static string saveFolderPath = "Assets/Dialogue/";
    private static string saveFileName = "SavedDialogue.txt";
    private string[] characters = { "Haley", "Ace", "MJ", "Kurt", "Jay" };
    // RR [NAME] Convos
    //private Dictionary<characters> characterCurrentDialogueOptions;

    private static List<string> dialogueStrings;

    public static void Save()
    {
        Directory.CreateDirectory(saveFolderPath);

        string filePath = saveFolderPath + saveFileName;

        if (!File.Exists(filePath))
            File.WriteAllText(filePath, "");

        File.WriteAllLines(filePath, dialogueStrings);

        Debug.Log($"Dialogue saved successfully. {dialogueStrings.Count} items saved.");
    }

    public static List<string> Load()
    {
        Directory.CreateDirectory(saveFolderPath);

        string filePath = saveFolderPath + saveFileName;

        List<string> dialogueStrings = new();
        dialogueStrings = new(File.ReadAllLines(filePath));

        Debug.Log($"Inventory loaded successfully. {dialogueStrings.Count} items loaded.");
        return dialogueStrings;
    }

    public static void ResetInventory()
    {
        string filePath = saveFolderPath + saveFileName;
        File.WriteAllText(filePath, "");
    }
}