using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    I'd like for this tutorial class to automatically set the venue, specified cutscenes, and have pop-ups that 
    explain what is going on in a concert such as an mini-game and the audience.

    This could be a good script for having some scriptable object which has the venue, next cinematic, and scripted events
    then if any of those are set before a concert, we can just set the variables.
    
*/
public class ConcertAutoSetUp : MonoBehaviour
{
    [SerializeField] TransitionData Cinematic;

    [SerializeField] Button leaveButton;
    [SerializeField] ConcertUI concertUI;
    [SerializeField] Venue presetVenue;
    
    //Make sure the tutorial minigames have priority over spawning
    
    //[SerializeField] GameObject tutorialConcertMiniGame;
    //[SerializeField] GameObject tutorialIntermissionMiniGame;

    [SerializeField] MinigameContainer stagedMiniGames;

    public bool CheckIfStoryContinues()
    {
        //Call story manager to get our next cinematic after this concert 
        // still thinking through the logic on this class and need the story manager
        if( GameStateManager.Instance.SelectedVenue != null)
        {
            return false;
        }
        return true;
    }

    public void SetUpConcert()
    {
        if(CheckIfStoryContinues() == false)
        {
            concertUI.StartConcert();
            return;
        }

        if(presetVenue != null)
        {
            GameStateManager.Instance.SelectedVenue = presetVenue;
            //stagedMiniGames.MiniGamesPrefabs.Add(tutorialConcertMiniGame);
            //stagedMiniGames.MiniGamesPrefabs.Add(tutorialIntermissionMiniGame);
            concertUI.StartConcert();
        }
        if(Cinematic != null && leaveButton != null)
        {
            leaveButton.onClick.RemoveAllListeners();
            leaveButton.onClick.AddListener(() => leaveButtonReplacement(Cinematic));
        }
    }

    public void leaveButtonReplacement(TransitionData SceneToLoad)
    {
        CustomSceneEvent.CustomTransitionCalled(SceneToLoad);
        //Call story manager to set a bool flag that we completed X events too
        //
    }



}
