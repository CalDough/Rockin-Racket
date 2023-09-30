using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;
using UnityEditor;

public class DialogueSaver
{
    // CHANGE TO THIS FOR FINAL BUILD
    //private string saveFolderPath = "Player/SaveFiles/";
    private static string saveFolderPath = "Assets/Scripts/Dialogue/";
    private static string saveFileName = "SavedDialogue.txt";

    private Dictionary<CharacterDialogueOptions.character, string[]> characterCurrentDialogueOptions;

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