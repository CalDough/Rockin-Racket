using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Mathematics;
public class MinigameStatusManager : MonoBehaviour
{
    [field: SerializeField] public int totalDifficulty { get; private set; } = 0; 

    // Changing the mini-game lists to integer counters
    public int completedMiniGamesCount = 0;
    public int failedMiniGamesCount = 0;
    public int missedMiniGamesCount = 0;
    public int canceledMiniGamesCount = 0;

    [Header("Audience Mood Bars")]
    [SerializeField] public float hype = 0f;
    [SerializeField] public float maxHype = 1000f;

    [SerializeField] public float comfort = 500f;
    [SerializeField] public float maxComfort = 1000f;

    [SerializeField] public float hypeInterval = 1f; 
    [SerializeField] public float comfortInterval = 1f; 

    [SerializeField] public float comfortLossPerSecond = -15f;
    [SerializeField] public float comfortModifier = 1f;
    [SerializeField] public float hypeModifier = 1f;

    [SerializeField] private List<BandAudioController> bandMembers;

    
    [SerializeField] public MiniGame OpenedMiniGame;

    [Header("Active Scriptable Objects")] 
    [SerializeField] private MinigameContainer ImmuneMiniGames;

    public static MinigameStatusManager Instance { get; private set; }

    [Header("Test Variables")] 
    //Testing if we want to store the hype for each song and max hype they player could gain
    //based on song length * total # of active band members
    //then we can give the player a grade like 900/1000 hype is an A grade concert
    [SerializeField] public float PotentialHype;
    [SerializeField] public List<float> PotentialHypeFromAllSongs;
    [SerializeField] public List<float> HypeEarnedFromAllSongs;

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

    
    public void ResetVariables()
    {

        totalDifficulty = 0;

        completedMiniGamesCount = 0;
        failedMiniGamesCount = 0;
        missedMiniGamesCount = 0;
        canceledMiniGamesCount = 0;

        hype = 0f;
        comfort = 500f;

        PotentialHype = 0f;
        PotentialHypeFromAllSongs.Clear();
        HypeEarnedFromAllSongs.Clear();

        OpenedMiniGame = null;

        StopCoroutine(HypeGeneration());
        StopCoroutine(ComfortGeneration());
        bandMembers.Clear();
        ConcertAudioEvent.RequestBandPlayers();
        
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


    public void ReceiveBandMembers(object sender, ConcertAudioEventArgs e)
    {
        if(e.BandAudioPlayer != null)
        {
            bandMembers.Add(e.BandAudioPlayer);
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
                foreach (BandAudioController member in bandMembers)
                {
                    /*
                    if (member.isPlaying )
                    {
                        hype += CalculateHypeContribution(member.instrumentBrokenValue, member.HypeGeneration);
                    }
                    else if (member.isSinging)
                    {
                        hype += CalculateHypeContribution(member.voiceBrokenValue, member.HypeGeneration);
                    }
                    */
                    hype += CalculateHypeContribution(member.instrumentBrokenValue, member.HypeGeneration);
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
                ModifyComfort(comfortLossPerSecond * comfortModifier);
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
        Debug.Log("MinigameStatusManager: Event Started: " + e.EventObject);
    }

    public void HandleEventFail(object sender, GameEventArgs e)
    {
        
        if(e == null){return; }
        Debug.Log("MinigameStatusManager: Event Fail: " + e.EventObject);
        failedMiniGamesCount++;
        AddMinigameVariables(e.EventObject.hypePenalty, e.EventObject.hypePenalty);
    }

    public void HandleEventCancel(object sender, GameEventArgs e)
    {
        
        if(e == null){return; }
        Debug.Log("MinigameStatusManager: Event Cancelled: " + e.EventObject);
        canceledMiniGamesCount++;
    }

    public void HandleEventComplete(object sender, GameEventArgs e)
    {
        
        if(e == null){return; }
        Debug.Log("MinigameStatusManager: Event Completed: " + e.EventObject);
        completedMiniGamesCount++;
        AddMinigameVariables(e.EventObject.hypeBonus, e.EventObject.comfortBonus);
    }

    public void HandleEventMiss(object sender, GameEventArgs e)
    {
        if(e == null){return; }
        Debug.Log("MinigameStatusManager: Event Missed: " + e.EventObject);
        missedMiniGamesCount++;
    }

    public void HandleGameStateStart(object sender, GameStateEventArgs e)
    {
        //Debug.Log("State Started: " + e.stateType);
        switch(e.stateType)
        {
            case GameModeType.Song:
                hype = 0;
                this.PotentialHype = GameStateManager.Instance.CurrentGameState.Duration * 50;
                this.maxHype = PotentialHype;
                float maxHypePotential = PotentialHype;
                PotentialHypeFromAllSongs.Add(maxHypePotential);
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
                HypeEarnedFromAllSongs.Add(hype);
                hype = 0;
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

    public void ModifyComfort(float amount)
    {
        comfort = Mathf.Clamp(comfort + amount, 0, maxComfort);
    }

    public void ModifyHype(float amount)
    {
        hype = Mathf.Clamp(hype + amount, 0, maxHype);
    }

    public void AddMinigameVariables(float hypeChange, float comfortChange)
    {
        ModifyHype(hypeChange);
        ModifyComfort(comfortChange);
    }

}