using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameTutorial : Tutorial
{
    
    public TutorialNote buttonClickedInfoUI; 
    public TutorialNote buttonUnclickedInfoUI;
    public MinigameController minigame;
    [SerializeField] private MinigameButton MinigameButton;
    public bool wasMinigameOpened = false;

    public override void StartTutorial()
    {
        isTutorialActive = true;
        minigame.MakeMinigameAvailable();
    }

    public override void CompleteTutorial()
    {
        isTutorialCompleted = true;

        //This isn't needed, but incase we want to change it so notes stay up during a tutorial 
        //I may want to revert some code and and disable notes when they beat the game
        /*
        if(!tutorialInfoUI.anim.GetCurrentAnimatorStateInfo(0).IsName("NoteHidden"))
        tutorialInfoUI.HideNote();

        if(!failureInfoUI.anim.GetCurrentAnimatorStateInfo(0).IsName("NoteHidden"))
        failureInfoUI.HideNote();

        if(!buttonClickedInfoUI.anim.GetCurrentAnimatorStateInfo(0).IsName("NoteHidden"))
        buttonClickedInfoUI.HideNote();

        if(!buttonUnclickedInfoUI.anim.GetCurrentAnimatorStateInfo(0).IsName("NoteHidden"))
        buttonUnclickedInfoUI.HideNote();
        */
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
        RestartMechanic();
    }

    public override void RestartMechanic()
    {
        minigame.MakeMinigameAvailable();
    }
    
    public void OnMinigameButtonClicked()
    {
        wasMinigameOpened = true;
    }

    public virtual void ShowClickedButtonInfo()
    {
        PauseGame();
        buttonClickedInfoUI.ShowNote();
    }

    public virtual void CloseClickedButtonInfo()
    {
        buttonClickedInfoUI.HideNote();
        ResumeGame();
    }

    public virtual void ShowUnclickButtonInfo()
    {
        PauseGame();
        buttonUnclickedInfoUI.ShowNote();
    }

    public virtual void CloseUnclickButtonInfo()
    {
        buttonUnclickedInfoUI.HideNote();
        ResumeGame();
        RestartMechanic();
    }

    void Start()
    {
        SubscribeEvents();
    }

    void OnDestroy()
    {
        UnsubscribeEvents();
    }

    private void SubscribeEvents()
    {
        MinigameEvents.OnMinigameFail += HandleEventFail;
        MinigameEvents.OnMinigameComplete += HandleEventComplete;
        MinigameEvents.OnMinigameStart += HandleEventStart;
        MinigameEvents.OnMinigameAvailable += HandleEventAvailable;
    }

    private void UnsubscribeEvents()
    {
        MinigameEvents.OnMinigameFail -= HandleEventFail;
        MinigameEvents.OnMinigameComplete -= HandleEventComplete;
        MinigameEvents.OnMinigameStart -= HandleEventStart;
        MinigameEvents.OnMinigameAvailable -= HandleEventAvailable;
    }

    public void HandleEventFail(object sender, GameEventArgs e)
    {
        if(e.EventObject != this.minigame){return;}

        if(wasMinigameOpened)
        {
            ShowFailTutorialInfo();
        }
        else //Never opened the minigame so we need to tell them to click it
        {
            ShowUnclickButtonInfo();
        }
        wasMinigameOpened = false;
    }

    public void HandleEventComplete(object sender, GameEventArgs e)
    {
        if(e.EventObject != this.minigame){return;}
        CompleteTutorial();
    }

    public void HandleEventAvailable(object sender, GameEventArgs e)
    {
        if(e.EventObject != this.minigame){return;}
        ShowClickedButtonInfo();
    }

    public void HandleEventStart(object sender, GameEventArgs e)
    {
        if(e.EventObject != this.minigame){return;}
        ShowTutorialInfo();
    }
}
