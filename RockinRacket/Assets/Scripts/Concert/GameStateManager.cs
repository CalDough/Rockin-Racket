using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    
    [SerializeField] public List<GameState> InspectorGameStates;

    [SerializeField] public List<GameState> SelectedMenuSongs;
    //Populate GameStates linked list from Venue first
    [SerializeField] public List<GameState> GameStatesFromVenue;
    //Current Story states are added after
    [SerializeField] public List<GameState> GameStatesFromStory;

    [SerializeField] public LinkedList<GameState> GameStates;

    [field:SerializeField] public GameState CurrentGameState { get; private set; }

    public bool CanStartLevel = false;
    public bool ConcertActive = false;
    public Venue SelectedVenue;

    [SerializeField] float levelTime = 0;
    [SerializeField] float totalTime = 0;
    [field:SerializeField] public int songSlotsAvailable{ get; private set; } = 0 ; 

    public int MoneyToGain = 0;
    public int FameToGain = 0;


    public static GameStateManager Instance { get; private set; }

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

    
    //Needs to calculate money from total attendees or have an event to signal end
    public void EndConcert()
    {
        GameManager.Instance.globalFame += FameToGain;
        GameManager.Instance.globalMoney += MoneyToGain;
        AudioManager.Instance.StopConcert();
        LocateConcertUIAndEndConcert();
        
    }
    public void LocateConcertUIAndEndConcert()
    {
        // Find an object with the ConcertUI script
        ConcertUI concertUI = FindObjectOfType<ConcertUI>();

        // If the script was found on an object, call the EndConcert method
        if (concertUI != null)
        {
            concertUI.EndConcert();
        }
        else
        {
            Debug.LogWarning("No GameObject with the ConcertUI script was found!");
        }
    }
    
    
    
    
    /*
        Gets songs set from venue and story into the queue then players selected songs to fill remaining song slots
    */
    public void LoadGameStatesFromLists()
    {
        if(ConcertActive){return;}
        
        //if(CurrentStatus != ConcertStatus.CanStart){return;}
        if(SelectedVenue == null){return;}
        // Initialize the GameStates linked list
        GameStates = new LinkedList<GameState>();

        // Counters for the songs
        int songsCount = 0;

        // Add GameStates from Venue
        foreach (GameState state in GameStatesFromVenue)
        {
            if (state.GetGameModeType() == GameModeType.Song)
            {
                
                songsCount++;
                
                
                if(state.Song != null)
                {  
                    if(state.Duration <= state.Song.Duration)
                    {
                        state.Duration = state.Song.Duration;
                    }
                        
                    state.UseDuration = true;
                    GameStates.AddLast(state);
                }
            }
        }


        if (songsCount > SelectedVenue.SongSlots)
        { CanStartLevel = false; Debug.LogWarning("To many venue songs!"); return;}

        // Add GameStates from Story
        foreach (GameState state in GameStatesFromStory)
        {
            if (state.GetGameModeType() == GameModeType.Song)
            {
                
                songsCount++;
                
            
                if(state.Song != null)
                {  
                    if(state.Duration <= state.Song.Duration)
                    {
                        state.Duration = state.Song.Duration;
                    }
                        
                    state.UseDuration = true;
                    GameStates.AddLast(state);
                }
            }
        }

        if (songsCount > SelectedVenue.SongSlots)
        { CanStartLevel = false; Debug.LogWarning("To many story songs!"); return;}

        // Add selected songs
        foreach (GameState state in SelectedMenuSongs)
        {
            if (state.GetGameModeType() == GameModeType.Song)
            {
                
                songsCount++;
                
                
                if(state.Song != null)
                {  
                    if(state.Duration <= state.Song.Duration)
                    {
                        state.Duration = state.Song.Duration;
                    }
                        
                    state.UseDuration = true;
                    GameStates.AddLast(state);
                }
            }
        }

        // Check if we haven't exceeded the maximum number of songs
        if (songsCount <= SelectedVenue.SongSlots)
        {CanStartLevel = true;}
        else if (songsCount <= 0)
        {CanStartLevel = false;Debug.LogWarning("The total number of songs is 0!");}
        else
        { CanStartLevel = false; Debug.LogWarning("The total number of songs exceeds the maximum limit for this venue!");}

        Debug.LogWarning("The total number of songs:" + songsCount);

    }

    /*
        After LoadGameStatesFromLists() has been completed and the level can start, this function does the following
        1. after every song in GameStates, we add an intermission GameState
        2. Checks for a custom intro scene data in the lists, if there isnt, we have a short intro
        3. Checks for a custom outro scene data in the lists, if there isnt, we have a short outro
        4. loop through venue and story list to add cutscenes and specific dialogue for the level
    */
    public void ModifyGameStates()
    {
        if(ConcertActive){return;}
        // Temporary list to store nodes to insert intermission after
        List<LinkedListNode<GameState>> songNodes = new List<LinkedListNode<GameState>>();
        
        // Find all song nodes
        for (LinkedListNode<GameState> node = GameStates.First; node != null; node = node.Next)
        {
            if (node.Value.GetGameModeType() == GameModeType.Song)
            {
                songNodes.Add(node);
            }
        }

        // Add Intermission after each Song, excpet band battle songs which we dont care
        foreach (LinkedListNode<GameState> songNode in songNodes)
        {
            
            GameState intermissionState = new GameState(); 
            intermissionState.GameType = GameModeType.Intermission;
            intermissionState.Duration = 20f;
            intermissionState.UseDuration = true;
            GameStates.AddAfter(songNode, intermissionState);
            
        }

        // Add SceneIntro at the start of the list
        GameState sceneIntroState = FindFirstGameStateOfType(GameModeType.SceneIntro);
        if (sceneIntroState == null) // If no SceneIntro found, create a new one
        {
            sceneIntroState = new GameState(); 
            sceneIntroState.GameType = GameModeType.SceneIntro; 
            sceneIntroState.Duration = 15;
            sceneIntroState.UseDuration = true;
        }
        GameStates.AddFirst(sceneIntroState);

        // Add SceneOutro at the end of the list
        GameState sceneOutroState = FindFirstGameStateOfType(GameModeType.SceneOutro);
        if (sceneOutroState == null) // If no SceneOutro found, create a new one
        {
            sceneOutroState = new GameState(); 
            sceneOutroState.GameType = GameModeType.SceneOutro; 
            sceneOutroState.Duration = 15;
            sceneOutroState.UseDuration = true;
        }
        GameStates.AddLast(sceneOutroState);

        // Insert Cutscene and Dialogue GameStates from Venue and story
        foreach (GameState state in GameStatesFromVenue)
        {
            if (state.GetGameModeType() == GameModeType.Cutscene || state.GetGameModeType() == GameModeType.Dialogue)
            {
                InsertGameStateAroundOccurrences(state, state.InsertionType, state.NumberOfStates, state.InsertAfter);
            }
        }
        foreach (GameState state in GameStatesFromStory)
        {
            if (state.GetGameModeType() == GameModeType.Cutscene || state.GetGameModeType() == GameModeType.Dialogue)
            {
                InsertGameStateAroundOccurrences(state, state.InsertionType, state.NumberOfStates, state.InsertAfter);
            }
        }
    }

    /*
        Returns true if the player can add an another song to the level play list of songs
    */
    public bool CheckCurrentSongs()
    {
        // Set currentSongs equal to maxSongs
        if(SelectedVenue == null)
        {
            return false;
        }
        songSlotsAvailable = SelectedVenue.SongSlots;
        foreach (GameState state in SelectedMenuSongs)
        {
            if (state.GetGameModeType() == GameModeType.Song )
            {
                songSlotsAvailable--;
            }
        }

        // Subtract for each song in GameStatesFromVenue
        foreach (GameState state in GameStatesFromVenue)
        {
            if (state.GetGameModeType() == GameModeType.Song )
            {
                songSlotsAvailable--;
            }
        }

        // Subtract for each song in GameStatesFromStory
        foreach (GameState state in GameStatesFromStory )
        {
            if (state.GetGameModeType() == GameModeType.Song )
            {
                songSlotsAvailable--;
            }
        }

        // Return true if currentSongs > 0, false otherwise
        return songSlotsAvailable > 0;
    }

    /*
        Finds first GameState of a certain type, useful for finding intro, outro, etc
    */
    public GameState FindFirstGameStateOfType(GameModeType type)
    {
        // Check in GameStatesFromVenue
        foreach (GameState state in GameStatesFromVenue)
        {
            if (state.GetGameModeType() == type)
            {
                return state;
            }
        }

        // If not found in GameStatesFromVenue, check in GameStatesFromStory
        foreach (GameState state in GameStatesFromStory)
        {
            if (state.GetGameModeType() == type)
            {
                return state;
            }
        }

        // If not found in either list, return null
        return null;
    }

    //WIP
    public void InitializeGame(List<GameState> gameStateList)
    {
        if(ConcertActive){return;}
        GameStates = new LinkedList<GameState>(gameStateList);
        LinkedListNode<GameState> node = GameStates.First;

        CurrentGameState = GameStates.First.Value;
    }

    //WIP
    public void EndCurrentGameState()
    {
        if(GameStates == null)
        {
            return;
        }
        //add debug and fix to know when concert fully over.
        if(GameStates.First.Next != null)
        {
            CurrentGameState.EndState();
            GameStates.RemoveFirst();
            CurrentGameState = GameStates.First.Value;
            CurrentGameState.StartState();
        }
        else
        {
            Debug.Log("ConcertOver");
            this.ConcertActive = false;
            EndConcert();
        }
    }

    //Used to add a gamestate into the linked list
    // Note if you use after 0 occurrenceCount, it will never insert
    public void InsertGameStateAroundOccurrences(GameState stateToInsert, GameModeType targetType, int numberOfOccurrences, bool insertAfter)
    {
        LinkedListNode<GameState> targetNode = GameStates.First;
        int occurrenceCount = 0;

        while (targetNode != null)
        {
            if (targetNode.Value.GetGameModeType() == targetType)
            {
                occurrenceCount++;
                if (occurrenceCount == numberOfOccurrences)
                {
                    // Insert the state either before or after the target node, based on the 'insertAfter' flag
                    if (insertAfter)
                    {
                        GameStates.AddAfter(targetNode, stateToInsert);
                    }
                    else
                    {
                        GameStates.AddBefore(targetNode, stateToInsert);
                    }
                    return;
                }
            }
            targetNode = targetNode.Next;
        }
    }

    public void SelectCustomSong(SongData song)
    {
        if (CheckCurrentSongs())
        {
            GameState newSongGameState = new GameState();
            newSongGameState.Song = song;
            newSongGameState.Duration = song.Duration;
            newSongGameState.GameType = GameModeType.Song;
            
            SelectedMenuSongs.Add(newSongGameState);
        }
        else
        {
            Debug.LogWarning("No available song slot for: " + song.SongName);
        }
    }

    public void RemoveCustomSongs()
    {
        this.SelectedMenuSongs.Clear();
    }

    public List<GameState> GetAllSongs()
    {
        List<GameState> allSongs = new List<GameState>();
        List<List<GameState>> allGameStatesLists = new List<List<GameState>> { GameStatesFromStory, GameStatesFromVenue, SelectedMenuSongs };
        foreach (List<GameState> gameStateList in allGameStatesLists)
        {
            foreach (GameState gameState in gameStateList)
            {
                if (gameState.GameType == GameModeType.Song)
                {
                    allSongs.Add(gameState);
                }
            }
        }

        return allSongs;
    }

    public List<GameState> GetAllStateOfType(GameModeType stateType)
    {
        List<GameState> allStates = new List<GameState>();
        foreach (GameState state in GameStates)
        {
            if (state.GameType == stateType)
            {
                allStates.Add(state);
            }
        }

        return allStates;
    }

    void Start()
    {
        GameStateEvent.OnGameStateStart += OnGameStateStart;
        GameStateEvent.OnGameStateEnd += OnGameStateEnd;
    }

    void OnDestroy()
    {
        GameStateEvent.OnGameStateStart -= OnGameStateStart;
        GameStateEvent.OnGameStateEnd -= OnGameStateEnd;
    }

    private void OnGameStateStart(object sender, GameStateEventArgs e)
    {
        // Handle the game state start here
        Debug.Log("Game state started: " + e.state.GameType);
        switch(e.stateType)
        {
            case GameModeType.Song:
                // Initialize your Song state here
                AudioManager.Instance.StartConcert();
                break;
            case GameModeType.SceneIntro:
                // Initialize your SceneIntro state here
                break;
            // add other cases as needed
            default:
                break;
        }
    }

    private void OnGameStateEnd(object sender, GameStateEventArgs e)
    {
        // Handle the game state end here
        Debug.Log("Game state ended: " + e.state.GameType);
        switch(e.stateType)
        {
            case GameModeType.Song:
                // Handle end of your Song state here
                AudioManager.Instance.StopConcert();
                break;
            case GameModeType.SceneIntro:
                // Handle end of your SceneIntro state here
                break;
            // add other cases as needed
            default:
                break;
        }
    }

    //Make sure  LoadVenue, and events are set before starting level
    public void StartConcert()
    {
        
        if(ConcertActive == true){return;}
        if(SelectedVenue == null){return;}

        LoadGameStatesFromLists();
        if(CanStartLevel != true)
        { return;}
        ModifyGameStates();
        GameEventManager.Instance.LoadEvents();
        GameEventManager.Instance.InstantiateGameEvents();
        GameEventManager.Instance.SetEventTimes();
        CopyGameStatesToInspector();
        CurrentGameState = GameStates.First.Value;
        CurrentGameState.StartState();
        ConcertActive = true;

    }

    public void LoadVenue()
    {
        if(ConcertActive){return;}
        if(SelectedVenue == null){return;}

        GameStatesFromVenue.Clear();

        foreach (GameState state in SelectedVenue.StagedGameStates)
        {
            GameStatesFromVenue.Add(state);
        }

    }

    public void CopyGameStatesToInspector()
    {
        // Make sure to clear the InspectorGameStates list before copying
        InspectorGameStates.Clear();

        // Copy each GameState from the LinkedList to the List
        foreach (GameState gameState in GameStates)
        {
            InspectorGameStates.Add(gameState);
        }
    }

    void Update()
    {
        if(ConcertActive)
        {
            totalTime+= Time.deltaTime;
            if(CurrentGameState.UseDuration)
            {
                if(levelTime < CurrentGameState.Duration)
                {
                    levelTime+= Time.deltaTime;
                }
                else
                {
                    levelTime = 0;
                    EndCurrentGameState();
                }
            }
        }
    }

}


