using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    This script is a singleton which manages the Game State of a concert that happens during a level.
    See Game State  for how a GameState works.

    This script basically requires a SelectedVenue to properly function.
    It contains some code for UI features. 
    When a concert begins through StartConcert()
    A venue with any pre-defined game states are fully loaded. 
    It will load all gamestates, related song data, dialogue into the manager.
    Then this script calls GameEventManager to figure out what events we want the player to experience for the venue and concert.

    Some variables and lists are meant to be for debugging/inspector viewing such as InspectorGameStates, CurrentGameState

*/
public class GameStateManager : MonoBehaviour
{
    //This list doesn't actually do anything, its just easier to convert linked list to a list to view in the inspector
    [SerializeField] public List<GameState> InspectorGameStates;
    
     //This helps view whats going on in the inspector such as current state and its variables
    [field:SerializeField] public GameState CurrentGameState { get; private set; }

    
    [SerializeField] public List<GameState> GameStatesFromVenue; // #1 Populate GameStates linked list from Venue first

    [SerializeField] public List<GameState> SelectedMenuSongs; //#2 GameStates added from the song selection menu
    
    [SerializeField] public List<GameState> GameStatesFromStory; //#3 Current Story states are added after, this might go unused

    //This is the final list of all the gamestates that will occur in the concert
    //It is coded as a linked list instead of a queue or list, so it is easier to insert states through the code and avoid null list entries
    //If it is a queue, then
    [SerializeField] public LinkedList<GameState> GameStates; 


    public bool CanStartLevel = false;
    public bool ConcertActive = false;
    public Venue SelectedVenue;

    [SerializeField] public float levelTime = 0;
    [SerializeField] public float totalTime = 0;
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


    /*
        If the concert is active, and our current Game State is not an infinite state like Cutscene, or dialogue
        We wait until the variable levelTime is greater than or equal to the GameStates time
        Then we change Game States
    */
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

    public void ResetVariables()
    {
        CanStartLevel = false;
        ConcertActive = false;
        SelectedMenuSongs.Clear();
        GameStatesFromVenue.Clear();
        GameStatesFromStory.Clear();
        CurrentGameState = new GameState();
        levelTime = 0;
        totalTime = 0;
        songSlotsAvailable = 0;
        MoneyToGain = 0;
        FameToGain = 0;
        GameStates.Clear();
        InspectorGameStates.Clear();
    }

    //
    //MAKE THIS A BUTTON ON THE END CONCERT UI SCREEN INSTEAD OF AUTO ENDING!!!
    //
    //Needs to calculate money from total attendees or have an event to signal end
    public void EndConcert()
    {
        GameManager.Instance.globalFame += FameToGain;
        GameManager.Instance.globalMoney += MoneyToGain;
        CanStartLevel = false;
        ConcertActive = false;
        ConcertAudioEvent.ConcertEnd();
        LocateConcertUIAndEndConcert();
        //GameEventManager.Instance.CleanUp();
        ResetVariables();
        
    }

    //This occurs when the scene changes through the pause menu
    public void EndConcertEarly()
    {
        CanStartLevel = false;
        ConcertActive = false;
        ConcertAudioEvent.ConcertEnd();
        LocateConcertUIAndEndConcert();
        //GameEventManager.Instance.CleanUp();
        
        ResetVariables();
    }

    // Concert UI script will do stuff like show the player's results and disable the musical UI
    public void LocateConcertUIAndEndConcert()
    {
        ConcertUI concertUI = FindObjectOfType<ConcertUI>(); // Find an object with the ConcertUI script

        // If the script was found on an object, call the EndConcert method
        if (concertUI != null)
        { concertUI.EndConcert(); }
        else
        { Debug.LogWarning("No GameObject with the ConcertUI script was found!"); }
    }
    
    /*
       This function checks and adds songs to the Gamestates list
       1. we get songs from the Venue that will always play
       2. we get songs from the story, if it is like a story mission to play that song
       3. we get any remaining songs that the player selected
       4. If the venue supports that many songs, we can start the next functions
    */
    public void LoadGameStatesFromLists()
    {

        if(ConcertActive){return;}
        //if(CurrentStatus != ConcertStatus.CanStart){return;}
        if(SelectedVenue == null){return;}
        // Initialize a new list for GameStates
        GameStates = new LinkedList<GameState>();
        int songsCount = 0;

        foreach (GameState state in GameStatesFromVenue)
        {
            if (state.GetGameModeType() == GameModeType.Song)
            {
                songsCount++;
                if(state.Song != null)
                {  
                    if(state.Duration <= state.Song.Duration)
                    {state.Duration = state.Song.Duration;} 
                    state.UseDuration = true;
                    GameStates.AddLast(state);
                }
            }
        }

        if (songsCount > SelectedVenue.SongSlots)
        { CanStartLevel = false; Debug.LogWarning("To many venue songs!"); return;}

        foreach (GameState state in GameStatesFromStory)
        {
            if (state.GetGameModeType() == GameModeType.Song)
            {
                songsCount++;
                if(state.Song != null)
                {  
                    if(state.Duration <= state.Song.Duration)
                    {state.Duration = state.Song.Duration;}
                    state.UseDuration = true;
                    GameStates.AddLast(state);
                }
            }
        }

        if (songsCount > SelectedVenue.SongSlots)
        { CanStartLevel = false; Debug.LogWarning("To many story songs!"); return;}

        foreach (GameState state in SelectedMenuSongs)
        {
            if (state.GetGameModeType() == GameModeType.Song)
            {
                songsCount++;
                if(state.Song != null)
                {  
                    if(state.Duration <= state.Song.Duration)
                    {state.Duration = state.Song.Duration;}
                    state.UseDuration = true;
                    GameStates.AddLast(state);
                }
            }
        }

        // Check if we haven't exceeded the maximum number of songs
        if (songsCount <= SelectedVenue.SongSlots) { CanStartLevel = true; }
        else if (songsCount <= 0) { CanStartLevel = false;Debug.LogWarning("The total number of songs is 0!"); }
        else { CanStartLevel = false; Debug.LogWarning("The total number of songs exceeds the maximum limit for this venue!"); }

        Debug.Log("The total number of songs:" + songsCount);

    }

    /*
        After LoadGameStatesFromLists() has been completed and the level can start, this function does the following
        1. after every song in GameStates, we add an intermission GameState
        2. Checks for a custom intro scene data in the lists, if there isnt, we have a short intro
        3. Checks for a custom outro scene data in the lists, if there isnt, we have a short outro
        4. loop through venue and story list to add cutscenes and specific dialogue for the level if there is any scenes for it
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

        // Add Intermission after each Song, except band battle songs which we dont care
        foreach (LinkedListNode<GameState> songNode in songNodes)
        {

            GameState intermissionState = new GameState
            {
                GameType = GameModeType.Intermission,
                Duration = 15f,
                UseDuration = false
            };
            GameStates.AddAfter(songNode, intermissionState);
            
        }

        // Add SceneIntro at the start of the list
        GameState sceneIntroState = FindFirstGameStateOfType(GameModeType.SceneIntro);
        if (sceneIntroState == null) // If no SceneIntro found, create a new one
        {
            sceneIntroState = new GameState
            {
                GameType = GameModeType.SceneIntro,
                Duration = 5,
                UseDuration = true
            };
        }
        GameStates.AddFirst(sceneIntroState);

        // Add SceneOutro at the end of the list
        GameState sceneOutroState = FindFirstGameStateOfType(GameModeType.SceneOutro);
        if (sceneOutroState == null) // If no SceneOutro found, create a new one
        {
            sceneOutroState = new GameState
            {
                GameType = GameModeType.SceneOutro,
                Duration = 5,
                UseDuration = true
            };
        }
        GameStates.AddLast(sceneOutroState);
        foreach (GameState state in GameStatesFromVenue)
        {
            if (state.GetGameModeType() == GameModeType.Intermission || state.GetGameModeType() == GameModeType.BandBattle)
            {
                InsertGameStateAroundOccurrences(state, state.InsertionType, state.NumberOfStates, state.InsertAfter, state.ReplaceState);
            }
        }
        // Insert Cutscene and Dialogue GameStates from Venue and story
        foreach (GameState state in GameStatesFromVenue)
        {
            if (state.GetGameModeType() == GameModeType.Cutscene || state.GetGameModeType() == GameModeType.Dialogue)
            {
                InsertGameStateAroundOccurrences(state, state.InsertionType, state.NumberOfStates, state.InsertAfter, state.ReplaceState);
            }
        }
        foreach (GameState state in GameStatesFromStory)
        {
            if (state.GetGameModeType() == GameModeType.Cutscene || state.GetGameModeType() == GameModeType.Dialogue)
            {
                InsertGameStateAroundOccurrences(state, state.InsertionType, state.NumberOfStates, state.InsertAfter, state.ReplaceState);
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
            if (state.GetGameModeType() == GameModeType.Song ){ songSlotsAvailable--; }
        }

        // Subtract for each song in GameStatesFromVenue
        foreach (GameState state in GameStatesFromVenue)
        {
            if (state.GetGameModeType() == GameModeType.Song ){ songSlotsAvailable--; }
        }

        // Subtract for each song in GameStatesFromStory
        foreach (GameState state in GameStatesFromStory )
        {
            if (state.GetGameModeType() == GameModeType.Song ){ songSlotsAvailable--; }
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
            { return state; }
        }

        // If not found in GameStatesFromVenue, check in GameStatesFromStory
        foreach (GameState state in GameStatesFromStory)
        {
            if (state.GetGameModeType() == type)
            { return state; }
        }

        // If not found in either list, return null
        return null;
    }

    //Make sure  LoadVenue, and events are set before starting level
    public void StartConcert()
    {
        
        if(ConcertActive == true){return;}
        if(SelectedVenue == null){return;}
        
        MinigameStatusManager.Instance.ResetVariables();

        Debug.Log("Loading Concert Data and starting concert");
        LoadVenue();
        LoadGameStatesFromLists();
        if(CanStartLevel != true)
        { return;}
        
       // Debug.Log("Song States Loaded, Creating Concert States");
        ModifyGameStates();
        
        
        //Debug.Log("Events Loaded Starting");
        //GameEventManager.Instance.LoadEvents();
        MinigameStatusManager.Instance.CheckInventory();
        
        //Debug.Log("Starting");
        CopyGameStatesToInspector();
        CurrentGameState = GameStates.First.Value;
        CurrentGameState.StartState();
        ConcertActive = true;

    }

    //WIP 
    //Initializing a concert should begin here, but I have yet to refactor things out of other functions to set it up
    // Currently StartConcert is taking the role of this function
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

    // Used to add a gamestate into the linked list
    // Note if you use after 0 occurrenceCount, it will never insert
    // Example: Insert Cutscene after Target: Song, Number 2, After. This will insert a cutscene after the 2nd song.
    public void InsertGameStateAroundOccurrences(GameState stateToInsert, GameModeType targetType, int numberOfOccurrences, bool insertAfter, bool replaceState)
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
                    if(replaceState == true)
                    {
                        GameStates.AddAfter(targetNode, stateToInsert);
                        GameStates.Remove(targetNode);
                        return;
                    }


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
                break;
            case GameModeType.SceneIntro:
                // Handle end of your SceneIntro state here
                break;
            // add other cases as needed
            default:
                break;
        }
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

    //This isn't used for anything other than letting the Unity Inspector and the InspectorGameStates be visible to us
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

}


