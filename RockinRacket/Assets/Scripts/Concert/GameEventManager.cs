using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
    This script is a singleton which manages the Game Events that happen during a level.
    See Game Event for how an event works.
    Upon an event ending through one of the ways, it will be disabled and put in one of the 4 lists so we can inform players at the end of a
    level what they missed, and got done.
    Events are currently set up to be divided among songs equally. Total events are determined by difficulty/10 = # of events
    Events can be sourced from other scripts that contain GameEventContainers and we will try to include some events with priority
    before others. These events would be venue events, story events, and band events.
    While using the Timeline feature in unity would be awesome, you can't randomly set and save timelines through scripts.

    To prevent massive update lag every frame, events check once a second to see if the time they are supposed to activate has occured.
    Events subscribe to the OnSecondPassed and update of this script.

    All events are created at the start of a scene so the concert can play smoothly without instantion lag

    Currently starting concerts does require activating the correct order of functions to load/find events, instantiate, set the correct song timer, then start concert

*/
public class GameEventManager : MonoBehaviour
{
    public event Action<float> OnSecondPassed;
    
    public int songNumber, intermissionNumber = 0;
    public float currentTime, pastTime = 0;
    [field:SerializeField] public int totalDifficulty { get; private set; } = 0 ; 

    //Use these to keep track of events if we want to track this data, else we can just remove events
    [SerializeField] private List<MiniGame> completedMiniGames;  
    [SerializeField] private List<MiniGame> failedMiniGames;  
    [SerializeField] private List<MiniGame> missedMiniGames;  
    [SerializeField] private List<MiniGame> cancelledMiniGames;  
    

    //These are the final lists of the prefabs for the mini-games, these have been loaded from staged,random and the Scriptable objects
    //Concert Events only happen during the concert, all these events will automatically end once the GameState changes and will count as a miss
    //intermission Events only happen during the intermission for selling items, etc, they are generally easier and do not punish concert status
    [SerializeField] private List<GameObject> concertMiniGames;  
    [SerializeField] private List<GameObject> intermissionMiniGames;  

    //These are the lists of the mini-games that have been spawned in the level
    [SerializeField] private List<GameObject> instanceConcertMiniGames = new List<GameObject>();  
    [SerializeField] private List<GameObject> instanceIntermissionMiniGames = new List<GameObject>();  

    [SerializeField] private List<GameObject> usedUniqueMiniGames  = new List<GameObject>();

    [Header("Event Scriptable Objects")]
    //Plug in event prefabs into this scriptable object to have them happen in testing
    [SerializeField] private GameEventContainer ConcertMiniGameSO;  
    [SerializeField] private GameEventContainer IntermissionMiniGameSO;  

    [Header("Empty Scriptable Objects")]
    //Leave these as empty scriptable objects so other scripts can access them and add to them
    //You can add stuff into the immune one before a concert though
    //Staged events are loaded from the venue and story
    [SerializeField] private GameEventContainer stagedMiniGames;  
    //Random events are loaded from settings such as Character Moods, Venue Enviroment, Gear Quality
    [SerializeField] private GameEventContainer randomMiniGames;  
    // This is filled by story manager, inventory/upgrade manager to remove duplicate story mini-games or equipment based ones
    [SerializeField] private GameEventContainer ImmuneMiniGames;

    public static GameEventManager Instance { get; private set; }

    //Singleton Code
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

    //After a concert, we need to reset the variables so they don't conflict with the last concert 
    public void CleanUp()
    {
        songNumber = 0;
        intermissionNumber = 0;
        currentTime = 0;
        pastTime = 0;
        totalDifficulty = 0;
        completedMiniGames.Clear();
        failedMiniGames.Clear();
        missedMiniGames.Clear();
        cancelledMiniGames.Clear();

        stagedMiniGames.MiniGamesPrefabs.Clear();
        randomMiniGames.MiniGamesPrefabs.Clear();
        ImmuneMiniGames.MiniGamesPrefabs.Clear();

        concertMiniGames.Clear();
        intermissionMiniGames.Clear();
        instanceConcertMiniGames.Clear();
        instanceIntermissionMiniGames.Clear();
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

    public void HandleEventStart(object sender, GameEventArgs e)
    {
        Debug.Log("Event Started: " + e.EventObject);
    }

    public void HandleEventFail(object sender, GameEventArgs e)
    {
        Debug.Log("Event Fail: " + e.EventObject);
        failedMiniGames.Add(e.EventObject);
    }

    public void HandleEventCancel(object sender, GameEventArgs e)
    {
        Debug.Log("Event Cancelled: " + e.EventObject);
        cancelledMiniGames.Add(e.EventObject);
    }

    public void HandleEventComplete(object sender, GameEventArgs e)
    {
        Debug.Log("Event Completed: " + e.EventObject);
        //GameEvents.Remove(e.eventObject);
        completedMiniGames.Add(e.EventObject);
    }

    public void HandleEventMiss(object sender, GameEventArgs e)
    {
        Debug.Log("Event Missed: " + e.EventObject);
        missedMiniGames.Add(e.EventObject);
    }
    
    public void HandleGameStateStart(object sender, GameStateEventArgs e)
    {
        //Debug.Log("State Started: " + e.stateType);
        switch(e.stateType)
        {
            case GameModeType.Song:
                this.songNumber++;
                StartState(GameModeType.Song);
                break;
            case GameModeType.Intermission:
                this.intermissionNumber++;
                StartState(GameModeType.Intermission);
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
                currentTime = 0;
                pastTime = 0;
                break;
            case GameModeType.Intermission:
                currentTime = 0;
                pastTime = 0;
                break;
            default:
                break;
        }
        MissAllActiveEvents();
    }

    public void MissAllActiveEvents()
    {
        foreach (GameObject GameEvent in concertMiniGames)
        {
            if (GameEvent.TryGetComponent(out MiniGame minigame))
            {
                if(minigame.isActiveEvent)
                {
                    minigame.Miss();
                }
            }
        }
        foreach (GameObject GameEvent in instanceIntermissionMiniGames)
        {
            if (GameEvent.TryGetComponent(out MiniGame minigame))
            {
                if(minigame.isActiveEvent)
                {
                    minigame.Miss();
                }
            }
        }
    }

    void Update()
    {
        // terrible
        if (GameStateManager.Instance.ConcertActive && GameStateManager.Instance.CurrentGameState.GameType == GameModeType.Song || GameStateManager.Instance.CurrentGameState.GameType == GameModeType.Intermission)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= pastTime + .5f)
            {
                OnSecondPassed?.Invoke(currentTime);
                pastTime = currentTime;
            }
        }
    }

    

    

    //All of these functions should use data from the venue, band, and attendees to get specific events related to them
    //It may be easier to calculate those events from a different class
    public void GetStageEvents()
    {

    }

    public void GetBandSkillEvents()
    {

    }

    public void GetBandMoodEvents()
    {

    }

    public void GetAttendeeEvents()
    {

    }

    //Call on start of a concert to create the lists for mini-games will may occur based on the band and stuff
    public void LoadEvents()
    {
        concertMiniGames.Clear();
        intermissionMiniGames.Clear();

        //All of these will check for their respective things and add it to the stagedMiniGames or randomMiniGames lists
        GetStageEvents();
        GetBandSkillEvents();
        GetBandMoodEvents();
        GetAttendeeEvents();


        // Load from stagedMiniGames
        foreach(GameObject prefab in stagedMiniGames.MiniGamesPrefabs)
        {
            AddMiniGameToCorrectList(prefab);
        }

        // Load from randomMiniGames
        foreach(GameObject prefab in randomMiniGames.MiniGamesPrefabs)
        {
            AddMiniGameToCorrectList(prefab);
        }

        // Load from ConcertMiniGameSO
        foreach(GameObject prefab in ConcertMiniGameSO.MiniGamesPrefabs)
        {
            AddMiniGameToCorrectList(prefab);
        }

        // Load from IntermissionMiniGameSO
        foreach(GameObject prefab in IntermissionMiniGameSO.MiniGamesPrefabs)
        {
            AddMiniGameToCorrectList(prefab);
        }

        //If we gave the player immunity to a mini-game from items or something, we can check for it here
        PruneList(concertMiniGames);
        PruneList(intermissionMiniGames);

    }

    private void AddMiniGameToCorrectList(GameObject prefab)
    {
        prefab.TryGetComponent(out MiniGame miniGame);

        if (miniGame == null)
        {return;}

        if (miniGame.gameType == GameModeType.Song)
        {
            concertMiniGames.Add(prefab);
        }
        else if (miniGame.gameType == GameModeType.Intermission)
        {
            intermissionMiniGames.Add(prefab);
        }
    }

    //Removes mini-games if they are in the Immune scriptable object
    private void PruneList(List<GameObject> miniGamesList)
    {
        for (int i = miniGamesList.Count - 1; i >= 0; i--)
        {
            if (ImmuneMiniGames.MiniGamesPrefabs.Contains(miniGamesList[i]))
            {
                miniGamesList.RemoveAt(i);
            }
        }
    }



    // Called at the start of every song and intermission state
    //Creates the mini-games and then sets their times to occur somewhere between 10% to 90% of the way into a song
    // total minigames created is based on (song difficulty + venue difficulty) * modifiers / 15 = total # for that state
    public void StartState(GameModeType stateType)
    {
        Debug.Log("Creating Game Events");
        GameObject eventsParent = GameObject.Find("EventContainer");
        
        if (eventsParent == null)
        {
            Canvas canvas = GameObject.FindObjectOfType<Canvas>();

            if (canvas == null)
            {
                Debug.LogError("No canvas exists in the scene.");
                return;
            }
            
            eventsParent = new GameObject("EventContainer");
            eventsParent.transform.SetParent(canvas.transform, false); 
        }

        int venueDifficulty = 10;
        if(GameStateManager.Instance.SelectedVenue != null)
        { venueDifficulty = GameStateManager.Instance.SelectedVenue.Difficulty;}
        
        int songDifficulty = 10;
        if(GameStateManager.Instance.CurrentGameState.Song != null)
        { songDifficulty = GameStateManager.Instance.CurrentGameState.Song.Difficulty;}
        
        float difficultyModifier = GameManager.Instance.difficultyModifier;

        totalDifficulty = (venueDifficulty + songDifficulty);

        int numberOfMiniGames = Mathf.FloorToInt((totalDifficulty * difficultyModifier) / 15);

        List<GameObject> availableMiniGames;
        if (stateType == GameModeType.Song)
        {availableMiniGames = new List<GameObject>(concertMiniGames); }
        else
        {availableMiniGames = new List<GameObject>(intermissionMiniGames); }

        float stateDuration = GameStateManager.Instance.CurrentGameState.Duration;
        
        // cancel spawning events if duration is 0, dont want to bug system
        if(stateDuration <= 0)
        { return;}

        float minActivationTime = stateDuration * 0.1f;
        float maxActivationTime = stateDuration * 0.9f;

        for (int i = 0; i < numberOfMiniGames; i++)
        {
            GameObject selectedMiniGame = null;
            if(stateType == GameModeType.Song)
            {
                selectedMiniGame = SelectAndRemoveMiniGame(ref availableMiniGames, concertMiniGames);
            }
            else
            {
                selectedMiniGame = SelectAndRemoveMiniGame(ref availableMiniGames, intermissionMiniGames);
            }

            if (selectedMiniGame == null)
            {break;}

            // instantiate and configure the mini-game
            GameObject miniGameInstance = Instantiate(selectedMiniGame); 
            MiniGame miniGame = miniGameInstance.GetComponent<MiniGame>();

            miniGameInstance.transform.SetParent(eventsParent.transform);
            miniGameInstance.transform.localPosition = Vector3.zero;
            miniGameInstance.transform.localRotation = Quaternion.identity;
            miniGameInstance.transform.localScale = Vector3.one;

            miniGame.activationTime = Mathf.Round(UnityEngine.Random.Range(minActivationTime, maxActivationTime)* 2) / 2;

            if(stateType == GameModeType.Song)
            {
                miniGame.activationNumber = songNumber;
                instanceConcertMiniGames.Add(miniGameInstance);
            }
            else
            {
                miniGame.activationNumber = intermissionNumber;
                instanceIntermissionMiniGames.Add(miniGameInstance);
            }
        }
    }

    //Gets a mini-game from the list and if none are available we reuse some
    private GameObject SelectAndRemoveMiniGame(ref List<GameObject> availableMiniGames, List<GameObject> sourceMiniGamesList)
    {
        // prioritize IsOneTimeMiniGame
        GameObject oneTimeMiniGame = availableMiniGames.Find(mg => 
        {
            mg.TryGetComponent(out MiniGame miniGame);
            return miniGame != null && miniGame.isOneTimeEvent;
        });

        if (oneTimeMiniGame != null)
        {
            availableMiniGames.Remove(oneTimeMiniGame);
            return oneTimeMiniGame;
        }

        //select a game that's not unique or hasn't been used yet
        GameObject suitableMiniGame = availableMiniGames.Find(mg => 
        {
            mg.TryGetComponent(out MiniGame miniGame);
            return miniGame != null && (!miniGame.isUniqueEvent || (miniGame.isUniqueEvent && !this.usedUniqueMiniGames.Contains(mg)));
        });

        if (suitableMiniGame != null)
        {
            availableMiniGames.Remove(suitableMiniGame);
            if (suitableMiniGame.GetComponent<MiniGame>().isUniqueEvent)
                usedUniqueMiniGames.Add(suitableMiniGame);
            return suitableMiniGame;
        }

        // If we've reached this point, it means we have run out of mini-games and need to reuse
        GameObject reusableMiniGame = sourceMiniGamesList.Find(mg => 
        {
            mg.TryGetComponent(out MiniGame miniGame);
            return miniGame != null && !miniGame.isUniqueEvent && !miniGame.isOneTimeEvent;
        });

        if (reusableMiniGame != null)
        {
            // no need to remove this from the availableMiniGames 
            return reusableMiniGame;
        }

        // if there's no suitable mini-game at all 
        return null;
    }

    
}
