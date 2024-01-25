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
    }
}
