using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;
using UnityEditor;

public class GameSaver
{
    // CHANGE TO THIS FOR FINAL BUILD
    //private static string saveFolderPath = "Player/SaveFiles/";
    private static string saveFolderPath = "Assets/SaveFiles/";
    private static string saveFileName = "GameInfo.txt";

    private static int Hub { get; set; }
    private static int Fame { get; set; }
    private static int Money { get; set; }

    public static void Save()
    {
        Directory.CreateDirectory(saveFolderPath);

        string filePath = saveFolderPath + saveFileName;

        if (!File.Exists(filePath))
            File.WriteAllText(filePath, "");


        File.WriteAllLines(filePath, LinesToSave());

        Debug.Log($"Game stats saved successfully.");
    }

    public static void Load()
    {
        Directory.CreateDirectory(saveFolderPath);

        string filePath = saveFolderPath + saveFileName;

        LoadLines(File.ReadAllLines(filePath));

        Debug.Log($"Game stats loaded successfully:");
    }
    private static string[] LinesToSave()
    {
        string[] linesToSave = { Hub.ToString(), Fame.ToString(), Money.ToString() };
        return linesToSave;
    }
    private static void LoadLines(string[] lines)
    {
        Hub = Int32.Parse(lines[0]);
        Fame = Int32.Parse(lines[1]);
        Money = Int32.Parse(lines[2]);
    }
    public static void SaveStats(int hub, int fame, int money)
    {
        Hub = hub;
        Fame = fame;
        Money = money;
    }
    public static void PrintStats()
    {
        Debug.Log("Hub: " + Hub);
        Debug.Log("Fame: " + Fame);
        Debug.Log("Money: " + Money);
    }
    public static int GetCurrentHub()
    {
        return Hub;
    }
    public static void NewGame()
    {
        Hub = 0;
        Fame = 0;
        Money = 0;
    }
}
