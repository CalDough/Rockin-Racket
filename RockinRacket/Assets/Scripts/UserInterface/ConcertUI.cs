using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConcertUI :  ScrollSelector<GameEvent>
{
    public int currentIndex = 0;
    //public Dictionary<int, ScrollButton> ButtonDict = new Dictionary<int, ScrollButton>();
    public List<ScrollButton> Buttons = new List<ScrollButton>();

    
    public GameObject FinishConcertButton;

    public TextMeshProUGUI InfoTextBox; 
    
    public TextMeshProUGUI ConcertCompletionTextBox; 

    public void EndConcert()
    {
        FinishConcertButton.SetActive(true);

        ConcertCompletionTextBox.gameObject.SetActive(true);
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
        GameEvent.OnEventStart -= HandleEventStart;
        GameEvent.OnEventFail -= HandleEventFail;
        GameEvent.OnEventCancel -= HandleEventCancel;
        GameEvent.OnEventComplete -= HandleEventComplete;
        GameEvent.OnEventMiss -= HandleEventMiss;
        GameStateEvent.OnGameStateStart -= HandleGameStateStart;
        GameStateEvent.OnGameStateEnd -= HandleGameStateEnd;
    }

    // Start is called before the first frame update
    void Start()
    {   
        AudioManager.Instance.CreateSoundObjects();
        GameStateManager.Instance.StartConcert();

        GameEvent.OnEventStart += HandleEventStart;
        GameEvent.OnEventFail += HandleEventFail;
        GameEvent.OnEventCancel += HandleEventCancel;
        GameEvent.OnEventComplete += HandleEventComplete;
        GameEvent.OnEventMiss += HandleEventMiss;
        GameStateEvent.OnGameStateStart += HandleGameStateStart;
        GameStateEvent.OnGameStateEnd += HandleGameStateEnd;
    }

    public void HandleEventStart(object sender, GameEventArgs e)
    {
        //Debug.Log("UI Event Started: " + e.eventObject);
        this.Items.Add(e.eventObject);
        
        CreateButton(currentIndex);
        currentIndex += 1;
    }

    public void HandleEventFail(object sender, GameEventArgs e)
    {
        //Debug.Log("Event Fail: " + e.eventObject);
        FindGameEventInListAndHide(e.eventObject);
    }

    public void HandleEventCancel(object sender, GameEventArgs e)
    {
        //Debug.Log("Event Cancelled: " + e.eventObject);
        FindGameEventInListAndHide(e.eventObject);
    }

    public void HandleEventComplete(object sender, GameEventArgs e)
    {
        //Debug.Log("Event Completed: " + e.eventObject);
        FindGameEventInListAndHide(e.eventObject);
    }

    public void HandleEventMiss(object sender, GameEventArgs e)
    {
        //Debug.Log("Event Missed: " + e.eventObject);
        FindGameEventInListAndHide(e.eventObject);
    }
    
    public void HandleGameStateStart(object sender, GameStateEventArgs e)
    {
        //Debug.Log("State Started: " + e.stateType);
        switch(e.stateType)
        {
            case GameModeType.Song:
                InfoTextBox.text = "State Song";
                break;
            case GameModeType.Intermission:
                InfoTextBox.text = "State Intermission";
                break;
            case GameModeType.Cutscene:
                InfoTextBox.text = "State Cutscene";
                break;
            case GameModeType.Dialogue:
                InfoTextBox.text = "State Dialogue";
                break;
            case GameModeType.BandBattle:
                InfoTextBox.text = "State Battle";
                break;
            case GameModeType.SceneIntro:
                InfoTextBox.text = "State Intro";
                break;
            case GameModeType.SceneOutro:
                InfoTextBox.text = "State Outro";
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
    
    public void FindGameEventInListAndHide(GameEvent game)
    {
        Debug.Log("Items count" + Items.Count);
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
