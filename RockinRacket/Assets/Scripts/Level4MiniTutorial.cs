using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4MiniTutorial : MonoBehaviour
{
    public Tutorial tutorial;

    void Start()
    {
        ConcertEvents.instance.e_ConcertStarted.AddListener(UpdateTutorialState);
    }

    void UpdateTutorialState()
    {
        if(ConcertController.instance.afterIntermission == false)
        {
            ShowConcertInfo();
        }
    }
    
    public void ShowConcertInfo()
    {
        tutorial.StartTutorial();
    }

}
