using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    This script is meant to help define what a concert state is. It defines what the state is for, how long, which songs/dialogue will occur.
    Insertion is the most confusing part. You can create a game state for a venue to have a pre-defined song, cutscene, or whatever. 
    Example for insertion:
        InsertionType = GamemodeType.Song
        GameType = Intermission.
        NumberOfStates = 1;
        InsertAfter = true;
    Including this into a list in the GameStateManager would insert the GameState after it finds the first song and it would set the variables accordingly
    See GameStateManager for further info.
*/
[System.Serializable]
public class GameState
{
    [Header("State Settings")]
    [SerializeField] public GameModeType GameType = GameModeType.Default;
    [field: SerializeField] public bool IgnoreInList { get; set; }
    [field: SerializeField] public float Duration { get; set; }
    [SerializeField] public bool UseDuration = true;

    [Header("Data Settings")]
    [SerializeField] public SongData Song; 
    [SerializeField] public TextAsset Story;
    [SerializeField] public int StoryID;

    [Header("Insertion Settings")]
    public int NumberOfStates = 0;
    public GameModeType InsertionType = GameModeType.Default;
    public bool InsertAfter = true;

    // These are methods that subclasses need to implement.
    public void StartState()
    {
        GameStateEvent.StateStart(this, GameType);
    }

    public void EndState()
    {
        GameStateEvent.StateEnd(this, GameType);
    }

    public GameModeType GetGameModeType()
    {
        return GameType;
    }
}

//This has overloaded function options but probably doesnt need it
public class GameStateEvent
{
    public static event EventHandler<GameStateEventArgs> OnGameStateEnd;
    public static event EventHandler<GameStateEventArgs> OnGameStateStart;

    public static void StateStart(GameState concertState)
    {
        OnGameStateStart?.Invoke(null, new GameStateEventArgs(concertState));
    }
    
    public static void StateStart(GameState concertState, GameModeType type)
    {
        OnGameStateStart?.Invoke(null, new GameStateEventArgs(concertState, type));
    }

    public static void StateEnd(GameState concertState)
    {
        OnGameStateEnd?.Invoke(null, new GameStateEventArgs(concertState));
    }

     public static void StateEnd(GameState concertState, GameModeType type)
    {
        OnGameStateEnd?.Invoke(null, new GameStateEventArgs(concertState, type));
    }
}

public class GameStateEventArgs : EventArgs
{
    public GameState state { get; private set; }
    public GameModeType stateType { get; private set; }

    public GameStateEventArgs(GameState concertState)
    {
        state = concertState;
    }

    public GameStateEventArgs(GameModeType concertStateType)
    {
        stateType = concertStateType;
    }
    
    public GameStateEventArgs(GameState concertState, GameModeType concertStateType)
    {
        state = concertState;
        stateType = concertStateType;
    }
}

// emits and holds time means that there is no gameplay during this Game Mode,
// its purely just waiting for a cutscene for a specified duration before alerting other scripts for when it starts and stops
[System.Serializable]
public enum GameModeType
{
    Default, //emits and holds time
    SceneIntro, //emits and holds time
    SceneOutro, //emits and holds time
    Song, //holds track to play
    Intermission, //only needs to emit event
    Cutscene, //holds scene to load
    Dialogue, //holds ink dialogue to select
    BandBattle,
}
