using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MinigameManager : MonoBehaviour
{
    [field: SerializeField] public int totalDifficulty { get; private set; } = 0; 

    // Changing the mini-game lists to integer counters
    private int completedMiniGamesCount = 0;
    private int failedMiniGamesCount = 0;
    private int missedMiniGamesCount = 0;
    private int canceledMiniGamesCount = 0;

    [Header("Active Scriptable Objects")] 
    [SerializeField] private MinigameContainer ImmuneMiniGames;

    public static MinigameManager Instance { get; private set; }

    // Singleton Code
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

    public bool IsMinigameAvailable(GameObject minigamePrefab)
    {
        MiniGame game = minigamePrefab.GetComponent<MiniGame>();

        if (game == null)
        {return true;} // If there's no Minigame component, then it's considered available

        foreach (GameObject immuneGamePrefab in ImmuneMiniGames.MiniGamesPrefabs)
        {
            MiniGame immuneGame = immuneGamePrefab.GetComponent<MiniGame>();
            if (immuneGame != null && immuneGame == game)
            {
                return false; // Found in immune list, so not available
            }
        }

        return true;
    }
    
    public bool IsMinigameAvailable(MiniGame minigamePrefab)
    {
        if (minigamePrefab == null)
        {return true;} // If there's no Minigame component, then it's considered available

        foreach (GameObject immuneGamePrefab in ImmuneMiniGames.MiniGamesPrefabs)
        {
            MiniGame immuneGame = immuneGamePrefab.GetComponent<MiniGame>();
            if (immuneGame != null && immuneGame == minigamePrefab)
            {
                return false; // Found in immune list, so not available
            }
        }

        return true;
    }


    private void CheckInventory()
    {
        //get a list of all games the player is immune to here
        //ImmuneMiniGames.MiniGamesPrefabs = ItemInventory.ItemMinigames();
    }
    
    public void HandleEventStart(object sender, GameEventArgs e)
    {
        Debug.Log("Event Started: " + e.EventObject);
    }

    public void HandleEventFail(object sender, GameEventArgs e)
    {
        Debug.Log("Event Fail: " + e.EventObject);
        failedMiniGamesCount++;
    }

    public void HandleEventCancel(object sender, GameEventArgs e)
    {
        Debug.Log("Event Cancelled: " + e.EventObject);
        canceledMiniGamesCount++;
    }

    public void HandleEventComplete(object sender, GameEventArgs e)
    {
        Debug.Log("Event Completed: " + e.EventObject);
        completedMiniGamesCount++;
    }

    public void HandleEventMiss(object sender, GameEventArgs e)
    {
        Debug.Log("Event Missed: " + e.EventObject);
        missedMiniGamesCount++;
    }

    void Start()
    {
        GameEvents.OnEventStart += HandleEventStart;
        GameEvents.OnEventFail += HandleEventFail;
        GameEvents.OnEventCancel += HandleEventCancel;
        GameEvents.OnEventComplete += HandleEventComplete;
        GameEvents.OnEventMiss += HandleEventMiss;
    }

    void OnDestroy()
    {
        GameEvents.OnEventStart -= HandleEventStart;
        GameEvents.OnEventFail -= HandleEventFail;
        GameEvents.OnEventCancel -= HandleEventCancel;
        GameEvents.OnEventComplete -= HandleEventComplete;
        GameEvents.OnEventMiss -= HandleEventMiss;
    }
}