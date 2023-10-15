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

    [Header("Audience Mood Bars")]
    [SerializeField] private float hype = 0f;
    [SerializeField] private float maxHype = 1000f;
    [SerializeField] private float comfort = 0f;
    [SerializeField] private float maxComfort = 1000f;
    [SerializeField] private float hypeInterval = 1f; 
    [SerializeField] private float comfortInterval = 1f; 
    [SerializeField] private List<BandRoleAudioController> bandMembers;


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
    
    private IEnumerator HypeGeneration()
    {
        while (true)
        {
            yield return new WaitForSeconds(hypeInterval);

            if (GameStateManager.Instance.CurrentGameState.GameType == GameModeType.Song) 
            {
                foreach (BandRoleAudioController member in bandMembers)
                {
                    if (member.isPlaying && member.instrumentBrokenValue < 5)
                    {
                        hype += CalculateHypeContribution(member.instrumentBrokenValue, member.HypeGeneration);
                    }

                    if (member.isSinging && member.voiceBrokenValue < 5)
                    {
                        hype += CalculateHypeContribution(member.voiceBrokenValue, member.HypeGeneration);
                    }
                }

                if (hype > maxHype)
                {
                    hype = maxHype;
                }
            }
        }
    }

    private float CalculateHypeContribution(float brokenValue, float maxContribution)
    {
        float contributionPercentage = 1f - 0.3f * brokenValue;
        return maxContribution * contributionPercentage;
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

     
    public void HandleGameStateStart(object sender, GameStateEventArgs e)
    {
        //Debug.Log("State Started: " + e.stateType);
        switch(e.stateType)
        {
            case GameModeType.Song:
                break;
            default:
                break;
        }
    }
    
    private void HandleGameStateEnd(object sender, GameStateEventArgs e)
    {
        //Debug.Log("Game state ended: " + e.state.GameType);
        switch(e.stateType)
        {
            case GameModeType.Song:
                break;
            default:
                break;
        }
    }

    void Start()
    {
        GameEvents.OnEventStart += HandleEventStart;
        GameEvents.OnEventFail += HandleEventFail;
        GameEvents.OnEventCancel += HandleEventCancel;
        GameEvents.OnEventComplete += HandleEventComplete;
        GameEvents.OnEventMiss += HandleEventMiss;
        
        GameStateEvent.OnGameStateStart += HandleGameStateStart;
        GameStateEvent.OnGameStateEnd += HandleGameStateEnd;
    }

    void OnDestroy()
    {
        GameEvents.OnEventStart -= HandleEventStart;
        GameEvents.OnEventFail -= HandleEventFail;
        GameEvents.OnEventCancel -= HandleEventCancel;
        GameEvents.OnEventComplete -= HandleEventComplete;
        GameEvents.OnEventMiss -= HandleEventMiss;
        
        GameStateEvent.OnGameStateStart -= HandleGameStateStart;
        GameStateEvent.OnGameStateEnd -= HandleGameStateEnd;
    }

    public void IncreaseComfort(float amount)
    {
        comfort += amount;
        if (comfort > maxComfort)
        {
            comfort = maxComfort;
        }
    }

    public void DecreaseComfort(float amount)
    {
        comfort -= amount;
        if (comfort < 0)
        {
            comfort = 0;
        }
    }

    public void IncreaseHype(float amount)
    {
        hype += amount;
        if (hype > maxHype)
        {
            hype = maxHype;
        }
    }

    public void DecreaseHype(float amount)
    {
        hype -= amount;
        if (hype < 0)
        {
            hype = 0;
        }
    }

}