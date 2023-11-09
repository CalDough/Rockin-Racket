using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/*
    This script is a example for a connecting GameEvent class. There are so many events so we might have to cut it down for simplicity.
    It would also make sense to use inheritence from the GameEvent class, but I haven't gotten time to work that out with this.
    GameEvent prefabs will have a script like this to handle connecting to UI, and mini-game logic like button presses etc. 
    See GameEvent script and the prefab for this an event for more info.
*/
public class ExampleMiniGame : MiniGame
{
/*
    //The ExampleMiniGame script is inheriting from Mini-Game, which is a monobehavior.
    //This ExampleMiniGame script is placed on an empty game object which is created in the UI.
    //This Panels object is a child of the empty object and is what we use to disable/hide and enable/show the actual mini-game
    //The script for the mini-game itself is never disabled
    public GameObject Panels; 

    //This bool is set only when the mini-game is fully completed
    public bool IsCompleted = false;

    //This Gametype variable is used to determine what state the mini-game will occur in. For now we only care about Song/Intermission
    public GameModeType gameType = GameModeType.Song;

    //This is when during the song the mini-game will activate. Once the mini-game passes or is equal to this value, we may Activate() it
    public float activationTime = 0;
    //This is to keep track of when the mini-game will occur based on how many Gamestates of the GameType have happened.
    public int activationNumber = 0;  

    //This is how long the event will last as a pop-up before automatically failing
    public float duration; 
    //This is the actual duration that we decrement as time passes until 0 seconds
    public float remainingDuration;  
    //This bool should be set to true if the event does not use a co-routine and timer
    public bool infiniteDuration;

    //This is used by GameEventManager to see if an event as done its Activate() function
    public bool isActiveEvent = false; 
    
    //This bool determines if an event may be spawned more than once per game state, true if it is unique
    public bool isUniqueEvent = false;

    //This bool determines if an event may only be spawned once per concert
    public bool isOneTimeEvent = false; 

    //Ignore this variable its for controlling the co-routine
    public Coroutine durationCoroutine = null;
*/
    //If your making an instrument event and it can apply to anyone, try these variables
    public bool randomMember = false;
    //There are 4 positions in the band, with the 5th one being Background tracks like speakers and stuff
    //Check the scene to see which position goes to which bad member
    public int ConcertPositionTarget = 1;
    //Instruments have up to 5 levels of being broken, this is the severity of the event
    public float BrokenLevelChange = 1;

    /*
        Here is an example of how to call an audio
                                    [Minigame] [Level of brokeness] [Target raccoon] [Affects Instrument? or voice bool]
        ConcertAudioEvent.AudioBroken(this, BrokenLevelChange, ConcertPositionTarget, true);

        Here is how to tell the audio that the event mini-game was successful
        ConcertAudioEvent.AudioFixed(this, BrokenLevelChange, ConcertPositionTarget, true);
    */

    public GameObject MiniGameObject; // This is an example variable for a prefab that wouldbe generated by this game

    //If your mini-game has custom UI elements, you should just couple them with the mini-game
    // Example is the Cleaning game where the Trash has a direct reference to the Mini-game
    //If you want the mini-game to be somewhat decoupled by using with events, take a look at Dial Tuning/Dial script
    //Dial script uses an event DialMatchedEventHandler OnDialMatched; instead of a direct reference to the mini-game

    public void MiniGameLogic()
    {
        //Pretend theres some logic here that checks if the game was completed
        //You can write whatever checks and logic that needs to be done for the mini-games completion
        //For example, if you have a UI element like a button that calls this function after being clicked

    }

    public void SetUpMiniGame()
    {
        //You can add any logic to spawn prefabs or set up vars in this function
        //This function should be added to either Activate() or Start() depending on what needs to be spawned and when they need set up
        //If your mini-game is simple enough, you can just override Activate() and have the set-up in there

    }




    /*
    This function is normally called when activationTime and activationNumber are equal to the GameEventManager's variables
    When activated, an event begins it's negative or positive effects on the concert.
    The UI panel displaying the notification will be visible when the event is activated.
    */
    public override void Activate()
    {base.Activate();}

    /*
    This function is called when the player FAILS the mini-game, such as the remainingDuration reaching 0
    Negative effects on the concert, such as bad audio, unhappy audience will still be active 
    */
    public override void End()
    {base.End();}

    /*
    This function is called when the game state changes from one to another, such as Song > Intermission
    Negative effects on the instruments should be removed. Negative effects on the audience can be kept.
    */
    public override void Miss()
    {base.Miss();}

    /*
    This function is called when the player SUCCESSFULLY COMPLETES the mini-game
    This should be called from the clear conditions in your mini-game, such as repairing all problems on an instrument
    Negative effects on the concert, such as bad audio, unhappy audience will be fixed
    */
    public override void Complete()
    {base.Complete();}

    /*
    These function is called when the player clicks on the UI pop-up. When the event is opened, we handle it through the HandleOpening() functions
    If your mini-game will open without player input but instead show up anyways based on a condition, you should still call this function.
    */
     public override void OpenEvent()
    { base.OpenEvent();}
    /*
    This one specifically is usually linked to the X button for a pop-up window
    */
    public override void CloseEvent() 
    {base.CloseEvent();}

    /*
    These directly control the visual UI panel being enabled. If your mini-game changes camera location you should include it in here and in HandleClosing()
    */
    public override void HandleOpening()
    {base.HandleOpening();}
    /*
    These function also calls the RestartMiniGameLogic if the event is not completed. 
    Add code to RestartMiniGameLogic() if you want the mini-game to reset upon opening or closing the UI Pop-up
    */
    public override void HandleClosing()
    {base.HandleClosing();}
    
    /*
    This co-routine handles the countdown of the event after it activates. You probably don't need to modify it unless
    your mini-game becomes harder the longer it is activated.
    */
    public override IEnumerator EventDurationCountdown()
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

    /*
    Add any code here to reset the functions and variables of your mini-game if you desire it
    */
    public override void RestartMiniGameLogic()
    {    }

 
    /*
    Add all the references and listeners to other events in these two functions
    If your game needs to listen to another mini-game or item event we will subscribe to them here
    */
    void OnEnable()
    {
    }

    void OnDisable()
    {
    }



    /*
    Add all the references and listeners to other events in these two functions
    If your game needs to listen to another mini-game or item event we will subscribe to them here
    Actually these could be combined with the earlier ones, it is just different since i forgot
    */
    void OnDestroy()
    {
        StateEvent.OnStateStart -= HandleGameStateStart;
        StateEvent.OnStateEnd -= HandleGameStateEnd;
    }

    
    void Start()
    {   
        StateEvent.OnStateStart += HandleGameStateStart;
        StateEvent.OnStateEnd += HandleGameStateEnd;
    }

    
    /*
    If your mini-game lasts between segments such as intermission and songs, then we should replace the HandleClosing() and 
    End event code here. 
    */
    public override void HandleGameStateStart(object sender, StateEventArgs e)
    {
        switch(e.stateType)
        {
            case StateType.Song:
                break;
            case StateType.Intermission:
                break;
            default:
                break;
        }
    }
    
    //When the GameStateManager ends the current state, we can handle it here
    //Example: Song ends so we can turn off all instrument events.
    public override void HandleGameStateEnd(object sender, StateEventArgs e)
    {
        switch(e.stateType)
        {
            case StateType.Song:
                HandleClosing();
                if(isActiveEvent){Miss();}
                break;
            case StateType.Intermission:
                HandleClosing();
                if(isActiveEvent){Miss();}
                break;
            default:
                break;
        }
    }
}
