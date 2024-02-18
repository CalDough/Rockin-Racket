using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchStandEndTutorial : Tutorial
{
    public GameObject leaveButton;
    bool hasLastCustomerBeenServed = false;
    public MerchTable merchTable;

    void Update()
    {
        if(hasLastCustomerBeenServed)
        {return;}

        if(merchTable.AllCustomersFulfilled)
        {
            hasLastCustomerBeenServed = true;
            ShowTutorialInfo();
        }
    }
    public override void StartTutorial()
    {
        isTutorialActive = true;
        
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
       
    }

    public override void CompleteTutorial()
    {
        isTutorialActive = false;
        isTutorialCompleted = true;
        tutorialInfoUI.HideNote();
        ResumeGame();
        leaveButton.SetActive(true);
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
