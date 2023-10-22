using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/*
    This script is a test UI script for the concert.
*/

public class ConcertUI :  ScrollSelector<MiniGame>
{
    public int currentIndex = 0;
    //public Dictionary<int, ScrollButton> ButtonDict = new Dictionary<int, ScrollButton>();
    public List<ScrollButton> Buttons = new List<ScrollButton>();

    
    public GameObject FinishConcertButton;

    public GameObject StartConcertButton;

    public TextMeshProUGUI InfoTextBox; 
    
    public TextMeshProUGUI ConcertCompletionTextBox; 


    public void EndConcert()
    {
        FinishConcertButton.SetActive(true);

        ConcertCompletionTextBox.gameObject.SetActive(true);
    }

    
    public void StartConcert()
    {
        GameStateManager.Instance.StartConcert();
        StartConcertButton.SetActive(false);
    }

    public override void OnButtonClick(int index)
    {
        //Debug.Log("Clicked " + index);
        if(index >= 0 && index < Items.Count)
        {
            //Debug.Log("Opening " + index);
            Items[index].OpenEvent();
        }
    }

    void OnDestroy()
    {
        GameEvents.OnEventStart -= HandleEventStart;
        GameEvents.OnEventFail -= HandleEventFail;
        GameEvents.OnEventCancel -= HandleEventCancel;
        GameEvents.OnEventComplete -= HandleEventComplete;
        GameEvents.OnEventMiss -= HandleEventMiss;
        GameStateEvent.OnGameStateStart -= HandleGameStateStart;
        GameStateEvent.OnGameStateEnd -= HandleGameStateEnd;
    }

    // Start is called before the first frame update
    void Start()
    {   

        GameEvents.OnEventStart += HandleEventStart;
        GameEvents.OnEventFail += HandleEventFail;
        GameEvents.OnEventCancel += HandleEventCancel;
        GameEvents.OnEventComplete += HandleEventComplete;
        GameEvents.OnEventMiss += HandleEventMiss;
        GameStateEvent.OnGameStateStart += HandleGameStateStart;
        GameStateEvent.OnGameStateEnd += HandleGameStateEnd;
    }



    public void HandleEventStart(object sender, GameEventArgs e)
    {
        //Debug.Log("ConcertUI: UI Event Started: " + e.EventObject);
        //this.Items.Add(e.EventObject);
        
        //CreateButton(currentIndex);
        //currentIndex += 1;
    }

    public void HandleEventFail(object sender, GameEventArgs e)
    {
        //Debug.Log("Event Fail: " + e.eventObject);
        //FindGameEventInListAndHide(e.EventObject);
    }

    public void HandleEventCancel(object sender, GameEventArgs e)
    {
        //Debug.Log("Event Cancelled: " + e.eventObject);
        //FindGameEventInListAndHide(e.EventObject);
    }

    public void HandleEventComplete(object sender, GameEventArgs e)
    {
        //Debug.Log("Event Completed: " + e.eventObject);
        //FindGameEventInListAndHide(e.EventObject);
    }

    public void HandleEventMiss(object sender, GameEventArgs e)
    {
        //Debug.Log("Event Missed: " + e.eventObject);
        //FindGameEventInListAndHide(e.EventObject);
    }
    
    public void HandleGameStateStart(object sender, GameStateEventArgs e)
    {
        //Debug.Log("State Started: " + e.stateType);
        switch(e.stateType)
        {
            case GameModeType.Song:
                InfoTextBox.text = "Song "+ e.state.Duration +" Seconds";
                break;
            case GameModeType.Intermission:
                InfoTextBox.text = "Intermission "+ e.state.Duration +" Seconds";
                break;
            case GameModeType.Cutscene:
                InfoTextBox.text = "Cutscene "+ e.state.Duration +" Seconds";
                break;
            case GameModeType.Dialogue:
                InfoTextBox.text = "Dialogue "+ e.state.Duration +" Seconds";
                break;
            case GameModeType.BandBattle:
                InfoTextBox.text = "Battle "+ e.state.Duration +" Seconds";
                break;
            case GameModeType.SceneIntro:
                InfoTextBox.text = "Intro "+ e.state.Duration +" Seconds";
                break;
            case GameModeType.SceneOutro:
                InfoTextBox.text = "Outro "+ e.state.Duration +" Seconds";
                break;
            default:
                break;
        }
    }
    
    private void HandleGameStateEnd(object sender, GameStateEventArgs e)
    {
        // Handle the game state end here
        //ebug.Log("Game state ended: " + e.state.GameType);
        switch(e.stateType)
        {
            case GameModeType.Song:
                CleanUpAfterGameState();
                break;
            case GameModeType.Intermission:
                CleanUpAfterGameState();
                break;
            default:
                break;
        }
        
    }
    
    public void CreateButton(int i)
    {
        
        if(ButtonPrefab == null)
        {
            //this section is giga bugged? dont add a debug here it will always be called even if a button is created
            return;
        }
        GameObject buttonGO = Instantiate(ButtonPrefab, ContentHolder);

        ScrollButton scrollButton = buttonGO.GetComponent<ScrollButton>();
        if(scrollButton == null) // If it's null, check the children
        {
            scrollButton = buttonGO.GetComponentInChildren<ScrollButton>();
        }

        if(scrollButton != null)
        {
            scrollButton.Setup(i, this);
            Buttons.Add(scrollButton);
        }
        else
        {
            Debug.LogError("No ScrollButton component found on the button prefab or its children.");
        }
    }
    
    public void FindGameEventInListAndHide(MiniGame game)
    {
        //Debug.Log("Items count" + Items.Count);
        int index = Items.IndexOf(game);
        if (index != -1)
        {
            Buttons[index].gameObject.SetActive(false);
        }
    }

    public void CleanUpAfterGameState()
    {
        Debug.Log("Cleaning up Buttons from last GameState");
        foreach (ScrollButton button in Buttons)
        {
            button.gameObject.SetActive(false);
        }
    }
}
