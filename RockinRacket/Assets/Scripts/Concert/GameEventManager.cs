using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

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
    [SerializeField] private List<GameObject> stagedEvents;  
    //Random events are loaded from settings such as Character Moods, Venue Enviroment, Gear Quality
    [SerializeField] private List<GameObject> randomEvents;  

    //Concert Events only happen during the concert, all these events will automatically end once the GameState changes and will count as a miss
    [SerializeField] private List<GameObject> GameEvents;  
    
    //intermission Events only happen during the intermission for selling items, etc, they are generally easier and do not punish concert status
    [SerializeField] private List<GameObject> intermissionEvents;  

    [SerializeField] private List<GameObject> InstanceGameEvents;  
    [SerializeField] private List<GameObject> InstanceIntermissionEvents;  
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

    public void CleanUp()
    {
        songNumber = 0;
        intermissionNumber = 0;
        currentTime = 0;
        pastTime = 0;
        totalDifficulty = 0;
        completedEvents.Clear();
        failedEvents.Clear();
        missedEvents.Clear();
        cancelledEvents.Clear();
        stagedEvents.Clear();
        randomEvents.Clear();
        GameEvents.Clear();
        intermissionEvents.Clear();
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
        //Debug.Log("State Started: " + e.stateType);
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

    //#3 on order
    public void SetEventTimes()
    {
        List<GameState> allSongs = GameStateManager.Instance.GetAllStateOfType(GameModeType.Song);
        List<GameState> allIntermission = GameStateManager.Instance.GetAllStateOfType(GameModeType.Intermission);
        // Calculate total song duration
        float totalDuration = 0f;
        float totalIntDuration = 0f;
        foreach (var song in allSongs)
        {
            totalDuration += song.Song.Duration;
        }

        foreach (var intermission in allIntermission)
        {
            totalIntDuration += intermission.Duration;
        }
        Debug.Log("intermission Length " + totalIntDuration);
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
                if(currentEventIndex >= GameEvents.Count) 
                {
                    Debug.LogWarning("currentEventIndex exceeded GameEvents.Count!");   return; 
                }
                // Calculate a random timestamp for this event, between lastTimestamp and maxEventTime * (i + 1)
                float eventTime = UnityEngine.Random.Range(lastTimestamp, maxEventTime * (i + 1));
                lastTimestamp = eventTime;

                if(InstanceGameEvents[currentEventIndex].TryGetComponent<GameEvent>(out GameEvent gm))
                {gm.songActivationTime = Mathf.FloorToInt(eventTime);   gm.activationNumber = s;}
                else
                {Debug.LogError("Failed to get GameEvent component from GameObject at index: " + currentEventIndex);}
                currentEventIndex++;
            }
        }
        /*
        for (int s = 1; s < allIntermission.Count+1; s++)
        {
            GameState intermissionState = allIntermission[s-1];
            float intDuration = intermissionState.Duration;
            float tenPercent = intDuration * 0.10f;
            float maxTime = intDuration - tenPercent;

            // Calculate the number of events for this song based on its proportion of total duration
            int eventsForThisSong = Mathf.RoundToInt((intDuration / totalDuration) * numberOfEvents);

            Debug.Log("Intermission Length " + intDuration + " song events " + eventsForThisSong);

            float maxEventTime = maxTime / eventsForThisSong;
            float lastTimestamp = 0;
            for (int i = 0; i < eventsForThisSong; i++)
            {
                if(currentEventIndex >= intermissionEvents.Count) 
                {
                    Debug.LogWarning("currentEventIndex exceeded GameEvents.Count!");   return; 
                }
                // Calculate a random timestamp for this event, between lastTimestamp and maxEventTime * (i + 1)
                float eventTime = UnityEngine.Random.Range(lastTimestamp, maxEventTime * (i + 1));
                lastTimestamp = eventTime;

                if(InstanceIntermissionEvents[currentEventIndex].TryGetComponent<GameEvent>(out GameEvent gm))
                {gm.songActivationTime = Mathf.FloorToInt(eventTime);   gm.activationNumber = s;}
                else
                {Debug.LogError("Failed to get GameEvent component from GameObject at index: " + currentEventIndex);}
                currentEventIndex++;
            }
        }
        */
    }
       

    public void MissAllActiveEvents()
    {
        foreach (GameObject GameEvent in GameEvents)
        {
            if (GameEvent.TryGetComponent<GameEvent>(out GameEvent gm))
            {
                if(gm.isActiveEvent)
                {
                    gm.Miss();
                }
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
        if(GameEvents.Count == 0)
        {return;}
        foreach(GameObject prefab in GameEvents)
        {
            GameObject instantiatedObject = Instantiate(prefab);
            instantiatedObject.transform.localPosition = eventsParent.transform.position;
            if(instantiatedObject.TryGetComponent<GameEvent>(out GameEvent gm))
            {
                gm.isActiveEvent = false;
            }
            else
            {
                Debug.LogWarning("GameObject " + instantiatedObject.name + " does not contain a GameEvent component. It will be destroyed.");
                Destroy(instantiatedObject);
                continue;
            }
            instantiatedObject.transform.parent = eventsParent.transform;
            InstanceGameEvents.Add(instantiatedObject);
        }
        foreach(GameObject prefab in intermissionEvents)
        {
            GameObject instantiatedObject = Instantiate(prefab);
            instantiatedObject.transform.localPosition = eventsParent.transform.position;
            if(instantiatedObject.TryGetComponent<GameEvent>(out GameEvent gm))
            {
                gm.isActiveEvent = false;
            }
            else
            {
                Debug.LogWarning("GameObject " + instantiatedObject.name + " does not contain a GameEvent component. It will be destroyed.");
                Destroy(instantiatedObject);
                continue;
            }
            instantiatedObject.transform.parent = eventsParent.transform;
            InstanceIntermissionEvents.Add(instantiatedObject);
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
        List<GameObject> eventPool = new List<GameObject>();

        //Make a pool of events that we can use in each song section
        //Some events have higher priority
        GetBandSkillEvents();
        GetBandMoodEvents();
        GetAttendeeEvents();

        
        foreach(GameObject gm in this.GameEventSO.EventPrefabs)
        {
            eventPool.Add(gm);
        }
        /*
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
        */
        foreach(GameObject prefab in GameEventSO.EventPrefabs)
        {
            GameEvent GameEvent = prefab.GetComponent<GameEvent>();
            if (GameEvent == null)
            {continue;}
            
            eventPool.Add(prefab);
        }
        //Since intermission and concert events are similar, but one involves music, and one involves guests, or other things
        //it makes sense that they may share from the same pool of events, so they need to be slightly separated
        //this area does for easier testing
        List<GameObject> GameEventPool = eventPool.FindAll(e => e.GetComponent<GameEvent>().eventType == EventType.Song || e.GetComponent<GameEvent>().eventType == EventType.All);
        List<GameObject> intermissionEventPool = eventPool.FindAll(e => e.GetComponent<GameEvent>().eventType == EventType.Intermission || e.GetComponent<GameEvent>().eventType == EventType.All);
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

    private void AddEventsToConcert(List<GameObject> eventPool, int eventsToAdd, List<GameObject> eventList)
    {
        int currentEventsAdded = 0;
        while(currentEventsAdded < eventsToAdd)
        {
            // Shuffle eventPool
            for(int i = 0; i < eventPool.Count; i++)
            {
                
                GameObject temp = eventPool[i];
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
