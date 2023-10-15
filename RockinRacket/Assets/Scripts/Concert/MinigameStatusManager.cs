using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Mathematics;
public class MinigameStatusManager : MonoBehaviour
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

    [SerializeField] private float comfort = 500f;
    [SerializeField] private float maxComfort = 1000f;

    [SerializeField] private float hypeInterval = 1f; 
    [SerializeField] private float comfortInterval = 1f; 

    [SerializeField] private float comfortLossPerSecond = 10f;
    [SerializeField] private float comfortModifier = 1f;
    [SerializeField] private float hypeModifier = 1f;

    [SerializeField] private List<BandRoleAudioController> bandMembers;

    
    [SerializeField] public MiniGame OpenedMiniGame;

    [Header("Active Scriptable Objects")] 
    [SerializeField] private MinigameContainer ImmuneMiniGames;

    public static MinigameStatusManager Instance { get; private set; }

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

    public void ResetVariables()
    {
        hype = 0f;
        comfort = 500f;

        StopCoroutine(HypeGeneration());
        StopCoroutine(ComfortGeneration());
        bandMembers.Clear();
        ConcertAudioEvent.RequestBandPlayers();
        
    }

    public void ReceiveBandMembers(object sender, ConcertAudioEventArgs e)
    {
        if(e.BandRoleAudioPlayer != null)
        {
            bandMembers.Add(e.BandRoleAudioPlayer);
        }
    }
    

    public void CheckInventory()
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
                    if (member.isPlaying )
                    {
                        hype += CalculateHypeContribution(member.instrumentBrokenValue, member.HypeGeneration);
                    }
                    else if (member.isSinging)
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

    private IEnumerator ComfortGeneration()
    {
        while (true)
        {
            yield return new WaitForSeconds(comfortInterval);

            if (GameStateManager.Instance.CurrentGameState.GameType == GameModeType.Song) 
            {
                DecreaseComfort(comfortLossPerSecond * comfortModifier);
            }
        }
    }


    private float CalculateHypeContribution(float brokenValue, float maxContribution)
    {
        float contributionPercentage = 1f - 0.3f * brokenValue;
        return maxContribution * contributionPercentage * hypeModifier;
    }

    public void HandleEventStart(object sender, GameEventArgs e)
    {
        Debug.Log("Event Started: " + e.EventObject);
    }

    public void HandleEventFail(object sender, GameEventArgs e)
    {
        Debug.Log("Event Fail: " + e.EventObject);
        failedMiniGamesCount++;
        AddMinigameVariables(e.EventObject.hypePenalty, e.EventObject.hypePenalty);
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
        AddMinigameVariables(e.EventObject.hypeBonus, e.EventObject.comfortBonus);
    }

    public void HandleEventMiss(object sender, GameEventArgs e)
    {
        Debug.Log("Event Missed: " + e.EventObject);
        missedMiniGamesCount++;
    }

    public void AddMinigameVariables(float hypeChange, float comfortChange)
    {
        if(hypeChange > 0)
        {
            IncreaseHype(hypeChange);
        }
        else
        {
            DecreaseHype(hypeChange);
        }
        
        if(comfortChange > 0)
        {
            IncreaseComfort(comfortChange);
        }
        else
        {
            DecreaseComfort(comfortChange); 
        }
    }
    
    public void HandleGameStateStart(object sender, GameStateEventArgs e)
    {
        //Debug.Log("State Started: " + e.stateType);
        switch(e.stateType)
        {
            case GameModeType.Song:
                StartCoroutine(HypeGeneration());
                StartCoroutine(ComfortGeneration());
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
                StopCoroutine(HypeGeneration());
                StopCoroutine(ComfortGeneration());
                break;
            default:
                break;
        }
    }

    private void HandleEventOpen(object sender, GameEventArgs e)
    {
        if(e.EventObject != null)
        {
            this.OpenedMiniGame = e.EventObject;
        }
    }

    private void HandleEventClose(object sender, GameEventArgs e)
    {
        if( this.OpenedMiniGame = e.EventObject)
        {
            this.OpenedMiniGame = null;
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

        ConcertAudioEvent.OnSendBandPlayers += ReceiveBandMembers;
        GameEvents.OnEventOpen += HandleEventOpen;
        GameEvents.OnEventOpen += HandleEventClose;
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
        
        ConcertAudioEvent.OnSendBandPlayers -= ReceiveBandMembers;
        GameEvents.OnEventOpen -= HandleEventOpen;
        GameEvents.OnEventOpen -= HandleEventClose;
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