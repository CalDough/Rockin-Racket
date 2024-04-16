using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4MiniTutorial : MonoBehaviour
{
    public Tutorial tutorial;

    void Start()
    {
        if(ConcertController.instance.afterIntermission == false)
        {
            ConcertEvents.instance?.e_ConcertStarted.AddListener(ShowConcertInfo);
        }
    }
    
    public void ShowConcertInfo()
    {
        tutorial.StartTutorial();
    }

}
