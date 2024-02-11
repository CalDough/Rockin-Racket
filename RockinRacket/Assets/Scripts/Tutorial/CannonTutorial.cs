using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTutorial : Tutorial
{

    public TutorialNote CannonInfoUI; 
    public GameObject cannon;
    public Transform cannonLocation;

    public ConcertAttendee attendee;
    public Animator attendeeAnim;
    public Transform attendeeSpot;
    public float attendeeTravelSpeed = 4f;

    public override void StartTutorial()
    {
        attendee.gameObject.SetActive(true);
        attendee.isInConcert = false;
        attendee.StartLerp(attendee.gameObject.transform.position, attendeeSpot.position, attendeeTravelSpeed );
        isTutorialActive = true;
        
    }

    public void StartAttendeeItemCoroutine()
    {
        if(!isTutorialActive){return;}
        RestartMechanic();
        ShowTutorialInfo();
    }

    public void MoveCannonIntoScene()
    {
        cannon.SetActive(true);
        StartCoroutine(MoveShirtCannonIntoScene());
    }

    IEnumerator MoveShirtCannonIntoScene()
    {
        float duration = 2.0f; 
        float elapsed = 0;

        Vector3 startPosition = cannon.transform.position;
        Vector3 endPosition = cannonLocation.position;

        while (elapsed < duration)
        {
            cannon.transform.position = Vector3.Lerp(startPosition, endPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        cannon.transform.position = endPosition; 
        ShowCannonInfo();
    }

    public void ShowCannonInfo()
    {
        PauseGame();
        CannonInfoUI.ShowNote();
    }

    public void CloseCannonInfo()
    {
        CannonInfoUI.HideNote();
        ResumeGame();
        RestartMechanic();
    }

    public override void ShowTutorialInfo()
    {
        if(!isTutorialActive){return;}
        PauseGame();
        tutorialInfoUI.ShowNote();
    }

    public override void CloseTutorialInfo()
    {
        tutorialInfoUI.HideNote();
        ResumeGame();
        
  
    }

    public override void CompleteTutorial()
    {
        if(isTutorialCompleted){return;}
        isTutorialActive = false;
        isTutorialCompleted = true;
        attendee.StopAllCoroutines();
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
        RestartMechanic();
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
        attendee.StopAllCoroutines();
        attendee.itemWaitMin = 0.5f;
        attendee.itemWaitMax = 0.5f;
        attendee.itemPatienceMax = 15;
        attendee.itemPatienceMin = 15;
        attendee.StartItemCoroutine();
        attendee.StartMoveCoroutine();
    }
}