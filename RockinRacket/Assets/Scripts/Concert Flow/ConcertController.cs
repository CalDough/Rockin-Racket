using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

/*
 * This class controls the state and flow of the concert
 * 
 * 
 * 
 * 
 */

public class ConcertController : MonoBehaviour
{
    public static ConcertController instance;

    [Header("Concert Details")]
    public ConcertData cData;
    public SongData currentSong;
    
    
    [Header("Start Screen Details")]
    [SerializeField] private GameObject startScreen;
    [SerializeField] private Button startConcert;



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
    }

    private void Start()
    {
        startConcert.onClick.AddListener(() => StartConcert());

        if (!startScreen.activeSelf)
        {
            startScreen.SetActive(true);
        }
    }

    /*
     * This method is called when the player clicks on the start concert button. This method
     * sends out the StartSong Event to start the first song of the concert
     */
    private void StartConcert()
    {
        // Disable Start Screen
        startScreen.SetActive(false);

        // Setting the current song
        currentSong = cData.concertSongsFirstHalf[0];

        // Start the concert by calling the 
        ConcertEvents.instance.e_ConcertStarted.Invoke();
        ConcertEvents.instance.e_SongStarted.Invoke();

       

        Debug.Log("<color=green> Song Started Called </color>");
        //StateManager.Instance.InitializeConcertData();
    }


}
