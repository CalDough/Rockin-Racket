using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Events;
using Newtonsoft.Json;


/*
    This script is a singleton which manages data that is very general or unfit for any specialized script.
    
*/

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    
    [Header("Global Variables Settings")]
    public int globalMoney; //Currency for shops, items
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
    public bool hasStartedGame;

    [Header("Concert Score Data")]
    public List<ConcertResultData> concertResultsList = new List<ConcertResultData>();

    [Header("SaveFile Settings")]
    private string saveFolderPath = "Player/SaveFiles/";
    private string saveFileName = "GameData.json";
    private string concertResultSaveFileName = "ConcertData.json";

    [Header("Current Concert Data")]
    public ConcertData currentConcertData;

    [Header("Events")]
    public UnityEvent e_updateBoardTextOnGameLoad;

    [Header("DEBUG MODE")]
    public bool isInDebugMode;
    public bool isMinigameOpen = false;

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
        
        if (isInDebugMode)
        {
            //NewGame();
        }
        else
        {
            LoadGame();
        }
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
            GlobalMoney = this.globalMoney,
            SetMode = this.SetMode,
            CompletedTutorial = this.CompletedTutorial,
            CompletedLevelOne = this.CompletedLevelOne,
            CompletedLevelTwo = this.CompletedLevelTwo,
            CompletedLevelThree = this.CompletedLevelThree,
            CompletedLevelFour = this.CompletedLevelFour,
            hasStartedGame = this.hasStartedGame
        };

        string jsonData = JsonUtility.ToJson(gameData, prettyPrint: true);
        
        File.WriteAllText(saveFolderPath + saveFileName, jsonData);

        SaveConcertResults(concertResultsList, concertResultSaveFileName);

        Debug.Log("Game saved to " + saveFolderPath + saveFileName);
    }

    /*
    * This method specifically saves concert results data to a separate file
    * 
    */
    private void SaveConcertResults(List<ConcertResultData> concertResults, string filename)
    {
        string jsonData = JsonConvert.SerializeObject(concertResults);
        string filePath = Path.Combine(saveFolderPath, filename);
        File.WriteAllText(filePath, jsonData);
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
            this.globalMoney = loadedData.GlobalMoney;
            this.SetMode = loadedData.SetMode;
            this.CompletedTutorial = loadedData.CompletedTutorial;
            this.CompletedLevelOne = loadedData.CompletedLevelOne;
            this.CompletedLevelTwo = loadedData.CompletedLevelTwo;
            this.CompletedLevelThree = loadedData.CompletedLevelThree;
            this.CompletedLevelFour = loadedData.CompletedLevelFour;
            this.hasStartedGame = true;
            
            SetDifficulty(this.SetMode);

            concertResultsList = LoadConcertResultsData(concertResultSaveFileName);

            Debug.Log("Game loaded from " + filePath);
        }
        else
        {
            NewGame();
            Debug.Log("No saved game data found. Loading default game data.");
        }
    }

    /*
    * This method specifically loads the concert results data
    * 
    */
    public List<ConcertResultData> LoadConcertResultsData(string fileName)
    {
        string filePath = Path.Combine(saveFolderPath, fileName);

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<ConcertResultData>>(jsonData);
        }
        else
        {
            Debug.LogError($"File not found at location {filePath}");
            return null;
        }
    }

    /*
     * This method loads the default state for variables in game manager when the player selects "New Game"
     * 
     * Comments added by Xander... if they are wrong please correct them
     */
    public void NewGame()
    {
        Debug.Log("new GameData");
        this.globalMoney = 100;
        SetDifficulty(this.SetMode);
        CompletedTutorial = false;
        CompletedLevelOne = false;
        CompletedLevelTwo = false;
        CompletedLevelThree = false;
        CompletedLevelFour = false;
        hasStartedGame = true;
        

        ConcertResultData tutorialResults = new ConcertResultData('A', 0, 0);
        concertResultsList.Add(tutorialResults);
        ConcertResultData levelOneResults = new ConcertResultData('X', 0, 0);
        concertResultsList.Add(levelOneResults);
        ConcertResultData levelTwoResults = new ConcertResultData('X', 0, 0);
        concertResultsList.Add(levelTwoResults);
        ConcertResultData levelThreeResults = new ConcertResultData('X', 0, 0);
        concertResultsList.Add(levelThreeResults);
        ConcertResultData levelFourResults = new ConcertResultData('X', 0, 0);
        concertResultsList.Add(levelFourResults);
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

        IncrementMoney(profit);

        e_updateBoardTextOnGameLoad.Invoke();
    }

    // called by shopManager on start
    public int GetHub()
    {
        int result = 0;
        if (CompletedLevelOne)
            result++;
        if (CompletedLevelTwo)
            result++;
        if (CompletedLevelThree)
            result++;
        if (CompletedLevelFour)
            result++;
        return result;
    }

    /*
     *  This method is called when transitioning back to the concert from the final level
     */
    public void SetIntermissionStateOverride()
    {
        currentConcertData.isPostIntermission = true;
    }


    [System.Serializable]
    private class GameData
    {
        public int GlobalMoney;
        public DifficultyMode SetMode;
        public bool CompletedTutorial;
        public bool CompletedLevelOne;
        public bool CompletedLevelTwo;
        public bool CompletedLevelThree;
        public bool CompletedLevelFour;
        public bool hasStartedGame;
    }
}

// The following class is used to score concert score data for each concert

[System.Serializable]
public class ConcertResultData
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




