using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class State
{
    [Header("State Settings")]
    [SerializeField] public StateType stateType = StateType.Default;
    [field: SerializeField] public bool ignoreState { get; set; }
    [field: SerializeField] public bool isManualDuration { get; set; }
    [field: SerializeField] public bool isCompleted { get; set; }
    [field: SerializeField] public float duration { get; set; }

    [Header("Data Settings")]
    [SerializeField] public SongData Song; 

    public State(State source)
    {
        this.stateType = source.stateType;
        this.ignoreState = source.ignoreState;
        this.isManualDuration = source.isManualDuration;
        this.isCompleted = source.isCompleted;
        this.duration = source.duration;

        this.Song = source.Song;
    }
}

public class StateEvent
{
    public static event EventHandler<StateEventArgs> OnStateEnd;
    public static event EventHandler<StateEventArgs> OnStateStart;

    public static void StateStart(State State)
    {
        OnStateStart?.Invoke(null, new StateEventArgs(State));
    }
    
    public static void StateStart(State State, StateType type)
    {
        OnStateStart?.Invoke(null, new StateEventArgs(State, type));
    }

    public static void StateEnd(State State)
    {
        OnStateEnd?.Invoke(null, new StateEventArgs(State));
    }

     public static void StateEnd(State State, StateType type)
    {
        OnStateEnd?.Invoke(null, new StateEventArgs(State, type));
    }
}

public class StateEventArgs : EventArgs
{
    public State state { get; private set; }
    public StateType stateType { get; private set; }

    public StateEventArgs(State State)
    {
        state = State;
    }

    public StateEventArgs(StateType StateType)
    {
        stateType = StateType;
    }
    
    public StateEventArgs(State State, StateType StateType)
    {
        state = State;
        stateType = StateType;
    }
}