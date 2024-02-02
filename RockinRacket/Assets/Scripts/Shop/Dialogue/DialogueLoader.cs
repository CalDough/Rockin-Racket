using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;
using UnityEditor;

public class DialogueLoader
{
    private static string folderPath = "Assets/Dialogue (Ken)/InkFiles/";

    public static (string, int) GetDialogueFile(string characterName)
    {
        // RR [NAME] Convos
        string filePath = folderPath + "RR " + characterName + " Convos.json";
        int hub = GameSaver.GetCurrentHub();
        if (!File.Exists(filePath))
        {
            Debug.Log("File at " + filePath + " not found");
            return (null, hub);
        }
        (string, int) result = (filePath, hub);
        return result;
    }
}