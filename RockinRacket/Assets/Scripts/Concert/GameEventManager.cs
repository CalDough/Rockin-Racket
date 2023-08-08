using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEventManager : MonoBehaviour
{
    public event Action<float> OnSecondPassed;
    
    public int songNumber, intermissionNumber = 0;
    public float currentTime, pastTime = 0;
    [field:SerializeField] public int totalDifficulty { get; private set; } = 0 ; 

    //Use these to keep track of events if we want to track this data, else we can just remove events
    [SerializeField] private List<GameEvent> completedEvents;  
    [SerializeField] private List<GameEvent> failedEvents;  
    [SerializeField] private List<GameEvent> missedEvents;  
    [SerializeField] private List<GameEvent> cancelledEvents;  
    

    //Staged events are loaded from the venue and story
    [SerializeField] private List<GameEvent> stagedEvents;  
    //Random events are loaded from settings such as Character Moods, Venue Enviroment, Gear Quality
    [SerializeField] private List<GameEvent> randomEvents;  

    //Concert Events only happen during the concert, all these events will automatically end once the GameState changes and will count as a miss
    [SerializeField] private List<GameEvent> GameEvents;  
    
    //intermission Events only happen during the intermission for selling items, etc, they are generally easier and do not punish concert status
    [SerializeField] private List<GameEvent> intermissionEvents;  


    [Header("Event Tester")]
    [SerializeField] private GameEventContainer GameEventSO;  


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

    void Start()
    {
        GameEvent.OnEventStart += HandleEventStart;
        GameEvent.OnEventFail += HandleEventFail;
        GameEvent.OnEventCancel += HandleEventCancel;
        GameEvent.OnEventComplete += HandleEventComplete;
        GameEvent.OnEventMiss += HandleEventMiss;
        GameStateEvent.OnGameStateStart += HandleGameStateStart;
        
        GameStateEvent.OnGameStateEnd += HandleGameStateEnd;
    }

    void OnDestroy()
    {
        GameEvent.OnEventStart -= HandleEventStart;
        GameEvent.OnEventFail -= HandleEventFail;
        GameEvent.OnEventCancel -= HandleEventCancel;
        GameEvent.OnEventComplete -= HandleEventComplete;
        GameEvent.OnEventMiss -= HandleEventMiss;
        GameStateEvent.OnGameStateStart -= HandleGameStateStart;
        GameStateEvent.OnGameStateEnd -= HandleGameStateEnd;
    }

    public void HandleEventStart(object sender, GameEventArgs e)
    {
        Debug.Log("Event Started: " + e.eventObject);
    }

    public void HandleEventFail(object sender, GameEventArgs e)
    {
        Debug.Log("Event Fail: " + e.eventObject);
        failedEvents.Add(e.eventObject);
    }

    public void HandleEventCancel(object sender, GameEventArgs e)
    {
        Debug.Log("Event Cancelled: " + e.eventObject);
        cancelledEvents.Add(e.eventObject);
    }

    public void HandleEventComplete(object sender, GameEventArgs e)
    {
        Debug.Log("Event Completed: " + e.eventObject);
        //GameEvents.Remove(e.eventObject);
        completedEvents.Add(e.eventObject);
    }

    public void HandleEventMiss(object sender, GameEventArgs e)
    {
        Debug.Log("Event Missed: " + e.eventObject);
        missedEvents.Add(e.eventObject);
    }
    
    public void HandleGameStateStart(object sender, GameStateEventArgs e)
    {
        Debug.Log("State Started: " + e.stateType);
        switch(e.stateType)
        {
            case GameModeType.Song:
                this.songNumber++;
                break;
            case GameModeType.Intermission:
                this.intermissionNumber++;
                break;
            default:
                break;
        }
    }
    
    private void HandleGameStateEnd(object sender, GameStateEventArgs e)
    {
        // Handle the game state end here
        Debug.Log("Game state ended: " + e.state.GameType);
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

    //#3 on order
    public void SetEventTimes()
    {
        List<GameState> allSongs = GameStateManager.Instance.GetAllStateOfType(GameModeType.Song);

        // Calculate total song duration
        float totalDuration = 0f;
        foreach (var song in allSongs)
        {
            totalDuration += song.Song.Duration;
        }

        int numberOfEvents = GameEvents.Count;
        int currentEventIndex = 0;

        //This might cause off by 1 error later but to simplfy the logic in the editor, we count songs by 1,2,3 etc instead of song 0
        for (int s = 1; s < allSongs.Count+1; s++)
        {
            GameState songState = allSongs[s-1];
            float songDuration = songState.Song.Duration;
            float tenPercentOfSong = songDuration * 0.10f;
            float maxTime = songDuration - tenPercentOfSong;

            // Calculate the number of events for this song based on its proportion of total duration
            int eventsForThisSong = Mathf.RoundToInt((songDuration / totalDuration) * numberOfEvents);

            Debug.Log("Song Length " + songDuration + " song events " + eventsForThisSong);

            float maxEventTime = maxTime / eventsForThisSong;
            float lastTimestamp = 0;
            for (int i = 0; i < eventsForThisSong; i++)
            {
                // Calculate a random timestamp for this event, between lastTimestamp and maxEventTime * (i + 1)
                float eventTime = UnityEngine.Random.Range(lastTimestamp, maxEventTime * (i + 1));
                lastTimestamp = eventTime;

                GameEvents[currentEventIndex].songActivationTime = eventTime;
                GameEvents[currentEventIndex].activationNumber = s;
                currentEventIndex++;
            }
        }
        /*
        // Distribute any remaining events
        int remainingEvents = numberOfEvents - currentEventIndex;
        if (remainingEvents > 0)
        {
            // Sort the songs by duration in descending order
            allSongs.Sort((a, b) => b.Song.songDuration.CompareTo(a.Song.songDuration));

            int songIndex = 0;
            while (remainingEvents > 0)
            {
                GameState longestSong = allSongs[songIndex % allSongs.Count];
                float songDuration = longestSong.Song.songDuration;
                float tenPercentOfSong = songDuration * 0.10f;
                float maxTime = songDuration - tenPercentOfSong;

                // Add an event at a random time within the allowable range
                float eventTime = UnityEngine.Random.Range(tenPercentOfSong, maxTime);
                GameEvents[currentEventIndex].songActivationTime = eventTime;
                GameEvents[currentEventIndex].songNumber = songIndex % allSongs.Count;
                currentEventIndex++;
                remainingEvents--;
                songIndex++;
            }
        }
        */
    }

    public void MissAllActiveEvents()
    {
        foreach (GameEvent GameEvent in GameEvents)
        {
            if (GameEvent.isActiveEvent)
            {
                GameEvent.Miss();
            }
        }
    }

    //#2 on order
    public void InstantiateGameEvents()
    {
        Debug.Log("Creating");
        
        // Try to find the existing EventContainer
        GameObject eventsParent = GameObject.Find("EventContainer");
        
        // If the EventContainer doesn't exist, create it
        if (eventsParent == null)
        {
            // Locate the canvas
            Canvas canvas = GameObject.FindObjectOfType<Canvas>();

            // If the canvas doesn't exist, print an error and return
            if (canvas == null)
            {
                Debug.LogError("No canvas exists in the scene.");
                return;
            }
            
            eventsParent = new GameObject("EventContainer");
            eventsParent.transform.SetParent(canvas.transform, false); // Setting the parent to be the canvas
        }

        if(GameEventSO.EventPrefabs.Count == 0)
        {
            return;
        }
        foreach(GameObject prefab in GameEventSO.EventPrefabs)
        {
            GameObject instantiatedObject = Instantiate(prefab);
            instantiatedObject.transform.localPosition = eventsParent.transform.position;
            GameEvent GameEvent = instantiatedObject.GetComponent<GameEvent>();

            // If the GameEvent component doesn't exist, delete the object
            if (GameEvent == null)
            {
                Debug.LogWarning("GameObject " + instantiatedObject.name + " does not contain a GameEvent component. It will be destroyed.");
                Destroy(instantiatedObject);
                continue;
            }
            GameEvent.isActiveEvent = false;
            instantiatedObject.transform.parent = eventsParent.transform;
            // Add the GameEvent to the GameEvents list
            GameEvents.Add(GameEvent);
        }
    }

    void Update()
    {
        // terrible
        if (GameStateManager.Instance.CurrentGameState.GameType == GameModeType.Song)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= pastTime + .5f)
            {
                OnSecondPassed?.Invoke(currentTime);
                pastTime = currentTime;
            }
        }
    }

    //#1 on order
    public bool LoadEvents()
    {
        Debug.Log("Loading Events");
        // Calculate total difficulty
        totalDifficulty = 0;
        totalDifficulty += GameStateManager.Instance.SelectedVenue.Difficulty;

        foreach(GameState SongState in GameStateManager.Instance.GetAllSongs())
        {
            totalDifficulty += SongState.Song.Difficulty;
        }
    
        totalDifficulty = (int)(totalDifficulty * GameManager.Instance.difficultyModifier);

        // Call other functions to initialize the concert

        //AnimalManager.Instance.InitializeAttendees();
        
        GameEvents.Clear();
        GetStageEvents(); //These events are core to the stage, story, or whatever, so we can ignore totalDifficulty when adding
        List<GameEvent> eventPool = new List<GameEvent>();

        //Make a pool of events that we can use in each song section
        //Some events have higher priority
        GetBandSkillEvents();
        GetBandMoodEvents();
        GetAttendeeEvents();
        
        //Since intermission and concert events are similar, but one involves music, and one involves guests, or other things
        //it makes sense that they may share from the same pool of events, so they need to be slightly separated
        //this area does for easier testing
        List<GameEvent> GameEventPool = eventPool.FindAll(e => e.eventType == EventType.Song || e.eventType == EventType.All);
        List<GameEvent> intermissionEventPool = eventPool.FindAll(e => e.eventType == EventType.Intermission || e.eventType == EventType.All);
        int GameEventsToAdd = totalDifficulty / 10;

        int intermissionTime = GameStateManager.Instance.GetAllStateOfType(GameModeType.Intermission).Count * 20;
        int intermissionEventsToAdd = (int)((intermissionTime * GameManager.Instance.difficultyModifier) / 10);
        if(eventPool.Count > 0)
        {
        AddEventsToConcert(GameEventPool, GameEventsToAdd, GameEvents);
        AddEventsToConcert(intermissionEventPool, intermissionEventsToAdd, intermissionEvents);
        }
        // If everything is successful, return true
        Debug.Log("Events Added:" + eventPool.Count + " Sized Pool of Events");
        return true;
    }

    private void AddEventsToConcert(List<GameEvent> eventPool, int eventsToAdd, List<GameEvent> eventList)
    {
        int currentEventsAdded = 0;
        while(currentEventsAdded < eventsToAdd)
        {
            // Shuffle eventPool
            for(int i = 0; i < eventPool.Count; i++)
            {
                GameEvent temp = eventPool[i];
                int randomIndex = UnityEngine.Random.Range(i, eventPool.Count);
                eventPool[i] = eventPool[randomIndex];
                eventPool[randomIndex] = temp;
            }

            // Add events from shuffled pool to eventList until we meet the required count or exhaust the pool
            for(int i = 0; i < eventPool.Count && currentEventsAdded < eventsToAdd; i++)
            {
                eventList.Add(eventPool[i]);
                currentEventsAdded++;
            }
        }
    }

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

}
