using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System;

/*
    This script is a singleton which manages data that is very general or unfit for any specialized script.
    
*/

public class GameManager : MonoBehaviour
{
    
    [Header("Global Variables Settings")]
    public int globalFame; //Player Score
    public int globalMoney; //Currency for shops, items
    public int praise; //Multiplies fame and money potentials for next level
    public int attention; //Multiplies number of potential attendees
    public DifficultyMode SetMode = DifficultyMode.Normal;
    public float difficultyModifier;

    [Header("All Items")]
    public Item[] allItems;


    [Header("SaveFile Settings")]
    private string saveFolderPath = "Player/SaveFiles/";
    private string saveFileName = "GameData.json";

    [Header("Concert Related Variables")]
    public bool isPostIntermission = false;
    public float currentAudienceRating = 0;
    public int localMoney = 0;
    public int numMerchTableCustomers = 5;
    public SceneIndex currentLevel;


    public static GameManager Instance { get; private set; }

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

    private void Start()
    {
        ItemInventory.Initialize(allItems);
        LoadGame();
    }



    public void SaveGame()
    {
        if (!Directory.Exists(saveFolderPath))
        {
            Directory.CreateDirectory(saveFolderPath);
        }

        var gameData = new GameData
        {
            GlobalFame = this.globalFame,
            GlobalMoney = this.globalMoney,
            Praise = this.praise,
            Attention = this.attention,
            SetMode = this.SetMode
        };

        string jsonData = JsonUtility.ToJson(gameData, prettyPrint: true);
        
        File.WriteAllText(saveFolderPath + saveFileName, jsonData);

        Debug.Log("Game saved to " + saveFolderPath + saveFileName);
    }

    public void LoadGame()
    {
        string filePath = Path.Combine(saveFolderPath, saveFileName);

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            GameData loadedData = JsonUtility.FromJson<GameData>(jsonData);
            this.globalFame = loadedData.GlobalFame;
            this.globalMoney = loadedData.GlobalMoney;
            this.praise = loadedData.Praise;
            this.attention = loadedData.Attention;
            this.SetMode = loadedData.SetMode;
            
            SetDifficulty(this.SetMode);
            Debug.Log("Game loaded from " + filePath);
        }
        else
        {
            NewGame();
            Debug.Log("No saved game data found. Loading default game data.");
            // Here you can put your logic for initializing data when there's no saved file
        }
    }

    public void NewGame()
    {
        this.globalFame = 100;
        this.globalMoney = 100;
        this.praise = 1; 
        this.attention = 1;
        SetDifficulty(this.SetMode);
    }

    public void SetDifficulty(DifficultyMode mode)
    {
        switch (mode)
        {
            case DifficultyMode.VeryEasy:
                difficultyModifier = 0.33f;
                break;
            case DifficultyMode.Easy:
                difficultyModifier = 0.66f;
                break;
            case DifficultyMode.Normal:
                difficultyModifier = 1.0f;
                break;
            case DifficultyMode.Hard:
                difficultyModifier = 1.33f;
                break;
            case DifficultyMode.VeryHard:
                difficultyModifier = 1.66f;
                break;
            default:
                difficultyModifier = 1.0f; // Default to normal
                break;
        }

        Debug.Log("Difficulty set to " + mode.ToString() + ". Modifier is now " + difficultyModifier);
    }

    public void IncrementMoney(int amount)
    {
        globalMoney += amount;
    }


    [System.Serializable]
    private class GameData
    {
        public int GlobalFame;
        public int GlobalMoney;
        public int Praise;
        public int Attention;
        public DifficultyMode SetMode;
    }
}   


