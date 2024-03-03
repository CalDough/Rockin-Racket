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
    public List<AnimatedDial> dials = new List<AnimatedDial>();
    public int unmatchedDialsCount;

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
        ConcertAudioEvent.AudioBroken(this, StressFactor, TargetBandMember);
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
        unmatchedDialsCount = positionObjects.Count; 
        foreach (RectTransform positionObject in positionObjects)
        {
            GameObject dialObject = Instantiate(dialPrefab, positionObject.position, Quaternion.identity, positionObject);
            AnimatedDial newDial = dialObject.GetComponent<AnimatedDial>();

            dials.Add(newDial);
            if (newDial)
            {
                newDial.RandomizeDial();
                newDial.OnDialMatched += () => {
                    unmatchedDialsCount--;
                    if (unmatchedDialsCount <= 0) {
                        FinishMinigame();
                    }
                };
            }
        }
    }

    public void RestartMiniGameLogic()
    {
        foreach (AnimatedDial dial in dials)
        {
            Destroy(dial.gameObject);
        }
        dials.Clear();
        SpawnDials();
    }

}
