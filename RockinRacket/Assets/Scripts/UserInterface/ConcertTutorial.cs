using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    I'd like for this tutorial class to automatically set the venue, specified cutscenes, and have pop-ups that 
    explain what is going on in a concert such as an mini-game and the audience.
    
*/
public class ConcertTutorial : MonoBehaviour
{

    [SerializeField] ConcertUI concertUI;
    [SerializeField] Venue tutorialVenue;
    
    //Make sure the tutorial minigames have priority over spawning
    
    [SerializeField] GameObject tutorialConcertMiniGame;
    [SerializeField] GameObject tutorialIntermissionMiniGame;

    [SerializeField] GameEventContainer stagedMiniGames;

    public void SetUpTutorial()
    {
        if(tutorialVenue != null)
        {
            GameStateManager.Instance.SelectedVenue = tutorialVenue;
            //stagedMiniGames.MiniGamesPrefabs.Add(tutorialConcertMiniGame);
            //stagedMiniGames.MiniGamesPrefabs.Add(tutorialIntermissionMiniGame);
            concertUI.StartConcert();
        }
    }



}
