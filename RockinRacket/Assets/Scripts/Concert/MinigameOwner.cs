using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameOwner : MonoBehaviour
{
    public string OwnerName = "";

    [Header("Default Settings")]
    public float defaultCooldownDuration = 12f;

    [Header("Current Settings")]
    public float currentCooldownDuration;

    public bool isOnCooldown = false;
    public int TimesCompleted = 0;
    public int TimesFailed = 0;
    
    public bool IsAvailable = false; //bool based on whether the player has unlocked the game or cannot play it

    [Header("Attempt Settings")]
    public bool useAttempts = false;
    public int maxAttempts = 5;
    private int attempts = 0;

    [SerializeField] private MiniGame AvailableMiniGame;  //the minigame in the scene

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
        // Will make a call to inventory to check for current prefab if the player has the item to own the mini-game
    }

    public void BeginCooldowns()
    {
        StartCoroutine(CooldownRoutine());
    }

    public void EndCooldowns()
    {
        StopAllCoroutines();
        isOnCooldown = false;
    }

    private IEnumerator CooldownRoutine()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(currentCooldownDuration);
        isOnCooldown = false;
    }

    private void ResetToDefault()
    {
        currentCooldownDuration = defaultCooldownDuration;
        //attempts = 0;

    }

    public void ActivateMiniGame()
    {   
        if(!IsAvailable)
        {return;}

        //if it's not a song we dont play games
        if(GameStateManager.Instance.CurrentGameState.GameType != GameModeType.Song)
        {
            Debug.Log("Not a song right now");
            return;
        }
        //if another game is open we cant open this one
        if(MinigameStatusManager.Instance.OpenedMiniGame != null)
        {
            Debug.Log("Another game is opened");
            return;
        }
        //if we ran out of attempts we cant play it
        if (useAttempts && attempts >= maxAttempts)
        {
            Debug.Log("Max attempts reached!");
            //edge case where its their final attempt and we should actually open the game
            return;
        }

        if(AvailableMiniGame)
        {
            //Minigame was closed and not completed and is still active, we reopen it when they press the button
            if(!AvailableMiniGame.IsCompleted && AvailableMiniGame.isActiveEvent)
            {
                Debug.Log("reopened");
                AvailableMiniGame.OpenEvent();
                return;
            }
            //if minigame was completed earlier then we need to reset the minigame then allow the player to play it
            if(AvailableMiniGame.IsCompleted && !isOnCooldown)
            {
                Debug.Log("restarted");
                AvailableMiniGame.IsCompleted = false;
                AvailableMiniGame.RestartMiniGameLogic(); //may be calling restart twice due to bad programming 
                AvailableMiniGame.Activate();
                AvailableMiniGame.OpenEvent();
            }
            //the minigame was never activated in the first place and not on cooldown
            else if(!isOnCooldown)
            {
                Debug.Log("started");
                AvailableMiniGame.Activate();
                AvailableMiniGame.OpenEvent();
            }
            //add to number of attempts if we want to limit the times played
            if (!isOnCooldown && useAttempts)
            {
                attempts++;
            }
        }
        //Reset cooldowns? I'm thinking of changing something here
        // Mini-games go on cooldown after starting and finishing them, not while active
        //begin new cooldowns
        //ResetToDefault();
        //BeginCooldowns();
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
                EndCooldowns();
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
        }
    }

    void HandleEventComplete(object sender, GameEventArgs e)
    {
        //Debug.Log("Event Completed: " + e.EventObject);
        if(e.EventObject == this.AvailableMiniGame)
        {
            this.TimesCompleted++;
            BeginCooldowns();
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
        }
    }
}
