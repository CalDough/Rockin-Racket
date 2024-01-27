using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashTutorial : Tutorial
{

    //public TutorialNote TrashInfoUI; 
    public CrowdTrashcan trashcan;
    public Transform trashcanLocation;

    public override void StartTutorial()
    {
        isTutorialActive = true;
        StartCoroutine(MoveTrashcanIntoScene());
        
    }

    IEnumerator MoveTrashcanIntoScene()
    {
        float duration = 2.0f; 
        float elapsed = 0;

        Vector3 startPosition = trashcan.transform.position;
        Vector3 endPosition = trashcanLocation.position;

        while (elapsed < duration)
        {
            trashcan.transform.position = Vector3.Lerp(startPosition, endPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        trashcan.transform.position = endPosition; 
        ShowTutorialInfo(); 
    }

    public override void ShowTutorialInfo()
    {
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
        //tutorialInfoUI.HideNote();
        ResumeGame();
    }

    public override void ShowFailTutorialInfo()
    {
        failureInfoUI.ShowNote();
        PauseGame();
    }

    public override void CloseFailureInfo()
    {
        tutorialInfoUI.HideNote();
        ResumeGame();
    }

    public override void PauseGame()
    {
        Time.timeScale = 0;
        Debug.Log("Time Paused"); 
    }

    public override  void ResumeGame()
    {
        Time.timeScale = 1; 
        Debug.Log("Time Unpaused"); 
    }

     public override void RestartMechanic()
    {
        failureInfoUI.HideNote();
        StartTutorial(); 
    }
}
