using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public float currentCooldown;
    public float currentChanceToOccur;
    public float chanceIncrease;
    public float chanceTimer;

    public bool isOnCooldown = false;
    public bool isMinigameActive = true;

    public Image radialTimerImage;

    private Coroutine occurChanceCoroutine;
    private Coroutine countdownCoroutine;

    public GameObject OpenMinigameButton;
    
    public GameObject MinigameParent;

    [SerializeField] private GameObject DefaultMiniGame;
    [SerializeField] private MiniGame AvailableMiniGame;  //current minigame that is spawned
    
    private void Start()
    {

        ResetToDefault();
        CheckInventory();
        SubscribeEvents();
        IsAvailable = MinigameStatusManager.Instance.IsMinigameAvailable(AvailableMiniGame);
    }

    private void SubscribeEvents()
    {
        GameEvents.OnEventStart += HandleEventStart;
        GameEvents.OnEventFail += HandleEventFail;
        GameEvents.OnEventCancel += HandleEventCancel;
        GameEvents.OnEventComplete += HandleEventComplete;
        GameEvents.OnEventMiss += HandleEventMiss;
        StateEvent.OnStateStart += HandleGameStateStart;
        StateEvent.OnStateEnd += HandleGameStateEnd;
    }

    private void UnsubscribeEvents()
    {
        GameEvents.OnEventStart -= HandleEventStart;
        GameEvents.OnEventFail -= HandleEventFail;
        GameEvents.OnEventCancel -= HandleEventCancel;
        GameEvents.OnEventComplete -= HandleEventComplete;
        GameEvents.OnEventMiss -= HandleEventMiss;
        StateEvent.OnStateStart -= HandleGameStateStart;
        StateEvent.OnStateEnd -= HandleGameStateEnd;
    }

    private void CheckInventory()
    {
        ResetToDefault();
        // Will make a call to inventory to check for new stats
    }

    public void BeginCooldowns()
    {
        if(occurChanceCoroutine != null)
        {
            StopCoroutine(occurChanceCoroutine);
        }

        currentCooldown = defaultCooldownDuration;
        isOnCooldown = true;
        occurChanceCoroutine = StartCoroutine(CooldownAndChanceRoutine());
    }

    public void EndCooldowns()
    {
        if(occurChanceCoroutine != null)
        {
            StopCoroutine(occurChanceCoroutine);
        }
        ResetToDefault();
    }

    public void OpenActiveMiniGame()
    {
        //Debug.Log("Attempting opening");
        if(StateManager.Instance.CurrentState.stateType != StateType.Song)
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
        
        // if it's not a song, return
        if(StateManager.Instance.CurrentState.stateType != StateType.Song)
        { Debug.Log("Not a song right now"); return;}

        // if another game is open, return
        if(MinigameStatusManager.Instance.OpenedMiniGame != null)
        { Debug.Log("Another game is opened");return;}

        // if the mini-game hasn't been completed, reopen it
        if(AvailableMiniGame && !AvailableMiniGame.IsCompleted && AvailableMiniGame.isActiveEvent)
        {
            Debug.Log("Reopened");
            AvailableMiniGame.OpenEvent();
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

    private IEnumerator CooldownAndChanceRoutine()
    {
        // Initial cooldown phase at the beggining of a level as well as after finishing any mini-game
        while (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
            yield return null;
        }

        isOnCooldown = false;

        // Chance to occur phase after the cooldown, we attempt a spawn every chanceTimer seconds, then check if a random value is within our range
        while (!isMinigameActive)
        {
            float randomValue = Random.value;
            
            if (randomValue <= currentChanceToOccur)
            {
                //Debug.Log(randomValue+", "+currentChanceToOccur+" :  T"+Time.time);
                SpawnMiniGame();
                isMinigameActive = true;
                break;
            }
            else
            {
                currentChanceToOccur = Mathf.Min(currentChanceToOccur + chanceIncrease, 1f);
                yield return new WaitForSeconds(chanceTimer);
            }
        }
    }

    private void ResetToDefault()
    {
        currentCooldown = defaultCooldownDuration;
        currentChanceToOccur = defaultChanceToOccur;
        chanceIncrease = defaultChanceIncrease;
        chanceTimer = defaultChanceTimer;
        isOnCooldown = true;
    }

    public void OnMinigameButtonClicked()
    {
        Debug.Log("Attempting to start game now and stopping timer");
        if(AvailableMiniGame)
        {
            Debug.Log("Minigame was available, already spawned");
        }
        else if (!AvailableMiniGame)
        {
            Debug.Log("Minigame was not available, spawning it");
            if(DefaultMiniGame && MinigameParent)
            {
                GameObject newGame = Instantiate(DefaultMiniGame);
                newGame.transform.SetParent(MinigameParent.gameObject.transform, false);
                if(newGame.TryGetComponent<MiniGame>(out MiniGame newMGComponent))
                {
                    AvailableMiniGame = newMGComponent;
                }
                else
                {
                    Destroy(newGame.gameObject);
                }
            }
        }
        if(countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
        }
        radialTimerImage.fillAmount = 0;
        radialTimerImage.transform.parent.gameObject.SetActive(false);
    
        isMinigameActive = true;
        ActivateMiniGame();
    }

    private void SpawnMiniGame()
    {
        OpenMinigameButton.SetActive(true);
        radialTimerImage.transform.parent.gameObject.SetActive(true);
        countdownCoroutine = StartCoroutine(MiniGameActivationCountdown());
    }

    public void HandleGameStateStart(object sender, StateEventArgs e)
    {
        //Debug.Log("State Started: " + e.stateType);
        switch(e.state.stateType)
        {
            case StateType.Song:
                BeginCooldowns();
                break;
            case StateType.Intermission:

                break;
            default:
                EndCooldowns();
                break;
        }
    }
    
    private void HandleGameStateEnd(object sender, StateEventArgs e)
    {
        //Debug.Log("Game state ended: " + e.state.GameType);
        switch(e.state.stateType)
        {
            case StateType.Song:
                EndCooldowns();
                OpenMinigameButton.SetActive(false);
                if(AvailableMiniGame && isMinigameActive)
                {AvailableMiniGame.Miss();}
                break;
            default:
                EndCooldowns();
                break;
        }
    }


    void OnDestroy()
    {
        UnsubscribeEvents();
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
            AvailableMiniGame.CloseEvent();
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
            radialTimerImage.transform.parent.gameObject.SetActive(false);
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
            AvailableMiniGame.CloseEvent();
            BeginCooldowns();
            radialTimerImage.transform.parent.gameObject.SetActive(false);
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
            //AvailableMiniGame.Miss();
            AvailableMiniGame.CloseEvent();
            BeginCooldowns();
            radialTimerImage.transform.parent.gameObject.SetActive(false);
            OpenMinigameButton.SetActive(false);
            isMinigameActive = false;
            isOnCooldown = true;
        }
    }

    private IEnumerator MiniGameActivationCountdown()
    {
        float countdownDuration = 30f; 
        float startTime = Time.time;
        while (Time.time - startTime < countdownDuration)
        {
            if (isMinigameActive)
            {
                yield break;
            }

            UpdateRadialTimer((Time.time - startTime) / countdownDuration); 

            yield return null;
        }

        if (!isMinigameActive)
        {
            FailMiniGame();
        }
    }

    private void FailMiniGame()
    {
         AvailableMiniGame.Activate();
        if (AvailableMiniGame != null)
        {
            AvailableMiniGame.End();
        }
        isMinigameActive = false;
        OpenMinigameButton.SetActive(false);
        ResetToDefault();
        BeginCooldowns();
    }

    private void UpdateRadialTimer(float progress)
    {
        radialTimerImage.fillAmount = 1 - progress; 
    }
}
