using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/*
    This script is meant to be put on a game object that will have an accompanying mini-game script.
    See Example Mini Game script for reference. 
    There are a lot of Event possibilities:
    1. Event Start: The event just started so it will get enabled
    2. Event Fail: You failed the event by not doing it or you failed in the event
    3. Event Miss: This should only be called on events that don't punish you for not doing them or they automatically resolve due to something
    4. Event Cancel: The concert game state changed from Song to Something else so you get a freebie and the event is missed
    5. Event Complete: The event was completed so it gives rewards or stops its negative effect
    6. Event Open: The player opens the event via UI so it should show the UI game
    7. Event Close: Opposite of above

*/
public class MiniGame : MonoBehaviour 
{
    public GameObject Panels; //This is the UI panel in the canvas that houses the actual event stuff
    public bool IsCompleted = false;

    // Refactor later, These two variables determine which song and what time in that song this event 
    // will check for and to set itself to active. Currently does not support intermissions.
    public float songActivationTime = 0;
    public int activationNumber = 0;  

    public float duration; //Time till the event automatically ends
    public float remainingDuration;  //Helper float for duration calculation
    public bool infiniteDuration; //True if the event does not expire
    public bool isActiveEvent = false; //Other classes like GameEventManager use this bool to check for active events

    public EventType eventType = EventType.Song;
    public Coroutine durationCoroutine = null;


    public virtual void Activate()
    {
        isActiveEvent = true;
        remainingDuration = duration;
        if (!infiniteDuration) {
            remainingDuration = duration;
            durationCoroutine = StartCoroutine(EventDurationCountdown());
        }
        GameEvents.EventStart(this);
    }

    //When the player fails to complete the event in time.
    public virtual void End()
    {
        isActiveEvent = false;
        if (durationCoroutine != null) {
            StopCoroutine(durationCoroutine);
        }
        GameEvents.EventFail(this);
        GameEvents.EventClosed(this);
        HandleClosing();
    }

    //When the player misses the event due to Game State Change.
    public virtual void Miss()
    {
        isActiveEvent = false;
        if (durationCoroutine != null) {
            StopCoroutine(durationCoroutine);
        }
        GameEvents.EventMiss(this);
        GameEvents.EventClosed(this);
        HandleClosing();
    }

    //When the player completes the event in time.
    public virtual void Complete()
    {
        isActiveEvent = false;
        if (durationCoroutine != null) {
            StopCoroutine(durationCoroutine);
        }
        GameEvents.EventComplete(this);
        GameEvents.EventClosed(this);
        this.IsCompleted = true;
        HandleClosing();
    }

    //Calls the event to inform the UI to open or close the game
    public virtual void OpenEvent()
    { 
        GameEvents.EventOpened(this); 
        HandleOpening();
    }
    public virtual void CloseEvent() 
    { 
        GameEvents.EventClosed(this); 
        HandleClosing();
    }
    
    // Since this is based on Time.deltaTime it will actually already be affected by the pausing of the time scale = 0
    public virtual IEnumerator EventDurationCountdown()
    {
        while (remainingDuration > 0)
        {
            yield return null;
            remainingDuration -= Time.deltaTime;

            if (remainingDuration <= 0)
            {
               End();
            }
        }
    }

    public virtual void RestartMiniGameLogic()
    {

    }

    public virtual void HandleOpening()
    {
        if(!IsCompleted)
        {
            Panels.SetActive(true);
        }
    }

    public virtual void HandleClosing()
    {
        Panels.SetActive(false);

        //If you want to reset the game if they did not complete it
        if(IsCompleted == false)
        {RestartMiniGameLogic();}
    }


    void OnEnable()
    {
        GameEventManager.Instance.OnSecondPassed += CheckActivationTime;
    }

    void OnDisable()
    {
        GameEventManager.Instance.OnSecondPassed -= CheckActivationTime;
    }

    // We are using custom event tick system through the 
    public virtual void CheckActivationTime(float currentTime)
    {
        if (!isActiveEvent && currentTime >= songActivationTime && activationNumber == GameEventManager.Instance.songNumber)
        {
            GameEventManager.Instance.OnSecondPassed -= CheckActivationTime;
            Activate();
        }
    }

    void OnDestroy()
    {
        /*
        Leaving these in here for reference as you may have an event that conflicts with another mini-game 
        and it will listen to when the other mini-game and might do something

        GameEvents.OnEventStart -= HandleEventStart;
        GameEvents.OnEventFail -= HandleEventFail;
        GameEvents.OnEventCancel -= HandleEventCancel;
        GameEvents.OnEventComplete -= HandleEventComplete;
        GameEvents.OnEventMiss -= HandleEventMiss;
        GameEvents.OnEventClose -= HandleEventClose;
        GameEvents.OnEventOpen -= HandleEventOpen;
        */
        GameStateEvent.OnGameStateStart -= HandleGameStateStart;
        GameStateEvent.OnGameStateEnd -= HandleGameStateEnd;
    }

    // Start is called before the first frame update
    void Start()
    {   
        /*
        GameEvents.OnEventStart += HandleEventStart;
        GameEvents.OnEventFail += HandleEventFail;
        GameEvents.OnEventCancel += HandleEventCancel;
        GameEvents.OnEventComplete += HandleEventComplete;
        GameEvents.OnEventMiss += HandleEventMiss;
        GameEvents.OnEventClose += HandleEventClose;
        GameEvents.OnEventOpen += HandleEventOpen;
        */

        GameStateEvent.OnGameStateStart += HandleGameStateStart;
        GameStateEvent.OnGameStateEnd += HandleGameStateEnd;
    }

    //When the GameStateManager starts a new state, we can handle it here
    //Example: Intermission starts and we know that this event should pop up instantly.
    public virtual void HandleGameStateStart(object sender, GameStateEventArgs e)
    {
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
    
    //When the GameStateManager ends the current state, we can handle it here
    //Example: Song ends so we can turn off all instrument events.
    public virtual void HandleGameStateEnd(object sender, GameStateEventArgs e)
    {
        switch(e.stateType)
        {
            case GameModeType.Song:
                HandleClosing();
                if(isActiveEvent){Miss();}
                break;
            case GameModeType.Intermission:
                HandleClosing();
                if(isActiveEvent){Miss();}
                break;
            default:
                break;
        }
    }
}   


