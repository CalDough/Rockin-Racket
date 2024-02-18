using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public bool isTutorialCompleted = false;
    public bool isTutorialActive = false;
    public TutorialNote tutorialInfoUI; 
    public TutorialNote failureInfoUI;

    public virtual void StartTutorial()
    {
        isTutorialActive = true;
        ShowTutorialInfo();
    }

    public virtual void ShowTutorialInfo()
    {
        PauseGame();
        tutorialInfoUI.ShowNote();
    }

    public virtual void CloseTutorialInfo()
    {
        tutorialInfoUI.HideNote();
        ResumeGame();
    }

    public virtual void CompleteTutorial()
    {
        isTutorialActive = false;
        isTutorialCompleted = true;
        tutorialInfoUI.HideNote();
        ResumeGame();
    }

    public virtual void ShowFailTutorialInfo()
    {
        failureInfoUI.ShowNote();
        PauseGame();
    }

    public virtual void CloseFailureInfo()
    {
        tutorialInfoUI.HideNote();
        ResumeGame();
    }

    public virtual void PauseGame()
    {
        TimeEvents.GamePaused();

        //Debug.Log("Time Paused"); 
    }

    public virtual  void ResumeGame()
    {
        TimeEvents.GameResumed();

        //Debug.Log("Time Unpaused"); 
    }

     public virtual void RestartMechanic()
    {
        failureInfoUI.HideNote();
        StartTutorial(); 
    }
}
