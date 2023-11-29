using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StateManager : MonoBehaviour
{
    [SerializeField] GameStateData currentGameState;
    [SerializeField]IntermissionHandler intermissionHandler;
    [SerializeField] public Venue ConcertVenue;
    [field:SerializeField] public State CurrentState { get; private set; }
    [field:SerializeField] public int CurrentIndex { get; private set; } = 0;
    [SerializeField] public List<State> AllStates;



    [Header("Live Values")]
    [SerializeField] public float stateDuration = 0;
    [SerializeField] public float stateRemainder = 0;
    [SerializeField] public bool canBeginConcert = false;
    [SerializeField] public bool concertIsActive = false;

    public static StateManager Instance { get; private set; }

    private bool alreadySwappedToIntermission = false;

    void Awake()
    {
        if (Instance != null && Instance != this) 
        { Destroy(this); } 
        else 
        { 
            Instance = this; 
        } 
    }

    void Update()
    {
        CheckForIntermission();
    }
    
    public void InitializeConcertData()
    {
        Debug.Log("Init Concert");
        MinigameStatusManager.Instance.ResetVariables();
        AllStates = new List<State>();
        foreach (var state in ConcertVenue.ConcertStates)
        {
            AllStates.Add(new State(state));
        }
        stateDuration = 0;
        stateRemainder = 0;
        canBeginConcert = true;
        concertIsActive = false;
        CurrentState = null;
        StartConcert();
    }

    public void StartConcert()
    {
        Debug.Log("Starting Concert");
        if (canBeginConcert && AllStates.Count > 0)
        {
            canBeginConcert = false;
            concertIsActive = true;
            CurrentState = AllStates[CurrentIndex];
            StartCoroutine(PlayState(CurrentState));
        }
    }
    /*
    This functionality will be moved to Intermission handler to listen for the event for concert end instead of this script handling it.
    */
    public void LocateConcertUIAndEndConcert()
    {
        if (intermissionHandler != null)
        { intermissionHandler.EndConcert(); }
        else
        { Debug.LogWarning("No GameObject with the IntermissionHandler script was found!"); }
    }

    public void EndConcert()
    {
        concertIsActive = false;
        LocateConcertUIAndEndConcert();
        ConcertAudioEvent.ConcertEnd();

    }

    public void EndConcertEarly()
    {
        concertIsActive = false;
        ConcertAudioEvent.ConcertEnd();
    }

    public void CompleteState()
    {
        if(CurrentState != null)
        CurrentState.isCompleted = true;
    }

    public void NextState()
    {
        if (CurrentIndex < AllStates.Count - 1)
        {
            CurrentState.isCompleted = true;
            StopCoroutine(PlayState(CurrentState));
            CurrentIndex++;
            CurrentState = AllStates[CurrentIndex];
            StartCoroutine(PlayState(CurrentState));
        }
        else
        {
            Debug.Log("Ending Concert");
            EndConcert();
        }
    }

    IEnumerator PlayState(State state)
    {
        StateEvent.StateStart(state);
        state.isCompleted = false;

        if (!state.isManualDuration)  
        {
            stateDuration = state.duration;
            float startTime = Time.time;
            while (Time.time - startTime < stateDuration && !state.isCompleted)
            {
                stateRemainder = stateDuration - (Time.time - startTime);
                yield return null;
            }
        }
        else
        {
            // for manual duration states, wait for manual trigger to call NextState()
            while (!state.isCompleted)
            {
                stateRemainder = 0;
                yield return null;
            }
        }

        StateEvent.StateEnd(state);
        NextState();
    }

    public StateType? GetNextStateType()
    {
        if (CurrentIndex < AllStates.Count - 1)
        {return AllStates[CurrentIndex + 1].stateType;}

        return null;
    }

    // Changes the game state to BackstageView when the concert switches to intermission
    private void CheckForIntermission()
    {
        if (CurrentState.stateType == StateType.Intermission && !alreadySwappedToIntermission)
        {
            alreadySwappedToIntermission = true;
            CanvasController.instance.SwapToBackstageView();
            //CameraSwapEvents.instance.e_SwapToBackstageView.Invoke();
            //currentGameState.CurrentConcertState = ConcertState.BackstageView;
        }

        if (CurrentState.stateType == StateType.IntermissionOutro)
        {
            Debug.Log("Runs");
            alreadySwappedToIntermission = false;
            CanvasController.instance.SwapToBandView();
            //CameraSwapEvents.instance.e_SwapToBandView.Invoke();
            //currentGameState.CurrentConcertState = ConcertState.BandView;
        }
    }

}