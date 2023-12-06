using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntermissionHandler : MonoBehaviour
{
    [Header("Needed Settings")]
    [SerializeField] private GameObject intermissionScreen; // Ken added code
    [SerializeField] GameStateData currentGameState;


    public Button nextStateButton;
    
    public GameObject FinishConcertButton;

    public GameObject StartConcertButton;
    
    public TextMeshProUGUI ConcertCompletionTextBox; 
    
    [Header("Default Settings For Test Concert")]
    [SerializeField] TransitionData Cinematic;
    [SerializeField] Button leaveButton;
    [SerializeField] Venue presetVenue;

    private bool intermissionActive = false;

    private void Update()
    {
        if (intermissionActive && currentGameState.CurrentConcertState == ConcertState.BackstageView)
        {
            //nextStateButton.gameObject.SetActive(true);

            if (!intermissionScreen.activeSelf)
            {
                intermissionScreen.SetActive(true); // Ken added code
            }
        }
        else if (intermissionActive && currentGameState.CurrentConcertState != ConcertState.BackstageView)
        {
            //nextStateButton.gameObject.SetActive(true);

            if (intermissionScreen.activeSelf)
            {
                intermissionScreen.SetActive(false); // Ken added code
            }
        }
    }

    public void EndConcert()
    {
        FinishConcertButton.SetActive(true);

        ConcertCompletionTextBox.gameObject.SetActive(true);
    }

    public void EndDialogueSection()
    {
        StateManager.Instance.StartStateTimer();
        CanvasController.instance.SwapToShopView();
    }
    
    public void StartConcert()
    {
        StateManager.Instance.InitializeConcertData();
        StartConcertButton.SetActive(false);
    }
    
    public void StartNextState()
    {
        if(StateManager.Instance.CurrentState.isManualDuration == true)
        {
            StateManager.Instance.CompleteState();
        }
    }

    void Start()
    {
        StateEvent.OnStateStart += HandleGameStateStart;
        StateEvent.OnStateEnd += HandleGameStateEnd;

        intermissionScreen.SetActive(false); // Ken added code
    }
    
    void OnDestroy()
    {
        StateEvent.OnStateStart -= HandleGameStateStart;
        StateEvent.OnStateEnd -= HandleGameStateEnd;
    }

    public void HandleGameStateStart(object sender, StateEventArgs e)
    {
        Debug.Log("State Started: " + e.state.stateType);

        if(e.state.stateType == StateType.Intermission)
        {
            //nextStateButton.gameObject.SetActive(true);
            intermissionActive = true;
            CanvasController.instance.SwapToBackstageView();
            intermissionScreen.SetActive(true); // Ken added code
        }

    }
    
    private void HandleGameStateEnd(object sender, StateEventArgs e)
    {

        //Debug.Log("Game state ended: " + e.state.GameType);
        //nextStateButton.gameObject.SetActive(false);
        intermissionActive = false;
        intermissionScreen.SetActive(false); // Ken added code
        if(e.state.stateType == StateType.Intermission)
        {
            intermissionActive = false;
            CanvasController.instance.SwapToBandView();
        }
    }

    public bool CheckIfStoryContinues()
    {
        //Call story manager to get our next cinematic after this concert 
        // still thinking through the logic on this class and need the story manager
        if( StateManager.Instance.ConcertVenue != null)
        {
            return false;
        }
        return true;
    }

    public void SetUpConcert()
    {
        /*
        if(CheckIfStoryContinues() == false)
        {
            StartConcert();
            return;
        }
        */
        if(presetVenue != null)
        {
            Debug.Log("Intermission Hander: Starting Concert");
            StateManager.Instance.ConcertVenue = presetVenue;
            StartConcert();
        }
        if(Cinematic != null && leaveButton != null)
        {
            Debug.Log("Intermission Hander:  Changing leave button");
            leaveButton.onClick.RemoveAllListeners();
            leaveButton.onClick.AddListener(() => leaveButtonReplacement(Cinematic));
        }
    }

    public void leaveButtonReplacement(TransitionData SceneToLoad)
    {
        CustomSceneEvent.CustomTransitionCalled(SceneToLoad);
        //Call story manager to set a bool flag that we completed X events too
    }
    

}
