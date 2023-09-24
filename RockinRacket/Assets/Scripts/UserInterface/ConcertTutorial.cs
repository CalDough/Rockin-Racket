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

    public void SetUpTutorial()
    {
        if(tutorialVenue != null)
        {
            GameStateManager.Instance.SelectedVenue = tutorialVenue;
            concertUI.StartConcert();
        }
    }



}
