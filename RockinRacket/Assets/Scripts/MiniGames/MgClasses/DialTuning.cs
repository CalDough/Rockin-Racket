using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialTuning : MinigameController
{
    [Header("Score Variables")]
    public int ScoreBonus = 25;
    public int ScorePenalty = -25;
    public float StressFactor = 1;
    public BandRoleName TargetBandMember = BandRoleName.Kurt;

    [Header("Reference Variables")]
    public GameObject ChildCanvasPanels; 
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
        ConcertEvents.instance.e_SongStarted.AddListener(HandleGameStateStart);
        ConcertEvents.instance.e_SongEnded.AddListener(HandleGameStateEnd);
    }

    public void HandleGameStateStart()
    {
        ResetSpawnTimer();
    }
    
    private void HandleGameStateEnd()
    {
        if(IsActive)
        {
            CancelMinigame();
        }
        if(CanActivate)
        {
            CanActivate = false;
            CancelMinigame();
        }
        StopSpawnTimer();
        StopAvailabilityTimer();
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
        StopSpawnTimer();
        StopAvailabilityTimer();
    }

    public override void FailMinigame()
    {
        IsActive = false;
        MinigameEvents.EventFail(this);
        // Fail minigame logic 
        StopGameplayTimer();
        StopAvailabilityTimer();
        CloseMinigame();
        ResetSpawnTimer();
        ConcertEvents.instance.e_ScoreChange.Invoke(ScorePenalty);
    }

    public override void FinishMinigame()
    {
        IsActive = false;
        MinigameEvents.EventComplete(this);
        // Finish minigame logic 
        StopGameplayTimer();
        StopAvailabilityTimer();
        CloseMinigame();
        ResetSpawnTimer();
        ConcertEvents.instance.e_ScoreChange.Invoke(ScoreBonus);
    }

    public override void CancelMinigame()
    {
        IsActive = false;
        MinigameEvents.EventCancel(this);
        // Cancel minigame logic 
        StopGameplayTimer();
        StopAvailabilityTimer();
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
