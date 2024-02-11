using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AudienceTutorial : Tutorial
{
    public ConcertAttendee attendee;
    public Animator attendeeAnim;
    public Transform attendeeSpot;

    public float attendeeTravelSpeed = 4f;

    public override void StartTutorial()
    {
        isTutorialActive = true;
        attendee.gameObject.SetActive(true);
        attendee.isInConcert = false;
        attendee.StartLerp(attendee.gameObject.transform.position, attendeeSpot.position, attendeeTravelSpeed );
        
    }

    public override void ShowTutorialInfo()
    {
        if(!isTutorialActive){return;}
        PauseGame();
        tutorialInfoUI.ShowNote();
    }

    public override void CloseTutorialInfo()
    {
        Debug.Log("close tut");
        tutorialInfoUI.HideNote();
        ResumeGame();
        //attendee.gameObject.transform.SetPositionAndRotation(new Vector3(0,-3.4f,0), quaternion.identity);
        attendee.itemWaitMin = 4;
        attendee.itemWaitMax = 5;
        attendee.itemPatienceMax = 5;
        attendee.itemPatienceMin = 5;
        attendee.StartItemCoroutine();
        attendee.StartMoveCoroutine();
    }

    public override void CompleteTutorial()
    {
        isTutorialActive = false;
        isTutorialCompleted = true;
        attendee.StopItemCoroutine();
        ResumeGame();
    }

    public override void ShowFailTutorialInfo()
    {
        if(!isTutorialActive){return;}
        failureInfoUI.ShowNote();
        PauseGame();
    }

    public override void CloseFailureInfo()
    {
        failureInfoUI.HideNote();
        ResumeGame();
        CompleteTutorial();
    }

    public override void PauseGame()
    {
        TimeEvents.GamePaused();
        //Debug.Log("Time Paused"); 
    }

    public override  void ResumeGame()
    {
        TimeEvents.GameResumed();
        //Debug.Log("Time Unpaused"); 
    }

     public override void RestartMechanic()
    {
        failureInfoUI.HideNote();
        StartTutorial(); 
    }
}
