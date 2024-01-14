using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialTuning : MinigameController
{
    public GameObject ChildCanvasPanels; 
    
    public BandRoleName TargetBandMember = BandRoleName.Kurt;
    public float StressFactor = 1;

    public GameObject dialPrefab; 
    public List<RectTransform> positionObjects; // List of points where dials will be instantiated
    public List<Dial> dials = new List<Dial>();

    /*
    Event and State Logic
    */

    void Start()
    {
        CanActivate = false;
        IsActive = false;
        SubscribeEvents();
    }

    void OnDestroy()
    {
        UnsubscribeEvents();
    }

    private void SubscribeEvents()
    {
        StateEvent.OnStateStart += HandleGameStateStart;
        StateEvent.OnStateEnd += HandleGameStateEnd;
    }

    private void UnsubscribeEvents()
    {
        StateEvent.OnStateStart -= HandleGameStateStart;
        StateEvent.OnStateEnd -= HandleGameStateEnd;
    }

    public void HandleGameStateStart(object sender, StateEventArgs e)
    {
        switch(e.state.stateType)
        {
            case StateType.Song:
                ResetSpawnTimer();
                break;
            default:
                break;
        }
    }
    
    private void HandleGameStateEnd(object sender, StateEventArgs e)
    {
        switch(e.state.stateType)
        {
            case StateType.Song:
                StopCoroutine(spawnTimerCoroutine);
                if(IsActive)
                {
                    CancelMinigame();
                }
                StopCoroutine(availabilityTimerCoroutine);
                break;
            default:
                break;
        }
    }


    /*
    Minigame Logic
    */

    public override void StartMinigame()
    {
        IsActive = true;
        MinigameEvents.EventStart(this);
        // Start minigame logic 
        RestartMiniGameLogic();
        ResetGameplayTimer();
    }

    public override void FailMinigame()
    {
        IsActive = false;
        MinigameEvents.EventFail(this);
        // Fail minigame logic 
        StopCoroutine(gameplayTimerCoroutine);
        CloseMinigame();
        ResetSpawnTimer();
    }

    public override void FinishMinigame()
    {
        IsActive = false;
        MinigameEvents.EventComplete(this);
        // Finish minigame logic 
        StopCoroutine(gameplayTimerCoroutine);
        CloseMinigame();
        ResetSpawnTimer();
    }

    public override void CancelMinigame()
    {
        IsActive = false;
        MinigameEvents.EventCancel(this);
        // Cancel minigame logic 
        StopCoroutine(gameplayTimerCoroutine);
        CloseMinigame();
        ResetSpawnTimer();
    }

    public override void OpenMinigame()
    {
        ChildCanvasPanels.SetActive(true);
    }

    public override void CloseMinigame()
    {
        ChildCanvasPanels.SetActive(false);
    }

    public void SpawnDials()
    {
        foreach (RectTransform positionObject in positionObjects)
        {
            GameObject dialObject = Instantiate(dialPrefab, positionObject.position, Quaternion.identity, positionObject);
            Dial newDial = dialObject.GetComponent<Dial>();
            
            if (newDial != null)
            {
                dialObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

                newDial.SetMarkerAngle(Random.Range(0f, 360f));
                newDial.currentAngle = Random.Range(0f, 360f);
                newDial.OnDialMatched += HandleDialMatched;
                dials.Add(newDial);
            }
            else
            {
                Debug.LogError("object does not have a Dial component!");
            }
        }
    }

    private void HandleDialMatched()
    {
        foreach (Dial dial in dials)
        {
            if (!dial.MatchingAngle)
            {return; }
        }

        // All dials are matched
        FinishMinigame();
    }

    public void RestartMiniGameLogic()
    {
        foreach (Dial dial in dials)
        {
            dial.OnDialMatched -= HandleDialMatched;
            Destroy(dial.gameObject);
        }
        dials.Clear();
        SpawnDials();
    }

}
