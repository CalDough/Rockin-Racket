using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Events;

/*
    This script is a singleton which manages data that is very general or unfit for any specialized script.
    
*/

[DefaultExecutionOrder(-100)]
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

    [Header("Concert Status Settings")]
    public bool CompletedTutorial;
    public bool CompletedLevelOne;
    public bool CompletedLevelTwo;
    public bool CompletedLevelThree;
    public bool CompletedLevelFour;

    [Header("Concert Score Data")]
    public ConcertResultData[] concertResultsList; // This is a list of structs

    [Header("SaveFile Settings")]
    private string saveFolderPath = "Player/SaveFiles/";
    private string saveFileName = "GameData.json";

    [Header("Current Concert Data")]
    public ConcertData currentConcertData;

    [Header("Events")]
    public UnityEvent e_updateBoardTextOnGameLoad;


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

        if (e_updateBoardTextOnGameLoad == null)
        {
            e_updateBoardTextOnGameLoad = new UnityEvent();
        }
    }

    private void Start()
    {
        ItemInventory.Initialize(allItems);
        StickerSaver.LoadStickerData();
        LoadGame();
    }

    /*
     * This method is called to save all of the global stat variables in the game manager class
     * 
     * Comments added by Xander... if they are wrong please correct them
     * 
     */
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
            SetMode = this.SetMode,
            CompletedTutorial = this.CompletedTutorial,
            CompletedLevelOne = this.CompletedLevelOne,
            CompletedLevelTwo = this.CompletedLevelTwo,
            CompletedLevelThree = this.CompletedLevelThree,
            CompletedLevelFour = this.CompletedLevelFour
        };

        string jsonData = JsonUtility.ToJson(gameData, prettyPrint: true);
        
        File.WriteAllText(saveFolderPath + saveFileName, jsonData);

        Debug.Log("Game saved to " + saveFolderPath + saveFileName);
    }

    /*
    * This method is called from the start screen to load in the saved data from the file
    * 
    * Comments added by Xander... if they are wrong please correct them
    * 
    */
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
            this.CompletedTutorial = loadedData.CompletedTutorial;
            this.CompletedLevelOne = loadedData.CompletedLevelOne;
            this.CompletedLevelTwo = loadedData.CompletedLevelTwo;
            this.CompletedLevelThree = loadedData.CompletedLevelThree;
            this.CompletedLevelFour = loadedData.CompletedLevelFour;
            
            SetDifficulty(this.SetMode);
            Debug.Log("Game loaded from " + filePath);
        }
        else
        {
            NewGame();
            Debug.Log("No saved game data found. Loading default game data.");
        }
    }

    /*
     * This method loads the default state for variables in game manager when the player selects "New Game"
     * 
     * Comments added by Xander... if they are wrong please correct them
     */
    public void NewGame()
    {
        this.globalFame = 100;
        this.globalMoney = 100;
        this.praise = 1; 
        this.attention = 1;
        SetDifficulty(this.SetMode);
        CompletedTutorial = false;
        CompletedLevelOne = false;
        CompletedLevelTwo = false;
        CompletedLevelThree = false;
        CompletedLevelFour = false;

        ConcertResultData tutorialResults = new ConcertResultData('X', 0, 0);
        concertResultsList[0] = tutorialResults;
        ConcertResultData levelOneResults = new ConcertResultData('X', 0, 0);
        concertResultsList[1] = levelOneResults;
        ConcertResultData levelTwoResults = new ConcertResultData('X', 0, 0);
        concertResultsList[2] = levelTwoResults;
        ConcertResultData levelThreeResults = new ConcertResultData('X', 0, 0);
        concertResultsList[3] = levelThreeResults;
        ConcertResultData levelFourResults = new ConcertResultData('X', 0, 0);
        concertResultsList[4] = levelFourResults;
    }

    /*
     * This method may be for a feature we are no longer using....
     * 
     * Should it be deleted?
     * 
     * Comments added by Xander... if they are wrong please correct them
     */
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

    /*
     * The following method is called at the end of the level to update the level completion to true and save the relevant data
     */
    public void UpdateLevelCompletionStatus(Levels levelName, char letter, short score, short profit)
    {
        Debug.Log($"<color=green>The level {levelName.ToString()} has been marked for completion and sent to game manager");

        byte resultIndex = 0;

        switch(levelName)
        {
            case Levels.TUTORIAL:
                CompletedTutorial = true;
                resultIndex = 0;
                break;
            case Levels.LEVEL_ONE:
                CompletedLevelOne = true;
                resultIndex = 1;
                break;
            case Levels.LEVEL_TWO:
                CompletedLevelTwo = true;
                resultIndex = 2;
                break;
            case Levels.LEVEL_THREE:
                CompletedLevelThree = true;
                resultIndex = 3;
                break;
            case Levels.LEVEL_FOUR:
                CompletedLevelFour = true;
                resultIndex = 4;
                break;
            default:
                Debug.LogError("Congratulations... you somehow passed an enum that doesn't exist");
                break;
        }

        concertResultsList[resultIndex].gradeLetter = letter;
        concertResultsList[resultIndex].gradeScore = score;
        concertResultsList[resultIndex].profitAmount = profit;
    }


    [System.Serializable]
    private class GameData
    {
        public int GlobalFame;
        public int GlobalMoney;
        public int Praise;
        public int Attention;
        public DifficultyMode SetMode;
        public bool CompletedTutorial;
        public bool CompletedLevelOne;
        public bool CompletedLevelTwo;
        public bool CompletedLevelThree;
        public bool CompletedLevelFour;
    }

    // The following struct is used to score concert score data for each concert
    public struct ConcertResultData
    {
        public char gradeLetter;
        public short gradeScore;
        public short profitAmount;

        public ConcertResultData(char letter, short score, short profit)
        {
            this.gradeLetter = letter;
            this.gradeScore = score;
            this.profitAmount = profit;
        }
    }
}   


