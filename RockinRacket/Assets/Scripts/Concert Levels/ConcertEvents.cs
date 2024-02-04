using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 * This class houses the initialization for all Unity Events used in a concert level
 * 
 * 
 */

public class ConcertEvents : MonoBehaviour
{
    public static ConcertEvents instance;

    // Declaring our Unity Events
    public UnityEvent e_ConcertStarted;
    public UnityEvent e_ConcertEnded;
    public UnityEvent e_TriggerIntermission;
    public UnityEvent e_SongStarted;
    public UnityEvent e_SongEnded;
    public UnityEvent<int> e_ScoreChange;



    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        // Initializating our Events
        if (e_ConcertStarted == null)
        {
            e_ConcertStarted = new UnityEvent();
        }

        if (e_ConcertEnded == null)
        {
            e_ConcertEnded = new UnityEvent();
        }

        if (e_TriggerIntermission == null)
        {
            e_TriggerIntermission = new UnityEvent();
        }

        if (e_SongStarted == null)
        {
            e_SongStarted = new UnityEvent();
        }

        if (e_SongEnded == null)
        {
            e_SongEnded = new UnityEvent();
        }

        if (e_ScoreChange == null)
        {
            e_ScoreChange = new UnityEvent<int>();
        }
    }
}
