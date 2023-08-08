using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleMiniGame : MonoBehaviour
{
    public GameEvent gameEvent;
    public GameObject Panels;
    public bool IsCompleted = false;

    void OnDestroy()
    {
        GameEvent.OnEventStart -= HandleEventStart;
        GameEvent.OnEventFail -= HandleEventFail;
        GameEvent.OnEventCancel -= HandleEventCancel;
        GameEvent.OnEventComplete -= HandleEventComplete;
        GameEvent.OnEventMiss -= HandleEventMiss;
        GameEvent.OnEventClose -= HandleEventClose;
        GameEvent.OnEventOpen -= HandleEventOpen;
        GameStateEvent.OnGameStateStart -= HandleGameStateStart;
        GameStateEvent.OnGameStateEnd -= HandleGameStateEnd;
    }

    // Start is called before the first frame update
    void Start()
    {   
        GameEvent.OnEventStart += HandleEventStart;
        GameEvent.OnEventFail += HandleEventFail;
        GameEvent.OnEventCancel += HandleEventCancel;
        GameEvent.OnEventComplete += HandleEventComplete;
        GameEvent.OnEventMiss += HandleEventMiss;
        GameEvent.OnEventClose += HandleEventClose;
        GameEvent.OnEventOpen += HandleEventOpen;

        GameStateEvent.OnGameStateStart += HandleGameStateStart;
        GameStateEvent.OnGameStateEnd += HandleGameStateEnd;
    }

    public void HandleEventStart(object sender, GameEventArgs e)
    {
        //Debug.Log("UI Event Started: " + e.eventObject);
        
    }

    public void HandleEventFail(object sender, GameEventArgs e)
    {
        //Debug.Log("Event Fail: " + e.eventObject);
        if(e.eventObject == this.gameEvent)
        {
            HandleClosing();
        }
    }

    public void HandleEventCancel(object sender, GameEventArgs e)
    {
        //Debug.Log("Event Cancelled: " + e.eventObject);
        if(e.eventObject == this.gameEvent)
        {
            HandleClosing();
        }
    }

    public void HandleEventComplete(object sender, GameEventArgs e)
    {
        //Debug.Log("Event Completed: " + e.eventObject);
        if(e.eventObject == this.gameEvent)
        {
            this.IsCompleted = true;
            HandleClosing();
        }
    }

    public void HandleEventMiss(object sender, GameEventArgs e)
    {
        //Debug.Log("Event Missed: " + e.eventObject);
        if(e.eventObject == this.gameEvent)
        {
            HandleClosing();
        }
    }
    
    public void HandleGameStateStart(object sender, GameStateEventArgs e)
    {
        //Debug.Log("State Started: " + e.stateType);
        switch(e.stateType)
        {
            case GameModeType.Song:
                break;
            case GameModeType.Intermission:
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
                HandleClosing();
                break;
            case GameModeType.Intermission:
                HandleClosing();
                break;
            default:
                break;
        }
        
    }

    public void HandleEventOpen(object sender, GameEventArgs e)
    {
        //Debug.Log("Event Missed: " + e.eventObject);
        if(e.eventObject == this.gameEvent)
        {
            HandleOpening();
        }
    }

    public void HandleEventClose(object sender, GameEventArgs e)
    {
        //Debug.Log("Event Missed: " + e.eventObject);
        if(e.eventObject == this.gameEvent)
        {
            HandleClosing();

            //if you want the player to start event again if they close it
            if(IsCompleted == false)
            {RestartMiniGameLogic();}
        }
    }

    public void RestartMiniGameLogic()
    {

    }

    public void HandleOpening()
    {
        //Debug.Log("opening event");
        if(!IsCompleted)
        {
        Panels.SetActive(true);
        }
    }

    public void HandleClosing()
    {
        Panels.SetActive(false);
    }

}
