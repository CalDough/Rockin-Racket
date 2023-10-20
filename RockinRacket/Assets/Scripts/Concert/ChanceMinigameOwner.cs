using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanceMinigameOwner : MonoBehaviour
{
    public string OwnerName = "";

    [Header("Default Settings")]
    public float defaultCooldownDuration = 12f;
    public float defaultChanceToOccur = 0.50f;
    public float defaultChanceIncrease = 0.05f;
    public float defaultChanceTimer = 1f;
    
    public bool IsAvailable = false; 
    public int TimesCompleted = 0;
    public int TimesFailed = 0;

    [Header("Current Settings")]
    public float currentCooldownDuration;
    public float currentChanceToOccur;
    public float chanceIncrease;
    public float chanceTimer;

    public bool isOnCooldown = false;
    public bool isMinigameActive = true;
    
    public GameObject OpenMinigameButton;
    [SerializeField] private MinigameContainer MiniGames;  //we randomly take from a pool of available minigames
    [SerializeField] private List<MiniGame> SpawnedMiniGames;  //old spawned minigames
    [SerializeField] private MiniGame AvailableMiniGame;  //current minigame that is spawned
    
    private void Start()
    {
        GameEvents.OnEventStart += HandleEventStart;
        GameEvents.OnEventFail += HandleEventFail;
        GameEvents.OnEventCancel += HandleEventCancel;
        GameEvents.OnEventComplete += HandleEventComplete;
        GameEvents.OnEventMiss += HandleEventMiss;

        ResetToDefault();
        CheckInventory();
        GameStateEvent.OnGameStateStart += HandleGameStateStart;
        GameStateEvent.OnGameStateEnd += HandleGameStateEnd;
        
        IsAvailable = MinigameStatusManager.Instance.IsMinigameAvailable(AvailableMiniGame);
    }

    private void CheckInventory()
    {
        ResetToDefault();
        // Will make a call to inventory to check for new stats
    }

    public void BeginCooldowns()
    {
        StartCoroutine(CheckOccurChanceRoutine());
    }

    public void EndCooldowns()
    {
        StopCoroutine(CheckOccurChanceRoutine());
    }

    public void OpenActiveMiniGame()
    {
        Debug.Log("Attempting opening");
        if(GameStateManager.Instance.CurrentGameState.GameType != GameModeType.Song)
        {
            Debug.Log("Not a song right now");
            return;
        }
        if(MinigameStatusManager.Instance.OpenedMiniGame != null)
        {
            Debug.Log("Another game is opened");
            return;
        }
        if(AvailableMiniGame && !AvailableMiniGame.IsCompleted && AvailableMiniGame.isActiveEvent)
        {
            Debug.Log("Reopened");
            AvailableMiniGame.OpenEvent();
            return;
        }
    } 

    public void ActivateMiniGame()
    {
        if(!IsAvailable)
            return;

        // if it's not a song, return
        if(GameStateManager.Instance.CurrentGameState.GameType != GameModeType.Song)
        {
            Debug.Log("Not a song right now");
            return;
        }

        // if another game is open, return
        if(MinigameStatusManager.Instance.OpenedMiniGame != null)
        {
            Debug.Log("Another game is opened");
            return;
        }

        // if the mini-game hasn't been completed, reopen it
        if(AvailableMiniGame && !AvailableMiniGame.IsCompleted && AvailableMiniGame.isActiveEvent)
        {
            Debug.Log("Reopened");
            return;
        }

        // if the mini-game was completed and isn't on cooldown, reset and open it
        if(AvailableMiniGame && AvailableMiniGame.IsCompleted && !isOnCooldown)
        {
            Debug.Log("Restarted");
            AvailableMiniGame.IsCompleted = false;
            AvailableMiniGame.RestartMiniGameLogic();
            AvailableMiniGame.Activate();
            isMinigameActive = true;
            return;
        }

        // if the mini-game wasn't activated at all and isn't on cooldown, start it
        if(AvailableMiniGame && !isOnCooldown)
        {
            Debug.Log("Started");
            AvailableMiniGame.Activate();
            isMinigameActive = true;
            return;
        }
    }

    private IEnumerator CheckOccurChanceRoutine()
    {
        while (true)
        {
            while (isMinigameActive)
            {yield return null;}

            if (!isOnCooldown)
            {
                float randomValue = Random.value;
                if (randomValue <= currentChanceToOccur)
                {
                    SpawnMiniGame();
                    StartCooldown();
                    isMinigameActive = true;
                    isOnCooldown = true;
                }
                else
                {
                    IncreaseOccurChance();
                }
                yield return new WaitForSeconds(chanceTimer);
            }
            else
            {
                yield return new WaitForSeconds(currentCooldownDuration);
                isOnCooldown = false;
                ResetToDefault();
            }
        }
    }

    private void StartCooldown()
    {
        isOnCooldown = true;
    }

    private void IncreaseOccurChance()
    {
        currentChanceToOccur += chanceIncrease;
    }

    private void ResetToDefault()
    {
        currentCooldownDuration = defaultCooldownDuration;
        currentChanceToOccur = defaultChanceToOccur;
        chanceIncrease = defaultChanceIncrease;
        chanceTimer = defaultChanceTimer;
        if(AvailableMiniGame)
        {
            AvailableMiniGame.CloseEvent();
        }
    }

    private void SpawnMiniGame()
    {
        OpenMinigameButton.SetActive(true);
        if(AvailableMiniGame)
        {
            AvailableMiniGame.Activate();
        }
        // Logic for starting the mini-game goes here
    }

    public void HandleGameStateStart(object sender, GameStateEventArgs e)
    {
        //Debug.Log("State Started: " + e.stateType);
        switch(e.stateType)
        {
            case GameModeType.Song:
                BeginCooldowns();
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
                EndCooldowns();
                OpenMinigameButton.SetActive(false);
                if(AvailableMiniGame && isMinigameActive)
                {AvailableMiniGame.Miss();}
                break;
            default:
                break;
        }
    }


    void OnDestroy()
    {
        GameEvents.OnEventStart -= HandleEventStart;
        GameEvents.OnEventFail -= HandleEventFail;
        GameEvents.OnEventCancel -= HandleEventCancel;
        GameEvents.OnEventComplete -= HandleEventComplete;
        GameEvents.OnEventMiss -= HandleEventMiss;
    }

    void HandleEventStart(object sender, GameEventArgs e)
    {
        //Debug.Log("Event Started: " + e.EventObject);
    }

    void HandleEventFail(object sender, GameEventArgs e)
    {
        //Debug.Log("Event Fail: " + e.EventObject);
        if(e.EventObject == this.AvailableMiniGame)
        {
            this.TimesFailed++;
            BeginCooldowns();
            OpenMinigameButton.SetActive(false);
            isMinigameActive = false;
            isOnCooldown = true;
        }
        
    }

    void HandleEventCancel(object sender, GameEventArgs e)
    {
        //Debug.Log("Event Cancelled: " + e.EventObject);
        if(e.EventObject == this.AvailableMiniGame)
        {
            AvailableMiniGame.Miss();
            AvailableMiniGame.CloseEvent();
            BeginCooldowns();
            OpenMinigameButton.SetActive(false);
            isMinigameActive = false;
            isOnCooldown = true;
        }
    }

    void HandleEventComplete(object sender, GameEventArgs e)
    {
        //Debug.Log("Event Completed: " + e.EventObject);
        if(e.EventObject == this.AvailableMiniGame)
        {
            this.TimesCompleted++;
            BeginCooldowns();
            OpenMinigameButton.SetActive(false);
            isMinigameActive = false;
            isOnCooldown = true;

        }
    }

    void HandleEventMiss(object sender, GameEventArgs e)
    {
        //Debug.Log("Event Missed: " + e.EventObject);
        if(e.EventObject == this.AvailableMiniGame)
        {
            AvailableMiniGame.Miss();
            AvailableMiniGame.CloseEvent();
            BeginCooldowns();
            OpenMinigameButton.SetActive(false);
            isMinigameActive = false;
            isOnCooldown = true;
        }
    }

}
