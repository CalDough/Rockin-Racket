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
    public float currentCooldown;
    public float currentChanceToOccur;
    public float chanceIncrease;
    public float chanceTimer;

    public bool isOnCooldown = false;
    public bool isMinigameActive = true;
    private Coroutine occurChanceCoroutine;

    public GameObject OpenMinigameButton;
    
    public GameObject MinigameParent;
    [SerializeField] private MinigameContainer MiniGames;  //we randomly take from a pool of available minigames
    //This random mini-game spawning is currently unimplemented but intended in the future
    //as well as the list of games spawned
    [SerializeField] private List<MiniGame> SpawnedMiniGames;  //old spawned minigames
    
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
        GameStateEvent.OnGameStateStart += HandleGameStateStart;
        GameStateEvent.OnGameStateEnd += HandleGameStateEnd;
    }

    private void UnsubscribeEvents()
    {
        GameEvents.OnEventStart -= HandleEventStart;
        GameEvents.OnEventFail -= HandleEventFail;
        GameEvents.OnEventCancel -= HandleEventCancel;
        GameEvents.OnEventComplete -= HandleEventComplete;
        GameEvents.OnEventMiss -= HandleEventMiss;
        GameStateEvent.OnGameStateStart -= HandleGameStateStart;
        GameStateEvent.OnGameStateEnd -= HandleGameStateEnd;
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
        if(!IsAvailable){return;}
        
        // if it's not a song, return
        if(GameStateManager.Instance.CurrentGameState.GameType != GameModeType.Song)
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

    private void SpawnMiniGame()
    {
        OpenMinigameButton.SetActive(true);
        if(AvailableMiniGame)
        {
            Debug.Log("Minigame was available");
            AvailableMiniGame.Activate();
        }
        else if (!AvailableMiniGame)
        {
            
            Debug.Log("Minigame was not available");
            if(DefaultMiniGame && MinigameParent)
            {
                GameObject newGame = Instantiate(DefaultMiniGame);
                newGame.transform.SetParent(MinigameParent.gameObject.transform, false);
                if(newGame.TryGetComponent<MiniGame>(out MiniGame newMGComponent))
                {
                    AvailableMiniGame = newMGComponent;
                    SpawnedMiniGames.Add(newMGComponent);
                    AvailableMiniGame.Activate();
                }
                else
                {
                    Destroy(newGame.gameObject);
                }
            }
        }
        ResetToDefault();
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
            case GameModeType.Intermission:

                break;
            default:
                EndCooldowns();
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
            OpenMinigameButton.SetActive(false);
            isMinigameActive = false;
            isOnCooldown = true;
        }
    }

}
